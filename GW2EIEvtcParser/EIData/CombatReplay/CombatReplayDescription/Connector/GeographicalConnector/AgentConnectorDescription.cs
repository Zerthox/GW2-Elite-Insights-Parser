﻿namespace GW2EIEvtcParser.EIData;

public class AgentConnectorDescription : GeographicalConnectorDescription
{
    public int MasterId { get; private set; }
    internal AgentConnectorDescription(AgentConnector connector, CombatReplayMap map, ParsedEvtcLog log) : base(connector, map, log)
    {
        MasterId = connector.Agent.UniqueID;
    }
}