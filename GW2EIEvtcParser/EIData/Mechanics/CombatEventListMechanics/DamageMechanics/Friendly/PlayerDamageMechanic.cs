﻿using GW2EIEvtcParser.ParsedData;

namespace GW2EIEvtcParser.EIData;


internal class PlayerDamageMechanic : DamageMechanic
{
    public PlayerDamageMechanic(MechanicPlotlySetting plotlySetting, string shortName, string description, string fullName, int internalCoolDown, CombatEventsGetter getter) : base(plotlySetting, shortName, description, fullName, internalCoolDown, getter)
    {
    }

    internal override void CheckMechanic(ParsedEvtcLog log, Dictionary<Mechanic, List<MechanicEvent>> mechanicLogs, Dictionary<int, SingleActor> regroupedMobs)
    {
        foreach (Player p in log.PlayerList)
        {
            foreach (HealthDamageEvent c in GetEvents(log, p.AgentItem))
            {
                if (Keep(c, log))
                {
                    InsertMechanic(log, mechanicLogs, c.Time, p);
                }
            }
        }
    }
}
