﻿namespace GW2EIEvtcParser.ParsedData;

public class DownEvent : StatusEvent
{
    internal DownEvent(CombatItem evtcItem, AgentData agentData) : base(evtcItem, agentData)
    {

    }

    internal DownEvent(AgentItem src, long time) : base(src, time)
    {

    }

}
