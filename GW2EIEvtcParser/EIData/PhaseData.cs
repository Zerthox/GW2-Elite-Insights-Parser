﻿using GW2EIEvtcParser.ParsedData;

namespace GW2EIEvtcParser.EIData;

public class PhaseData
{
    public long Start { get; private set; }
    public long End { get; private set; }
    public long DurationInS { get; private set; }
    public long DurationInMS { get; private set; }
    public long DurationInM { get; private set; }
    public string Name { get; internal set; } = "";
    public bool DrawStart { get; internal set; } = true;
    public bool DrawEnd { get; internal set; } = true;
    public bool DrawArea { get; internal set; } = true;
    public bool DrawLabel { get; internal set; } = true;
    public bool CanBeSubPhase { get; internal set; } = true;

    public bool BreakbarPhase { get; internal set; } = false;

    public IReadOnlyList<SingleActor> Targets => _targets;
    private readonly List<SingleActor> _targets = [];
    public IReadOnlyList<SingleActor> SecondaryTargets => _secondaryTargets;
    private readonly List<SingleActor> _secondaryTargets = [];

    public IReadOnlyList<SingleActor> AllTargets => _secondaryTargets.Count != 0 ? _allTargets : _targets;
    private readonly List<SingleActor> _allTargets = [];

    internal PhaseData(long start, long end)
    {
        Start = start;
        End = end;
        DurationInM = (End - Start) / 60000;
        DurationInMS = (End - Start);
        DurationInS = (End - Start) / 1000;
    }

    internal PhaseData(long start, long end, string name) : this(start, end)
    {
        Name = name;
    }

    public bool InInterval(long time)
    {
        return Start <= time && time <= End;
    }

    public bool IntersectsWindow(long start, long end)
    {
        long maxStart = Math.Max(start, Start);
        long minEnd = Math.Min(end, End);
        return minEnd - maxStart > 0;
    }

    internal void AddTarget(SingleActor? target)
    {
        if (target == null || _targets.Contains(target))
        {
            return;
        }
        _secondaryTargets.Remove(target); // promote to target, all targets stays unchanged
        _targets.Add(target);
    }

    internal void RemoveTarget(SingleActor target)
    {
        //TODO(Rennorb) @perf
        _targets.Remove(target);
    }

    internal void AddTargets(IEnumerable<SingleActor?> targets)
    {
        _targets.AddRange(targets.Where(x => x != null)!);
    }

    internal void AddSecondaryTarget(SingleActor? target)
    {
        if (target == null || AllTargets.Contains(target))
        {
            return;
        }
        _secondaryTargets.Add(target);
        RefreshAllTargetsList();
    }

    public bool IsSecondaryTarget(SingleActor target)
    {
        return _secondaryTargets.Contains(target);
    }

    internal void RemoveSecondaryTarget(SingleActor target)
    {
        _secondaryTargets.Remove(target);
        RefreshAllTargetsList();
    }

    internal void AddSecondaryTargets(IEnumerable<SingleActor?> targets)
    {
        _secondaryTargets.AddRange(targets.Where(x => x != null)!);
        RefreshAllTargetsList();
    }

    private void RefreshAllTargetsList()
    {
        _allTargets.Clear();
        _allTargets.AddRange(_targets);
        _allTargets.AddRange(_secondaryTargets);
    }

    internal void OverrideStart(long start)
    {
        Start = start;
        DurationInM = (End - Start) / 60000;
        DurationInMS = (End - Start);
        DurationInS = (End - Start) / 1000;
    }

    internal void OverrideEnd(long end)
    {
        End = end;
        DurationInM = (End - Start) / 60000;
        DurationInMS = (End - Start);
        DurationInS = (End - Start) / 1000;
    }

    /// <summary>
    /// Override times in a manner that the phase englobes the targets activities in the phase
    /// </summary>
    /// <param name="log"></param>
    internal void OverrideTimes(ParsedEvtcLog log)
    {
        OverrideStartTime(log);
        OverrideEndTime(log);
    }
    /// <summary>
    /// Override start in a manner that the phase starts when the targets are active
    /// </summary>
    /// <param name="log"></param>
    internal void OverrideStartTime(ParsedEvtcLog log)
    {
        if (Targets.Count > 0)
        {
            long start = long.MaxValue;
            foreach (SingleActor target in Targets)
            {
                long startTime = target.FirstAware;
                SpawnEvent? spawned = log.CombatData.GetSpawnEvents(target.AgentItem).FirstOrDefault();
                if (spawned != null)
                {
                    startTime = spawned.Time;
                }
                EnterCombatEvent? enterCombat = log.CombatData.GetEnterCombatEvents(target.AgentItem).FirstOrDefault();
                if (enterCombat != null)
                {
                    startTime = enterCombat.Time;
                }
                start = Math.Min(start, startTime);
            }
            OverrideStart(Math.Max(Math.Max(Start, start), log.FightData.FightStart));
        }
    }
    /// <summary>
    /// Override end in a manner that the phase ends when the targets are gone (if possible)
    /// </summary>
    /// <param name="log"></param>
    internal void OverrideEndTime(ParsedEvtcLog log)
    {
        if (Targets.Count > 0)
        {
            long end = long.MinValue;
            foreach (SingleActor target in Targets)
            {
                long deadTime = target.LastAware;
                DeadEvent? died = log.CombatData.GetDeadEvents(target.AgentItem).FirstOrDefault();
                if (died != null)
                {
                    deadTime = died.Time;
                }
                end = Math.Max(end, deadTime);
            }
            OverrideEnd(Math.Min(Math.Min(End, end), log.FightData.FightEnd));
        }
    }
}
