﻿using GW2EIEvtcParser.EIData;
using GW2EIEvtcParser.ParsedData;
using static GW2EIEvtcParser.EncounterLogic.EncounterCategory;
using static GW2EIEvtcParser.EncounterLogic.EncounterLogicUtils;
using static GW2EIEvtcParser.SkillIDs;
using static GW2EIEvtcParser.SpeciesIDs;

namespace GW2EIEvtcParser.EncounterLogic;

internal abstract class StrikeMissionLogic : FightLogic
{

    protected StrikeMissionLogic(int triggerID) : base(triggerID)
    {
        MechanicList.Add(new MechanicGroup([    
            new PlayerDstBuffApplyMechanic(ExposedPlayer, new MechanicPlotlySetting(Symbols.TriangleLeft, Colors.Purple, 10), "Exposed", "Exposed Applied (Increased incoming damage)", "Exposed Applied", 0),
            new PlayerDstBuffApplyMechanic(Debilitated, new MechanicPlotlySetting(Symbols.TriangleDown, Colors.Purple, 10), "Debilitated", "Debilitated Applied (Reduced outgoing damage)", "Debilitated Applied", 0),
            new PlayerDstBuffApplyMechanic(Infirmity, new MechanicPlotlySetting(Symbols.TriangleUp, Colors.Purple, 10), "Infirmity", "Infirmity Applied (Reduced incoming healing)", "Infirmity Applied", 0),
        ])
        );
        ParseMode = ParseModeEnum.Instanced10;
        SkillMode = SkillModeEnum.PvE;
        EncounterCategoryInformation.Category = FightCategory.Strike;
        EncounterID |= EncounterIDs.EncounterMasks.StrikeMask;
    }
    internal override FightData.EncounterStartStatus GetEncounterStartStatus(CombatData combatData, AgentData agentData, FightData fightData)
    {
        if (IsInstance)
        {
            return base.GetEncounterStartStatus(combatData, agentData, fightData);
        }
        if (TargetHPPercentUnderThreshold(GenericTriggerID, fightData.FightStart, combatData, Targets))
        {
            return FightData.EncounterStartStatus.Late;
        }
        return FightData.EncounterStartStatus.Normal;
    }
    internal override IReadOnlyList<TargetID> GetTargetsIDs()
    {
        return new[] { GetTargetID(GenericTriggerID) };
    }
}
