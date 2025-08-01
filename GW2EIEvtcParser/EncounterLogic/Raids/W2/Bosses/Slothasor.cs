﻿using System;
using System.Numerics;
using GW2EIEvtcParser.EIData;
using GW2EIEvtcParser.Exceptions;
using GW2EIEvtcParser.Extensions;
using GW2EIEvtcParser.ParsedData;
using GW2EIEvtcParser.ParserHelpers;
using static GW2EIEvtcParser.ArcDPSEnums;
using static GW2EIEvtcParser.EncounterLogic.EncounterLogicPhaseUtils;
using static GW2EIEvtcParser.ParserHelper;
using static GW2EIEvtcParser.ParserHelpers.EncounterImages;
using static GW2EIEvtcParser.SkillIDs;
using static GW2EIEvtcParser.SpeciesIDs;

namespace GW2EIEvtcParser.EncounterLogic;

internal class Slothasor : SalvationPass
{
    internal readonly MechanicGroup Mechanics = new MechanicGroup([
            new PlayerDstHealthDamageHitMechanic(TantrumDamage, new MechanicPlotlySetting(Symbols.CircleOpen,Colors.Yellow), "Tantrum", "Tantrum (Triple Circles after Ground slamming)","Tantrum", 5000),
            new MechanicGroup([
                new PlayerDstBuffApplyMechanic(VolatilePoisonBuff, new MechanicPlotlySetting(Symbols.Circle,Colors.Red), "Poison", "Volatile Poison Application (Special Action Key)","Poison (Action Key)", 0),
                new PlayerDstHealthDamageHitMechanic(VolatilePoisonSkill, new MechanicPlotlySetting(Symbols.CircleOpen,Colors.Red), "Poison dmg", "Stood in Volatile Poison","Poison dmg", 0),
            ]),
            new PlayerDstHealthDamageHitMechanic(Halitosis, new MechanicPlotlySetting(Symbols.TriangleRightOpen,Colors.LightOrange), "Breath", "Halitosis (Flame Breath)","Flame Breath", 0),
            new PlayerDstHealthDamageHitMechanic(SporeRelease, new MechanicPlotlySetting(Symbols.Pentagon,Colors.Red), "Shake", "Spore Release (Coconut Shake)","Shake", 0),
            new PlayerDstBuffApplyMechanic(MagicTransformation, new MechanicPlotlySetting(Symbols.Hexagram,Colors.Teal), "Slub", "Magic Transformation (Ate Magic Mushroom)","Slub Transform", 0), 
            //new Mechanic(Nauseated, "Nauseated", ParseEnum.BossIDS.Slothasor, new MechanicPlotlySetting("diamond-tall-open",Colors.LightPurple), "Slub CD",0), //can be skipped imho, identical person and timestamp as Slub Transform
            new PlayerDstBuffApplyMechanic(FixatedSlothasor, new MechanicPlotlySetting(Symbols.Star,Colors.Magenta), "Fixate", "Fixated by Slothasor","Fixated", 0),
            new PlayerDstHealthDamageHitMechanic([ToxicCloud1, ToxicCloud2], new MechanicPlotlySetting(Symbols.PentagonOpen,Colors.DarkGreen), "Floor", "Toxic Cloud (stood in green floor poison)","Toxic Floor", 0),
            new MechanicGroup([
                new PlayerDstBuffApplyMechanic(Fear, new MechanicPlotlySetting(Symbols.SquareOpen,Colors.Red), "Fear", "Hit by fear after breakbar","Feared", 0)
                    .UsingChecker((ba,log) => ba.AppliedDuration == 8000),
                new EnemyDstBuffApplyMechanic(NarcolepsyBuff, new MechanicPlotlySetting(Symbols.DiamondTall,Colors.DarkTeal), "CC", "Narcolepsy (Breakbar)","Breakbar", 0),
                new EnemyDstBuffRemoveMechanic(NarcolepsyBuff, new MechanicPlotlySetting(Symbols.DiamondTall,Colors.Red), "CC Fail", "Narcolepsy (Failed CC)","CC Fail", 0)
                    .UsingChecker((br,log) => br.RemovedDuration > 120000),
                new EnemyDstBuffRemoveMechanic(NarcolepsyBuff, new MechanicPlotlySetting(Symbols.DiamondTall,Colors.DarkGreen), "CCed", "Narcolepsy (Breakbar broken)","CCed", 0)
                    .UsingChecker( (br,log) => br.RemovedDuration <= 120000),
            ]),
            new PlayerDstBuffApplyMechanic(SlipperySlubling, new MechanicPlotlySetting(Symbols.Star,Colors.Yellow), "Slppr.Slb", "Slippery Slubling","Slippery Slubling", 0),
        ]);
    public Slothasor(int triggerID) : base(triggerID)
    {
        MechanicList.Add(Mechanics);
        Extension = "sloth";
        Icon = EncounterIconSlothasor;
        EncounterCategoryInformation.InSubCategoryOrder = 0;
        EncounterID |= 0x000001;
        ChestID = ChestID.SlothasorChest;
    }

    protected override CombatReplayMap GetCombatMapInternal(ParsedEvtcLog log)
    {
        return new CombatReplayMap(CombatReplaySlothasor,
                        (654, 1000),
                        (5822, -3491, 9549, 2205)/*,
                        (-12288, -27648, 12288, 27648),
                        (2688, 11906, 3712, 14210)*/);
    }

    internal override void UpdatePlayersSpecAndGroup(IReadOnlyList<Player> players, CombatData combatData, FightData fightData)
    {
        base.UpdatePlayersSpecAndGroup(players, combatData, fightData);
        var slubTransformApplyAtStart = combatData.GetBuffApplyData(MagicTransformation).Where(x => x.Time <= fightData.FightStart + 5000).FirstOrDefault();
        if (slubTransformApplyAtStart != null)
        {
            var transformedPlayer = players.FirstOrDefault(x => x.AgentItem == slubTransformApplyAtStart.To);
            if (transformedPlayer != null)
            {
                var transfoExit = combatData.GetBuffRemoveAllDataBySrc(MagicTransformation, transformedPlayer.AgentItem).FirstOrDefault(x => x.Time >= slubTransformApplyAtStart.Time);
                if (transfoExit != null)
                {
                    var enterCombat = combatData.GetEnterCombatEvents(transformedPlayer.AgentItem).Where(x => x.Time <= transfoExit.Time + 1000).LastOrDefault();
                    if (enterCombat != null && enterCombat.Spec != Spec.Unknown && enterCombat.Subgroup != 0)
                    {
                        transformedPlayer.AgentItem.OverrideSpec(enterCombat.Spec);
                        transformedPlayer.OverrideGroup(enterCombat.Subgroup);
                    }
                }
            }
        }
    }

    protected override void SetInstanceBuffs(ParsedEvtcLog log)
    {
        base.SetInstanceBuffs(log);

        if (log.FightData.Success && log.CombatData.GetBuffData(SlipperySlubling).Any())
        {
            InstanceBuffs.MaybeAdd(GetOnPlayerCustomInstanceBuff(log, SlipperySlubling));
        }
    }

    internal override IReadOnlyList<TargetID> GetTrashMobsIDs()
    {
        return
        [
            TargetID.Slubling1,
            TargetID.Slubling2,
            TargetID.Slubling3,
            TargetID.Slubling4,
            TargetID.PoisonMushroom
        ];
    }

    internal override List<InstantCastFinder> GetInstantCastFinders()
    {
        return
        [
            new DamageCastFinder(VolatileAura, VolatileAura),
            new BuffLossCastFinder(PurgeSlothasor, VolatilePoisonBuff),
        ];
    }

    internal override List<PhaseData> GetPhases(ParsedEvtcLog log, bool requirePhases)
    {
        long fightEnd = log.FightData.FightEnd;
        List<PhaseData> phases = GetInitialPhase(log);
        SingleActor mainTarget = Targets.FirstOrDefault(x => x.IsSpecies(TargetID.Slothasor)) ?? throw new MissingKeyActorsException("Slothasor not found");
        phases[0].AddTarget(mainTarget, log);
        if (!requirePhases)
        {
            return phases;
        }
        var sleepy = mainTarget.GetCastEvents(log, log.FightData.FightStart, fightEnd).Where(x => x.SkillID == NarcolepsySkill);
        long start = 0;
        int i = 1;
        foreach (CastEvent c in sleepy)
        {
            var phase = new PhaseData(start, Math.Min(c.Time, fightEnd), "Phase " + i++);
            phase.AddParentPhase(phases[0]);
            phase.AddTarget(mainTarget, log);
            start = c.EndTime;
            phases.Add(phase);
        }
        var lastPhase = new PhaseData(start, fightEnd, "Phase " + i++);
        lastPhase.AddParentPhase(phases[0]);
        lastPhase.AddTarget(mainTarget, log);
        phases.Add(lastPhase);
        return phases;
    }

    internal static void FindMushrooms(FightData fightData, AgentData agentData, List<CombatItem> combatData, IReadOnlyDictionary<uint, ExtensionHandler> extensions)
    {
        var mushroomAgents = combatData
            .Where(x => MaxHealthUpdateEvent.GetMaxHealth(x) == 14940 && x.IsStateChange == StateChange.MaxHealthUpdate)
            .Select(x => agentData.GetAgent(x.SrcAgent, x.Time))
            .Where(x => x.Type == AgentItem.AgentType.Gadget && (x.HitboxWidth == 146 || x.HitboxWidth == 210) && x.HitboxHeight == 300)
            .ToList();
        if (mushroomAgents.Count > 0)
        {
            int idToKeep = mushroomAgents.GroupBy(x => x.ID).Select(x => (x.Key, x.Count())).MaxBy(x => x.Item2).Key;
            foreach (AgentItem mushroom in mushroomAgents)
            {
                if (!mushroom.IsSpecies(idToKeep))
                {
                    continue;
                }
                var copyEventsFrom = new List<AgentItem>() { mushroom };
                var hpUpdates = combatData.Where(x => x.SrcMatchesAgent(mushroom) && x.IsStateChange == StateChange.HealthUpdate);
                var aliveUpdates = hpUpdates.Where(x => HealthUpdateEvent.GetHealthPercent(x) == 100).ToList();
                var deadUpdates = hpUpdates.Where(x => HealthUpdateEvent.GetHealthPercent(x) == 0).ToList();
                long lastDeadTime = long.MinValue;
                foreach (CombatItem aliveEvent in aliveUpdates)
                {
                    CombatItem? deadEvent = deadUpdates.FirstOrDefault(x => x.Time > lastDeadTime && x.Time > aliveEvent.Time);
                    if (deadEvent == null)
                    {
                        lastDeadTime = Math.Min(fightData.LogEnd, mushroom.LastAware);
                    }
                    else
                    {
                        lastDeadTime = deadEvent.Time;
                    }
                    AgentItem aliveMushroom = agentData.AddCustomNPCAgent(aliveEvent.Time, lastDeadTime, mushroom.Name, mushroom.Spec, TargetID.PoisonMushroom, false, mushroom.Toughness, mushroom.Healing, mushroom.Condition, mushroom.Concentration, mushroom.HitboxWidth, mushroom.HitboxHeight);
                    AgentManipulationHelper.RedirectEventsAndCopyPreviousStates(combatData, extensions, agentData, mushroom, copyEventsFrom, aliveMushroom, false);
                    copyEventsFrom.Add(aliveMushroom);
                }
            }
        }
    }

    internal override void EIEvtcParse(ulong gw2Build, EvtcVersionEvent evtcVersion, FightData fightData, AgentData agentData, List<CombatItem> combatData, IReadOnlyDictionary<uint, ExtensionHandler> extensions)
    {
        FindMushrooms(fightData, agentData, combatData, extensions);
        base.EIEvtcParse(gw2Build, evtcVersion, fightData, agentData, combatData, extensions);
    }

    internal override void ComputeNPCCombatReplayActors(NPC target, ParsedEvtcLog log, CombatReplay replay)
    {
        long castDuration;
        (long start, long end) lifespan;

        switch (target.ID)
        {
            case (int)TargetID.Slothasor:
                foreach (CastEvent cast in target.GetAnimatedCastEvents(log))
                {
                    switch (cast.SkillID)
                    {
                        // Halitosis - Fire breath
                        case Halitosis:
                            long preCastTime = 1000;
                            castDuration = 2000;
                            (long start, long end) lifespanPrecast = (cast.Time, cast.Time + preCastTime);
                            lifespan = (lifespanPrecast.end, lifespanPrecast.end + castDuration);
                            uint range = 600;
                            if (target.TryGetCurrentFacingDirection(log, lifespanPrecast.start, out var facingHalitosis))
                            {
                                var connector = new AgentConnector(target);
                                var rotationConnector = new AngleConnector(facingHalitosis);
                                var openingAngle = 60;
                                replay.Decorations.Add(new PieDecoration(range, openingAngle, lifespanPrecast, Colors.Orange, 0.1, connector).UsingRotationConnector(rotationConnector));
                                replay.Decorations.Add(new PieDecoration(range, openingAngle, lifespan, Colors.Orange, 0.4, connector).UsingRotationConnector(rotationConnector));
                            }
                            break;
                        // Tantrum - 3 sets ground AoEs
                        case TantrumSkill:
                            // Generic indicator of casting
                            if (!log.CombatData.HasEffectData)
                            {
                                lifespan = (cast.Time, cast.EndTime);
                                var tantrum = new CircleDecoration(300, lifespan, Colors.LightOrange, 0.4, new AgentConnector(target));
                                replay.Decorations.AddWithFilledWithGrowing(tantrum.UsingFilled(false), true, lifespan.end);
                            }
                            break;
                        // Spore Release - Shake
                        case SporeRelease:
                            // Generic indicator of casting
                            if (!log.CombatData.HasEffectData)
                            {
                                lifespan = (cast.Time, cast.EndTime);
                                var sporeRelease = new CircleDecoration(700, lifespan, Colors.Red, 0.4, new AgentConnector(target)).UsingFilled(false);
                                replay.Decorations.AddWithFilledWithGrowing(sporeRelease, true, lifespan.end);
                            }
                            break;
                        default:
                            break;
                    }
                }

                // Narcolepsy - Invulnerability
                var narcolepsies = target.GetBuffStatus(log, NarcolepsyBuff).Where(x => x.Value > 0);
                foreach (var narcolepsy in narcolepsies)
                {
                    replay.Decorations.Add(new OverheadProgressBarDecoration(CombatReplayOverheadProgressBarMajorSizeInPixel, (narcolepsy.Start, narcolepsy.End), Colors.LightBlue, 0.6, Colors.Black, 0.2, [(narcolepsy.Start, 0), (narcolepsy.Start + 120000, 100)], new AgentConnector(target))
                        .UsingRotationConnector(new AngleConnector(180)));
                }

                // Tantrum - Knockdown AoEs
                if (log.CombatData.TryGetEffectEventsBySrcWithGUID(target.AgentItem, EffectGUIDs.SlothasorTantrum, out var tantrums))
                {
                    foreach (EffectEvent effect in tantrums)
                    {
                        lifespan = effect.ComputeLifespan(log, 2000);
                        var circle = new CircleDecoration(100, lifespan, Colors.LightOrange, 0.1, new PositionConnector(effect.Position));
                        replay.Decorations.Add(circle);
                    }
                }
                break;
            case (int)TargetID.PoisonMushroom:
                break;
            default:
                break;
        }

    }

    internal override void ComputePlayerCombatReplayActors(PlayerActor p, ParsedEvtcLog log, CombatReplay replay)
    {
        base.ComputePlayerCombatReplayActors(p, log, replay);
        // Poison
        var poisonToDrop = p.GetBuffStatus(log, VolatilePoisonBuff).Where(x => x.Value > 0);
        foreach (Segment seg in poisonToDrop)
        {
            int toDropStart = (int)seg.Start;
            int toDropEnd = (int)seg.End;
            var circle = new CircleDecoration(180, seg, "rgba(255, 255, 100, 0.5)", new AgentConnector(p));
            replay.Decorations.AddWithFilledWithGrowing(circle.UsingFilled(false), true, toDropStart + 8000);
            if (!log.CombatData.HasEffectData && p.TryGetCurrentInterpolatedPosition(log, toDropEnd, out var position))
            {
                replay.Decorations.Add(new CircleDecoration(900, 180, (toDropEnd, toDropStart + 90000), Colors.GreenishYellow, 0.3, new PositionConnector(position)).UsingGrowingEnd(toDropEnd + 82000));
            }
            replay.Decorations.AddOverheadIcon(seg, p, ParserIcons.VolatilePoisonOverhead);
        }
        // Transformation
        var slubTrans = p.GetBuffStatus(log, MagicTransformation).Where(x => x.Value > 0);
        foreach (Segment seg in slubTrans)
        {
            replay.Decorations.Add(new CircleDecoration(180, seg, "rgba(0, 80, 255, 0.3)", new AgentConnector(p)));
        }
        // Fixated
        var fixatedSloth = p.GetBuffStatus(log, FixatedSlothasor).Where(x => x.Value > 0);
        foreach (Segment seg in fixatedSloth)
        {
            replay.Decorations.Add(new CircleDecoration(120, seg, "rgba(255, 80, 255, 0.3)", new AgentConnector(p)));
            replay.Decorations.AddOverheadIcon(seg, p, ParserIcons.FixationPurpleOverhead);
        }
    }

    internal override void ComputeEnvironmentCombatReplayDecorations(ParsedEvtcLog log, CombatReplayDecorationContainer environmentDecorations)
    {
        base.ComputeEnvironmentCombatReplayDecorations(log, environmentDecorations);
        if (log.CombatData.TryGetEffectEventsByGUID(EffectGUIDs.SlothasorSporeReleaseProjectileImpacts, out var sporeReleaseImpacts))
        {
            foreach (var sporeReleaseImpact in sporeReleaseImpacts)
            {
                // TODO: confirm size
                var lifespan = sporeReleaseImpact.ComputeLifespan(log, 1000);
                var circle = new CircleDecoration(100, lifespan, Colors.Red, 0.2, new PositionConnector(sporeReleaseImpact.Position));
                environmentDecorations.Add(circle);
            }
        }
        if (log.CombatData.TryGetEffectEventsByGUID(EffectGUIDs.SlothasorGrowingVolatilePoison, out var growingVolatilePoisons))
        {
            foreach (var growingVolatilePoison in growingVolatilePoisons)
            {
                var volatilePoisonApply = log.CombatData.GetBuffApplyData(VolatilePoisonBuff).LastOrDefault(x => x.Time <= growingVolatilePoison.Time);
                if (volatilePoisonApply != null)
                {
                    // Compute life span not reliable, has a dynamic end, which cuts the AoE short when encounter ends, use the expected durations
                    environmentDecorations.Add(new CircleDecoration(900, 180, (growingVolatilePoison.Time, volatilePoisonApply.Time + 90000), Colors.GreenishYellow, 0.3, new PositionConnector(growingVolatilePoison.Position)).UsingGrowingEnd(growingVolatilePoison.Time + 82000));
                }
                
            }       
        }
    }
}
