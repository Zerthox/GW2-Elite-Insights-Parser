﻿using GW2EIEvtcParser.Extensions;
using GW2EIEvtcParser.ParsedData;
using GW2EIEvtcParser.ParserHelpers;
using static GW2EIEvtcParser.EncounterLogic.EncounterCategory;

namespace GW2EIEvtcParser.EncounterLogic;

internal abstract class SunquaPeak : FractalLogic
{
    public SunquaPeak(int triggerID) : base(triggerID)
    {
        EncounterCategoryInformation.SubCategory = SubFightCategory.SunquaPeak;
        EncounterID |= EncounterIDs.FractalMasks.SunquaPeakMask;
    }

    internal override void EIEvtcParse(ulong gw2Build, EvtcVersionEvent evtcVersion, FightData fightData, AgentData agentData, List<CombatItem> combatData, IReadOnlyDictionary<uint, ExtensionHandler> extensions)
    {
        // Set manual FractalScale for old logs without the event
        AddFractalScaleEvent(gw2Build, combatData, new List<(ulong, byte)>
        {
            ( GW2Builds.September2020SunquaPeakRelease, 100),
            ( GW2Builds.June2023BalanceAndSOTOBetaAndSilentSurfNM, 99),
        });
        base.EIEvtcParse(gw2Build, evtcVersion, fightData, agentData, combatData, extensions);
    }
}
