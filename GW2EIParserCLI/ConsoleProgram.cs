using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;
using GW2EIEvtcParser;
using GW2EIParserCommons;
using GW2EIParserCommons.Exceptions;
using Tracing;

namespace GW2EIParser;

static class ConsoleProgram
{

    /// <returns>0 on success, other value on error</returns>
    public static int ParseAll(List<string> logFiles, ProgramHelper programHelper, bool batchToDiscord)
    {
        using var _t = new AutoTrace("ParseAll");
        programHelper.ExecuteMemoryCheckTask();
        var operations = new List<ConsoleOperationController>(logFiles.Count);
        logFiles.ForEach(logFile => operations.Add(new ConsoleOperationController(logFile)));
        if (programHelper.ParseMultipleLogs())
        {
            var state = new ThreadingState()
            {
                ProgramHelper = programHelper,
                NoMoreFiles = false,
                FileQueue = new(),
            };

            var parallelism = programHelper.GetMaxParallelRunning();
            for (int i = 0; i < parallelism - 1; i++)
            {
                var t = new Thread(EnterParserThread);
                t.Start(state);
            }

            foreach (var operation in operations)
            {
                state.FileQueue.Enqueue(operation);
            }

            state.NoMoreFiles = true;
            EnterParserThread(state); // we take the last thread
        }
        else
        {
            foreach (var operation in operations)
            {
                ParseLog(operation, programHelper);
            }
        }
        if (batchToDiscord)
        {
            Console.WriteLine("Discord: Preparing batch for discord");
            programHelper.HandleBatchedDiscordEmbed([], operations, (message) => {
                Console.WriteLine(message);
            });
        }
        return 0;
    }

    public class ThreadingState
    {
        public ProgramHelper ProgramHelper;
        public volatile bool NoMoreFiles;
        public ConcurrentQueue<ConsoleOperationController> FileQueue;
    }

    static void EnterParserThread(object state_)
    {
        var state = (ThreadingState)state_;
        while (true)
        {
            ConsoleOperationController operation;
            while (!state.FileQueue.TryDequeue(out operation))
            {
                if (state.NoMoreFiles && state.FileQueue.IsEmpty) { return; }
                //NOTE(Rennorb): Don't even bother with synchronizing. Just wait a bit.
                Thread.Sleep(10);
            }

            if (string.IsNullOrWhiteSpace(operation.InputFile)) { Debugger.Break(); }

            ParseLog(operation, state.ProgramHelper);
        }
    }

    private static void ParseLog(ConsoleOperationController operation, ProgramHelper programHelper)
    {
        using var _t = new AutoTrace("Parse One");
        try
        {
            programHelper.DoWork(operation);
            operation.FinalizeStatus(true);
        }
        catch (ProgramException ex)
        {
            var finalException = ParserHelper.GetFinalException(ex);
            operation.UpdateProgress("Program: " + finalException.Source);
            operation.UpdateProgress("Program: " + finalException.StackTrace);
            operation.UpdateProgress("Program: " + finalException.TargetSite);
            operation.UpdateProgress("Program: " + finalException.Message);
            operation.FinalizeStatus(false);
        }
        catch (Exception)
        {
            operation.UpdateProgress("Program: something terrible has happened");
            operation.FinalizeStatus(false);
        }
        finally
        {
            programHelper.GenerateTraceFile(operation);
        }
        Console.WriteLine("Processed - " + JsonSerializer.Serialize(new ConsoleResultObject(operation), ConsoleResultObject.Serializer));
    }
}
