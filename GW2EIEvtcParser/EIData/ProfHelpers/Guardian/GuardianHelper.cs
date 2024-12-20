﻿using GW2EIEvtcParser.EncounterLogic;
using GW2EIEvtcParser.Extensions;
using GW2EIEvtcParser.ParsedData;
using GW2EIEvtcParser.ParserHelpers;
using static GW2EIEvtcParser.ArcDPSEnums;
using static GW2EIEvtcParser.EIData.Buff;
using static GW2EIEvtcParser.EIData.DamageModifiersUtils;
using static GW2EIEvtcParser.EIData.ProfHelper;
using static GW2EIEvtcParser.EIData.SkillModeDescriptor;
using static GW2EIEvtcParser.ParserHelper;
using static GW2EIEvtcParser.SkillIDs;
using static GW2EIEvtcParser.DamageModifierIDs;

namespace GW2EIEvtcParser.EIData;

internal static class GuardianHelper
{
    internal static readonly List<InstantCastFinder> InstantCastFinder =
    [
        new BuffGainCastFinder(ShieldOfWrathSkill, ShieldOfWrathBuff),
        new BuffGainCastFinder(ZealotsFlameSkill, ZealotsFlameBuff)
            .UsingICD(0),
        //new BuffLossCastFinder(9115,9114,InstantCastFinder.DefaultICD), // Virtue of Justice
        //new BuffLossCastFinder(9120,9119,InstantCastFinder.DefaultICD), // Virtue of Resolve
        //new BuffLossCastFinder(9118,9113,InstantCastFinder.DefaultICD), // Virtue of Courage

        // Meditations
        new DamageCastFinder(JudgesIntervention, JudgesIntervention)
            .UsingDisableWithEffectData(),
        new BuffGainCastFinder(JudgesIntervention, MercifulAndJudgesInterventionSelfBuff)
            .UsingChecker((evt, combatData, agentData, skillData) => combatData.HasRelatedEffectDst(EffectGUIDs.GuardianGenericTeleport2, evt.To, evt.Time + 120))
            .UsingNotAccurate(true),
        new BuffGainCastFinder(MercifulInterventionSkill, MercifulAndJudgesInterventionSelfBuff)
            .UsingChecker((evt, combatData, agentData, skillData) => combatData.HasRelatedEffectDst(EffectGUIDs.GuardianMercifulIntervention, evt.To, evt.Time + 200))
            .UsingNotAccurate(true),
        new EffectCastFinderByDst(ContemplationOfPurity, EffectGUIDs.GuardianContemplationOfPurity1)
            .UsingDstBaseSpecChecker(Spec.Guardian),
        new DamageCastFinder(SmiteCondition, SmiteCondition),
        new DamageCastFinder(LesserSmiteCondition, LesserSmiteCondition)
            .UsingOrigin(EIData.InstantCastFinder.InstantCastOrigin.Trait),
        
        // Shouts
        new EffectCastFinderByDst(SaveYourselves, EffectGUIDs.GuardianSaveYourselves)
            .UsingDstBaseSpecChecker(Spec.Guardian)
            .UsingSecondaryEffectChecker(EffectGUIDs.GuardianShout),
        // distinguish by boons, check duration/stacks to counteract pure of voice
        new EffectCastFinderByDst(Advance, EffectGUIDs.GuardianShout)
            .UsingDstBaseSpecChecker(Spec.Guardian)
            .UsingChecker((evt, combatData, agentData, skillData) =>
            {
                return CombatData.FindRelatedEvents(combatData.GetBuffData(Aegis).OfType<BuffApplyEvent>(), evt.Time)
                    .Any(apply => apply.By == evt.Dst && apply.To == evt.Dst && apply.AppliedDuration + ServerDelayConstant >= 20000 && apply.AppliedDuration - ServerDelayConstant <= 40000);
            }) // identify advance by self-applied 20s to 40s aegis
            .UsingNotAccurate(true),
        new EffectCastFinderByDst(StandYourGround, EffectGUIDs.GuardianShout)
            .UsingDstBaseSpecChecker(Spec.Guardian)
            .UsingChecker((evt, combatData, agentData, skillData) =>
            {
                return 5 <= CombatData.FindRelatedEvents(combatData.GetBuffData(Stability).OfType<BuffApplyEvent>(), evt.Time)
                    .Count(apply => apply.By == evt.Dst && apply.To == evt.Dst);
            }) // identify stand your ground by self-applied 5+ stacks of stability
            .UsingNotAccurate(true),
        // hold the line boons may overlap with save yourselves/pure of voice

        // Signets
        new EffectCastFinderByDst(SignetOfJudgmentSkill, EffectGUIDs.GuardianSignetOfJudgement2)
            .UsingDstBaseSpecChecker(Spec.Guardian),
        new DamageCastFinder(LesserSignetOfWrath, LesserSignetOfWrath)
            .UsingOrigin(EIData.InstantCastFinder.InstantCastOrigin.Trait),
        
        //new DamageCastFinder(9097,9097), // Symbol of Blades
        new DamageCastFinder(GlacialHeart, GlacialHeart)
            .UsingOrigin(EIData.InstantCastFinder.InstantCastOrigin.Trait)
            .WithBuilds(GW2Builds.StartOfLife, GW2Builds.March2024BalanceAndCerusLegendary),
        new EXTHealingCastFinder(GlacialHeartHeal, GlacialHeartHeal)
            .UsingOrigin(EIData.InstantCastFinder.InstantCastOrigin.Trait)
            .WithBuilds(GW2Builds.March2024BalanceAndCerusLegendary),
        new DamageCastFinder(ShatteredAegis, ShatteredAegis)
            .UsingOrigin(EIData.InstantCastFinder.InstantCastOrigin.Trait),
        new EXTHealingCastFinder(SelflessDaring, SelflessDaring)
            .UsingOrigin(EIData.InstantCastFinder.InstantCastOrigin.Trait),
        // Pistol    
        new EffectCastFinder(DetonateJurisdiction, EffectGUIDs.GuardianDetonateJurisdictionLevel1)
            .UsingSrcBaseSpecChecker(Spec.Guardian),
        new EffectCastFinder(DetonateJurisdiction, EffectGUIDs.GuardianDetonateJurisdictionLevel2)
            .UsingSrcBaseSpecChecker(Spec.Guardian),
        new EffectCastFinder(DetonateJurisdiction, EffectGUIDs.GuardianDetonateJurisdictionLevel3)
            .UsingSrcBaseSpecChecker(Spec.Guardian),
    ];


    internal static readonly IReadOnlyList<DamageModifierDescriptor> OutgoingDamageModifiers =
    [
        // Zeal
        new BuffOnFoeDamageModifier(Mod_FieryWrath, Burning, "Fiery Wrath", "7% on burning target", DamageSource.NoPets, 7.0, DamageType.Strike, DamageType.All, Source.Guardian, ByPresence, BuffImages.FieryWrath, DamageModifierMode.All),
        new BuffOnFoeDamageModifier(Mod_SymbolicExposure, Vulnerability, "Symbolic Exposure", "5% on vuln target", DamageSource.NoPets, 5.0, DamageType.Strike, DamageType.All, Source.Guardian, ByPresence, BuffImages.SymbolicExposure, DamageModifierMode.All),
        new BuffOnActorDamageModifier(Mod_SymbolicAvenger, SymbolicAvenger, "Symbolic Avenger", "2% per stack", DamageSource.NoPets, 2.0, DamageType.Strike, DamageType.All, Source.Guardian, ByStack, BuffImages.SymbolicAvenger, DamageModifierMode.All)
            .WithBuilds(GW2Builds.July2019Balance),
        // Radiance
        new BuffOnActorDamageModifier(Mod_Retribution, Retaliation, "Retribution", "10% under retaliation", DamageSource.NoPets, 10.0, DamageType.Strike, DamageType.All, Source.Guardian, ByPresence, BuffImages.RetributionTrait, DamageModifierMode.All)
            .WithBuilds(GW2Builds.StartOfLife, GW2Builds.May2021Balance),
        new BuffOnActorDamageModifier(Mod_Retribution, Resolution, "Retribution", "10% under resolution", DamageSource.NoPets, 10.0, DamageType.Strike, DamageType.All, Source.Guardian, ByPresence, BuffImages.RetributionTrait, DamageModifierMode.All)
            .WithBuilds(GW2Builds.May2021Balance),
        // Virtues
        new BuffOnActorDamageModifier(Mod_UnscathedContenderAegis, Aegis, "Unscathed Contender", "20% under aegis", DamageSource.NoPets, 20.0, DamageType.Strike, DamageType.All, Source.Guardian, ByPresence, BuffImages.UnscathedContender, DamageModifierMode.All)
            .WithBuilds(GW2Builds.StartOfLife, GW2Builds.February2023Balance),
        new BuffOnActorDamageModifier(Mod_UnscathedContenderAegis, Aegis, "Unscathed Contender (Aegis)", "7% under aegis", DamageSource.NoPets, 7.0, DamageType.Strike, DamageType.All, Source.Guardian, ByPresence, BuffImages.UnscathedContender, DamageModifierMode.All)
            .WithBuilds(GW2Builds.February2023Balance),
        new DamageLogDamageModifier(Mod_UnscathedContenderHP, "Unscathed Contender (HP)", "7% if hp >=90%", DamageSource.NoPets, 7.0, DamageType.Strike, DamageType.All, Source.Guardian, BuffImages.UnscathedContender, (x, log) => x.IsOverNinety, DamageModifierMode.All)
            .WithBuilds( GW2Builds.February2023Balance),
        new BuffOnActorDamageModifier(Mod_PowerOfTheVirtuous, NumberOfBoons, "Power of the Virtuous", "1% per boon", DamageSource.NoPets, 1.0, DamageType.Strike, DamageType.All, Source.Guardian, ByStack, BuffImages.PowerOfTheVirtuous,  DamageModifierMode.All)
            .WithBuilds(GW2Builds.StartOfLife, GW2Builds.May2021Balance),
        new BuffOnActorDamageModifier(Mod_InspiredVirtue, NumberOfBoons, "Inspired Virtue", "1% per boon", DamageSource.NoPets, 1.0, DamageType.Strike, DamageType.All, Source.Guardian, ByStack, BuffImages.InspiredVirtue, DamageModifierMode.All)
            .WithBuilds(GW2Builds.May2021Balance),
        new BuffOnActorDamageModifier(Mod_InspiringVirtue, InspiringVirtue, "Inspiring Virtue", "10% (6s) after activating a virtue ", DamageSource.NoPets, 10.0, DamageType.Strike, DamageType.All, Source.Guardian, ByPresence, BuffImages.VirtuousSolace, DamageModifierMode.All)
            .WithBuilds(GW2Builds.February2020Balance),
    ];

    internal static readonly IReadOnlyList<DamageModifierDescriptor> IncomingDamageModifiers =
    [
        new BuffOnActorDamageModifier(Mod_SignetOfJudgment, SignetOfJudgmentBuff, "Signet of Judgment", "-10%", DamageSource.NoPets, -10, DamageType.StrikeAndCondition, DamageType.All, Source.Guardian, ByPresence, BuffImages.SignetOfJudgment, DamageModifierMode.All),
        new BuffOnActorDamageModifier(Mod_SignetOfJudgmentPI, SignetOfJudgmentPI, "Signet of Judgment (PI)", "-12%", DamageSource.NoPets, -12, DamageType.StrikeAndCondition, DamageType.All, Source.Guardian, ByPresence, BuffImages.SignetOfJudgment, DamageModifierMode.All),
        new CounterOnActorDamageModifier(Mod_RenewedFocus, RenewedFocus, "Renewed Focus", "Invulnerable", DamageSource.NoPets, DamageType.All, DamageType.All, Source.Guardian, BuffImages.RenewedFocus, DamageModifierMode.All)
    ];

    internal static readonly IReadOnlyList<Buff> Buffs =
    [        
        // Skills
        new Buff("Zealot's Flame", ZealotsFlameBuff, Source.Guardian, BuffStackType.Queue, 25, BuffClassification.Other, BuffImages.ZealotsFlame),
        new Buff("Purging Flames", PurgingFlames, Source.Guardian, BuffClassification.Other, BuffImages.PurgingFlames),
        new Buff("Litany of Wrath", LitanyOfWrath, Source.Guardian, BuffClassification.Other, BuffImages.LitanyOfWrath),//.WithBuilds(GW2Builds.StartOfLife, GW2Builds.November2023Balance),
        //new Buff("Litany of Wrath", LitanyOfWrath , Source.Berserker, BuffStackType.Queue, 9, BuffClassification.Other, BuffImages.BloodReckoning).WithBuilds(GW2Builds.November2023Balance),// TBC: looks like a similar change they made to Blood Reckning when they reworked Dead or Alive durint October2022Balance
        new Buff("Renewed Focus", RenewedFocus, Source.Guardian, BuffClassification.Other, BuffImages.RenewedFocus),
        new Buff("Shield of Wrath", ShieldOfWrathBuff, Source.Guardian, BuffStackType.Stacking, 3, BuffClassification.Other, BuffImages.ShieldOfWrath),
        new Buff("Binding Blade (Self)", BindingBladeSelf, Source.Guardian, BuffStackType.Stacking, 25, BuffClassification.Other, BuffImages.BindingBlade),
        new Buff("Binding Blade", BindingBlade, Source.Guardian, BuffClassification.Other, BuffImages.BindingBlade),
        new Buff("Banished", Banished, Source.Guardian, BuffStackType.StackingConditionalLoss, 25, BuffClassification.Other, BuffImages.Banish),
        new Buff("Merciful Intervention (Self)", MercifulAndJudgesInterventionSelfBuff, Source.Guardian, BuffClassification.Support, BuffImages.MercifulIntervention),
        new Buff("Merciful Intervention (Target)", MercifulInterventionTargetBuff, Source.Guardian, BuffClassification.Support, BuffImages.MercifulIntervention),
        // Signets
        new Buff("Signet of Resolve", SignetOfResolve, Source.Guardian, BuffClassification.Other, BuffImages.SignetOfResolve),
        new Buff("Signet of Resolve (Shared)", SignetOfResolveShared, Source.Guardian, BuffStackType.Stacking, 25, BuffClassification.Defensive, BuffImages.SignetOfResolve)
            .WithBuilds(GW2Builds.StartOfLife, GW2Builds.June2022Balance),
        new Buff("Signet of Resolve (PI)", SignetOfResolvePI, Source.Guardian, BuffClassification.Other, BuffImages.SignetOfResolve)
            .WithBuilds(GW2Builds.June2022Balance),
        new Buff("Bane Signet", BaneSignet, Source.Guardian, BuffClassification.Other, BuffImages.BaneSignet),
        new Buff("Bane Signet (PI)", BaneSignetPI, Source.Guardian, BuffStackType.Stacking, 25, BuffClassification.Offensive, BuffImages.BaneSignet)
            .WithBuilds(GW2Builds.StartOfLife, GW2Builds.June2022Balance),
        new Buff("Bane Signet (PI)", BaneSignetPI, Source.Guardian, BuffClassification.Other, BuffImages.BaneSignet)
            .WithBuilds(GW2Builds.June2022Balance),
        new Buff("Signet of Judgment", SignetOfJudgmentBuff, Source.Guardian, BuffClassification.Other, BuffImages.SignetOfJudgment),
        new Buff("Signet of Judgment (PI)", SignetOfJudgmentPI, Source.Guardian, BuffStackType.Stacking, 25, BuffClassification.Defensive, BuffImages.SignetOfJudgment)
            .WithBuilds(GW2Builds.StartOfLife, GW2Builds.June2022Balance),
        new Buff("Signet of Judgment (PI)", SignetOfJudgmentPI, Source.Guardian, BuffClassification.Other, BuffImages.SignetOfJudgment)
            .WithBuilds(GW2Builds.June2022Balance),
        new Buff("Signet of Mercy", SignetOfMercyBuff, Source.Guardian, BuffClassification.Other, BuffImages.SignetOfMercy),
        new Buff("Signet of Mercy (PI)", SignetOfMercyPI, Source.Guardian, BuffStackType.Stacking, 25, BuffClassification.Defensive, BuffImages.SignetOfMercy)
            .WithBuilds(GW2Builds.StartOfLife, GW2Builds.June2022Balance),
        new Buff("Signet of Mercy (PI)", SignetOfMercyPI, Source.Guardian, BuffClassification.Other, BuffImages.SignetOfMercy)
            .WithBuilds(GW2Builds.June2022Balance),
        new Buff("Signet of Wrath", SignetOfWrath, Source.Guardian, BuffClassification.Other, BuffImages.SignetOfWrath),
        new Buff("Signet of Wrath (PI)", SignetOfWrathPI, Source.Guardian, BuffStackType.Stacking, 25, BuffClassification.Offensive, BuffImages.SignetOfWrath)
            .WithBuilds(GW2Builds.StartOfLife, GW2Builds.June2022Balance),
        new Buff("Signet of Wrath (PI)", SignetOfWrathPI, Source.Guardian, BuffClassification.Other, BuffImages.SignetOfWrath)
            .WithBuilds(GW2Builds.June2022Balance),
        new Buff("Signet of Courage", SignetOfCourage, Source.Guardian, BuffClassification.Other, BuffImages.SignetOfCourage),
        new Buff("Signet of Courage (Shared)", SignetOfCourageShared , Source.Guardian, BuffStackType.Stacking, 25, BuffClassification.Defensive, BuffImages.SignetOfCourage).WithBuilds(GW2Builds.StartOfLife, GW2Builds.June2022Balance),
        new Buff("Signet of Courage (PI)", SignetOfCouragePI , Source.Guardian, BuffClassification.Other, BuffImages.SignetOfCourage)
            .WithBuilds(GW2Builds.June2022Balance),
        // Virtues
        new Buff("Virtue of Justice", VirtueOfJustice, Source.Guardian, BuffClassification.Other, BuffImages.VirtueOfJustice),
        new Buff("Virtue of Courage", VirtueOfCourage, Source.Guardian, BuffClassification.Other, BuffImages.VirtueOfCourage),
        new Buff("Virtue of Resolve", VirtueOfResolve, Source.Guardian, BuffClassification.Other, BuffImages.VirtueOfResolve),
        new Buff("Justice", Justice, Source.Guardian, BuffClassification.Other, BuffImages.VirtueOfJustice),
        // Traits
        new Buff("Strength in Numbers", StrengthinNumbers, Source.Guardian, BuffClassification.Defensive, BuffImages.StrengthInNumbers)
            .WithBuilds(GW2Builds.StartOfLife, GW2Builds.June2022Balance),
        new Buff("Invigorated Bulwark", InvigoratedBulwark, Source.Guardian, BuffStackType.Stacking, 5, BuffClassification.Other, BuffImages.InvigoratedBulwark),
        new Buff("Virtue of Resolve (Battle Presence)", VirtueOfResolveBattlePresence, Source.Guardian, BuffStackType.Queue, 2, BuffClassification.Defensive, BuffImages.BattlePresence),
        new Buff("Virtue of Resolve (Battle Presence - Absolute Resolve)", VirtueOfResolveBattlePresenceAbsoluteResolve, Source.Guardian, BuffStackType.Queue, 2, BuffClassification.Defensive, BuffImages.VirtueOfResolve),
        new Buff("Symbolic Avenger", SymbolicAvenger, Source.Guardian, BuffStackType.Stacking, 5, BuffClassification.Other, BuffImages.SymbolicAvenger)
            .WithBuilds(GW2Builds.July2019Balance),
        new Buff("Inspiring Virtue", InspiringVirtue, Source.Guardian, BuffStackType.Queue, 99, BuffClassification.Other, BuffImages.VirtuousSolace)
            .WithBuilds(GW2Builds.February2020Balance, GW2Builds.February2020Balance2),
        new Buff("Inspiring Virtue", InspiringVirtue, Source.Guardian, BuffClassification.Other, BuffImages.VirtuousSolace).WithBuilds(GW2Builds.February2020Balance2),
        new Buff("Force of Will", ForceOfWill, Source.Guardian, BuffClassification.Other, BuffImages.ForceOfWill),
        // Spear
        new Buff("Symbol of Luminance", SymbolOfLuminanceBuff, Source.Guardian, BuffClassification.Other, BuffImages.SymbolOfLuminance),
        new Buff("Illuminated", Illuminated, Source.Guardian, BuffClassification.Other, BuffImages.Illuminated),
    ];

    private static HashSet<int> Minions =
    [
        (int)MinionID.BowOfTruth,
        (int)MinionID.HammerOfWisdom,
        (int)MinionID.ShieldOfTheAvenger,
        (int)MinionID.SwordOfJustice,
    ];
    internal static bool IsKnownMinionID(int id)
    {
        return Minions.Contains(id);
    }

    internal static void ComputeProfessionCombatReplayActors(PlayerActor player, ParsedEvtcLog log, CombatReplay replay)
    {
        Color color = Colors.Guardian;

        // Ring of Warding (Hammer 5)
        if (log.CombatData.TryGetEffectEventsBySrcWithGUID(player.AgentItem, EffectGUIDs.GuardianRingOfWarding, out var ringOfWardings))
        {
            var skill = new SkillModeDescriptor(player, Spec.Guardian, RingOfWarding, SkillModeCategory.CC);
            foreach (EffectEvent effect in ringOfWardings)
            {
                (long start, long end) lifespan = effect.ComputeLifespan(log, 5000);
                AddCircleSkillDecoration(replay, effect, color, skill, lifespan, 180, ParserIcons.EffectRingOfWarding);
            }
        }
        // Line of Warding (Staff 5)
        if (log.CombatData.TryGetEffectEventsBySrcWithGUID(player.AgentItem, EffectGUIDs.GuardianLineOfWarding, out var lineOfWardings))
        {
            var skill = new SkillModeDescriptor(player, Spec.Guardian, LineOfWarding, SkillModeCategory.CC);
            foreach (EffectEvent effect in lineOfWardings)
            {
                (long, long) lifespan = effect.ComputeLifespan(log, 5000);
                var connector = new PositionConnector(effect.Position);
                var rotationConnector = new AngleConnector(effect.Rotation.Z);
                replay.Decorations.Add(new RectangleDecoration(500, 70, lifespan, color, 0.5, connector).UsingFilled(false).UsingRotationConnector(rotationConnector).UsingSkillMode(skill));
                replay.Decorations.Add(new IconDecoration(ParserIcons.EffectLineOfWarding, CombatReplaySkillDefaultSizeInPixel, CombatReplaySkillDefaultSizeInWorld, 0.5f, lifespan, connector).UsingSkillMode(skill));
            }
        }
        // Wall of Reflection
        if (log.CombatData.TryGetEffectEventsBySrcWithGUID(player.AgentItem, EffectGUIDs.GuardianWallOfReflection, out var wallOfReflections))
        {
            var skill = new SkillModeDescriptor(player, Spec.Guardian, WallOfReflection, SkillModeCategory.ProjectileManagement);
            foreach (EffectEvent effect in wallOfReflections)
            {
                (long, long) lifespan = effect.ComputeDynamicLifespan(log, 10000); // 10s with trait
                var connector = new PositionConnector(effect.Position);
                var rotationConnector = new AngleConnector(effect.Rotation.Z);
                replay.Decorations.Add(new RectangleDecoration(500, 70, lifespan, color, 0.5, connector).UsingFilled(false).UsingRotationConnector(rotationConnector).UsingSkillMode(skill));
                replay.Decorations.Add(new IconDecoration(ParserIcons.EffectWallOfReflection, CombatReplaySkillDefaultSizeInPixel, CombatReplaySkillDefaultSizeInWorld, 0.5f, lifespan, connector).UsingSkillMode(skill));
            }
        }
        // Sanctuary
        if (log.CombatData.TryGetEffectEventsBySrcWithGUID(player.AgentItem, EffectGUIDs.GuardianSanctuary, out var sanctuaries))
        {
            var skill = new SkillModeDescriptor(player, Spec.Guardian, SanctuaryGuardian, SkillModeCategory.CC | SkillModeCategory.ProjectileManagement);
            foreach (EffectEvent effect in sanctuaries)
            {
                (long, long) lifespan = effect.ComputeDynamicLifespan(log, 7000); // 7s with trait
                AddCircleSkillDecoration(replay, effect, color, skill, lifespan, 240, ParserIcons.EffectSanctuary);
            }
        }
        // Shield of the Avenger
        if (log.CombatData.TryGetEffectEventsByMasterWithGUID(player.AgentItem, EffectGUIDs.GuardianShieldOfTheAvenger, out var shieldOfTheAvengers))
        {
            var skill = new SkillModeDescriptor(player, Spec.Guardian, ShieldOfTheAvenger, SkillModeCategory.ProjectileManagement);
            foreach (EffectEvent effect in shieldOfTheAvengers)
            {
                (long start, long end) lifespan = effect.ComputeLifespan(log, 5000);
                AddCircleSkillDecoration(replay, effect, color, skill, lifespan, 180, ParserIcons.EffectShieldOfTheAvenger);
            }
        }

        // Signet of Mercy
        if (log.CombatData.TryGetEffectEventsBySrcWithGUID(player.AgentItem, EffectGUIDs.GuardianSignetOfMercyLightTray, out var signetOfMercy))
        {
            var skill = new SkillModeDescriptor(player, Spec.Guardian, SignetOfMercySkill, SkillModeCategory.Heal);
            foreach (EffectEvent effect in signetOfMercy)
            {
                // Only displays if fully channeled.
                (long start, long end) lifespan = effect.ComputeLifespanWithSecondaryEffectAndPosition(log, EffectGUIDs.GuardianGenericLightEffect, effect.Duration);
                AddCircleSkillDecoration(replay, effect, color, skill, lifespan, 180, ParserIcons.EffectSignetOfMercy);
            }
        }

        // Hunter's Ward
        if (log.CombatData.TryGetEffectEventsBySrcWithGUID(player.AgentItem, EffectGUIDs.DragonhunterHuntersWardCage, out var huntersWards))
        {
            var skill = new SkillModeDescriptor(player, Spec.Guardian, HuntersWard, SkillModeCategory.CC);
            foreach (EffectEvent effect in huntersWards)
            {
                long duration = log.FightData.Logic.SkillMode == FightLogic.SkillModeEnum.WvW ? 3000 : 5000;
                (long start, long end) lifespan = effect.ComputeDynamicLifespan(log, duration);
                AddCircleSkillDecoration(replay, effect, color, skill, lifespan, 140, ParserIcons.EffectHuntersWard); // radius approximation
            }
        }

        // Symbol of Energy
        if (log.CombatData.TryGetEffectEventsBySrcWithGUID(player.AgentItem, EffectGUIDs.DragonhunterSymbolOfEnergy, out var symbolsOfEnergy))
        {
            var skill = new SkillModeDescriptor(player, Spec.Guardian, SymbolOfEnergy, SkillModeCategory.ShowOnSelect);
            foreach (EffectEvent effect in symbolsOfEnergy)
            {
                (long start, long end) lifespan = effect.ComputeLifespan(log, 4000);
                AddCircleSkillDecoration(replay, effect, color, skill, lifespan, 180, ParserIcons.EffectSymbolOfEnergy);
            }
        }

        // Symbol of Vengeance
        if (log.CombatData.TryGetEffectEventsBySrcWithGUID(player.AgentItem, EffectGUIDs.FirebrandSymbolOfVengeance1, out var symbolsOfVengeance))
        {
            var skill = new SkillModeDescriptor(player, Spec.Guardian, SymbolOfVengeance, SkillModeCategory.ShowOnSelect | SkillModeCategory.CC); // CC when traited
            foreach (EffectEvent effect in symbolsOfVengeance)
            {
                (long start, long end) lifespan = effect.ComputeLifespan(log, 4000);
                AddCircleSkillDecoration(replay, effect, color, skill, lifespan, 180, ParserIcons.EffectSymbolOfVengeance);
            }
        }

        // Symbol of Punishment
        if (log.CombatData.TryGetEffectEventsBySrcWithGUID(player.AgentItem, EffectGUIDs.GuardianSymbolOfPunishment1, out var symbolsOfPunishment))
        {
            var skill = new SkillModeDescriptor(player, Spec.Guardian, SymbolOfPunishment, SkillModeCategory.ShowOnSelect);
            foreach (EffectEvent effect in symbolsOfPunishment)
            {
                (long start, long end) lifespan = effect.ComputeLifespan(log, 4000);
                AddCircleSkillDecoration(replay, effect, color, skill, lifespan, 180, ParserIcons.EffectSymbolOfPunishment);
            }
        }

        // Symbol of Blades
        if (log.CombatData.TryGetEffectEventsBySrcWithGUID(player.AgentItem, EffectGUIDs.GuardianSymbolOfBlades, out var symbolsOfBlades))
        {
            var skill = new SkillModeDescriptor(player, Spec.Guardian, SymbolOfBlades, SkillModeCategory.ShowOnSelect);
            foreach (EffectEvent effect in symbolsOfBlades)
            {
                (long start, long end) lifespan = effect.ComputeLifespan(log, 5000);
                AddCircleSkillDecoration(replay, effect, color, skill, lifespan, 180, ParserIcons.EffectSymbolOfBlades);
            }
        }

        // Symbol of Resolution
        if (log.CombatData.TryGetEffectEventsBySrcWithGUID(player.AgentItem, EffectGUIDs.GuardianSymbolOfResolution, out var symbolsOfResolution))
        {
            var skill = new SkillModeDescriptor(player, Spec.Guardian, SymbolOfWrath_SymbolOfResolution, SkillModeCategory.ShowOnSelect);
            foreach (EffectEvent effect in symbolsOfResolution)
            {
                (long start, long end) lifespan = effect.ComputeLifespan(log, 4000);
                AddCircleSkillDecoration(replay, effect, color, skill, lifespan, 180, ParserIcons.EffectSymbolOfResolution);
            }
        }

        // Solar Storm
        if (log.CombatData.TryGetEffectEventsBySrcWithGUID(player.AgentItem, EffectGUIDs.GuardianSolarStormAerealEffect, out var solarStorms))
        {
            // Skill definition has radius of 360, each hit has a radius of 180.
            var skill = new SkillModeDescriptor(player, Spec.Guardian, SolarStorm, SkillModeCategory.ShowOnSelect);
            foreach (EffectEvent effect in solarStorms)
            {
                (long start, long end) lifespan = effect.ComputeLifespan(log, 2000); // 2000 apromixated duration
                AddCircleSkillDecoration(replay, effect, color, skill, lifespan, 360, ParserIcons.EffectSolarStorm);
            }
            // Spear Impact
            if (log.CombatData.TryGetEffectEventsBySrcWithGUID(player.AgentItem, EffectGUIDs.GuardianSolarStormSpearImpact, out var spearImpacts))
            {
                foreach (EffectEvent effect in spearImpacts)
                {
                    (long start, long end) lifespan = effect.ComputeLifespan(log, 500); // 500 as a visual display
                    var connector = new PositionConnector(effect.Position);
                    replay.Decorations.Add(new CircleDecoration(180, lifespan, color, 0.5, connector).UsingFilled(false).UsingSkillMode(skill));
                }
            }
        }

        // Symbol of Luminance
        if (log.CombatData.TryGetEffectEventsBySrcWithGUID(player.AgentItem, EffectGUIDs.GuardianSymbolOfLuminance3, out var symbolsOfLuminance))
        {
            var skill = new SkillModeDescriptor(player, Spec.Guardian, SymbolOfLuminanceSkill, SkillModeCategory.CC);
            foreach (EffectEvent effect in symbolsOfLuminance)
            {
                (long start, long end) lifespan = effect.ComputeLifespan(log, 4000);
                AddCircleSkillDecoration(replay, effect, color, skill, lifespan, 180, ParserIcons.EffectSymbolOfLuminance);
            }
        }
    }
}
