﻿namespace GW2EIEvtcParser.ParsedData;

public class BreakbarStateEvent : StatusEvent
{

    public readonly ArcDPSEnums.BreakbarState State;

    internal BreakbarStateEvent(CombatItem evtcItem, AgentData agentData) : base(evtcItem, agentData)
    {
        State = GetBreakbarState(evtcItem);
    }

    internal static ArcDPSEnums.BreakbarState GetBreakbarState(CombatItem evtcItem)
    {
        return ArcDPSEnums.GetBreakbarState(evtcItem.Value);
    }

    internal BreakbarStateEvent(AgentItem agentItem, long time, ArcDPSEnums.BreakbarState state) : base(agentItem, time)
    {
        State = state;
    }

}
