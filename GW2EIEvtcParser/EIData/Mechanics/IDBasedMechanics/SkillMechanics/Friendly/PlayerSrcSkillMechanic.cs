﻿using GW2EIEvtcParser.ParsedData;

namespace GW2EIEvtcParser.EIData;


internal class PlayerSrcSkillMechanic : PlayerSkillMechanic
{

    public PlayerSrcSkillMechanic(long mechanicID, MechanicPlotlySetting plotlySetting, string shortName, string description, string fullName, int internalCoolDown) : base(mechanicID, plotlySetting, shortName, description, fullName, internalCoolDown)
    {
    }

    public PlayerSrcSkillMechanic(long[] mechanicIDs, MechanicPlotlySetting plotlySetting, string shortName, string description, string fullName, int internalCoolDown) : base(mechanicIDs, plotlySetting, shortName, description, fullName, internalCoolDown)
    {
    }

    protected override AgentItem GetAgentItem(HealthDamageEvent ahde)
    {
        return ahde.From;
    }
}
