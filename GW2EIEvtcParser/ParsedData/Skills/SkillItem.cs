﻿using GW2EIEvtcParser.EIData;
using GW2EIEvtcParser.ParserHelpers;
using GW2EIGW2API;
using GW2EIGW2API.GW2API;
using static GW2EIEvtcParser.ArcDPSEnums;
using static GW2EIEvtcParser.SkillIDs;

namespace GW2EIEvtcParser.ParsedData;

public class SkillItem
{

    internal static (long, long) GetArcDPSCustomIDs(EvtcVersionEvent evtcVersion)
    {
        if (evtcVersion.Build >= ArcDPSBuilds.InternalSkillIDsChange)
        {
            return (ArcDPSDodge20220307, ArcDPSGenericBreakbar20220307);
        }
        else
        {
            return (ArcDPSDodge, ArcDPSGenericBreakbar);
        }
    }

    private static readonly Dictionary<long, string> _overrideNames = new()
    {
        { WeaponSwap, "Weapon Swap" },
        { Resurrect, "Resurrect" },
        { Bandage, "Bandage" },
        { ArcDPSDodge, "Dodge" },
        { ArcDPSDodge20220307, "Dodge" },
        { ArcDPSGenericBreakbar, "Generic Breakbar" },
        { ArcDPSGenericBreakbar20220307, "Generic Breakbar" },
        { WaterBlastCombo1, "Water Blast Combo" },
        { WaterBlastCombo2, "Water Blast Combo" },
        { WaterLeapCombo, "Water Leap Combo" },
        { LightningLeapCombo, "Lightning Leap Combo" },
        { MendingMight, "Mending Might" },
        { InvigoratingBond, "Invigorating Bond" },
        #region Sigils
        { WaveOfHealing_MinorSigilOfWater, "Wave of Healing (Minor Sigil of Water)" },
        { WaveOfHealing_MajorSigilOfWater, "Wave of Healing (Major Sigil of Water)" },
        { WaveOfHealing_SuperiorSigilOfWater, "Wave of Healing (Superior Sigil of Water)" },
        { WaveOfHealing_MinorSigilOfRenewal, "Wave of Healing (Minor Sigil of Renewal)" },
        { WaveOfHealing_MajorSigilOfRenewal, "Wave of Healing (Major Sigil of Renewal)" },
        { WaveOfHealing_SuperiorSigilOfRenewal, "Wave of Healing (Superior Sigil of Renewal)" },
        { FrostBurst_MinorSigilOfHydromancy, "Frost Burst (Minor Sigil of Hydromancy)" },
        { FrostBurst_MajorSigilOfHydromancy, "Frost Burst (Major Sigil of Hydromancy)" },
        { FrostBurst_SuperiorSigilOfHydromancy, "Frost Burst (Superior Sigil of Hydromancy)" },
        { RingOfEarth_MinorSigilOfGeomancy, "Ring of Earth (Minor Sigil of Geomancy)" },
        { RingOfEarth_MajorSigilOfGeomancy, "Ring of Earth (Major Sigil of Geomancy)" },
        { RingOfEarth_SuperiorSigilOfGeomancy, "Ring of Earth (Superior Sigil of Geomancy)" },
        { LightningStrike_SigilOfAir, "Lightning Strike (Minor/Major/Superior Sigil of Air)" },
        { FlameBlast_SigilOfFire, "Flame Blast (Minor/Major/Superior Sigil of Fire)" },
        { Snowball_SigilOfMischief, "Snowball (Minor/Major/Superior Sigil of Mischief)" },
        { SuperiorSigilOfSeverance, "Superior Sigil of Severance" },
        { MinorSigilOfDoom, "Minor Sigil of Doom" },
        { MajorSigilOfDoom, "Major Sigil of Doom" },
        { SuperiorSigilOfDoom, "Superior Sigil of Doom" },
        { MinorSigilOfBlood, "Minor Sigil of Blood" },
        { MajorSigilOfBlood, "Major Sigil of Blood" },
        { SuperiorSigilOfBlood, "Superior Sigil of Blood" },
        { MajorSigilOfLeeching, "Major Sigil of Leeching" },
        { SuperiorSigilOfLeeching, "Superior Sigil of Leeching" },
        { SuperiorSigilOfVision, "Superior Sigil of Vision" },
        { SuperiorSigilOfConcentration, "Superior Sigil of Concentration" },
        #endregion Sigils
        #region Runes
        { RuneOfNightmare, "Rune of the Nightmare" },
        { FrozenBurst_RuneOfIce, "Frozen Burst (Rune of the Ice)" },
        { HuntersCall_RuneOfMadKing, "Hunter's Call (Rune of the Mad King)" },
        { ArtilleryBarrage_RuneofCitadel, "Artillery Barrage (Rune of the Citadel)" },
        { HandOfGrenth_RuneOfGrenth, "Hand of Grenth (Rune of Grenth)" },
        #endregion Runes
        { PortalEntranceWhiteMantleWatchwork, "Portal Entrance" },
        { PortalExitWhiteMantleWatchwork, "Portal Exit" },
        { PlateOfSpicyMoaWingsGastricDistress, "Plate of Spicy Moa Wings (Gastric Distress)" },
        { ThrowGunkEttinGunk, "Throw Gunk (Ettin Gunk)" },
        { SmashBottle, "Smash (Bottle)" },
        { ThrowBottle, "Throw (Bottle)" },
        { ThrowNetUnderwaterNet, "Throw Net (Underwater Net)" },
        // Mounts
        #region Mounts
        { BondOfLifeSkill, "Bond of Life" },
        { BondOfVigorSkill, "Bond of Vigor" },
        { BondOfFaithSkill, "Bond of Faith" },
        { StealthMountSkill, "Stealth 2.0" },
        // Skyscale
        { SkyscaleSkill, "Skyscale" },
        { SkyscaleFireballSkill, "Fireball" },
        { SkyscaleBlastSkill, "Blast" },
        { SkyscaleBlastDamage, "Blast (Damage)" },
        #endregion Mounts
        #region Relics
        { RelicOfTheWizardsTower, "Relic of the Wizard's Tower" },
        { RelicOfIsgarrenTargetBuff, "Relic of Isgarren" },
        { RelicOfTheDragonhunterTargetBuff, "Relic of the Dragonhunter" },
        { RelicOfPeithaTargetBuff, "Relic of Peitha" },
        { RelicOfPeithaBlade, "Relic of Peitha (Blade)" },
        { RelicOfFireworksBuffLoss, "Relic of Fireworks (Buff Loss)" },
        { RelicOfTheFlockBarrier, "Relic of the Flock (Barrier)" },
        { RelicOfMercyHealing, "Relic of Mercy" },
        { MabonsStrength, "Relic of Mabon" },
        { NouryssHungerDamageBuff, "Relic of Nourys" },
        { RelicOfTheFoundingBarrier, "Relic of the Founding (Barrier)" },
        { RelicOfTheClawBuffLoss, "Relic of the Claw (Buff Loss)" },
        { RelicOfTheStormsingerChain, "Relic of the Stormsinger (Chain)" },
        #endregion Relics
        #region Elementalist
        { DualFireAttunement, "Dual Fire Attunement" },
        { FireWaterAttunement, "Fire Water Attunement" },
        { FireAirAttunement, "Fire Air Attunement" },
        { FireEarthAttunement, "Fire Earth Attunement" },
        { DualWaterAttunement, "Dual Water Attunement" },
        { WaterFireAttunement, "Water Fire Attunement" },
        { WaterAirAttunement, "Water Air Attunement" },
        { WaterEarthAttunement, "Water Earth Attunement" },
        { DualAirAttunement, "Dual Air Attunement" },
        { AirFireAttunement, "Air Fire Attunement" },
        { AirWaterAttunement, "Air Water Attunement" },
        { AirEarthAttunement, "Air Earth Attunement" },
        { DualEarthAttunement, "Dual Earth Attunement" },
        { EarthFireAttunement, "Earth Fire Attunement" },
        { EarthWaterAttunement, "Earth Water Attunement" },
        { EarthAirAttunement, "Earth Air Attunement" },
        { ShatteringIceDamage, "Shattering Ice (Hit)" },
        { ArcaneShieldDamage, "Arcane Shield (Explosion)" },
        { FirestormGlyphOfStormsOrFieryGreatsword, "Firestorm (Glyph of Storms / Fiery Greatsword)" },
        #endregion Elementalist
        #region Engineer
        { HealingMistOrSoothingDetonation, "Healing Mist or Soothing Detonation" },
        { MechCoreBarrierEngine, "Mech Core: Barrier Engine" },
        { MedBlasterHeal, "Med Blaster (Heal)" },
        { SoothingDetonation, "Soothing Detonation" },
        { HealingTurretHeal, "Healing Turret (Heal)" },
        { BladeBurstOrParticleAccelerator, "Blade Burst or Particle Accelerator" },
        { DetonateThrowMineOrMineField, "Detonate (Throw Mine / Mine Field)" },
        #endregion Engineer
        #region Guardian
        { SelflessDaring, "Selfless Daring" }, // The game maps this name incorrectly to "Selflessness Daring"
        { ProtectorsStrikeCounterHit, "Protector's Strike (Counter Hit)" },
        { HuntersVerdictPull, "Hunter's Verdict (Pull)" },
        { MantraOfSolace, "Mantra of Solace" },
        { RestoringReprieveOrRejunevatingRespite, "Restoring Reprieve or Rejunevating Respite" },
        { OpeningPassageOrClarifiedConclusion, "Opening Passage or Clarified Conclusion" },
        { PotentHasteOrOverwhelmingCelerity, "Potent Haste or Overwhelming Celerity" },
        { PortentOfFreedomOrUnhinderedDelivery, "Portent of Freedom or Unhindered Delivery" },
        { RushingJusticeStrike, "Rushing Justice (Hit)" },
        { ExecutionersCallingDualStrike, "Executioner's Calling (Dual Strike)" },
        { FireJurisdictionLevel1, "Fire Jurisdiction (Level 1)" },
        { FireJurisdictionLevel2, "Fire Jurisdiction (Level 2)" },
        { FireJurisdictionLevel3, "Fire Jurisdiction (Level 3)" },
        #endregion Guardian
        #region Mesmer
        { PowerReturn, "Power Return" },
        { PowerCleanse, "Power Cleanse" },
        { PowerBreak, "Power Break" },
        { PowerLock, "Power Lock" },
        { BlinkOrPhaseRetreat, "Blink or Phase Retreat" },
        { MirageCloakDodge, "Mirage Cloak" },
        { UnstableBladestormProjectiles, "Unstable Bladestorm (Projectile Hit)" },
        { PhantasmalBerserkerProjectileDamage, "Phantasmal Berserker (Greatsword Projectile Hit)" },
        { HealingPrism, "Healing Prism" },
        #endregion Mesmer
        #region Necromancer
        { DesertEmpowerment, "Desert Empowerment" },
        { SandCascadeBarrier, "Sand Cascade (Barrier)" },
        { SandFlare, "Sand Cascade" },
        { SadisticSearingActivation, "Sadistic Searing (Activation)" },
        { MarkOfBloodOrChillblains, "Mark of Blood / Chillblains" },
        #endregion Necromancer
        #region Ranger
        { WindborneNotes, "Windborne Notes" },
        { NaturalHealing, "Natural Healing" }, // The game does not map this one at all
        { LiveVicariously, "Live Vicariously" }, // The game maps this name incorrectly to "Vigorous Recovery"
        { EntangleDamage, "Entangle (Hit)" },
        { AstralWispAttachment, "Astral Wisp Attachment" },
        { GlyphOfUnityCA, "Glyph of Unity (CA)" },
        { BloodMoonDaze, "Blood Moon (Daze)" },
        { ChargeGazelleMergeSkill, "Charge (Travel)" },
        { ChargeGazelleMergeImpact, "Charge (Impact)" },
        { SmokeAssaultMergeHit, "Smoke Assault (Multi Hit)" },
        { OneWolfPackDamage, "One Wolf Pack (Hit)" },
        { OverbearingSmashLeap, "Overbearing Smash (Leap)" },
        { UnleashedOverbearingSmashLeap, "Unleashed Overbearing Smash (Leap)" },
        { RangerPetSpawned, "Ranger Pet Spawned" },
        { WolfsOnslaughtFollowUp, "Wolf's Onslaught (Follow Up)" },
        #endregion Ranger
        #region Revenant
        { EnergyExpulsion, "Energy Expulsion" },
        { RiftSlashRiftHit, "Rift Slash (Rift Hit)" },
        { UnrelentingAssaultMultihit, "Unrelenting Assault (Multi Hit)" },
        { ImpossibleOddsHit, "Impossible Odds (Hit)" },
        { EmbraceTheDarknessDamage, "Embrace the Darkness (Hit)" },
        { TrueNatureDragon, "True Nature - Dragon" },
        { TrueNatureDemon, "True Nature - Demon" },
        { TrueNatureDwarf, "True Nature - Dwarf" },
        { TrueNatureAssassin, "True Nature - Assassin" },
        { TrueNatureCentaur, "True Nature - Centaur" },
        { DarkrazorsDaringHit, "Darkrazor's Daring (Hit)" },
        { IcerazorsIreHit, "Icerazor's Ire (Hit)" },
        { PhantomsOnslaughtDamage, "Phantom's Onslaught (Hit)" },
        { KallaSummonsDespawnSkill, "Despawn" },
        { KallaSummonsSaluteAnimationSkill, "Salute" },
        { GenerousAbundanceCentaur, "Generous Abundance (Centaur)" },
        { GenerousAbundanceOther, "Generous Abundance (Other)" },
        { BlitzMinesDrop, "Blitz Mines (Drop)" },
        { BlitzMines, "Blitz Mines (Detonation)" },
        #endregion Revenant
        #region Thief
        { EscapistsFortitude, "Escapist's Fortitude" }, // The game maps this to the wrong skill
        { SoulStoneVenomSkill, "Soul Stone Venom" },
        { SoulStoneVenomStrike, "Soul Stone Venom (Hit)" },
        { BasiliskVenomStunBreakbarDamage, "Basilisk Venom (Stun)" },
        { TwilightComboSecondProjectile, "Twilight Combo (Secondary)" },
        { ThievesGuildMinionDespawnSkill, "Despawn" },
        { ImpairingDaggersHit1, "Impairing Daggers (Dagger Hit 1)" },
        { ImpairingDaggersHit2, "Impairing Daggers (Dagger Hit 2)" },
        { ImpairingDaggersHit3, "Impairing Daggers (Dagger Hit 3)" },
        { ImpairingDaggersDaredevilMinionHit1, "Impairing Daggers (Dagger Hit 1)" },
        { ImpairingDaggersDaredevilMinionHit2, "Impairing Daggers (Dagger Hit 2)" },
        { ImpairingDaggersDaredevilMinionHit3, "Impairing Daggers (Dagger Hit 3)" },
        { BoundHit, "Bound (Hit)" },
        { BarbedSpearMelee, "Barbed Spear (Melee)" },
        { BarbedSpearRanged, "Barbed Spear (Ranged)" },
        #endregion Thief
        #region Warrior
        { RushDamage, "Rush (Hit)" },
        { MightyThrowScatter, "Mighty Throw (Scattered Spear)" },
        { HarriersTossAdrenalineLevel1, "Harrier's Toss (Adrenaline Level 1)" },
        { HarriersTossAdrenalineLevel2, "Harrier's Toss (Adrenaline Level 2)" },
        { HarriersTossAdrenalineLevel3, "Harrier's Toss (Adrenaline Level 3)" },
        { BerserkEndSkill, "Berserk (End)" },
        #endregion Warrior
        // Special Forces Training Area
        { MushroomKingsBlessing, "Mushroom King's Blessing (PoV Only)" },
        #region Raids
        // Gorseval
        { GhastlyRampage,"Ghastly Rampage" },
        { ProtectiveShadow,"Protective Shadow" },
        { GhastlyRampageBegin,"Ghastly Rampage (Begin)" },
        // Sabetha
        { ShadowStepSabetha, "Shadow Step" },
        // Slothasor
        { TantrumSkill, "Tantrum Start" },
        { NarcolepsySkill, "Sleeping" },
        { FearMeSlothasor, "Fear Me!" },
        { PurgeSlothasor, "Purge" },
        // Bandit Trio
        { ThrowOilKeg, "Throw (Oil Keg)" },
        // Matthias
        { ShieldHuman, "Shield (Human)" },
        { AbominationTransformation, "Abomination Transformation" },
        { ShieldAbomination, "Shield (Abomination)" },
        // Escort
        { GlennaCap, "Capture" },
        { OverHere, "Over Here!" },
        // Xera
        { InterventionSAK, "Intervetion" },
        // Cairn
        { CelestialDashSAK, "Celestial Dash" },
        // Mursaar Overseer
        { ClaimSAK, "Claim" },
        { DispelSAK, "Dispel" },
        { ProtectSAK, "Protect" },
        // Soulless Horror
        { IssueChallengeSAK, "Issue Challenge" },
        // Broken King
        { NumbingBreachCast, "Numbing Breach (Cast)" },
        // Dhuum
        { MajorSoulSplit, "Major Soul Split" },
        { ExpelEnergySAK, "Expel Energy" },
        // Keep Construct
        { MagicBlastCharge, "Magic Blast Charge" },
        // Conjured Amalgamate
        { ConjuredSlashSAK, "Conjured Slash" },
        { ConjuredProtection, "Conjured Protection" },
        // Adina
        { DoubleRotatingEarthRays, "Double Rotating Earth Rays" },
        { TripleRotatingEarthRays, "Triple Rotating Earth Rays" },
        { Terraform, "Terraform" },
        // Sabir
        { RegenerativeBreakbar, "Regenerative Breakbar" },
        // Qadim the Peerless
        { RuinousNovaCharge, "Ruinous Nova Charge" },
        { FluxDisruptorActivateCast, "Flux Disruptor: Activate" },
        { FluxDisruptorDeactivateCast, "Flux Disruptor: Deactivate" },
        { PlayerLiftUpQadimThePeerless, "Player Lift Up Mechanic" },
        { UnleashSAK, "Unleash" },
        //{56036, "Magma Bomb" },
        { ForceOfRetaliationCast, "Force of Retaliation Cast" },
        { PeerlessQadimTPCenter, "Teleport Center" },
        { EatPylon, "Eat Pylon" },
        { BigMagmaDrop, "Big Magma Drop" },
        // Ura
        { UraDispelSAK, "Dispel" },
        #endregion Raids
        #region Strikes
        // Voice and Claw
        { KodanTeleport, "Kodan Teleport" },
        // Mai Trin (Aetherblade Hideout)
        { ReverseThePolaritySAK, "Reverse the Polarity!" },
        // Cerus
        // - Normal Mode
        { CrushingRegretNM, "Crushing Regret (NM)" },
        { WailOfDespairNM, "Wail of Despair (NM)" },
        { EnviousGazeNM, "Envious Gaze (NM)" },
        { MaliciousIntentNM, "Malicious Intent (NM)" },
        { InsatiableHungerSkillNM, "Insatiable Hunger (NM)" },
        { CryOfRageNM, "Cry of Rage (NM)" },
        // - Empowered Normal Mode
        { CrushingRegretEmpoweredNM, "Crushing Regret (Empowered NM)" },
        { WailOfDespairEmpoweredNM, "Wail of Despair (Empowered NM)" },
        { EnviousGazeEmpoweredNM, "Envious Gaze (Empowered NM)" },
        { MaliciousIntentEmpoweredNM, "Malicious Intent (Empowered NM)" },
        { InsatiableHungerEmpoweredSkillNM, "Insatiable Hunger (Empowered NM)" },
        { CryOfRageEmpoweredNM, "Cry of Rage (Empowered NM)" },
        // - Challenge Mode
        { CrushingRegretCM, "Crushing Regret (CM)" },
        { WailOfDespairCM, "Wail of Despair (CM)" },
        { EnviousGazeCM, "Envious Gaze (CM)" },
        { MaliciousIntentCM, "Malicious Intent (CM)" },
        { InsatiableHungerSkillCM, "Insatiable Hunger (CM)" },
        { CryOfRageCM, "Cry of Rage (CM)" },
        // - Empowered Challenge Mode
        { CrushingRegretEmpoweredCM, "Crushing Regret (Empowered CM)" },
        { WailOfDespairEmpoweredCM, "Wail of Despair (Empowered CM)" },
        { EnviousGazeEmpoweredCM, "Envious Gaze (Empowered CM)" },
        { MaliciousIntentEmpoweredCM, "Malicious Intent (Empowered CM)" },
        { InsatiableHungerEmpoweredSkillCM, "Insatiable Hunger (Empowered CM)" },
        { CryOfRageEmpoweredCM, "Cry of Rage (Empowered CM)" },
        // - Misc
        { PetrifySkill, "Petrify" },
        { EnragedSmashNM, "Enraged Smash (NM)" },
        { EnragedSmashCM, "Enraged Smash (CM)" },
        #endregion Strikes
        #region Fractals
        // Artsariiv
        { NovaLaunchSAK, "Nova Launch" },
        // Arkk
        { HypernovaLaunchSAK, "Hypernova Launch" },
        // Kanaxai
        { FrighteningSpeedWindup, "Frightening Speed (Windup)" },
        { FrighteningSpeedReturn, "Frightening Speed (Return)" },
        { DreadVisageKanaxaiSkill, "Dread Visage (Kanaxai)" },
        { DreadVisageKanaxaiSkillIsland, "Dread Visage (Kanaxai Island)" },
        { DreadVisageAspectSkill, "Dread Visage (Aspect)" },
        { RendingStormSkill, "Rending Storm (Axe)" },
        { GatheringShadowsSkill, "Gathering Shadows (Breakbar)" },
        #endregion Fractals
        #region WvW
        // World vs World
        { WvWSpendingSupplies, "Spending Supply (Building / Repairing)" },
        { WvWPickingUpSupplies, "Picking Up Supplies" },
        // - Arrow Cart
        { DeployArrowCart, "Deploy Arrow Cart" },
        { DeploySuperiorArrowCart, "Deploy Superior Arrow Cart" },
        { DeployGuildArrowCart, "Deploy Guild Arrow Cart" },
        // - Ballista
        { DeployBallista, "Deploy Ballista" },
        { DeploySuperiorBallista, "Deploy Superior Ballista" },
        { DeployGuildBallista, "Deploy Guild Ballista" },
        // - Catapult
        { DeployCatapult, "Deploy Catapult" },
        { DeploySuperiorCatapult, "Deploy Superior Catapult" },
        { DeployGuildCatapult, "Deploy Guild Catapult" },
        // - Flame Ram
        { DeployFlameRam, "Deploy Flame Ram" },
        { DeploySuperiorFlameRam, "Deploy Superior Flame Ram" },
        { DeployGuildFlameRam, "Deploy Guild Flame Ram" },
        // - Golem
        { DeployAlphaSiegeSuit, "Deploy Alpha Siege Golem" },
        { DeployOmegaSiegeSuit, "Deploy Omega Siege Golem" },
        { DeployGuildSiegeSuit, "Deploy Guild Siege Golem" },
        // - Shield Generator
        { DeployShieldGenerator, "Deploy Shield Generator" },
        { DeploySuperiorShieldGenerator, "Deploy Superior Shield Generator" },
        { DeployGuildShieldGenerator, "Deploy Guild Shield Generator" },
        // - Trebuchet
        { DeployTrebuchet, "Deploy Trebuchet" },
        { DeploySuperiorTrebuchet, "Deploy Superior Trebuchet" },
        { DeployGuildTrebuchet, "Deploy Guild Trebuchet" },
        #endregion WvW
    };

    private static readonly Dictionary<long, string> _overrideIcons = new()
    {
        { WeaponSwap, SkillImages.WeaponSwap },
        { WeaponStow, SkillImages.WeaponStow },
        { WeaponDraw, SkillImages.WeaponDraw },
        { Resurrect, SkillImages.Resurrect },
        { Bandage, SkillImages.Bandage },
        { LevelUp, ParserIcons.LevelUp },
        { LevelUp2, ParserIcons.LevelUp },
        { ArcDPSGenericBreakbar, ParserIcons.Breakbar },
        { ArcDPSDodge, SkillImages.Dodge },
        { ArcDPSGenericBreakbar20220307, ParserIcons.Breakbar },
        { ArcDPSDodge20220307, SkillImages.Dodge },
        { Poisoned, BuffImages.Poison },
        { WhirlingAssault, SkillImages.WhirlingAssault },
        #region ComboIcons
        // Combos
        { WaterBlastCombo1, ParserIcons.Healing },
        { WaterBlastCombo2, ParserIcons.Healing },
        { WaterLeapCombo, ParserIcons.Healing },
        { WaterWhirlCombo, ParserIcons.Healing },
        { LeechingBolt1, ParserIcons.Healing },
        { LeechingBolt2, ParserIcons.Healing },
        { PoisonLeapCombo, ParserIcons.Combo },
        { PoisonBlastCombo, ParserIcons.Combo },
        { PoisonBlastCombo2, ParserIcons.Combo },
        { PoisonWhirlCombo, ParserIcons.Combo },
        { LightningLeapCombo, ParserIcons.Combo },
        { LightningWhirlCombo, ParserIcons.Combo },
        { DarkWhirlCombo, ParserIcons.Combo },
        { DarkBlastCombo, ParserIcons.Combo },
        { DarkBlastCombo2, ParserIcons.Combo },
        { FireWhirlCombo, ParserIcons.Combo },
        { IceWhirlCombo, ParserIcons.Combo },
        { ChaosWhirlCombo, ParserIcons.Combo },
        { SmokeWhirlCombo, ParserIcons.Combo },
        { LightWhirlCombo, ParserIcons.Combo },
        #endregion ComboIcons
        #region ItemIcons
        { LightningStrike_SigilOfAir, ItemImages.SuperiorSigilOfAir },
        { FlameBlast_SigilOfFire, ItemImages.SuperiorSigilOfFire },
        { RingOfEarth_MinorSigilOfGeomancy, ItemImages.MinorSigilOfGeomancy },
        { RingOfEarth_MajorSigilOfGeomancy, ItemImages.MajorSigilOfGeomancy },
        { RingOfEarth_SuperiorSigilOfGeomancy, ItemImages.SuperiorSigilOfGeomancy },
        { FrostBurst_MinorSigilOfHydromancy, ItemImages.MinorSigilOfHydromancy },
        { FrostBurst_MajorSigilOfHydromancy, ItemImages.MajorSigilOfHydromancy },
        { FrostBurst_SuperiorSigilOfHydromancy, ItemImages.SuperiorSigilOfHydromancy },
        { WaveOfHealing_MinorSigilOfWater, ItemImages.MinorSigilOfWater },
        { WaveOfHealing_MajorSigilOfWater, ItemImages.MajorSigilOfWater },
        { WaveOfHealing_SuperiorSigilOfWater, ItemImages.SuperiorSigilOfWater },
        { WaveOfHealing_MinorSigilOfRenewal, ItemImages.MinorSigilOfRenewal },
        { WaveOfHealing_MajorSigilOfRenewal, ItemImages.MajorSigilOfRenewal },
        { WaveOfHealing_SuperiorSigilOfRenewal, ItemImages.SuperiorSigilOfRenewal },
        { MajorSigilOfRestoration, ItemImages.MajorSigilOfRestoration },
        { SuperiorSigilOfRestoration, ItemImages.SuperiorSigilOfRestoration },
        { SuperiorSigilOfSeverance, ItemImages.SuperiorSigilOfSeverance },
        { MinorSigilOfDoom, ItemImages.MinorSigilOfDoom },
        { MajorSigilOfDoom, ItemImages.MajorSigilOfDoom },
        { SuperiorSigilOfDoom, ItemImages.SuperiorSigilOfDoom },
        { MinorSigilOfBlood, "https://wiki.guildwars2.com/images/d/d4/Minor_Sigil_of_Blood.png" },
        { MajorSigilOfBlood, "https://wiki.guildwars2.com/images/9/9b/Major_Sigil_of_Blood.png" },
        { SuperiorSigilOfBlood, "https://wiki.guildwars2.com/images/a/ab/Superior_Sigil_of_Blood.png" },
        { MajorSigilOfLeeching, "https://wiki.guildwars2.com/images/3/3f/Major_Sigil_of_Leeching.png" },
        { SuperiorSigilOfLeeching, "https://wiki.guildwars2.com/images/0/05/Superior_Sigil_of_Leeching.png" },
        { Snowball_SigilOfMischief, "https://wiki.guildwars2.com/images/b/b7/Superior_Sigil_of_Mischief.png" },
        { SuperiorSigilOfVision, ItemImages.SuperiorSigilOfVision },
        { SuperiorSigilOfConcentration, ItemImages.SuperiorSigilOfConcentration },
        { SuperiorSigilOfDraining, ItemImages.SuperiorSigilOfConcentration },
        { RuneOfTormenting, "https://wiki.guildwars2.com/images/e/ec/Superior_Rune_of_Tormenting.png" },
        { RuneOfNightmare, "https://wiki.guildwars2.com/images/2/2e/Superior_Rune_of_the_Nightmare.png" },
        { SuperiorRuneOfTheDolyak, "https://wiki.guildwars2.com/images/2/28/Superior_Rune_of_the_Dolyak.png" },
        { FrozenBurst_RuneOfIce, "https://wiki.guildwars2.com/images/7/78/Superior_Rune_of_the_Ice.png" },
        { HuntersCall_RuneOfMadKing, "https://wiki.guildwars2.com/images/e/ed/Superior_Rune_of_the_Mad_King.png" },
        { ArtilleryBarrage_RuneofCitadel, "https://wiki.guildwars2.com/images/f/f4/Superior_Rune_of_the_Citadel.png" },
        { HandOfGrenth_RuneOfGrenth, "https://wiki.guildwars2.com/images/6/6e/Superior_Rune_of_Grenth.png" },
        { PortalEntranceWhiteMantleWatchwork, ItemImages.WatchworkPortalDevice },
        { PortalExitWhiteMantleWatchwork, ItemImages.WatchworkPortalDevice },
        { PlateOfSpicyMoaWingsGastricDistress, ItemImages.PlateOfSpicyMoaWings },
        { ThrowGunkEttinGunk, SkillImages.ThrowGunk },
        { SmashBottle, SkillImages.SmashBottle },
        { ThrowBottle, SkillImages.ThrowBottle },
        { ThrowNetUnderwaterNet, SkillImages.NetShot },
#endregion ItemIcons
        #region MountIcons
        { BondOfLifeSkill, SkillImages.BondOfLife },
        { BondOfVigorSkill, SkillImages.BondOfVigor },
        { BondOfFaithSkill, SkillImages.BondOfFaith },
        { StealthMountSkill, SkillImages.StealthMount },
        // Skyscale
        { SkyscaleSkill, SkillImages.Skyscale },
        { SkyscaleFireballSkill, SkillImages.SkyscaleFireball },
        { SkyscaleFireballDamage, SkillImages.SkyscaleFireball },
        { SkyscaleBlastSkill, SkillImages.SkyscaleBlast },
        // Raptor
        { RaptorTailSpin, SkillImages.RaptorTailSpin },
        // Warclaw
        { WarclawBattleMaulSkill, SkillImages.WarclawBattleMaul },
        { WarclawBattleMaulDamage, SkillImages.WarclawBattleMaul },
        { WarclawLance, SkillImages.WarclawLance },
        { WarclawChainPull1, SkillImages.WarclawChainPull },
        { WarclawChainPull2, SkillImages.WarclawChainPull },
        #endregion MountIcons
        #region RelicIcons
        // Relics
        { RelicOfTheAfflicted, "https://wiki.guildwars2.com/images/9/91/Relic_of_the_Afflicted.png" },
        { RelicOfTheCitadel, "https://wiki.guildwars2.com/images/3/36/Relic_of_the_Citadel.png" },
        { RelicOfMercyHealing, "https://wiki.guildwars2.com/images/e/ed/Relic_of_Mercy.png" },
        { RelicOfTheFlock, "https://wiki.guildwars2.com/images/1/1b/Relic_of_the_Flock.png" },
        { RelicOfTheFlockBarrier, "https://wiki.guildwars2.com/images/1/1b/Relic_of_the_Flock.png" },
        { RelicOfTheIce, "https://wiki.guildwars2.com/images/5/55/Relic_of_the_Ice.png" },
        { RelicOfTheKrait, "https://wiki.guildwars2.com/images/7/7e/Relic_of_the_Krait.png" },
        { RelicOfTheNightmare, "https://wiki.guildwars2.com/images/5/51/Relic_of_the_Nightmare.png" },
        { RelicOfTheSunless, "https://wiki.guildwars2.com/images/b/b6/Relic_of_the_Sunless.png" },
        { RelicOfAkeem, "https://wiki.guildwars2.com/images/5/50/Relic_of_Akeem.png" },
        { RelicOfTheWizardsTower, "https://wiki.guildwars2.com/images/e/ea/Relic_of_the_Wizard%27s_Tower.png" },
        { RelicOfTheMirage, "https://wiki.guildwars2.com/images/d/d3/Relic_of_the_Mirage.png" },
        { RelicOfCerusHit, ItemImages.RelicOfCerus },
        { RelicOfDagdaHit, ItemImages.RelicOfDagda },
        { RelicOfFireworks, ItemImages.RelicOfFireworks },
        { RelicOfVass, ItemImages.RelicOfVass },
        { RelicOfTheFirebrand, ItemImages.RelicOfTheFirebrand },
        { RelicOfIsgarrenTargetBuff, ItemImages.RelicOfIsgarren },
        { RelicOfTheDragonhunterTargetBuff, ItemImages.RelicOfTheDragonhunter },
        { RelicOfPeithaTargetBuff, ItemImages.RelicOfPeitha },
        { RelicOfPeithaBlade, ItemImages.RelicOfPeitha },
        { MabonsStrength, ItemImages.RelicOfMabon },
        { NouryssHungerDamageBuff, ItemImages.RelicOfNourys },
        { RelicOfFireworksBuffLoss, "https://i.imgur.com/o9suJSt.png" },
        { RelicOfKarakosaHealing, "https://wiki.guildwars2.com/images/6/6e/Relic_of_Karakosa.png" },
        { RelicOfNayosHealing, "https://wiki.guildwars2.com/images/e/e4/Relic_of_Nayos.png" },
        { RelicOfTheDefenderHealing, ItemImages.RelicOfTheDefender },
        { RelicOfTheFoundingBarrier, "https://wiki.guildwars2.com/images/8/81/Relic_of_the_Founding.png" },
        { RelicOfTheTwinGenerals, "https://wiki.guildwars2.com/images/0/0e/Relic_of_the_Twin_Generals.png" },
        { RelicOfTheClawBuffLoss, "https://i.imgur.com/x6AU9B4.png" },
        { RelicOfTheClaw, ItemImages.RelicOfTheClaw },
        { RelicOfTheBlightbringer, ItemImages.RelicOfTheBlightbringer },
        { RelicOfSorrowBuff, ItemImages.RelicOfTheSorrow },
        { RelicOfSorrowHeal, ItemImages.RelicOfTheSorrow },
        { RelicOfTheStormsingerChain, ItemImages.RelicOfTheStormsinger },
        { RelicOfTheBeehive, "https://wiki.guildwars2.com/images/4/49/Relic_of_the_Beehive.png" },
        { RelicOfMountBalrior, ItemImages.RelicOfMountBalrior },
#endregion RelicIcons
        #region ElementalistIcons
        { DualFireAttunement, SkillImages.FireAttunement },
        { FireWaterAttunement, SkillImages.FireWaterAttunement },
        { FireAirAttunement, SkillImages.FireAirAttunement },
        { FireEarthAttunement, SkillImages.FireEarthAttunement },
        { DualWaterAttunement, SkillImages.WaterAttunement },
        { WaterFireAttunement, SkillImages.WaterFireAttunement },
        { WaterAirAttunement, SkillImages.WaterAirAttunement },
        { WaterEarthAttunement, SkillImages.WaterEarthAttunement },
        { DualAirAttunement, SkillImages.AirAttunement },
        { AirFireAttunement, SkillImages.AirFireAttunement },
        { AirWaterAttunement, SkillImages.AirWaterAttunement },
        { AirEarthAttunement, SkillImages.AirEarthAttunement },
        { DualEarthAttunement, SkillImages.EarthAttunement },
        { EarthFireAttunement, SkillImages.EarthFireAttunement },
        { EarthWaterAttunement, SkillImages.EarthWaterAttunement },
        { EarthAirAttunement, SkillImages.EarthAirAttunement },
        { EarthenBlast, TraitImages.EarthenBlast },
        { ElectricDischarge, TraitImages.ElectricDischarge },
        { LightningJolt, SkillImages.OverloadAir },
        { ShatteringIceDamage, SkillImages.ShatteringIce },
        { ArcaneShieldDamage, SkillImages.ArcaneShield },
        { ConeOfColdHealing, SkillImages.CondOfCold },
        { HealingRipple, TraitImages.HealingRipple },
        { HealingRain, SkillImages.HealingRain },
        { ElementalRefreshmentBarrier, TraitImages.ElementalRefreshment },
        { FirestormGlyphOfStormsOrFieryGreatsword, "https://i.imgur.com/6BTB3XT.png" },
        { LightningStrikeWvW, SkillImages.LightningStrike },
        { ChainLightningWvW, SkillImages.ChainLightning },
        { FlameBurstWvW, SkillImages.FlameBurst },
        { MistForm, SkillImages.MistForm },
        { VaporForm, SkillImages.VaporForm },
        { FieryRushLeap, SkillImages.FieryRush },
        { LesserCleansingFire, SkillImages.CleansingFire },
        { FlashFreezeDelayed, SkillImages.FlashFreeze },
#endregion  ElementalistIcons
        #region EngineerIcons
        { ShredderGyroHit, SkillImages.ShredderGyro },
        { ShredderGyroDamage, SkillImages.ShredderGyro },
        { HealingMistOrSoothingDetonation, "https://i.imgur.com/cS05J70.png" },
        { ThermalReleaseValve, "https://wiki.guildwars2.com/images/0/0c/Thermal_Release_Valve.png" },
        { RefractionCutterBlade, "https://wiki.guildwars2.com/images/1/10/Refraction_Cutter.png" },
        { MechCoreBarrierEngine, "https://wiki.guildwars2.com/images/d/da/Mech_Core-_Barrier_Engine.png" },
        { JumpShotEOD, "https://wiki.guildwars2.com/images/b/b5/Jump_Shot.png" },
        { MedBlasterHeal, "https://wiki.guildwars2.com/images/6/65/Med_Blaster.png" },
        { SoothingDetonation, "https://wiki.guildwars2.com/images/a/ad/Soothing_Detonation.png" },
        { HealingTurretHeal, "https://wiki.guildwars2.com/images/4/42/Healing_Turret.png" },
        { HardStrikeJadeMech, "https://wiki.guildwars2.com/images/6/62/Hard_Strike.png" },
        { HeavySmashJadeMech, "https://wiki.guildwars2.com/images/9/98/Heavy_Smash_%28Mech%29.png" },
        { TwinStrikeJadeMech, "https://wiki.guildwars2.com/images/3/31/Twin_Strike_%28Mech%29.png" },
        { RecallMech_MechSkill, "https://wiki.guildwars2.com/images/5/56/Recall_Mech.png" },
        { RapidRegeneration, "https://wiki.guildwars2.com/images/7/7a/Rapid_Regeneration.png" },
        { BladeBurstOrParticleAccelerator, "https://i.imgur.com/09MY813.png" },
        { MedicalDispersionFieldHeal, "https://wiki.guildwars2.com/images/a/a6/Medical_Dispersion_Field.png" },
        { ImpactSavantBarrier, "https://wiki.guildwars2.com/images/8/82/Impact_Savant.png" },
        { JumpShotWvW, "https://wiki.guildwars2.com/images/b/b5/Jump_Shot.png" },
        { NetAttack, "https://wiki.guildwars2.com/images/3/3d/Net_Shot.png" },
        { FireTurretDamage, "https://wiki.guildwars2.com/images/5/55/Flame_Turret.png" },
        { StaticShield, "https://wiki.guildwars2.com/images/9/90/Static_Shield.png" },
        { NetTurretDamageUW, "https://wiki.guildwars2.com/images/c/ce/Net_Turret.png" },
        { JadeEnergyShot1JadeMech, SkillImages.Anchor },
        { JadeEnergyShot2JadeMech, SkillImages.Anchor },
        { RifleBurstGrenadeDamage, "https://wiki.guildwars2.com/images/e/ed/Grenade_Barrage.png" },
        { GyroExplosion, "https://wiki.guildwars2.com/images/4/4e/Function_Gyro_%28tool_belt_skill%29.png" },
        { DetonateThrowMineOrMineField, "https://wiki.guildwars2.com/images/d/d1/Detonate_Mine_Field.png" },
        { ConduitSurge, "https://wiki.guildwars2.com/images/6/6b/Conduit_Surge.png" },
        { CrashDown2, "https://wiki.guildwars2.com/images/4/4a/Crash_Down.png" },
        { SystemShockerBarrier, "https://wiki.guildwars2.com/images/3/3f/System_Shocker.png" },
#endregion EngineerIcons
            #region GuardianIcons
            { ProtectorsStrikeCounterHit, "https://wiki.guildwars2.com/images/e/e0/Protector%27s_Strike.png" },
            { SwordOfJusticeDamage, "https://wiki.guildwars2.com/images/8/81/Sword_of_Justice.png" },
            { GlacialHeart, "https://wiki.guildwars2.com/images/4/4f/Glacial_Heart.png" },
            { GlacialHeartHeal, "https://wiki.guildwars2.com/images/4/4f/Glacial_Heart.png" },
            { ShatteredAegis, "https://wiki.guildwars2.com/images/d/d0/Shattered_Aegis.png" },
            { SelflessDaring, "https://wiki.guildwars2.com/images/9/9c/Selfless_Daring.png" },
            { HuntersVerdictPull, "https://wiki.guildwars2.com/images/8/86/Judgment_%28skill%29.png" },
            { Chapter1SearingSpell, "https://wiki.guildwars2.com/images/d/d3/Chapter_1-_Searing_Spell.png" },
            { Chapter2IgnitingBurst, "https://wiki.guildwars2.com/images/5/53/Chapter_2-_Igniting_Burst.png" },
            { Chapter3HeatedRebuke,  "https://wiki.guildwars2.com/images/e/e7/Chapter_3-_Heated_Rebuke.png" },
            { Chapter4ScorchedAftermath, "https://wiki.guildwars2.com/images/c/c9/Chapter_4-_Scorched_Aftermath.png" },
            { EpilogueAshesOfTheJust, "https://wiki.guildwars2.com/images/6/6d/Epilogue-_Ashes_of_the_Just.png" },
            { Chapter1DesertBloomHeal, "https://wiki.guildwars2.com/images/f/fd/Chapter_1-_Desert_Bloom.png" },
            { Chapter1DesertBloomSkill, "https://wiki.guildwars2.com/images/f/fd/Chapter_1-_Desert_Bloom.png" },
            { Chapter2RadiantRecovery, "https://wiki.guildwars2.com/images/9/95/Chapter_2-_Radiant_Recovery.png" },
            { Chapter2RadiantRecoveryHealing, "https://wiki.guildwars2.com/images/9/95/Chapter_2-_Radiant_Recovery.png" },
            { Chapter3AzureSun, "https://wiki.guildwars2.com/images/b/bf/Chapter_3-_Azure_Sun.png" },
            { Chapter4ShiningRiver, "https://wiki.guildwars2.com/images/1/16/Chapter_4-_Shining_River.png" },
            { EpilogueEternalOasis, "https://wiki.guildwars2.com/images/5/5f/Epilogue-_Eternal_Oasis.png" },
            { Chapter1UnflinchingCharge, "https://wiki.guildwars2.com/images/3/30/Chapter_1-_Unflinching_Charge.png" },
            { Chapter2DaringChallenge,  "https://wiki.guildwars2.com/images/7/79/Chapter_2-_Daring_Challenge.png" },
            { Chapter3ValiantBulwark,  "https://wiki.guildwars2.com/images/7/73/Chapter_3-_Valiant_Bulwark.png" },
            { Chapter4StalwartStand, "https://wiki.guildwars2.com/images/8/89/Chapter_4-_Stalwart_Stand.png" },
            { EpilogueUnbrokenLines, "https://wiki.guildwars2.com/images/d/d8/Epilogue-_Unbroken_Lines.png" },
            { FlameRushOld, "https://wiki.guildwars2.com/images/a/a8/Flame_Rush.png" },
            { FlameSurgeOld, "https://wiki.guildwars2.com/images/7/7e/Flame_Surge.png" },
            { MantraOfTruthDamage, "https://wiki.guildwars2.com/images/f/ff/Echo_of_Truth.png" },
            { RestoringReprieveOrRejunevatingRespite, "https://i.imgur.com/RUJNIoM.png" },
            { OpeningPassageOrClarifiedConclusion, "https://i.imgur.com/2M93tOd.png" },
            { PotentHasteOrOverwhelmingCelerity, "https://i.imgur.com/vBBKfGz.png" },
            { PortentOfFreedomOrUnhinderedDelivery, "https://i.imgur.com/b6RUVTr.png" },
            { RushingJusticeStrike, SkillImages.RushingJustice },
            { ExecutionersCallingDualStrike, "https://wiki.guildwars2.com/images/d/da/Executioner%27s_Calling.png" },
            { AdvancingStrikeSkill, "https://wiki.guildwars2.com/images/6/6b/Advancing_Strike.png" },
            { SwordOfJusticeSomething, "https://wiki.guildwars2.com/images/8/81/Sword_of_Justice.png" },
            { ShieldOfTheAvengerSomething, "https://wiki.guildwars2.com/images/2/2c/Shield_of_the_Avenger.png" },
            { ShieldOfTheAvengerShatter, "https://wiki.guildwars2.com/images/2/2c/Shield_of_the_Avenger.png" },
            { VirtueOfJusticePassiveBurn, SkillImages.VirtueOfJustice },
            { HuntersWardImpacts, SkillImages.HuntersWard },
            { FireJurisdictionLevel1, SkillImages.FireJurisdiction },
            { FireJurisdictionLevel2, SkillImages.FireJurisdiction },
            { FireJurisdictionLevel3, SkillImages.FireJurisdiction },
            { DaybreakingSlashWave, "https://wiki.guildwars2.com/images/1/1f/Daybreaking_Slash.png" },
            { BindingBladeSelf, SkillImages.BindingBlade },
            { ReceiveTheLightPulse, "https://wiki.guildwars2.com/images/e/eb/%22Receive_the_Light%21%22.png" },
            { SolarStormIlluminatedHealing, "https://wiki.guildwars2.com/images/f/fd/Solar_Storm.png" },
#endregion GuardianIcons
            #region MesmerIcons
            { HealingPrism, "https://wiki.guildwars2.com/images/f/f4/Healing_Prism.png" },
            { SignetOfTheEther, SkillImages.SignetOfTheEther },
            { BlinkOrPhaseRetreat, "https://i.imgur.com/yxMF7D1.png" },
            { MirageCloakDodge, SkillImages.MirageCloak },
            { UnstableBladestormProjectiles, "https://wiki.guildwars2.com/images/9/93/Unstable_Bladestorm.png" },
            { PhantasmalBerserkerProjectileDamage, "https://wiki.guildwars2.com/images/e/e8/Phantasmal_Berserker.png" },
            { PhantasmalBerserkerPhantasmDamage, "https://wiki.guildwars2.com/images/e/e8/Phantasmal_Berserker.png" },
            { WindsOfChaosStaffClone, "https://wiki.guildwars2.com/images/1/1d/Winds_of_Chaos.png" },
            { IllusionaryUnload, "https://wiki.guildwars2.com/images/f/f0/Unload.png" },
            { IllusionaryRiposteHit, SkillImages.IllusionaryRiposte },
            { IllusionarySwordAttack, "https://wiki.guildwars2.com/images/b/ba/Mage_Strike.png" },
            { BlurredFrenzySwordman, "https://wiki.guildwars2.com/images/6/60/Blurred_Frenzy.png" },
            { MindSlashSwordClone, "https://wiki.guildwars2.com/images/a/a0/Mind_Slash.png" },
            { MindGashSwordClone, "https://wiki.guildwars2.com/images/d/de/Mind_Gash.png" },
            { MindStabSwordClone, "https://wiki.guildwars2.com/images/d/de/Mind_Stab.png" },
            { WhirlingDefensesIllusionaryWarden, "https://wiki.guildwars2.com/images/e/e8/Whirling_Defense.png" },
            { CutterBurst, "https://wiki.guildwars2.com/images/4/4c/Flying_Cutter.png" },
            { CutterBurstMindblade, "https://wiki.guildwars2.com/images/4/4c/Flying_Cutter.png" },
            { RestorativeMantras, "https://wiki.guildwars2.com/images/6/61/Restorative_Mantras.png" },
            { SignetOfTheEtherHeal, SkillImages.SignetOfTheEther },
            { IllusionaryInspiration, "https://wiki.guildwars2.com/images/0/01/Illusionary_Inspiration.png" },
            { BackFire, "https://wiki.guildwars2.com/images/7/72/Phantasmal_Mage.png" },
            { MageStrike, "https://wiki.guildwars2.com/images/b/ba/Mage_Strike.png" },
            { IllusionaryLeap, "https://wiki.guildwars2.com/images/1/18/Illusionary_Leap.png" },
            { DisenchantingBolt, "https://wiki.guildwars2.com/images/d/d0/Phantasmal_Disenchanter.png" },
            { IllusionaryCounterHit, "https://wiki.guildwars2.com/images/e/e5/Illusionary_Counter.png" },
            { SpatialSurge, "https://wiki.guildwars2.com/images/8/87/Spatial_Surge.png" },
            { EtherBolt, "https://wiki.guildwars2.com/images/4/4a/Ether_Bolt.png" },
            { PhantasmalWhalersVolley, "https://wiki.guildwars2.com/images/6/6e/Phantasmal_Whaler.png" },
            { IllusionOfLifeBuff, SkillImages.IllusionOfLife },
            { MindBlast, "https://wiki.guildwars2.com/images/6/61/Mind_Blast.png" },
            { PowerBlock, "https://wiki.guildwars2.com/images/a/af/Power_Block.png" },
            { PhantasmalMageAttack, "https://wiki.guildwars2.com/images/7/72/Phantasmal_Mage.png" },
            { PhantasmalDuelistAttack, "https://wiki.guildwars2.com/images/7/7a/Phantasmal_Duelist.png" },
            { FlyingCutterExtra, "https://wiki.guildwars2.com/images/4/4c/Flying_Cutter.png" },
            { EchoOfMemoryExtra, "https://wiki.guildwars2.com/images/9/95/Echo_of_Memory.png" },
            { SplitSurgeSecondaryBeams, "https://wiki.guildwars2.com/images/f/fd/Split_Surge.png" },
            { PersistenceOfMemory, "https://wiki.guildwars2.com/images/c/ce/Persistence_of_Memory.png" },
            { FriendlyFireIllu, "https://wiki.guildwars2.com/images/c/c0/Friendly_Fire.png" },
            { PhantasmalSharpshooterAttack, "https://wiki.guildwars2.com/images/3/3b/Phantasmal_Sharpshooter.png" },
            #endregion  MesmerIcons
            #region NecromancerIcons
            { LifeFromDeath, "https://wiki.guildwars2.com/images/5/5e/Life_from_Death.png" },
            { ChillingNova, "https://wiki.guildwars2.com/images/8/82/Chilling_Nova.png" },
            { ManifestSandShadeShadeHit, "https://wiki.guildwars2.com/images/a/a4/Manifest_Sand_Shade.png" },
            { NefariousFavorSomething, "https://wiki.guildwars2.com/images/8/83/Nefarious_Favor.png" },
            { NefariousFavorShadeHit, "https://wiki.guildwars2.com/images/8/83/Nefarious_Favor.png" },
            { SandCascadeBarrier, "https://wiki.guildwars2.com/images/1/1e/Sand_Cascade.png" },
            { SandCascadeShadeHit, "https://wiki.guildwars2.com/images/1/1e/Sand_Cascade.png" },
            { GarishPillarHit, "https://wiki.guildwars2.com/images/4/40/Garish_Pillar.png" },
            { GarishPillarShadeHit, "https://wiki.guildwars2.com/images/4/40/Garish_Pillar.png" },
            { DesertShroudHit, "https://wiki.guildwars2.com/images/0/08/Desert_Shroud.png" },
            { SandstormShroudHit, "https://wiki.guildwars2.com/images/3/34/Sandstorm_Shroud.png" },
            { SandFlare, "https://wiki.guildwars2.com/images/f/f0/Sand_Flare.png" },
            { DesertEmpowerment, "https://wiki.guildwars2.com/images/c/c3/Desert_Empowerment.png" },
            { CascadingCorruption, "https://wiki.guildwars2.com/images/9/9e/Cascading_Corruption.png" },
            { DeathlyHaste, "https://wiki.guildwars2.com/images/9/9d/Deathly_Haste.png" },
            { DoomApproaches, "https://wiki.guildwars2.com/images/2/28/Doom_Approaches.png" },
            { UnstableExplosion, "https://wiki.guildwars2.com/images/c/c9/Mark_of_Horror.png" },
            { SadisticSearing, TraitImages.SadisticSearing },
            { SadisticSearingActivation, TraitImages.SadisticSearing },
            { SoulEater, TraitImages.SoulEater },
            { LesserSignetOfVampirism, SkillImages.SignetOfVampirism },
            { SignetOfVampirismSkill2, SkillImages.SignetOfVampirism },
            { SignetOfVampirismHeal2, SkillImages.SignetOfVampirism },
            { SignetOfVampirismHeal, SkillImages.SignetOfVampirism },
            { LifeTransferSomething, "https://wiki.guildwars2.com/images/1/14/Life_Transfer.png" },
            { MarkOfBloodOrChillblains, "https://i.imgur.com/oMv0Emd.png" },
            { VampiricStrikes, TraitImages.VampiricPresence },
            { LifeBlast, "https://wiki.guildwars2.com/images/c/c1/Life_Blast.png" },
            { FiendLeechWvW, "https://wiki.guildwars2.com/images/7/7e/Summon_Blood_Fiend.png" },
            { BoneSlash, "https://wiki.guildwars2.com/images/8/8a/Bone_Slash.png" },
            { NecroticBite1, "https://wiki.guildwars2.com/images/c/cb/Necrotic_Bite.png" },
            { NecroticBite2, "https://wiki.guildwars2.com/images/c/cb/Necrotic_Bite.png" },
            { UnholyFeastSomething, "https://wiki.guildwars2.com/images/8/89/Unholy_Feast.png" },
            { VampiricStrikes2, "https://wiki.guildwars2.com/images/3/3a/Vampiric.png" },
            { TaintedShacklesTicks, "https://wiki.guildwars2.com/images/b/bf/Tainted_Shackles.png" },
            { BloodBank, "https://wiki.guildwars2.com/images/c/cb/Blood_Bank.png" },
            { ShamblingSlash, "https://wiki.guildwars2.com/images/a/ad/Shambling_Slash.png" },
            { AuguryOfDeath, "https://wiki.guildwars2.com/images/7/77/Augury_of_Death.png" },
            { SoulSpiralHeal, "https://wiki.guildwars2.com/images/6/66/Soul_Spiral.png" },
#endregion  NecromancerIcons
            #region RangerIcons
            // Ranger
            { WindborneNotes, "https://wiki.guildwars2.com/images/8/84/Windborne_Notes.png" },
            { InvigoratingBond, "https://wiki.guildwars2.com/images/0/0d/Invigorating_Bond.png" },
            { OpeningStrike, "https://wiki.guildwars2.com/images/9/9e/Opening_Strike.png" },
            { RuggedGrowth, "https://wiki.guildwars2.com/images/7/73/Rugged_Growth.png" },
            { AquaSurge_Player, "https://wiki.guildwars2.com/images/0/07/Aqua_Surge.png" },
            { AquaSurge_WaterSpiritNPC, "https://wiki.guildwars2.com/images/0/07/Aqua_Surge.png" },
            { SolarFlare_Player, "https://wiki.guildwars2.com/images/5/5c/Solar_Flare.png" },
            { SolarFlare_SunSpiritNPC, "https://wiki.guildwars2.com/images/5/5c/Solar_Flare.png" },
            { Quicksand_Player, "https://wiki.guildwars2.com/images/3/3b/Quicksand.png" },
            { Quicksand_StoneSpiritNPC, "https://wiki.guildwars2.com/images/3/3b/Quicksand.png" },
            { ColdSnap_Player, "https://wiki.guildwars2.com/images/f/f2/Cold_Snap.png" },
            { ColdSnap_FrostSpiritNPC, "https://wiki.guildwars2.com/images/f/f2/Cold_Snap.png" },
            { CallLightning_Player, "https://wiki.guildwars2.com/images/5/59/Call_Lightning_%28Ranger%29.png" },
            { CallLightning_StormSpiritNPC, "https://wiki.guildwars2.com/images/5/59/Call_Lightning_%28Ranger%29.png" },
            { NaturesRenewal_Player, "https://wiki.guildwars2.com/images/c/c1/Nature%27s_Renewal.png" },
            { NaturesRenewal_SpiritOfNatureRenewalNPC, "https://wiki.guildwars2.com/images/c/c1/Nature%27s_Renewal.png" },
            { NaturesRenewalHealing, "https://wiki.guildwars2.com/images/c/c1/Nature%27s_Renewal.png" },
            { SignetOfRenewalBuff, "https://wiki.guildwars2.com/images/1/11/Signet_of_Renewal.png" },
            { EntangleDamage, "https://wiki.guildwars2.com/images/6/67/Entangle.png" },
            { SpiritOfNature, "https://wiki.guildwars2.com/images/a/a3/Spirit_of_Nature.png" },
            { ConsumingBite, "https://wiki.guildwars2.com/images/6/68/Consuming_Bite.png" },
            { NarcoticSpores,  "https://wiki.guildwars2.com/images/8/84/Narcotic_Spores.png" },
            { CripplingAnguish, "https://wiki.guildwars2.com/images/c/c8/Crippling_Anguish.png" },
            { KickGazelle, "https://wiki.guildwars2.com/images/b/bc/Kick_%28gazelle%29.png" },
            { ChargeGazelle, "https://wiki.guildwars2.com/images/a/af/Charge_%28gazelle%29.png" },
            { HeadbuttGazelle, "https://wiki.guildwars2.com/images/8/82/Headbutt_%28gazelle%29.png" },
            { SolarBeam, "https://wiki.guildwars2.com/images/a/a9/Solar_Beam.png" },
            { AstralWispAttachment, "https://wiki.guildwars2.com/images/f/ff/Astral_Wisp.png" },
            { CultivatedSynergyPlayer, "https://wiki.guildwars2.com/images/a/a2/Cultivated_Synergy.png" },
            { CultivatedSynergyPet, "https://wiki.guildwars2.com/images/a/a2/Cultivated_Synergy.png" },
            { BloodMoonDaze, "https://wiki.guildwars2.com/images/b/b1/Blood_Moon.png" },
            { LiveVicariously, "https://wiki.guildwars2.com/images/6/64/Live_Vicariously.png" },
            { NaturalHealing, "https://wiki.guildwars2.com/images/c/c1/Natural_Healing_%28ranger_trait%29.png" },
            { GlyphOfUnityCA, "https://wiki.guildwars2.com/images/4/4c/Glyph_of_Unity_%28Celestial_Avatar%29.png" },
            { ChargeGazelleMergeImpact, "https://wiki.guildwars2.com/images/a/af/Charge_%28gazelle%29.png" },
            { SmokeAssaultMergeHit, "https://wiki.guildwars2.com/images/9/92/Smoke_Assault.png" },
            { OneWolfPackDamage, "https://wiki.guildwars2.com/images/3/3b/One_Wolf_Pack.png" },
            { PredatorsCunning, "https://wiki.guildwars2.com/images/c/cc/Predator%27s_Cunning.png" },
            { OverbearingSmashLeap, "https://wiki.guildwars2.com/images/9/9a/Overbearing_Smash.png" },
            { UnleashedOverbearingSmashLeap, "https://wiki.guildwars2.com/images/c/cd/Unleashed_Overbearing_Smash.png" },
            { RangerPetSpawned, "https://wiki.guildwars2.com/images/thumb/1/11/Activate_Pet.png/25px-Activate_Pet.png" },
            { QuickDraw, TraitImages.QuickDraw },
            { WingSwipeWyvern, "https://wiki.guildwars2.com/images/3/38/Wing_Swipe.png" },
            { WingBuffetWyvern, "https://wiki.guildwars2.com/images/5/5f/Wing_Buffet.png" },
            { TailLashWyvern, "https://wiki.guildwars2.com/images/d/d9/Tail_Lash_%28wyvern_skill%29.png" },
            { TailLashDevourer, "https://wiki.guildwars2.com/images/f/f5/Tail_Lash.png" },
            { TwinDartsDevourer, "https://wiki.guildwars2.com/images/f/fe/Twin_Darts.png" },
            { RetreatDevourer, "https://wiki.guildwars2.com/images/2/21/Burrow.png" },
            { BlackHoleMinion, "https://wiki.guildwars2.com/images/9/9d/Black_Hole.png" },
            { JacarandasEmbraceMinion, "https://wiki.guildwars2.com/images/d/d2/Jacaranda%27s_Embrace.png" },
            { CallLightningJacaranda, "https://wiki.guildwars2.com/images/f/f0/Call_Lightning_%28soulbeast%29.png" },
            { RootSlap, "https://wiki.guildwars2.com/images/7/7d/Root_Slap.png" },
            { Peck, "https://wiki.guildwars2.com/images/8/83/Peck.png" },
            { FrenziedAttack, "https://wiki.guildwars2.com/images/8/81/Frenzied_Attack.png" },
            { SignetOfRenewalHeal, SkillImages.SignetOfRenewal },
            { HeavyShotTurtle, "https://wiki.guildwars2.com/images/a/a4/Turtle_Siege.png" },
            { JadeCannonTurtle, "https://wiki.guildwars2.com/images/2/27/Jade_Cannon_%28turtle%29.png" },
            { SlamTurtle, "https://wiki.guildwars2.com/images/5/50/Slam_%28turtle%29.png" },
            { Swoop, "https://wiki.guildwars2.com/images/7/7b/Swoop.png" },
            { TailSwipePet, "https://wiki.guildwars2.com/images/e/ed/Tail_Swipe.png" },
            { BiteCanine, "https://wiki.guildwars2.com/images/d/d1/Snap_%28wolf%29.png" },
            { BrutalChargeCanine, "https://wiki.guildwars2.com/images/2/2c/Brutal_Charge_%28canine%29.png" },
            { BiteDrake, "https://wiki.guildwars2.com/images/e/e7/Bite_%28drake%29.png" },
            { BiteBear, "https://wiki.guildwars2.com/images/a/a6/Bite_%28bear%29.png" },
            { BiteSmokescale, "https://wiki.guildwars2.com/images/c/c1/Bite_%28smokescale%29.png" },
            { SmokeAssaultSmokescaleSkill, "https://wiki.guildwars2.com/images/9/92/Smoke_Assault.png" },
            { SmokeAssaultSmokescaleDamage, "https://wiki.guildwars2.com/images/9/92/Smoke_Assault.png" },
            { MaulPorcine, "https://wiki.guildwars2.com/images/1/1f/Maul_%28porcine%29.png" },
            { JabPorcine, "https://wiki.guildwars2.com/images/d/d5/Jab_%28porcine%29.png" },
            { BrutalChargePorcine, "https://wiki.guildwars2.com/images/a/a5/Brutal_Charge_%28porcine%29.png" },
            { SlashBear, "https://wiki.guildwars2.com/images/5/52/Slash_%28bear%29.png" },
            { SlashBird, "https://wiki.guildwars2.com/images/b/b6/Slash_%28bird%29.png" },
            { SwoopBird, "https://wiki.guildwars2.com/images/e/e3/Swoop_%28bird%29.png" },
            { BiteFeline, "https://wiki.guildwars2.com/images/c/c2/Bite_%28feline%29.png" },
            { SlashFeline, "https://wiki.guildwars2.com/images/c/c3/Maul_%28feline%29.png" },
            { MaulFeline, "https://wiki.guildwars2.com/images/c/c3/Maul_%28feline%29.png" },
            { CripplingLeap, "https://wiki.guildwars2.com/images/e/e2/Crippling_Leap.png" },
            { HornetStingWvW, "https://wiki.guildwars2.com/images/5/5f/Hornet_Sting.png" },
            { LongRangeShotWvW, "https://wiki.guildwars2.com/images/b/bf/Long_Range_Shot.png" },
            { PointBlankShotWvW, "https://wiki.guildwars2.com/images/5/55/Point-Blank_Shot.png" },
            { RapidFireWvW, "https://wiki.guildwars2.com/images/4/4b/Rapid_Fire.png" },
            { ColdSnapFrostSpirit, "https://wiki.guildwars2.com/images/f/f2/Cold_Snap.png" },
            { QuakeStoneSpirit, "https://wiki.guildwars2.com/images/3/3b/Quicksand.png" },
            { ExplodingSpore, "https://wiki.guildwars2.com/images/2/21/Exploding_Spores.png" },
            { VenomousOutburstPet, "https://wiki.guildwars2.com/images/2/26/Venomous_Outburst.png" },
            { EnvelopingHazePet, "https://wiki.guildwars2.com/images/0/0e/Enveloping_Haze.png" },
            { RendingVinesPet, "https://wiki.guildwars2.com/images/8/89/Rending_Vines.png" },
            { GlyphOfUnitySomething, "https://wiki.guildwars2.com/images/b/b1/Glyph_of_Unity.png" },
            { CripplingTalonWvW, "https://wiki.guildwars2.com/images/a/a1/Crippling_Talon.png" },
            { HiltBashWvW, "https://wiki.guildwars2.com/images/c/c3/Hilt_Bash.png" },
            { HarmonicCry, "https://wiki.guildwars2.com/images/1/1d/Harmonic_Cry.png" },
            { QuickeningScreech, "https://wiki.guildwars2.com/images/f/f7/Quickening_Screech.png" },
            { TakedownSmokescale, "https://wiki.guildwars2.com/images/e/e6/Takedown.png" },
            { PhasePounceWhiteTiger, "https://wiki.guildwars2.com/images/2/20/Phase_Pounce.png" },
            { PhotosynthesizeJacaranda, "https://wiki.guildwars2.com/images/2/2f/Photosynthesize.png" },
            { EvilEye, "https://wiki.guildwars2.com/images/2/27/Evil_Eye.png" },
            { TormentingVisionSpinegazer, "https://wiki.guildwars2.com/images/0/0b/Tormenting_Visions.png" },
            { WolfsOnslaughtFollowUp, "https://wiki.guildwars2.com/images/6/61/Wolf%27s_Onslaught.png" },
            { EletroctuteJuvenileSkyChak, "https://wiki.guildwars2.com/images/9/9f/Electrocute_%28chak%29.png" },
            #endregion RangerIcons
            #region RevenantIcons
            { RiftSlashRiftHit, "https://wiki.guildwars2.com/images/a/a8/Rift_Slash.png" },
            { UnrelentingAssaultMultihit, "https://wiki.guildwars2.com/images/e/e9/Unrelenting_Assault.png" },
            { EnchantedDaggers2, SkillImages.EnchantedDaggers },
            { ImpossibleOddsHit, SkillImages.ImpossibleOdds },
            { EmbraceTheDarknessDamage, SkillImages.EmbraceTheDarkness },
            { NaturalHarmony, "https://wiki.guildwars2.com/images/d/d9/Natural_Harmony.png" },
            { NaturalHarmonyHeal, "https://wiki.guildwars2.com/images/d/d9/Natural_Harmony.png" },
            { ProjectTranquility, "https://wiki.guildwars2.com/images/e/e7/Project_Tranquility.png" },
            { ProjectTranquilityHeal, "https://wiki.guildwars2.com/images/e/e7/Project_Tranquility.png" },
            { VentarisWill, "https://wiki.guildwars2.com/images/b/b6/Ventari%27s_Will.png" },
            { DarkrazorsDaringHit, "https://wiki.guildwars2.com/images/7/77/Darkrazor%27s_Daring.png" },
            { DarkrazorsDaringSkillMinion, "https://wiki.guildwars2.com/images/7/77/Darkrazor%27s_Daring.png" },
            { DarkrazorsDaringSkillMinionReworked, "https://wiki.guildwars2.com/images/7/77/Darkrazor%27s_Daring.png" },
            { IcerazorsIreHit, "https://wiki.guildwars2.com/images/2/2d/Icerazor%27s_Ire.png" },
            { IcerazorsIreSkillMinion, "https://wiki.guildwars2.com/images/2/2d/Icerazor%27s_Ire.png" },
            { IcerazorsIreSkillMinionReworked, "https://wiki.guildwars2.com/images/2/2d/Icerazor%27s_Ire.png" },
            { BreakrazorsBastionMinion, SkillImages.BreakrazorsBastion },
            { BreakrazorsBastionSkillMinionReworked, SkillImages.BreakrazorsBastion },
            { RazorclawsRageSkillMinion, SkillImages.RazorclawsRage },
            { RazorclawsRageHitReworked, SkillImages.RazorclawsRage },
            { RazorclawsRageSkillMinionReworked, SkillImages.RazorclawsRage },
            { SoulcleavesSummitSkillMinion, SkillImages.SoulcleavesSummit },
            { SoulcleavesSummitHitReworked, SkillImages.SoulcleavesSummit },
            { KallaSummonsSaluteAnimationSkill, "https://wiki.guildwars2.com/images/1/1a/Royal_Decree.png" },
            { KallaSummonsDespawnSkill, BuffImages.Downed },
            { PhantomsOnslaughtDamage, "https://wiki.guildwars2.com/images/b/b2/Phantom%27s_Onslaught.png" },
            { DeathDropDodge, SkillImages.Dodge },
            { SaintsShieldDodge, SkillImages.Dodge },
            { ImperialImpactDodge, SkillImages.Dodge },
            { LesserBanishEnchantment, "https://render.guildwars2.com/file/D327055AA824ABDDAD70E2606E1C9AF018FF9902/961449.png" },
            { BalanceInDiscord, "https://wiki.guildwars2.com/images/a/a2/Balance_in_Discord.png" },
            { HealersGift, "https://wiki.guildwars2.com/images/8/85/Healer%27s_Gift.png" },
            { EnergyExpulsionHeal, "https://wiki.guildwars2.com/images/0/0d/Energy_Expulsion.png" },
            { PurifyingEssenceHeal, "https://wiki.guildwars2.com/images/e/e1/Purifying_Essence.png" },
            { HealingOrbRevenant, "https://wiki.guildwars2.com/images/6/6b/Rejuvenating_Assault.png" },
            { WordsOfCensure, "https://wiki.guildwars2.com/images/5/58/Words_of_Censure.png" },
            { GenerousAbundanceCentaur, "https://wiki.guildwars2.com/images/1/1a/Generous_Abundance.png" },
            { GenerousAbundanceOther, "https://wiki.guildwars2.com/images/1/1a/Generous_Abundance.png" },
            { GlaringResolve, "https://wiki.guildwars2.com/images/5/55/Fierce_Infusion.png" },
            { ElevatedCompassion, "https://wiki.guildwars2.com/images/f/f4/Elevated_Compassion.png" },
            { FrigidBlitzExtra, "https://wiki.guildwars2.com/images/2/2b/Frigid_Blitz.png" },
            { EchoingEruptionExtra, "https://wiki.guildwars2.com/images/b/ba/Echoing_Eruption.png" },
            { PhaseTraversal2, "https://wiki.guildwars2.com/images/f/f2/Phase_Traversal.png" },
            { CoalescenceOfRuinExtra, "https://wiki.guildwars2.com/images/1/10/Coalescence_of_Ruin.png" },
            { AbyssalRaze2, "https://wiki.guildwars2.com/images/5/52/Abyssal_Raze.png" },
            { AbyssalStrike_SecondHit, "https://wiki.guildwars2.com/images/9/98/Abyssal_Fire.png" },
            { BlitzMinesDrop, "https://wiki.guildwars2.com/images/8/86/Abyssal_Blitz.png" },
            #endregion RevenantIcons
            #region ThiefIcons
            { ThrowMagneticBomb, "https://wiki.guildwars2.com/images/e/e7/Throw_Magnetic_Bomb.png" },
            { DetonatePlasma, "https://wiki.guildwars2.com/images/3/3d/Detonate_Plasma.png" },
            { UnstableArtifact, "https://wiki.guildwars2.com/images/d/dd/Unstable_Artifact.png" },
            { MindShock, "https://wiki.guildwars2.com/images/e/e6/Mind_Shock.png" },
            { SoulStoneVenomSkill, "https://wiki.guildwars2.com/images/d/d6/Soul_Stone_Venom.png" },
            { SoulStoneVenomStrike, "https://wiki.guildwars2.com/images/d/d6/Soul_Stone_Venom.png" },
            { Pitfall, "https://wiki.guildwars2.com/images/6/67/Pitfall.png" },
            { BasiliskVenomStunBreakbarDamage, "https://wiki.guildwars2.com/images/3/3a/Basilisk_Venom.png" },
            { Bound, TraitImages.BoundingDodger },
            { ImpalingLotus, TraitImages.LotusTraining },
            { Dash, TraitImages.UnhinderedCombatant },
            { TwilightComboSecondProjectile, "https://wiki.guildwars2.com/images/d/dc/Twilight_Combo.png" },
            { HauntShot, "https://wiki.guildwars2.com/images/b/be/Haunt_Shot.png" },
            { GraspingShadows, "https://wiki.guildwars2.com/images/e/ef/Grasping_Shadows.png" },
            { DawnsRepose, "https://wiki.guildwars2.com/images/3/31/Dawn%27s_Repose.png" },
            { EternalNight, "https://wiki.guildwars2.com/images/7/7b/Eternal_Night.png" },
            { ShadowFlareDeadeyeMinion, "https://wiki.guildwars2.com/images/3/3d/Shadow_Flare.png" },
            { DoubleTapDeadeyeMinion, "https://wiki.guildwars2.com/images/3/3d/Shadow_Flare.png" },
            { BrutalAimDeadeyeMinion, "https://wiki.guildwars2.com/images/c/c0/Brutal_Aim.png" },
            { DeathsJudgmentDeadeyeMinion, "https://wiki.guildwars2.com/images/5/57/Death%27s_Judgment.png" },
            { UnloadThiefMinion, "https://wiki.guildwars2.com/images/f/f0/Unload.png" },
            { BlackPowderThiefMinion, "https://wiki.guildwars2.com/images/3/3e/Black_Powder.png" },
            { TwistingFangThiefMinion1, "https://wiki.guildwars2.com/images/d/d4/Twisting_Fangs.png" },
            { TwistingFangThiefMinion2, "https://wiki.guildwars2.com/images/d/d4/Twisting_Fangs.png" },
            { TwistingFangThiefMinion3, "https://wiki.guildwars2.com/images/d/d4/Twisting_Fangs.png" },
            { ScorpionWireThiefMinion, "https://wiki.guildwars2.com/images/c/c8/Scorpion_Wire.png" },
            { TripleThreatSpecterMinion, "https://wiki.guildwars2.com/images/5/59/Triple_Threat.png" },
            { ShadowBoltSpecterMinion, "https://wiki.guildwars2.com/images/f/f5/Shadow_Bolt.png" },
            { DoubleBoltSpecterMinion, "https://wiki.guildwars2.com/images/6/6c/Double_Bolt.png" },
            { TripleBoltSpecterMinion, "https://wiki.guildwars2.com/images/b/b2/Triple_Bolt.png" },
            { ImpairingDaggersDaredevilMinionHit1, SkillImages.ImpairingDaggers },
            { ImpairingDaggersDaredevilMinionHit2, SkillImages.ImpairingDaggers },
            { ImpairingDaggersDaredevilMinionHit3, SkillImages.ImpairingDaggers },
            { ImpairingDaggersDaredevilMinionSkill, SkillImages.ImpairingDaggers },
            { ImpairingDaggersHit1, SkillImages.ImpairingDaggers },
            { ImpairingDaggersHit2, SkillImages.ImpairingDaggers },
            { ImpairingDaggersHit3, SkillImages.ImpairingDaggers },
            { VaultDaredevilMinion, "https://wiki.guildwars2.com/images/c/c9/Vault.png" },
            { WeakeningChargeDaredevilMinion, "https://wiki.guildwars2.com/images/f/f7/Weakening_Charge.png" },
            { BoundHit, TraitImages.BoundingDodger },
            { ThievesGuildMinionDespawnSkill, BuffImages.Downed },
            { FlankingStrikeWvW, "https://wiki.guildwars2.com/images/1/1f/Flanking_Strike.png" },
            { BlindingPowderWvW, "https://wiki.guildwars2.com/images/5/56/Blinding_Powder.png" },
            { BlackPowderWvW, "https://wiki.guildwars2.com/images/3/3e/Black_Powder.png" },
            { ShadowAssault, "https://wiki.guildwars2.com/images/6/66/Shadow_Assault.png" },
            { ShadowShot, "https://wiki.guildwars2.com/images/4/46/Shadow_Strike.png" },
            { ShadowStrike, "https://wiki.guildwars2.com/images/6/66/Shadow_Assault.png" },
            { InfiltratorsStrikeSomething, "https://wiki.guildwars2.com/images/2/2c/Infiltrator%27s_Strike.png" },
            { BarbedSpearRanged, "https://wiki.guildwars2.com/images/a/a4/Barbed_Spear.png" },
            { TraversingDuskHeal, "https://wiki.guildwars2.com/images/1/10/Traversing_Dusk.png" },
            { DarkSaviorHealing, "https://wiki.guildwars2.com/images/c/cd/Shadow_Savior.png" },
            { ShieldingRestorationBarrier, "https://wiki.guildwars2.com/images/e/ea/Shielding_Restoration.png" },
            #endregion ThiefIcons
            #region WarriorIcons
            { MendingMight, "https://wiki.guildwars2.com/images/e/e7/Mending_Might.png" },
            { LossAversion, "https://wiki.guildwars2.com/images/8/85/Loss_Aversion.png" },
            { KingOfFires, "https://wiki.guildwars2.com/images/7/70/King_of_Fires.png" },
            { DragonSlashBoost, "https://wiki.guildwars2.com/images/7/75/Dragon_Slash%E2%80%94Boost.png" },
            { DragonSlashForce, "https://wiki.guildwars2.com/images/b/b5/Dragon_Slash%E2%80%94Force.png" },
            { DragonSlashReach, "https://wiki.guildwars2.com/images/e/eb/Dragon_Slash%E2%80%94Reach.png" },
            { Triggerguard, "https://wiki.guildwars2.com/images/4/4e/Triggerguard.png" },
            { FlickerStep, "https://wiki.guildwars2.com/images/d/de/Flicker_Step.png" },
            { ExplosiveThrust, "https://wiki.guildwars2.com/images/9/99/Explosive_Thrust.png" },
            { SteelDivide, "https://wiki.guildwars2.com/images/9/9a/Steel_Divide.png" },
            { SwiftCut, "https://wiki.guildwars2.com/images/e/e3/Swift_Cut.png" },
            { BloomingFire, "https://wiki.guildwars2.com/images/d/d0/Blooming_Fire.png" },
            { ArtillerySlash, "https://wiki.guildwars2.com/images/6/68/Artillery_Slash.png" },
            { CycloneTrigger, "https://wiki.guildwars2.com/images/6/6c/Cyclone_Trigger.png" },
            { BreakStep, "https://wiki.guildwars2.com/images/7/76/Break_Step.png" },
            { RushDamage, "https://wiki.guildwars2.com/images/4/42/Rush.png" },
            { DragonspikeMineDamage, "https://wiki.guildwars2.com/images/3/3e/Dragonspike_Mine.png" },
            { FullCounterHit, SkillImages.FullCounter },
            { AimedShotWvW, "https://wiki.guildwars2.com/images/8/86/Aimed_Shot.png" },
            { ChargeWvW, "https://wiki.guildwars2.com/images/b/b0/Charge_%28warrior_skill%29.png" },
            { MaceSmashWvW1, "https://wiki.guildwars2.com/images/3/37/Mace_Smash.png" },
            { MaceSmashWvW2, "https://wiki.guildwars2.com/images/3/37/Mace_Smash.png" },
            { CrushingBlowWvW1, "https://wiki.guildwars2.com/images/1/10/Crushing_Blow.png" },
            { CrushingBlowWvW2, "https://wiki.guildwars2.com/images/1/10/Crushing_Blow.png" },
            { PulverizeWvW1, "https://wiki.guildwars2.com/images/1/18/Pulverize.png" },
            { PulverizeWvW2, "https://wiki.guildwars2.com/images/1/18/Pulverize.png" },
            { BannerOfDefenseBarrier, "https://wiki.guildwars2.com/images/f/f1/Banner_of_Defense.png" },
            { ShieldBashWvW, "https://wiki.guildwars2.com/images/8/86/Shield_Bash.png" },
            { BodyShotWvW, "https://wiki.guildwars2.com/images/7/7e/Bola_Shot.png" },
            { ThrowBolasWvW, "https://wiki.guildwars2.com/images/6/6a/Throw_Bolas.png" },
            { CallToArmsWvW, "https://wiki.guildwars2.com/images/9/9a/Call_of_Valor.png" },
            { RifleButtWvW, "https://wiki.guildwars2.com/images/2/2e/Rifle_Butt.png" },
            { VolleyWvW, "https://wiki.guildwars2.com/images/5/5d/Volley.png" },
            { BleedingShotWvW, "https://wiki.guildwars2.com/images/9/97/Fierce_Shot.png" },
            { BolaTossWvW, "https://wiki.guildwars2.com/images/7/7e/Bola_Shot.png" },
            { MagebaneTetherBuff, TraitImages.MagebaneTether },
            { MagebaneTetherSkill, TraitImages.MagebaneTether },
            { EnchantmentCollapse, "https://wiki.guildwars2.com/images/7/7f/Enchantment_Collapse.png" },
            { LineBreakerHeal, "https://wiki.guildwars2.com/images/0/0e/Line_Breaker.png" },
            { VigorousShouts, "https://wiki.guildwars2.com/images/d/de/Vigorous_Shouts.png" },
            { MightyThrowScatter, "https://wiki.guildwars2.com/images/9/97/Mighty_Throw.png" },
            { WildThrowExtra, "https://wiki.guildwars2.com/images/a/a6/Wild_Throw.png" },
            { SpearmarshalsSupportBombard, "https://wiki.guildwars2.com/images/f/ff/Spearmarshal%27s_Support.png" },
            { ShrugItOffHeal, "https://wiki.guildwars2.com/images/c/c8/Shrug_It_Off.png" },
            { BerserkEndSkill, "https://i.imgur.com/dnoZYre.png" },
            #endregion WarriorIcons
            #region EncounterIcons
            // Silent Surf Fractal
            { GrapplingHook, "https://wiki.guildwars2.com/images/c/c8/Scorpion_Wire.png" },
            { Parachute, "https://wiki.guildwars2.com/images/f/fd/Feathers_%28skill%29.png" },
            { BlackPowderCharge, "https://wiki.guildwars2.com/images/7/75/Powder_Keg_%28skill%29.png" },
            { FlareSilentSurf, "https://wiki.guildwars2.com/images/2/21/Reclaimed_Energy.png" },
            // Special Action Keys
            // - Training Area
            { MushroomKingsBlessing, "https://wiki.guildwars2.com/images/8/86/Cap_Hop.png" },
            // - Icebrood Saga
            { SpiritNovaTier1, BuffImages.SpiritNova },
            { SpiritNovaTier2, BuffImages.SpiritNova },
            { SpiritNovaTier3, BuffImages.SpiritNova },
            { SpiritNovaTier4, BuffImages.SpiritNova },
            { NightTerrorTier1, BuffImages.NightTerror },
            { NightTerrorTier2, BuffImages.NightTerror },
            { NightTerrorTier3, BuffImages.NightTerror },
            { NightTerrorTier4, BuffImages.NightTerror },
            { ShatteredPsycheTier1, BuffImages.ShatteredPsyche },
            { ShatteredPsycheTier2, BuffImages.ShatteredPsyche },
            { ShatteredPsycheTier3, BuffImages.ShatteredPsyche },
            { ShatteredPsycheTier4, BuffImages.ShatteredPsyche },
            // - Sabetha
            { SapperBombSkill, BuffImages.SapperBomb },
            // - Slothasor
            { PurgeSlothasor, "https://wiki.guildwars2.com/images/a/aa/Purge.png" },
            { Eat, "https://wiki.guildwars2.com/images/7/7b/Eat.png" },
            // - Bandit Trio
            { Beehive, BuffImages.ThrowJar },
            { ThrowOilKeg, "https://wiki.guildwars2.com/images/5/5f/Throw_Keg.png" },
            // - Matthias
            { UnstableBloodMagic, "https://wiki.guildwars2.com/images/a/aa/Purge.png" },
            // - Escort Glenna
            { OverHere, "https://wiki.guildwars2.com/images/b/b7/Over_Here%21.png" },
            { HaresSpeedSkill, "https://wiki.guildwars2.com/images/0/05/Hare%27s_Speed.png" },
            // - Xera
            { InterventionSAK, "https://wiki.guildwars2.com/images/3/3f/Intervention.png" },
            // - Cairn
            { CelestialDashSAK, SkillImages.CelestialDash },
            // - Mursaar Overseer
            { ClaimSAK, BuffImages.Claim },
            { DispelSAK, BuffImages.Dispel },
            { ProtectSAK, BuffImages.Protect },
            // - Soulless Horror
            { IssueChallengeSAK, "https://wiki.guildwars2.com/images/1/13/Rally_the_Crowd.png" },
            // Eater of Souls
            { ReclaimedEnergySkill, BuffImages.ReclaimedEnergy },
            // Eyes of Judgment
            { ThrowLight, "https://wiki.guildwars2.com/images/8/8c/Throw_Light.png" },
            { Flare, "https://wiki.guildwars2.com/images/5/54/Flare.png" },
            // - Dhuum
            { ExpelEnergySAK, "https://wiki.guildwars2.com/images/c/c1/Core_Capture.png" },
            // - Conjured Amalgamate
            { ConjuredSlashPlayer, "https://wiki.guildwars2.com/images/5/59/Conjured_Slash.png" },
            { ConjuredProtection, "https://wiki.guildwars2.com/images/0/02/Conjured_Protection.png" },
            { ConjuredSlashSAK, "https://wiki.guildwars2.com/images/5/59/Conjured_Slash.png" },
            { ConjuredProtectionSAK, "https://wiki.guildwars2.com/images/0/02/Conjured_Protection.png" },
            // - Sabir
            { FlashDischargeSAK, "https://wiki.guildwars2.com/images/5/59/Flash_Discharge.png" },
            // - Qadim the Peerless
            { FluxDisruptorActivateCast, "https://wiki.guildwars2.com/images/d/d5/Flux_Disruptor-_Activate.png" },
            { FluxDisruptorDeactivateCast, "https://wiki.guildwars2.com/images/3/34/Flux_Disruptor-_Deactivate.png" },
            { PlayerLiftUpQadimThePeerless, ParserIcons.GenericBlueArrowUp },
            { UnleashSAK, "https://wiki.guildwars2.com/images/9/99/Touch_of_the_Sun.png" },
            // - Ura
            { UraDispelSAK, "https://wiki.guildwars2.com/images/0/07/Consume_Bloodstone_Fragment.png" },
            // - Mai Trin (Aetherble Hideout)
            { ReverseThePolaritySAK, "https://wiki.guildwars2.com/images/f/f8/Prod.png" },
            // - Dadga (Cosmic Observatory)
            { PurifyingLight, "https://wiki.guildwars2.com/images/1/1e/Purifying_Light_%28Dagda%29.png" },
            // - Artsariiv
            { NovaLaunchSAK, SkillImages.CelestialDash },
            // - Arkk
            { HypernovaLaunchSAK, SkillImages.CelestialDash },
            // Freezie
            { FireSnowball, "https://wiki.guildwars2.com/images/d/d0/Fire_Snowball.png" },
            // Generic Encounter Skills
            // - Ura
            { ThrummingPresenceDamage, BuffImages.ConjuredBarrier },
            { ThrummingPresenceBuff, BuffImages.ConjuredBarrier },
            #endregion  EncounterIcons
            #region WvWIcons
            { WvWSpendingSupplies, "https://wiki.guildwars2.com/images/b/b7/Repair_Master.png" },
            { WvWPickingUpSupplies, "https://wiki.guildwars2.com/images/9/94/Supply.png" },
            { DeployTrapWvW, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { ImprovedPouredTar, "https://wiki.guildwars2.com/images/1/1a/Pour_Tar.png" },
            { EngulfingBurningOil, "https://wiki.guildwars2.com/images/6/6d/Pour_Oil.png" },
            { FirePackedIncendiaryShells, "https://wiki.guildwars2.com/images/e/e0/Fire_Incendiary_Shells.png" },
            { HollowedBoulderShot, "https://wiki.guildwars2.com/images/4/4d/Fire_Boulder.png" },
            { FireHealingOasis, "https://wiki.guildwars2.com/images/4/49/Healing_Oasis.png" },
            { SmokeScreen, "https://wiki.guildwars2.com/images/0/0a/Smoke_Screen_%28engineer_skill%29.png" },
            { SiegeBubble, "https://wiki.guildwars2.com/images/4/4d/Siege_Bubble.png" },
            { EjectionSeat, "https://wiki.guildwars2.com/images/b/ba/Eject.png" },
            { PunchGolem, "https://wiki.guildwars2.com/images/8/8f/Punch.png" },
            { BombShell, "https://wiki.guildwars2.com/images/b/b0/Bomb_Shell.png" },
            { GatlingFists1, "https://wiki.guildwars2.com/images/8/89/Gatling_Fists.png" },
            { GatlingFists2, "https://wiki.guildwars2.com/images/8/89/Gatling_Fists.png" },
            { BoilingOil, "https://wiki.guildwars2.com/images/6/6d/Pour_Oil.png" },
            // - Cannon
            { FireCannon, "https://wiki.guildwars2.com/images/5/5e/Fire_%28Cannon%29.png" },
            { FireCannonStrips1, "https://wiki.guildwars2.com/images/5/5e/Fire_%28Cannon%29.png" },
            { FireCannonStrips2, "https://wiki.guildwars2.com/images/5/5e/Fire_%28Cannon%29.png" },
            { FireCannonRadius, "https://wiki.guildwars2.com/images/5/5e/Fire_%28Cannon%29.png" },
            { Grapeshot, "https://wiki.guildwars2.com/images/4/46/Fire_Grapeshot.png" },
            { GrapeshotDamage, "https://wiki.guildwars2.com/images/4/46/Fire_Grapeshot.png" },
            { GrapeshotDamageDoubleBleeds1, "https://wiki.guildwars2.com/images/4/46/Fire_Grapeshot.png" },
            { GrapeshotDamageDoubleBleeds2, "https://wiki.guildwars2.com/images/4/46/Fire_Grapeshot.png" },
            { IceShot1, "https://wiki.guildwars2.com/images/9/9f/Ice_Shot.png" },
            { IceShot2, "https://wiki.guildwars2.com/images/9/9f/Ice_Shot.png" },
            { IceShotDamage, "https://wiki.guildwars2.com/images/9/9f/Ice_Shot.png" },
            { IceShotRadiusDamage, "https://wiki.guildwars2.com/images/9/9f/Ice_Shot.png" },
            // - Mortar
            { TurnLeftMortar, "https://wiki.guildwars2.com/images/4/4c/Turn_Left.png" },
            { TurnRightMortar, "https://wiki.guildwars2.com/images/4/4c/Turn_Right.png" },
            { ConcussionBarrageDamage, "https://wiki.guildwars2.com/images/e/e0/Fire_Incendiary_Shells.png" },
            { ConcussionBarrageSkill, "https://wiki.guildwars2.com/images/e/e0/Fire_Incendiary_Shells.png" },
            { FireExplosiveShellsDamage, "https://wiki.guildwars2.com/images/2/2a/Fire_Exploding_Shells.png" },
            { FireExplosiveShellsSkill, "https://wiki.guildwars2.com/images/2/2a/Fire_Exploding_Shells.png" },
            // - Arrow Cart
            { DeployArrowCart, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { DeploySuperiorArrowCart, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { DeployGuildArrowCart, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { VolleyArrowCart, "https://wiki.guildwars2.com/images/b/be/Fire_%28Arrow_Cart%29.png" },
            { FireImprovedArrows, "https://wiki.guildwars2.com/images/b/be/Fire_%28Arrow_Cart%29.png" },
            { FireDistantVolley, "https://wiki.guildwars2.com/images/b/be/Fire_%28Arrow_Cart%29.png" },
            { FireDevastatingArrows, "https://wiki.guildwars2.com/images/b/be/Fire_%28Arrow_Cart%29.png" },
            { CripplingVolley, "https://wiki.guildwars2.com/images/6/63/Fire_Crippling_Arrows.png" },
            { FireImprovedCripplingArrows, "https://wiki.guildwars2.com/images/6/63/Fire_Crippling_Arrows.png" },
            { FireReapingArrow, "https://wiki.guildwars2.com/images/6/63/Fire_Crippling_Arrows.png" },
            { FireStaggeringArrows, "https://wiki.guildwars2.com/images/6/63/Fire_Crippling_Arrows.png" },
            { FireSufferingArrows, "https://wiki.guildwars2.com/images/6/63/Fire_Crippling_Arrows.png" },
            { BarbedVolley, "https://wiki.guildwars2.com/images/6/63/Fire_Crippling_Arrows.png" },
            { FireImprovedBarbedArrows, "https://wiki.guildwars2.com/images/f/f2/Fire_Barbed_Arrows.png" },
            { FirePenetratingSniperArrows, "https://wiki.guildwars2.com/images/f/f2/Fire_Barbed_Arrows.png" },
            { FireExsanguinatingArrows, "https://wiki.guildwars2.com/images/f/f2/Fire_Barbed_Arrows.png" },
            { FireMercilessArrows, "https://wiki.guildwars2.com/images/f/f2/Fire_Barbed_Arrows.png" },
            { ToxicUnveilingVolley, "https://wiki.guildwars2.com/images/a/a4/Toxic_Unveiling_Volley.png" },
            // - Ballista
            { DeployBallista, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { DeploySuperiorBallista, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { DeployGuildBallista, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { BallistaBolt, "https://wiki.guildwars2.com/images/8/8f/Fire_%28Ballista%29.png" },
            { SwiftBoltDamage, "https://wiki.guildwars2.com/images/8/8f/Fire_%28Ballista%29.png" },
            { SniperBoltDamage, "https://wiki.guildwars2.com/images/8/8f/Fire_%28Ballista%29.png" },
            { ImprovedShatteringBoltDamage, "https://wiki.guildwars2.com/images/6/6b/Fire_Shattering_Bolt.png" },
            { GreaterShatteringBoltDamage, "https://wiki.guildwars2.com/images/6/6b/Fire_Shattering_Bolt.png" },
            { ReinforcedShotDamage, "https://wiki.guildwars2.com/images/8/89/Fire_Reinforced_Shot.png" },
            { GreaterReinforcedShotDamage, "https://wiki.guildwars2.com/images/8/89/Fire_Reinforced_Shot.png" },
            { ImprovedReinforcedShotDamage, "https://wiki.guildwars2.com/images/8/89/Fire_Reinforced_Shot.png" },
            { AntiairBallistaBoltDamage1, "https://wiki.guildwars2.com/images/5/50/Antiair_Bolt.png" },
            { AntiairBallistaBoltDamage2, "https://wiki.guildwars2.com/images/5/50/Antiair_Bolt.png" },
            { AntiairBallistaBoltDamage3, "https://wiki.guildwars2.com/images/5/50/Antiair_Bolt.png" },
            { AntiairBallistaBoltDamage4, "https://wiki.guildwars2.com/images/5/50/Antiair_Bolt.png" },
            // - Catapult
            { DeployCatapult, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { DeploySuperiorCatapult, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { DeployGuildCatapult, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { TurnLeftCatapult, "https://wiki.guildwars2.com/images/4/4c/Turn_Left.png" },
            { TurnRightCatapult, "https://wiki.guildwars2.com/images/4/4c/Turn_Right.png" },
            { FireBoulderSkill, "https://wiki.guildwars2.com/images/4/4d/Fire_Boulder.png" },
            { FireBoulder, "https://wiki.guildwars2.com/images/4/4d/Fire_Boulder.png" },
            { HeavyBoulderShot1, "https://wiki.guildwars2.com/images/4/4d/Fire_Boulder.png" },
            { HeavyBoulderShot2, "https://wiki.guildwars2.com/images/4/4d/Fire_Boulder.png" },
            { FireHeavyBoulder1, "https://wiki.guildwars2.com/images/4/4d/Fire_Boulder.png" },
            { FireHeavyBoulder2, "https://wiki.guildwars2.com/images/4/4d/Fire_Boulder.png" },
            { FireLargeHeavyBoulderSkill, "https://wiki.guildwars2.com/images/4/4d/Fire_Boulder.png" },
            { FireLargeHeavyBoulder, "https://wiki.guildwars2.com/images/4/4d/Fire_Boulder.png" },
            { FireHollowedBoulderSkill, "https://wiki.guildwars2.com/images/4/4d/Fire_Boulder.png" },
            { FireHollowedBoulder, "https://wiki.guildwars2.com/images/4/4d/Fire_Boulder.png" },
            { GravelShotSkill, "https://wiki.guildwars2.com/images/d/dd/Fire_Gravel.png" },
            { GravelShot2, "https://wiki.guildwars2.com/images/d/dd/Fire_Gravel.png" },
            { GravelShot3, "https://wiki.guildwars2.com/images/d/dd/Fire_Gravel.png" },
            { GravelShot4, "https://wiki.guildwars2.com/images/d/dd/Fire_Gravel.png" },
            { RendingGravelSkill, "https://wiki.guildwars2.com/images/d/dd/Fire_Gravel.png" },
            { FireHollowedGravel, "https://wiki.guildwars2.com/images/d/dd/Fire_Gravel.png" },
            { FireHollowedGravelSkill, "https://wiki.guildwars2.com/images/d/dd/Fire_Gravel.png" },
            { FireLargeRendingGravelSkill, "https://wiki.guildwars2.com/images/d/dd/Fire_Gravel.png" },
            { HollowedGravelShot, "https://wiki.guildwars2.com/images/d/dd/Fire_Gravel.png" },
            // - Flame Ram
            { DeployFlameRam, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { DeploySuperiorFlameRam, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { DeployGuildFlameRam, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { Ram, SkillImages.FireTrebuchet },
            { AcceleratedRam, SkillImages.FireTrebuchet },
            { ImpactSlam, "https://wiki.guildwars2.com/images/1/1f/Impact_Slam.png" },
            { WeakeningFlameBlast, "https://wiki.guildwars2.com/images/f/f8/Ring_of_Fire.png" },
            { IntenseFlameBlast, "https://wiki.guildwars2.com/images/f/f8/Ring_of_Fire.png" },
            // - Golem
            { DeployAlphaSiegeSuit, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { DeployOmegaSiegeSuit, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { DeployGuildSiegeSuit, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { PunchSiegeGolem, "https://wiki.guildwars2.com/images/8/8f/Punch.png" },
            { RocketPunch1, "https://wiki.guildwars2.com/images/6/6b/Rocket_Punch.png" },
            { RocketPunch2, "https://wiki.guildwars2.com/images/6/6b/Rocket_Punch.png" },
            { WhirlingAssaultSiegeGolem, "https://wiki.guildwars2.com/images/8/8b/Whirling_Assault.png" },
            { WhirlingInferno, "https://wiki.guildwars2.com/images/1/1b/Whirling_Inferno.png" },
            { HealingShieldBubble, "https://wiki.guildwars2.com/images/e/e3/Shield_Bubble.png" },
            { PullSiegeGolem, "https://wiki.guildwars2.com/images/4/41/Pull_%28Siege_Golem%29.png" },
            { RocketSalvo, "https://wiki.guildwars2.com/images/8/80/Rocket_Salvo.png" },
            { EjectSiegeGolem, "https://wiki.guildwars2.com/images/b/ba/Eject.png" },
            // - Shield Generator
            { DeployShieldGenerator, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { DeploySuperiorShieldGenerator, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { DeployGuildShieldGenerator, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { ForceBallDamage1, "https://wiki.guildwars2.com/images/f/f9/Force_Ball.png" },
            { ForceBallDamage2, "https://wiki.guildwars2.com/images/f/f9/Force_Ball.png" },
            { ForceBallDamage3, "https://wiki.guildwars2.com/images/f/f9/Force_Ball.png" },
            { ForceBallDamage4, "https://wiki.guildwars2.com/images/f/f9/Force_Ball.png" },
            { ForceWall1, "https://wiki.guildwars2.com/images/3/36/Force_Wall.png" },
            { ForceWall2, "https://wiki.guildwars2.com/images/3/36/Force_Wall.png" },
            // - Trebuchet
            { DeployTrebuchet, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { DeploySuperiorTrebuchet, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { DeployGuildTrebuchet, "https://wiki.guildwars2.com/images/b/bb/Deploy_Siege.png" },
            { TurnLeftTrebuchet, "https://wiki.guildwars2.com/images/4/4c/Turn_Left.png" },
            { TurnRightTrebuchet, "https://wiki.guildwars2.com/images/4/4c/Turn_Right.png" },
            { FireTrebuchetSkill, SkillImages.FireTrebuchet },
            { FireColossalExplosiveShot, SkillImages.FireTrebuchet },
            { FireCorrosiveShot1, SkillImages.FireTrebuchet },
            { FireCorrosiveShot2, SkillImages.FireTrebuchet },
            { FireMegaExplosiveShot1, SkillImages.FireTrebuchet },
            { FireMegaExplosiveShot2, SkillImages.FireTrebuchet },
            { FireMegaExplosiveShot3, SkillImages.FireTrebuchet },
            { FireTrebuchetDamage1, SkillImages.FireTrebuchet },
            { FireTrebuchetDamage2, SkillImages.FireTrebuchet },
            { FireTrebuchetDamage3, SkillImages.FireTrebuchet },
            { FireTrebuchetDamage4, SkillImages.FireTrebuchet },
            { FirePutridCow, "https://wiki.guildwars2.com/images/0/00/Fire_Rotting_Cow.png" },
            { FireRottingCow1, "https://wiki.guildwars2.com/images/0/00/Fire_Rotting_Cow.png" },
            { FireRottingCow2, "https://wiki.guildwars2.com/images/0/00/Fire_Rotting_Cow.png" },
            { FireBloatedPutridCow, "https://wiki.guildwars2.com/images/0/00/Fire_Rotting_Cow.png" },
            { RottenCow1, "https://wiki.guildwars2.com/images/0/00/Fire_Rotting_Cow.png" },
            { RottenCow2, "https://wiki.guildwars2.com/images/0/00/Fire_Rotting_Cow.png" },
            { RottenCow3, "https://wiki.guildwars2.com/images/0/00/Fire_Rotting_Cow.png" },
            { RottenCow4, "https://wiki.guildwars2.com/images/0/00/Fire_Rotting_Cow.png" },
            { RottenCow5, "https://wiki.guildwars2.com/images/0/00/Fire_Rotting_Cow.png" },
            { FireHealingOasisSkill, "https://wiki.guildwars2.com/images/4/49/Healing_Oasis.png" },
            { FireHealingOasisHealing, "https://wiki.guildwars2.com/images/4/49/Healing_Oasis.png" },
            // - Dragon Banner
            { DragonsMight, "https://wiki.guildwars2.com/images/9/9b/Dragon%27s_Might.png" },
            { DragonTalon1, "https://wiki.guildwars2.com/images/e/e9/Dragon_Talon.png" },
            { DragonTalon2, "https://wiki.guildwars2.com/images/e/e9/Dragon_Talon.png" },
            { DragonTalon3, "https://wiki.guildwars2.com/images/e/e9/Dragon_Talon.png" },
            { DragonBlast, "https://wiki.guildwars2.com/images/3/3e/Dragon_Blast.png" },
            { DragonsWings, "https://wiki.guildwars2.com/images/4/45/Dragon%27s_Wings.png" },
            { DragonsBreath, "https://wiki.guildwars2.com/images/2/29/Dragon%27s_Breath_%28skill%29.png" },
            // - Turtle Banner
            { ShellShock, "https://wiki.guildwars2.com/images/2/2f/Shell_Shock_%28skill%29.png" },
            { Shellter, "https://wiki.guildwars2.com/images/a/a4/Shell-ter.png" },
            { SavedByTheShell, "https://wiki.guildwars2.com/images/f/fc/Saved_by_the_Shell.png" },
            { ShellWall, "https://wiki.guildwars2.com/images/f/f5/Shell_Wall.png" },
            { BombShellRiverOfSouls, "https://wiki.guildwars2.com/images/b/b0/Bomb_Shell.png" },
            // - Centaur Banner
            { SpiritCentaur, "https://wiki.guildwars2.com/images/9/95/Spirit_Centaur.png" },
            { StampedeOfArrows, "https://wiki.guildwars2.com/images/f/f8/Stampede_of_Arrows.png" },
            { CentaurDash, "https://wiki.guildwars2.com/images/6/6b/Centaur_Dash.png" },
            { SpikeBarricade, "https://wiki.guildwars2.com/images/3/3f/Spike_Barricade.png" },
            { OutrunACentaur, "https://wiki.guildwars2.com/images/7/7d/Outrun_a_Centaur.png" },
            { RendingThrashCentaurBannerSkill, BuffImages.RendingThrashCentaurBanner },
            { CripplingStrikeCentaurBannerSkill, BuffImages.CripplingStrikeCentaurBanner },
            { KickDustCentaurBannerSkill, BuffImages.KickDustCentaurBanner },
            #endregion WvWIcons
            #region FinisherIcons         
            { Finisher1, ItemImages.BasicFinisher },
            { Finisher2, ItemImages.BasicFinisher },
            { RabbitRankFinisher, ItemImages.RabbitRankFinisher },
            { DeerRankFinisher, ItemImages.DeerRankFinisher },
            { DolyakRankFinisher, ItemImages.DolyakRankFinisher },
            { WolfRankFinisher, ItemImages.WolfRankFinisher },
            { TigerRankFinisher, ItemImages.TigerRankFinisher },
            { BearRankFinisher, ItemImages.BearRankFinisher },
            { SharkRankFinisher, ItemImages.SharkRankFinisher },
            { PhoenixRankFinisher, ItemImages.PhoenixRankFinisher },
            { DragonRankFinisher, ItemImages.DragonRankFinisher },
            { MordremRabbitFinisher, ItemImages.MordremRabbitFinisher },
            { MordremDeerFinisher, ItemImages.MordremDeerFinisher },
            { MordremDolyakFinisher, ItemImages.MordremDolyakFinisher },
            { MordremWolfFinisher, ItemImages.MordremWolfFinisher },
            { MordremTigerFinisher, ItemImages.MordremTigerFinisher },
            { MordremBearFinisher, ItemImages.MordremBearFinisher },
            { MordremSharkFinisher, ItemImages.MordremSharkFinisher },
            { MordremPhoenixFinisher, ItemImages.MordremPhoenixFinisher },
            { MordremDragonFinisher, ItemImages.MordremDragonFinisher },
            { WvWGoldenDolyakFinisher, ItemImages.WvWGoldenDolyakFinisher },
            { WvWSilverDolyakFinisher, ItemImages.WvWSilverDolyakFinisher },
            { ChineseWorldTournamentFinisher, ItemImages.ChineseWorldTournamentFinisher },
            { NorthAmericanWorldTournamentFinisher, ItemImages.NorthAmericanWorldTournamentFinisher },
            { EuropeanWorldTournamentFinisher, ItemImages.EuropeanWorldTournamentFinisher },
            { AscalonianLeaderFinisher, ItemImages.AscalonianLeaderFinisher },
            { CuteQuagganFinisher, ItemImages.CuteQuagganFinisher },
            { BirthdayFinisher, ItemImages.BirthdayFinisher },
            { ChoyaFinisher, ItemImages.ChoyaFinisher },
            { CowFinisher, ItemImages.CowFinisher },
            { GiftFinisher, ItemImages.GiftFinisher },
            { GolemPummelerFinisher, ItemImages.GolemPummelerFinisher },
            { GraveFinisher, ItemImages.GraveFinisher },
            { GreatJungleWurmFinisher, ItemImages.GreatJungleWurmFinisher },
            { GuildFlagFinisher, ItemImages.GuildFlagFinisher },
            { GuildShieldFinisher, ItemImages.GuildShieldFinisher },
            { HiddenMinstrelFinisher, ItemImages.HiddenMinstrelFinisher },
            { LeyLineFinisher, ItemImages.LeyLineFinisher },
            { LlamaFinisher, ItemImages.LlamaFinisher },
            { MadKingFinisher, ItemImages.MadKingFinisher },
            { MysticalDragonFinisher, ItemImages.MysticalDragonFinisher },
            { RainbowUnicornFinisher, ItemImages.RainbowUnicornFinisher },
            { RevenantFinisher, ItemImages.RevenantFinisher },
            { SandsharkFinisher, ItemImages.SandsharkFinisher },
            { ScarecrowFinisher, ItemImages.ScarecrowFinisher },
            { SnowGlobeFinisher, ItemImages.SnowGlobeFinisher },
            { SnowmanFinisher1, ItemImages.SnowmanFinisher },
            { SnowmanFinisher2, ItemImages.SnowmanFinisher },
            { SuperExplosiveFinisher, ItemImages.SuperExplosiveFinisher },
            { TwistedWatchworkFinisher, ItemImages.TwistedWatchworkFinisher },
            { WhumpTheGiantFinisher, ItemImages.WhumpTheGiantFinisher },
            { WizardLightningFinisher, ItemImages.WizardLightningFinisher },
            { SpectreFinisher, ItemImages.SpectreFinisher},
            { VigilMegalaserFinisher, ItemImages.VigilMegalaserFinisher },
            { ThornrootFinisher, ItemImages.ThornrootFinisher },
            { SanctifiedFinisher, ItemImages.SanctifiedFinisher },
            { MartialFinisher, ItemImages.MartialFinisher },
            { ToxicOffshootFinisher, ItemImages.ToxicOffshootFinisher },
            { SkrittScavengerFinisher, ItemImages.SkrittScavengerFinisher },
            { ChickenadoFinisher, ItemImages.ChickenadoFinisher },
            { PactFleetFinisher, ItemImages.PactFleetFinisher },
            { RealmPortalSpikeFinisher, ItemImages.RealmPortalSpikeFinisher },
            { AvatarOfDeathFinisher, ItemImages.AvatarOfDeathFinisher },
            { HonorGuardFinisher, ItemImages.HonorGuardFinisher },
	        #endregion FinisherIcons
    };

    private static readonly Dictionary<long, ulong> _nonCritable = new()
    {
        { LightningStrike_SigilOfAir, GW2Builds.StartOfLife },
        { FlameBlast_SigilOfFire, GW2Builds.StartOfLife },
        { FireAttunementSkill, GW2Builds.December2018Balance },
        { Mug, GW2Builds.StartOfLife },
        { PulmonaryImpactSkill, GW2Builds.HoTRelease },
        { ConjuredSlashPlayer, GW2Builds.StartOfLife },
        { LightningJolt, GW2Builds.StartOfLife },
        { Sunspot, GW2Builds.December2018Balance },
        { EarthenBlast, GW2Builds.December2018Balance },
        { ChillingNova, GW2Builds.December2018Balance },
        { LesserBanishEnchantment, GW2Builds.December2018Balance },
        { LesserEnfeeble, GW2Builds.December2018Balance },
        { SpitefulSpirit, GW2Builds.December2018Balance },
        { LesserSpinalShivers, GW2Builds.December2018Balance },
        { PowerBlock, GW2Builds.December2018Balance },
        { ShatteredAegis, GW2Builds.December2018Balance },
        { GlacialHeart, GW2Builds.December2018Balance },
        { ThermalReleaseValve, GW2Builds.December2018Balance },
        { LossAversion, GW2Builds.December2018Balance },
        { Epidemic, GW2Builds.May2017Balance },
    };

    private const string DefaultIcon = SkillImages.MonsterSkill;

    // Fields
    public readonly long ID;
    //public int Range { get; private set; } = 0;
    public readonly bool AA;

    public bool IsSwap => ID == WeaponSwap || ElementalistHelper.IsElementalSwap(ID) || RevenantHelper.IsLegendSwap(ID) || HarbingerHelper.IsHarbingerShroudTransform(ID);
    public bool IsDodge(SkillData skillData) => IsAnimatedDodge(skillData) || ID == MirageCloakDodge;
    public bool IsAnimatedDodge(SkillData skillData) => ID == skillData.DodgeId || VindicatorHelper.IsVindicatorDodge(ID);
    public readonly string Name = "";
    public readonly string Icon = "";
    private readonly WeaponDescriptor? _weaponDescriptor;
    public bool IsWeaponSkill => _weaponDescriptor != null;
    internal readonly GW2APISkill? ApiSkill;
    private SkillInfoEvent? _skillInfo;

    internal const string DefaultName = "UNKNOWN";

    public bool UnknownSkill => Name == DefaultName;

    // Constructor

    [Obsolete("Dont use this, testing only")] //TODO(Rennorb) @cleanup
    public SkillItem(bool swap) { ID = swap ? WeaponSwap : default; }

    internal SkillItem(long ID, string name, GW2APIController apiController)
    {
        this.ID = ID;
        Name = name.Replace("\0", "");
        ApiSkill = apiController.GetAPISkill(ID);
        //
        if (_overrideNames.TryGetValue(ID, out var overrideName))
        {
            Name = overrideName;
        }
        else if (ApiSkill != null && (UnknownSkill || Name.All(char.IsDigit)))
        {
            Name = ApiSkill.Name;
        }
        if (_overrideIcons.TryGetValue(ID, out var icon))
        {
            Icon = icon;
        }
        else
        {
            Icon = ApiSkill != null ? ApiSkill.Icon : DefaultIcon;
        }
        if (ApiSkill != null && ApiSkill.Type == "Weapon"
            && ApiSkill.WeaponType != "None" && ApiSkill.Professions.Count > 0
            && WeaponDescriptor.IsWeaponSlot(ApiSkill.Slot))
        {
            // Special handling of specter shroud as it is not done in the same way 
            var isSpecterShroud = ApiSkill.Professions.Contains("Thief") && ApiSkill.Facts.Any(x => x.Text != null && x.Text.Contains("Tethered Ally"));
            if (!isSpecterShroud)
            {
                _weaponDescriptor = new WeaponDescriptor(ApiSkill);
            }
        }
        AA = (ApiSkill?.Slot == "Weapon_1" || ApiSkill?.Slot == "Downed_1");
        if (AA)
        {
            if (ApiSkill?.Categories != null)
            {
                AA = AA && !ApiSkill.Categories.Contains("StealthAttack") && !ApiSkill.Categories.Contains("Ambush"); // Ambush in case one day it's added
            }
            if (ApiSkill?.Description != null)
            {
                AA = AA && !ApiSkill.Description.Contains("Ambush.");
            }
        }
#if DEBUG
        Name = ID + "-" + Name;
#endif
    }

    public static bool CanCrit(long id, ulong gw2Build)
    {
        if (_nonCritable.TryGetValue(id, out ulong build))
        {
            return gw2Build < build;
        }
        return true;
    }

    internal int FindFirstWeaponSet(IReadOnlyList<(int to, int from)> swaps)
    {
        int swapped = WeaponSetIDs.NoSet;
        // we started on a proper weapon set
        if (_weaponDescriptor != null)
        {
            swapped = _weaponDescriptor.FindFirstWeaponSet(swaps);
        }
        return swapped;
    }

    internal bool EstimateWeapons(WeaponSets weaponSets, int swapped, bool validForCurrentSwap)
    {
        bool keep = WeaponSetIDs.IsWeaponSet(swapped);
        if (_weaponDescriptor == null || !keep || !validForCurrentSwap || ApiSkill == null)
        {
            return false;
        }
        weaponSets.SetWeapons(_weaponDescriptor, ApiSkill, swapped);
        return true;
    }

    internal void AttachSkillInfoEvent(SkillInfoEvent skillInfo)
    {
        if (ID == skillInfo.SkillID)
        {
            _skillInfo = skillInfo;
        }
    }

    // Public Methods

    /*public void SetCCAPI()//this is 100% off the GW2 API is not a reliable source of finding skill CC
    {
        CC = 0;
        if (_apiSkill != null)
        {
            GW2APISkillDetailed apiskilldet = (GW2APISkillDetailed)_apiSkill;
            GW2APISkillCheck apiskillchec = (GW2APISkillCheck)_apiSkill;
            GW2APIfacts[] factsList = apiskilldet != null ? apiskilldet.facts : apiskillchec.facts;
            bool daze = false;
            bool stun = false;
            bool knockdown = false;
            bool flaot = false;
            bool knockback = false;
            bool launch = false;
            bool pull = false;
           
            foreach (GW2APIfacts fact in factsList)
            {
                if (daze == false)
                {
                    if (fact.text == "Daze" || fact.status == "Daze")
                    {
                        if (fact.duration < 1)
                        {
                            CC += 100;
                        }
                        else
                        {
                            CC += fact.duration * 100;
                        }
                        daze = true;
                    }

                }
                if (stun == false)
                {
                    if (fact.text == "Stun" || fact.status == "Stun")
                    {
                        if (fact.duration < 1)
                        {
                            CC += 100;
                        }
                        else
                        {
                            CC += fact.duration * 100;
                        }
                        stun = true;
                    }
                }
                if (knockdown == false)
                {
                    if (fact.text == "Knockdown" || fact.status == "Knockdown")
                    {
                        if (fact.duration < 1)
                        {
                            CC += 100;
                        }
                        else
                        {
                            CC += fact.duration * 100;
                        }
                        knockdown = true;
                    }
                }
                if (launch == false)
                {
                    if (fact.text == "Launch" || fact.status == "Launch")
                    {

                        CC += 232;//Wiki says either 232 or 332 based on duration? launch doesn't provide duration in api however
                       
                        launch = true;
                    }
                }
                if (knockback == false)
                {
                    if (fact.text == "Knockback" || fact.status == "Knockback")
                    {

                        CC += 150;//always 150 unless special case of 232 for ranger pet?
                        knockback = true;
                    }
                }
                if (pull == false)
                {
                    if (fact.text == "Pull" || fact.status == "Pull")
                    {

                        CC += 150;

                        pull = true;
                    }
                }
                if (flaot == false)
                {
                    if (fact.text == "Float" || fact.status == "Float")
                    {
                        if (fact.duration < 1)
                        {
                            CC += 100;
                        }
                        else
                        {
                            CC += fact.duration * 100;
                        }
                        flaot = true;
                    }
                }
                if (fact.text == "Stone Duration" || fact.status == "Stone Duration")
                {
                    if (fact.duration < 1)
                    {
                        CC += 100;
                    }
                    else
                    {
                        CC += fact.duration * 100;
                    }
                    
                }

            
            }
            if (ID == 30725)//toss elixir x
            {
                CC = 300;
            }
            if (ID == 29519)//MOA signet
            {
                CC = 1000;
            }
           
        }
    }*/
}
