namespace GW2EIEvtcParser;

/// <summary>
/// Pool of mechanic IDs used in the parser, always custom.
/// <para>Naming convention: </para>
/// <list type="bullet">
/// <item>No "id" inside the name.</item>
/// <item>Value must be strictly positive</item>
/// <item>Prefix the name with "Mech_"</item>
/// <item>A masking system is in place, based on categories (Raid, Fractal, ...)</item>
/// </list>
/// </summary>
public static class MechanicIDs
{
    private const int CommonMask = 0x01000000;
    private const int RaidWingMask = 0x02000000;
    private const int FractalMask = 0x03000000;
    private const int RaidEncounterMask = 0x04000000;
    private const int OpenWorldMask = 0x05000000;
    private const int StoryInstanceMask = 0x06000000;
    private const int WvWMask = 0x07000000;
    private const int GolemMask = 0x08000000;
    private const int ConvergenceMask = 0x09000000;

    private const int SpiritValeMask = RaidWingMask | 0x00010000;
    private const int SalvationPassMask = RaidWingMask | 0x00020000;
    private const int StrongholdOfTheFaithfulMask = RaidWingMask | 0x00030000;
    private const int BastionOfThePenitentMask = RaidWingMask | 0x00040000;
    private const int HallOfChainsMask = RaidWingMask | 0x00050000;
    private const int MythwrightGambitMask = RaidWingMask | 0x00060000;
    private const int TheKeyOfAhdashimMask = RaidWingMask | 0x00070000;
    private const int MountBalriorMask = RaidWingMask | 0x00080000;


    #region COMMONS
    private static int _commonCount = 0;
    public static readonly int Mech_PlayerDead = CommonMask | ++_commonCount;
    public static readonly int Mech_PlayerDowned = CommonMask | ++_commonCount;
    public static readonly int Mech_PlayerUp = CommonMask | ++_commonCount;
    public static readonly int Mech_PlayerRes = CommonMask | ++_commonCount;
    public static readonly int Mech_PlayerDC = CommonMask | ++_commonCount;
    public static readonly int Mech_PlayerSpawn = CommonMask | ++_commonCount;
    public static readonly int Mech_PlayerKD = CommonMask | ++_commonCount;
    public static readonly int Mech_PlayerKBP = CommonMask | ++_commonCount;
    public static readonly int Mech_PlayerFloat = CommonMask | ++_commonCount;
    public static readonly int Mech_PlayerLaunch = CommonMask | ++_commonCount;
    public static readonly int Mech_PlayerLockOut = CommonMask | ++_commonCount;
    public static readonly int Mech_PlayerFloatSinkWater = CommonMask | ++_commonCount;
    #endregion COMMONS

    #region RAID ENCOUNTERS
    private const int FestivalMask = RaidEncounterMask | 0x00010000;
    private const int IBSMask = RaidEncounterMask | 0x00020000;
    private const int EODMask = RaidEncounterMask | 0x00030000;
    private const int CoreMask = RaidEncounterMask | 0x00040000;
    private const int SotOMask = RaidEncounterMask | 0x00050000;
    private const int VoEMask = RaidEncounterMask | 0x00060000;

    private static int _raidEncounterCommonCount = 0;
    public static readonly int Mech_PlayerExposed = RaidEncounterMask | ++_raidEncounterCommonCount;
    public static readonly int Mech_PlayerDebilitated = RaidEncounterMask | ++_raidEncounterCommonCount;
    public static readonly int Mech_PlayerInfirmity = RaidEncounterMask | ++_raidEncounterCommonCount;
    #region FESTIVAL
    private static int _festivalCount = 0;
    public static readonly int Mech_AuroraBeamTarget = FestivalMask | ++_festivalCount;
    public static readonly int Mech_AuroraBeam = FestivalMask | ++_festivalCount;
    public static readonly int Mech_AuroraBeamCast = FestivalMask | ++_festivalCount;
    public static readonly int Mech_GiantSnowballTarget = FestivalMask | ++_festivalCount;
    public static readonly int Mech_GiantSnowball = FestivalMask | ++_festivalCount;
    public static readonly int Mech_GiantSnowballCast = FestivalMask | ++_festivalCount;
    public static readonly int Mech_Blizzard = FestivalMask | ++_festivalCount;
    public static readonly int Mech_FrostPatch = FestivalMask | ++_festivalCount;
    public static readonly int Mech_FrostPatchCast = FestivalMask | ++_festivalCount;
    public static readonly int Mech_JuttingIceSpikes = FestivalMask | ++_festivalCount;
    public static readonly int Mech_FireSnowball = FestivalMask | ++_festivalCount;
    public static readonly int Mech_IcyBarrier = FestivalMask | ++_festivalCount;
    #endregion FESTIVAL
    #region IBS
    private static int _ibsCount = 0;
    public static readonly int Mech_IceArmSwing = IBSMask | ++_ibsCount;
    public static readonly int Mech_IceArmSwingCC = IBSMask | ++_ibsCount;
    public static readonly int Mech_IceArmSwingCast = IBSMask | ++_ibsCount;
    public static readonly int Mech_IceShatter = IBSMask | ++_ibsCount;
    public static readonly int Mech_IceCrystal = IBSMask | ++_ibsCount;
    public static readonly int Mech_FrostBite = IBSMask | ++_ibsCount;
    public static readonly int Mech_IceFrail = IBSMask | ++_ibsCount;
    public static readonly int Mech_IceFrailCC = IBSMask | ++_ibsCount;
    public static readonly int Mech_DeadlyIceShockwave = IBSMask | ++_ibsCount;
    public static readonly int Mech_DeadlyIceShockwaveCast = IBSMask | ++_ibsCount;
    public static readonly int Mech_IceShockwave = IBSMask | ++_ibsCount;
    public static readonly int Mech_SpinningIce = IBSMask | ++_ibsCount;
    public static readonly int Mech_IceQuake = IBSMask | ++_ibsCount;
    public static readonly int Mech_IceShockWaveFraenir = IBSMask | ++_ibsCount;
    public static readonly int Mech_IceArmSwingFraenir = IBSMask | ++_ibsCount;
    public static readonly int Mech_FrozenMissile = IBSMask | ++_ibsCount;
    public static readonly int Mech_FrozenMissileCast = IBSMask | ++_ibsCount;
    public static readonly int Mech_SeismicCrush = IBSMask | ++_ibsCount;
    public static readonly int Mech_SeismicCrushCast = IBSMask | ++_ibsCount;
    public static readonly int Mech_FrigidFusillade = IBSMask | ++_ibsCount;
    public static readonly int Mech_FrigidFusilladeCast = IBSMask | ++_ibsCount;
    public static readonly int Mech_Frozen = IBSMask | ++_ibsCount;
    public static readonly int Mech_Unfrozen = IBSMask | ++_ibsCount;
    public static readonly int Mech_Snowblind = IBSMask | ++_ibsCount;
    public static readonly int Mech_Groundshaker = IBSMask | ++_ibsCount;
    public static readonly int Mech_Groundpiercer = IBSMask | ++_ibsCount;
    public static readonly int Mech_UnrelentingPainApply = IBSMask | ++_ibsCount;
    public static readonly int Mech_ImmobileApplyVC = IBSMask | ++_ibsCount;
    public static readonly int Mech_EnragedBC = IBSMask | ++_ibsCount;
    public static readonly int Mech_DeadlySynergy = IBSMask | ++_ibsCount;
    public static readonly int Mech_KodanTeleport = IBSMask | ++_ibsCount;
    public static readonly int Mech_Grasp = IBSMask | ++_ibsCount;
    public static readonly int Mech_Cascade = IBSMask | ++_ibsCount;
    public static readonly int Mech_BoneskinnerCharge = IBSMask | ++_ibsCount;
    public static readonly int Mech_BoneskinnerChargeCastEnd = IBSMask | ++_ibsCount;
    public static readonly int Mech_CrushingCruelty = IBSMask | ++_ibsCount;
    public static readonly int Mech_DeathWind = IBSMask | ++_ibsCount;
    public static readonly int Mech_DeathWindCastEnd = IBSMask | ++_ibsCount;
    public static readonly int Mech_DouseInDarkness = IBSMask | ++_ibsCount;
    public static readonly int Mech_DouseInDarknessCastEnd = IBSMask | ++_ibsCount;
    public static readonly int Mech_BoneskinnerBarrageWisp = IBSMask | ++_ibsCount;
    public static readonly int Mech_BoneskinnerBreakbarStart = IBSMask | ++_ibsCount;
    public static readonly int Mech_BoneskinnerExposed = IBSMask | ++_ibsCount;
    public static readonly int Mech_IcyEchoes = IBSMask | ++_ibsCount;
    public static readonly int Mech_Detonate = IBSMask | ++_ibsCount;
    public static readonly int Mech_LethalCoalescence = IBSMask | ++_ibsCount;
    public static readonly int Mech_FlameWall = IBSMask | ++_ibsCount;
    public static readonly int Mech_CallAssassins = IBSMask | ++_ibsCount;
    public static readonly int Mech_ChargeCW = IBSMask | ++_ibsCount;
    public static readonly int Mech_ChainsOfFrost = IBSMask | ++_ibsCount;
    public static readonly int Mech_ChainsOfFrostApply = IBSMask | ++_ibsCount;
    public static readonly int Mech_ChainsOfFrostCast = IBSMask | ++_ibsCount;
    public static readonly int Mech_SlitheringRime = IBSMask | ++_ibsCount;
    public static readonly int Mech_LethalCoalescenceSoaked = IBSMask | ++_ibsCount;
    public static readonly int Mech_LethalCoalescenceSoakedStart = IBSMask | ++_ibsCount;
    public static readonly int Mech_LethalCoalescenceBuff = IBSMask | ++_ibsCount;
    public static readonly int Mech_SpreadingIceOwn = IBSMask | ++_ibsCount;
    public static readonly int Mech_SpreadingIceOwnCast = IBSMask | ++_ibsCount;
    public static readonly int Mech_SpreadingIceOthers = IBSMask | ++_ibsCount;
    public static readonly int Mech_IcySlice = IBSMask | ++_ibsCount;
    public static readonly int Mech_IceTempest = IBSMask | ++_ibsCount;
    public static readonly int Mech_FrigidVortex = IBSMask | ++_ibsCount;
    public static readonly int Mech_FrigidVortexCast = IBSMask | ++_ibsCount;
    public static readonly int Mech_FrigidVortexApply = IBSMask | ++_ibsCount;
    public static readonly int Mech_IceShatterWhisper = IBSMask | ++_ibsCount;
    public static readonly int Mech_WhisperTPBack = IBSMask | ++_ibsCount;
    public static readonly int Mech_WhisperTPOut = IBSMask | ++_ibsCount;
    public static readonly int Mech_ViciousSlam = IBSMask | ++_ibsCount;
    #endregion IBS

    #region EOD
    private static int _eodCount = 0;
    public static readonly int Mech_NightmareFusilladeMain = EODMask | ++_eodCount;
    public static readonly int Mech_NightmareFusilladeSide = EODMask | ++_eodCount;
    public static readonly int Mech_ElectricBlast = EODMask | ++_eodCount;
    public static readonly int Mech_ToxicOrb = EODMask | ++_eodCount;
    public static readonly int Mech_Heartpiercer = EODMask | ++_eodCount;
    public static readonly int Mech_HeartpiercerNoStab = EODMask | ++_eodCount;
    public static readonly int Mech_FissureOfTorment = EODMask | ++_eodCount;
    public static readonly int Mech_TormentingWave = EODMask | ++_eodCount;
    public static readonly int Mech_TormentingWaveDead = EODMask | ++_eodCount;
    public static readonly int Mech_LeyBreach = EODMask | ++_eodCount;
    public static readonly int Mech_LeyBreachTarget = EODMask | ++_eodCount;
    public static readonly int Mech_ToxicBullet = EODMask | ++_eodCount;
    public static readonly int Mech_FocusedDestructionDown = EODMask | ++_eodCount;
    public static readonly int Mech_FocusedDestructionDead = EODMask | ++_eodCount;
    public static readonly int Mech_PhotonSaturation = EODMask | ++_eodCount;
    public static readonly int Mech_AHSharedDestruction = EODMask | ++_eodCount;
    public static readonly int Mech_KaleidoscopicChaos = EODMask | ++_eodCount;
    public static readonly int Mech_ChaosAndDestruction = EODMask | ++_eodCount;
    public static readonly int Mech_MagBeam = EODMask | ++_eodCount;
    public static readonly int Mech_MagneticBomb = EODMask | ++_eodCount;
    public static readonly int Mech_BeamTargetGreen = EODMask | ++_eodCount;
    public static readonly int Mech_BeamTargetRed = EODMask | ++_eodCount;
    public static readonly int Mech_BeamTargetBlue = EODMask | ++_eodCount;
    public static readonly int Mech_GraspingHorror = EODMask | ++_eodCount;
    public static readonly int Mech_DeathsEmbrace = EODMask | ++_eodCount;
    public static readonly int Mech_DeathsEmbraceCast = EODMask | ++_eodCount;
    public static readonly int Mech_DeathsHandInBetween = EODMask | ++_eodCount;
    public static readonly int Mech_DeathsHandDropped = EODMask | ++_eodCount;
    public static readonly int Mech_DeathsHandTarget = EODMask | ++_eodCount;
    public static readonly int Mech_ImminentDeath = EODMask | ++_eodCount;
    public static readonly int Mech_ImminentDeathApply = EODMask | ++_eodCount;
    public static readonly int Mech_WallOfFear = EODMask | ++_eodCount;
    public static readonly int Mech_WaveOfTorment = EODMask | ++_eodCount;
    public static readonly int Mech_TerrifyingApparition = EODMask | ++_eodCount;
    public static readonly int Mech_TerrifyingApparitionTarget = EODMask | ++_eodCount;
    public static readonly int Mech_ClarityLost = EODMask | ++_eodCount;
    public static readonly int Mech_ClarityKept = EODMask | ++_eodCount;
    public static readonly int Mech_XJJZhaitansReachPull = EODMask | ++_eodCount;
    public static readonly int Mech_XJJZhaitansReachKnock = EODMask | ++_eodCount;
    public static readonly int Mech_XJJHallucinations = EODMask | ++_eodCount;
    public static readonly int Mech_HatredFixated = EODMask | ++_eodCount;
    public static readonly int Mech_InevitabilityOfDeath = EODMask | ++_eodCount;
    public static readonly int Mech_PowerOfTheVoid = EODMask | ++_eodCount;
    public static readonly int Mech_DevouringVoid = EODMask | ++_eodCount;
    public static readonly int Mech_UndevouredLost = EODMask | ++_eodCount;
    public static readonly int Mech_UndevouredKept = EODMask | ++_eodCount;
    public static readonly int Mech_DragonSlashWave = EODMask | ++_eodCount;
    public static readonly int Mech_DragonSlashBurst = EODMask | ++_eodCount;
    public static readonly int Mech_DragonSlashRush = EODMask | ++_eodCount;
    public static readonly int Mech_TestReflexesLost = EODMask | ++_eodCount;
    public static readonly int Mech_TestReflexesKept = EODMask | ++_eodCount;
    public static readonly int Mech_StormOfSwords = EODMask | ++_eodCount;
    public static readonly int Mech_RainOfBlades = EODMask | ++_eodCount;
    public static readonly int Mech_MindbladeFixated = EODMask | ++_eodCount;
    public static readonly int Mech_EnforcerRushingJustice = EODMask | ++_eodCount;
    public static readonly int Mech_EnforcerFixated = EODMask | ++_eodCount;
    public static readonly int Mech_BoomingCommand = EODMask | ++_eodCount;
    public static readonly int Mech_ExplosiveUppercut = EODMask | ++_eodCount;
    public static readonly int Mech_FallOfTheAxeSmall = EODMask | ++_eodCount;
    public static readonly int Mech_FallOfTheAxeBig = EODMask | ++_eodCount;
    public static readonly int Mech_ElectricRain = EODMask | ++_eodCount;
    public static readonly int Mech_JadeBusterCannon = EODMask | ++_eodCount;
    public static readonly int Mech_SniperRicochet = EODMask | ++_eodCount;
    public static readonly int Mech_EnchancedDestructiveAuraApply = EODMask | ++_eodCount;
    public static readonly int Mech_DestructiveAuraApply = EODMask | ++_eodCount;
    public static readonly int Mech_LethalInspiration = EODMask | ++_eodCount;
    public static readonly int Mech_EnchancedDestructiveAura = EODMask | ++_eodCount;
    public static readonly int Mech_MostResistanceNotGained = EODMask | ++_eodCount;
    public static readonly int Mech_MostResistanceGained = EODMask | ++_eodCount;
    public static readonly int Mech_KOTargetedExpulsion = EODMask | ++_eodCount;
    public static readonly int Mech_KOTargetOrder = EODMask | ++_eodCount;
    public static readonly int Mech_KOSharedDestruction = EODMask | ++_eodCount;
    public static readonly int Mech_KOSharedDestructionSuccess = EODMask | ++_eodCount;
    public static readonly int Mech_KOSharedDestructionFail = EODMask | ++_eodCount;
    public static readonly int Mech_HTTargetedExpulsionTarget = EODMask | ++_eodCount;
    public static readonly int Mech_HTTargetedExpulsion = EODMask | ++_eodCount;
    public static readonly int Mech_VoidPoolBait = EODMask | ++_eodCount;
    public static readonly int Mech_VoidPool = EODMask | ++_eodCount;
    public static readonly int Mech_InfluenceOfTheVoidApply = EODMask | ++_eodCount;
    public static readonly int Mech_InfluenceOfTheVoid = EODMask | ++_eodCount;
    public static readonly int Mech_HTOrbPush = EODMask | ++_eodCount;
    public static readonly int Mech_NopeRopesLost = EODMask | ++_eodCount;
    public static readonly int Mech_NopeRopesKept = EODMask | ++_eodCount;
    public static readonly int Mech_VoidExplosition = EODMask | ++_eodCount;
    public static readonly int Mech_VoidExplositionChampion = EODMask | ++_eodCount;
    public static readonly int Mech_MagicDischarge = EODMask | ++_eodCount;
    public static readonly int Mech_HTGreenSuccess = EODMask | ++_eodCount;
    public static readonly int Mech_HTGreenFail = EODMask | ++_eodCount;
    public static readonly int Mech_LightningOfJormag = EODMask | ++_eodCount;
    public static readonly int Mech_FlameOfPrimordus = EODMask | ++_eodCount;
    public static readonly int Mech_StormFall = EODMask | ++_eodCount;
    public static readonly int Mech_BreathOfJormag = EODMask | ++_eodCount;
    public static readonly int Mech_GraspOfJormag = EODMask | ++_eodCount;
    public static readonly int Mech_FrostMeteor = EODMask | ++_eodCount;
    public static readonly int Mech_LavaSlam = EODMask | ++_eodCount;
    public static readonly int Mech_JawsOfDestruction = EODMask | ++_eodCount;
    public static readonly int Mech_CrystalBarrage = EODMask | ++_eodCount;
    public static readonly int Mech_BrandingBeam = EODMask | ++_eodCount;
    public static readonly int Mech_BrandedArtillery = EODMask | ++_eodCount;
    public static readonly int Mech_VoidPoolKralk = EODMask | ++_eodCount;
    public static readonly int Mech_PoolOfUndeath = EODMask | ++_eodCount;
    public static readonly int Mech_SwarmOfMordremoth = EODMask | ++_eodCount;
    public static readonly int Mech_GravityCrush = EODMask | ++_eodCount;
    public static readonly int Mech_NightmareEpoch = EODMask | ++_eodCount;
    public static readonly int Mech_MordremothShockwave = EODMask | ++_eodCount;
    public static readonly int Mech_MordremothShockwaveCast = EODMask | ++_eodCount;
    public static readonly int Mech_PoisonRoar = EODMask | ++_eodCount;
    public static readonly int Mech_SkullPiercerKick = EODMask | ++_eodCount;
    public static readonly int Mech_SkullPiercerChargedShot = EODMask | ++_eodCount;
    public static readonly int Mech_DeathScream = EODMask | ++_eodCount;
    public static readonly int Mech_RottingBile = EODMask | ++_eodCount;
    public static readonly int Mech_GiantStomp = EODMask | ++_eodCount;
    public static readonly int Mech_ScreamOfZhaitain = EODMask | ++_eodCount;
    public static readonly int Mech_PutridDeluge = EODMask | ++_eodCount;
    public static readonly int Mech_ZhaitanTailSlam = EODMask | ++_eodCount;
    public static readonly int Mech_CorruptedWaters = EODMask | ++_eodCount;
    public static readonly int Mech_HydroBurst = EODMask | ++_eodCount;
    public static readonly int Mech_CallLightning = EODMask | ++_eodCount;
    public static readonly int Mech_FrozenFury = EODMask | ++_eodCount;
    public static readonly int Mech_RollingFlame = EODMask | ++_eodCount;
    public static readonly int Mech_ShatterEarth = EODMask | ++_eodCount;
    public static readonly int Mech_TsunamiSlamOrb = EODMask | ++_eodCount;
    public static readonly int Mech_ClawSlap = EODMask | ++_eodCount;
    public static readonly int Mech_VoidPoolSooWon = EODMask | ++_eodCount;
    public static readonly int Mech_TsunamiSlamTail = EODMask | ++_eodCount;
    public static readonly int Mech_TormentOfTheVoid = EODMask | ++_eodCount;
    public static readonly int Mech_MagicHail = EODMask | ++_eodCount;
    public static readonly int Mech_VoidObliteratorFirebomb = EODMask | ++_eodCount;
    public static readonly int Mech_VoidObliteratorBreath = EODMask | ++_eodCount;
    public static readonly int Mech_VoidObliteratorCharge = EODMask | ++_eodCount;
    public static readonly int Mech_VoidObliteratorChargeNoStab = EODMask | ++_eodCount;
    public static readonly int Mech_VoidGoliathGlacialSlam = EODMask | ++_eodCount;
    public static readonly int Mech_VoidGoliathGlacialSlamNoStab = EODMask | ++_eodCount;
    public static readonly int Mech_GraspOfTheVoid = EODMask | ++_eodCount;
    #endregion EOD

    #region SOTO
    private static int _sotoCount = 0;
    public static readonly int Mech_DancedStarsLost = SotOMask | ++_sotoCount;
    public static readonly int Mech_DancedStarsKept = SotOMask | ++_sotoCount;
    public static readonly int Mech_SpinningNebula = SotOMask | ++_sotoCount;
    public static readonly int Mech_SpinningNebulaCast = SotOMask | ++_sotoCount;
    public static readonly int Mech_DemonicBlast = SotOMask | ++_sotoCount;
    public static readonly int Mech_SoulFeast = SotOMask | ++_sotoCount;
    public static readonly int Mech_SoulFeastTarget = SotOMask | ++_sotoCount;
    public static readonly int Mech_PlanetCrash = SotOMask | ++_sotoCount;
    public static readonly int Mech_PlanetCrashCast = SotOMask | ++_sotoCount;
    public static readonly int Mech_PlanetCrashCastStop = SotOMask | ++_sotoCount;
    public static readonly int Mech_PlanetCrashCastDone = SotOMask | ++_sotoCount;
    public static readonly int Mech_ChargingConstellation = SotOMask | ++_sotoCount;
    public static readonly int Mech_ShootingStarsTarget = SotOMask | ++_sotoCount;
    public static readonly int Mech_ShootingStarsCast = SotOMask | ++_sotoCount;
    public static readonly int Mech_ResidualAnxiety = SotOMask | ++_sotoCount;
    public static readonly int Mech_COLostControl = SotOMask | ++_sotoCount;
    public static readonly int Mech_COSharedDestructionTarget = SotOMask | ++_sotoCount;
    public static readonly int Mech_COTargetOrder = SotOMask | ++_sotoCount;
    public static readonly int Mech_COExtremeVulnerability = SotOMask | ++_sotoCount;
    public static readonly int Mech_DemonicAuraRemove = SotOMask | ++_sotoCount;
    public static readonly int Mech_DemonicAuraLost = SotOMask | ++_sotoCount;
    public static readonly int Mech_PurifyingLightCast = SotOMask | ++_sotoCount;
    public static readonly int Mech_PurifyingLightHitSoulFeast = SotOMask | ++_sotoCount;
    public static readonly int Mech_PurifyingLightHitDagda = SotOMask | ++_sotoCount;
    public static readonly int Mech_DemonicFeverTarget = SotOMask | ++_sotoCount;
    public static readonly int Mech_Insatiable = SotOMask | ++_sotoCount;
    public static readonly int Mech_InsatiableCast = SotOMask | ++_sotoCount;
    public static readonly int Mech_CrushingRegret = SotOMask | ++_sotoCount;
    public static readonly int Mech_CrushingRegretEmpowered = SotOMask | ++_sotoCount;
    public static readonly int Mech_CrushingRegretApply = SotOMask | ++_sotoCount;
    public static readonly int Mech_CrushingRegretCast= SotOMask | ++_sotoCount;
    public static readonly int Mech_CrushingRegretSuccess = SotOMask | ++_sotoCount;
    public static readonly int Mech_CrushingRegretFail = SotOMask | ++_sotoCount;
    public static readonly int Mech_WallOfDespair = SotOMask | ++_sotoCount;
    public static readonly int Mech_WallOfDespairEmpowered = SotOMask | ++_sotoCount;
    public static readonly int Mech_WallOfDespairCast = SotOMask | ++_sotoCount;
    public static readonly int Mech_PoolOfDespair = SotOMask | ++_sotoCount;
    public static readonly int Mech_PoolOfDespairEmpowered = SotOMask | ++_sotoCount;
    public static readonly int Mech_EnviousGaze = SotOMask | ++_sotoCount;
    public static readonly int Mech_EnviousGazeEmpowered = SotOMask | ++_sotoCount;
    public static readonly int Mech_EnviousGazeStrip = SotOMask | ++_sotoCount;
    public static readonly int Mech_EnviousGazeCast = SotOMask | ++_sotoCount;
    public static readonly int Mech_MaliciousIntent = SotOMask | ++_sotoCount;
    public static readonly int Mech_MaliciousIntentTarget = SotOMask | ++_sotoCount;
    public static readonly int Mech_MaliciousIntentCast = SotOMask | ++_sotoCount;
    public static readonly int Mech_CryOfRage = SotOMask | ++_sotoCount;
    public static readonly int Mech_CryOfRageEmpowered = SotOMask | ++_sotoCount;
    public static readonly int Mech_CryOfRageCast = SotOMask | ++_sotoCount;
    public static readonly int Mech_EnragedSmash = SotOMask | ++_sotoCount;
    public static readonly int Mech_EnragedSmashDown = SotOMask | ++_sotoCount;
    public static readonly int Mech_EnragedSmashCast = SotOMask | ++_sotoCount;
    public static readonly int Mech_Petrify = SotOMask | ++_sotoCount;
    public static readonly int Mech_PetrifyCast = SotOMask | ++_sotoCount;
    public static readonly int Mech_PetrifyHeal = SotOMask | ++_sotoCount;
    public static readonly int Mech_UnboundedOptimismLost = SotOMask | ++_sotoCount;
    public static readonly int Mech_UnboundedOptimismKept = SotOMask | ++_sotoCount;
    public static readonly int Mech_EmpoweredCerus = SotOMask | ++_sotoCount;
    public static readonly int Mech_EmpoweredDespairCerus = SotOMask | ++_sotoCount;
    public static readonly int Mech_EmpoweredEnvyCerus = SotOMask | ++_sotoCount;
    public static readonly int Mech_EmpoweredGluttonyCerus = SotOMask | ++_sotoCount;
    public static readonly int Mech_EmpoweredMaliceCerus = SotOMask | ++_sotoCount;
    public static readonly int Mech_EmpoweredRageCerus = SotOMask | ++_sotoCount;
    public static readonly int Mech_EmpoweredRegretCerus = SotOMask | ++_sotoCount;
    public static readonly int Mech_DespairKilled = SotOMask | ++_sotoCount;
    public static readonly int Mech_EnvyKilled = SotOMask | ++_sotoCount;
    public static readonly int Mech_GluttonyKilled = SotOMask | ++_sotoCount;
    public static readonly int Mech_MaliceKilled = SotOMask | ++_sotoCount;
    public static readonly int Mech_RageKilled = SotOMask | ++_sotoCount;
    public static readonly int Mech_RegretKilled = SotOMask | ++_sotoCount;
    public static readonly int Mech_EmpoweredDespairKilled = SotOMask | ++_sotoCount;
    public static readonly int Mech_EmpoweredEnvyKilled = SotOMask | ++_sotoCount;
    public static readonly int Mech_EmpoweredGluttonyKilled = SotOMask | ++_sotoCount;
    public static readonly int Mech_EmpoweredMaliceKilled = SotOMask | ++_sotoCount;
    public static readonly int Mech_EmpoweredRageKilled = SotOMask | ++_sotoCount;
    public static readonly int Mech_EmpoweredRegretKilled = SotOMask | ++_sotoCount;
    #endregion SOTO

    #region VOE
    private static int _voeCount = 0;
    public static readonly int Mech_KelaStomp = VoEMask | ++_voeCount;
    public static readonly int Mech_KelaStompCC = VoEMask | ++_voeCount;
    public static readonly int Mech_KelaClawSlam = VoEMask | ++_voeCount;
    public static readonly int Mech_KelaClawSlamCC = VoEMask | ++_voeCount;
    public static readonly int Mech_KelaLightningStrike = VoEMask | ++_voeCount;
    public static readonly int Mech_KelaLightningStrikeCC = VoEMask | ++_voeCount;
    public static readonly int Mech_KelaCrocTackle = VoEMask | ++_voeCount;
    public static readonly int Mech_KelaCrocTackleCC = VoEMask | ++_voeCount;
    public static readonly int Mech_KelaTornado = VoEMask | ++_voeCount;
    public static readonly int Mech_KelaTornadoCC = VoEMask | ++_voeCount;
    public static readonly int Mech_KelaAmbush = VoEMask | ++_voeCount;
    public static readonly int Mech_KelaTantrum = VoEMask | ++_voeCount;
    public static readonly int Mech_ScaldingWave = VoEMask | ++_voeCount;
    public static readonly int Mech_KelaFixated = VoEMask | ++_voeCount;
    public static readonly int Mech_Hunted = VoEMask | ++_voeCount;
    public static readonly int Mech_ShreddedArmor = VoEMask | ++_voeCount;
    public static readonly int Mech_LooseSand = VoEMask | ++_voeCount;
    public static readonly int Mech_AchSurefootedLost = VoEMask | ++_voeCount;
    public static readonly int Mech_AchSurefootedKept = VoEMask | ++_voeCount;
    public static readonly int Mech_BittingSwarm = VoEMask | ++_voeCount;
    public static readonly int Mech_BittingSwarmFirst = VoEMask | ++_voeCount;
    public static readonly int Mech_BittingSwarmContaminated = VoEMask | ++_voeCount;
    public static readonly int Mech_RelentlessSpeed = VoEMask | ++_voeCount;
    public static readonly int Mech_AteCroc = VoEMask | ++_voeCount;
    public static readonly int Mech_AteArtifact = VoEMask | ++_voeCount;
    public static readonly int Mech_AtePlayer = VoEMask | ++_voeCount;
    public static readonly int Mech_PlayerEaten = VoEMask | ++_voeCount;
    public static readonly int Mech_TankEaten = VoEMask | ++_voeCount;
    #endregion VOE

    #endregion RAID ENCOUNTERS

    #region FRACTALS
    private const int NightmareMask = FractalMask | 0x00010000;
    private const int ShatteredObservatoryMask = FractalMask | 0x00020000;
    private const int SunquaPeakMask = FractalMask | 0x00030000;
    private const int SilentSurfMask = FractalMask | 0x00040000;
    private const int LonelyTowerMask = FractalMask | 0x00050000;
    private const int KinfallMask = FractalMask | 0x00060000;

    private static int _fractalCount = 0;
    public static readonly int Mech_FluxBombBuff = FractalMask | ++_fractalCount;
    public static readonly int Mech_FluxBombHit = FractalMask | ++_fractalCount;
    public static readonly int Mech_FractalVindicator = FractalMask | ++_fractalCount;
    public static readonly int Mech_ToxicSicknessReceived = FractalMask | ++_fractalCount;
    public static readonly int Mech_ToxicSicknessApplied = FractalMask | ++_fractalCount;
    public static readonly int Mech_ToxicSicknessHitOther = FractalMask | ++_fractalCount;
    public static readonly int Mech_ToxicSicknessHitByOther = FractalMask | ++_fractalCount;
    #region KINFALL
    private static int _kinfallCount = 0;
    public static readonly int Mech_DeathlyRime = KinfallMask | ++_kinfallCount;
    public static readonly int Mech_LifeFireApply = KinfallMask | ++_kinfallCount;
    public static readonly int Mech_LifeFireRemove = KinfallMask | ++_kinfallCount;
    public static readonly int Mech_VitreousSpiketHit = KinfallMask | ++_kinfallCount;
    public static readonly int Mech_FaillingIceHit = KinfallMask | ++_kinfallCount;
    public static readonly int Mech_FrozenTeethHit = KinfallMask | ++_kinfallCount;
    public static readonly int Mech_LoftedCryOfFlashHit = KinfallMask | ++_kinfallCount;
    public static readonly int Mech_TerrestrialCryOfFlashHit = KinfallMask | ++_kinfallCount;
    public static readonly int Mech_GorefrostTarget = KinfallMask | ++_kinfallCount;
    public static readonly int Mech_GorefrostHit = KinfallMask | ++_kinfallCount;
    public static readonly int Mech_FreezingFanHit = KinfallMask | ++_kinfallCount;
    public static readonly int Mech_LethalCoalescenceApply = KinfallMask | ++_kinfallCount;
    public static readonly int Mech_WintryOrbHit = KinfallMask | ++_kinfallCount;
    public static readonly int Mech_HailstormWhisperingShadowHit = KinfallMask | ++_kinfallCount;
    public static readonly int Mech_EmpoweredWhsiperingShadow = KinfallMask | ++_kinfallCount;
    public static readonly int Mech_ShatterstepLost = KinfallMask | ++_kinfallCount;
    public static readonly int Mech_ShatterstepKept = KinfallMask | ++_kinfallCount;
    #endregion KINFALL
    #region NIGHTMARE
    private static int _nightmareCount = 0;
    public static readonly int Mech_CascadeOfTorment = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_BlastWave = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_TantrumMAMA = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_LeapMAMA = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_ToxicShoot = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_KnightJump = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_SweepingStrikes = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_MiasmaMAMA = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_GrenadeBarrage = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_GrenadeBarrageReflected = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_BulletsMAMA = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_Extraction = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_HomingGrenades = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_KnightsGaze = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_NightmareDevastationMAMA = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_VileSpit = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_TailLashSiax = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_HallucinationSpawnedSiax = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_CausticExplosionSiaxCastStart = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_CausticExplosionSiaxCastEnd = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_CausticExplosionSiaxBreakbarStart = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_CausticExplosionSiaxBreakbarEnd = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_HallucinationSiaxFixated = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_LungeNightmare = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_UpswingEnsolyss = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_UpswingHallucinationNightmare = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_MiasmaEnsolyss = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_CausticExplosionEnsolyssCastStart = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_CausticExplosionEnsolyssCastEndFail = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_CausticExplosionEnsolyssCastEndSuccess = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_CausticExplosionEnsolyssHit = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_NightmareDevastationEnsolyss = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_TailLashEnsolyss = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_RampageEnsolyss = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_CausticGraspEnsolyss = NightmareMask | ++_nightmareCount;
    public static readonly int Mech_TormentingBlastEnsolyss = NightmareMask | ++_nightmareCount;
    #endregion NIGHTMARE
    #region SHATTERED OBSERVATORY
    private static int _shatteredCount = 0;
    public static readonly int Mech_FixatedBloom = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_HitByEye = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_CorporealReassingment = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_CombustionRush = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_PunishingKick = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_CranialCascade = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_RadiantFury = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_FocusedAnger = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_HorizonStrikeSkorvald = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_CrimsonDawn = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_SolarCyclone = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_SkorvaldsIre = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_BloomExplode = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_SpiralStrike = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_WaveOfMutilation = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_VaultArtsariiv = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_SlamArtsariiv = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_TeleportLunge = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_AstralSurge = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_RedMarble = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_TawShot = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_TawShotReflected = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_SparkSpawn = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_HorizonStrikeArkk = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_HorizonStrikeArkkNormal = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_SolarFury = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_SolarDischarge = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_SolarStomp = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_DiffractiveEdge = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_FocusedRage = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_StarburstCascade = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_OverheadSmashArkk = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_ExplodeArkk = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_CosmicMeteor = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_ArkkBreakbarStart = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_ArkkBreakbarFail = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_ArkkBreakbarSuccess = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_OverheadSmashArkkArchDiviner = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_RollingChaos = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_CosmicStreaks = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_WhirlingDevastation = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_PullArkkGladiatorStart = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_PullArkkGladiatorFail = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_PullArkkGladiatorSuccess = ShatteredObservatoryMask | ++_shatteredCount;
    public static readonly int Mech_SpinningCut = ShatteredObservatoryMask | ++_shatteredCount;
    #endregion SHATTERED OBSERVATORY
    #region SILENT SURF
    private static int _silentSurfCount = 0;
    public static readonly int Mech_RendingStorm = SilentSurfMask | ++_silentSurfCount;
    public static readonly int Mech_RendingStormTarget = SilentSurfMask | ++_silentSurfCount;
    public static readonly int Mech_Harrowshot = SilentSurfMask | ++_silentSurfCount;
    public static readonly int Mech_ExtremeVulnApply = SilentSurfMask | ++_silentSurfCount;
    public static readonly int Mech_DreadVisageDeath = SilentSurfMask | ++_silentSurfCount;
    public static readonly int Mech_FrighteningSpeedDeath = SilentSurfMask | ++_silentSurfCount;
    public static readonly int Mech_KanaxaiExposedPlayer = SilentSurfMask | ++_silentSurfCount;
    public static readonly int Mech_KanaxaiFear = SilentSurfMask | ++_silentSurfCount;
    public static readonly int Mech_Phantasmagoria = SilentSurfMask | ++_silentSurfCount;
    public static readonly int Mech_KanaxaiExposed = SilentSurfMask | ++_silentSurfCount;
    #endregion SILENT SURF
    #region LONELY TOWER
    private static int _lonelyTowerCount = 0;
    public static readonly int Mech_DespairAttunement = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_EnvyAttunement = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_GluttonyAttunement = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_MaliceAttunement = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_RageAttunement = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_RegretAttunement = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_DespairEmpowerment = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_EnvyEmpowerment = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_GluttonyEmpowerment = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_MaliceEmpowerment = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_RageEmpowerment = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_RegretEmpowerment = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_RainOfDespair = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_WaveOfEnvy = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_Inhale = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_SpikeOfMalice = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_RageFissure = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_Consumed = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_CruelDetonation = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_WallOfTalons = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_PoolOfDraining = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_UnliddedEye = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_EyeOfJudgment = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_EparchBreakbar = LonelyTowerMask | ++_lonelyTowerCount;
    public static readonly int Mech_EparchRegret = LonelyTowerMask | ++_lonelyTowerCount;
    #endregion LONELY TOWER
    #region SUNQUA PEAK
    private static int _sunquaPeakCount = 0;
    public static readonly int Mech_ElementalWhirl = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_ElementalManipulationAir = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_FulgorSphere = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_VolatileWind = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_WindBurst = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_WindBurstNoStab = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_CallOfStorms = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_WhirlwindShield = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_ElementalManipulationFire = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_RoilingFlames = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_RoilingFlamesReflected = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_VolatileFire = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_CallMeteorSummon = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_CallMeteorHit = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_FlameBurst = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_AiFirestorm = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_ElementalManipulationWater = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_TorrentialBolt = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_TorrentialBoltReflected = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_VolatileWater = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_AquaticBurst = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_TidalBarrier = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_TidalBargain = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_TidalBargainDown = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_EmpathicManipulation = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_FocusedWrath = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_FocusedWrathReflected = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_NegativeBurst = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_Terrorstorm = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_CrushingGuilt = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_CrushingGuiltDown = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_FixatedByFear = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_EmpathicManipulationFear = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_EmpathicManipulationFearInterrupt = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_EmpathicManipulationSorrow = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_EmpathicManipulationSorrowInterrupt = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_EmpathicManipulationGuilt = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_EmpathicManipulationGuiltInterrupt = SunquaPeakMask | ++_sunquaPeakCount;
    public static readonly int Mech_CacophonousMind = SunquaPeakMask | ++_sunquaPeakCount;
    #endregion SUNQUA PEAK
    #endregion FRACTALS

    #region CONVERGENCE
    private const int OuterNayosConvMask = ConvergenceMask | 0x00010000;
    private const int MountBalriorConvMask = ConvergenceMask | 0x00020000;

    public static readonly int Mech_EssenceCollected = ConvergenceMask | 1;
    #endregion CONVERGENCE

    #region OPEN WORLD
    #region SOOWON
    private const int SooWonMask = OpenWorldMask | 0x00010000;

    private static int _sooWonCount = 0;
    public static readonly int Mech_SooWonSlam = SooWonMask | ++_sooWonCount;
    public static readonly int Mech_SooWonAcidPool = SooWonMask | ++_sooWonCount;
    public static readonly int Mech_SooWonClawSlap = SooWonMask | ++_sooWonCount;
    public static readonly int Mech_SooWonTailSlap = SooWonMask | ++_sooWonCount;
    public static readonly int Mech_SooWonBite = SooWonMask | ++_sooWonCount;
    public static readonly int Mech_SooWonWaveHalf = SooWonMask | ++_sooWonCount;
    public static readonly int Mech_SooWonWaveFull = SooWonMask | ++_sooWonCount;
    public static readonly int Mech_SooWonWisp = SooWonMask | ++_sooWonCount;
    public static readonly int Mech_SooWonGreenFailed = SooWonMask | ++_sooWonCount;
    public static readonly int Mech_SooWonBubble = SooWonMask | ++_sooWonCount;
    public static readonly int Mech_SooWonWhirlpool = SooWonMask | ++_sooWonCount;
    public static readonly int Mech_SooWonTailSpawn = SooWonMask | ++_sooWonCount;
    public static readonly int Mech_SooWonTailKilled = SooWonMask | ++_sooWonCount;
    public static readonly int Mech_SooWonTailDespawn = SooWonMask | ++_sooWonCount;
    public static readonly int Mech_SooWonSideSwap = SooWonMask | ++_sooWonCount;
    public static readonly int Mech_SooWonCC = SooWonMask | ++_sooWonCount;
    #endregion SOOWON
    #endregion OPEN WORLD

    #region WVW
    private const int EternalBattlegroundsMask = WvWMask | 0x00010000;
    private const int GreenAlpineBorderlandsMask = WvWMask | 0x00020000;
    private const int BlueAlpineBorderlandsMask = WvWMask | 0x00030000;
    private const int RedDesertBorderlandsMask = WvWMask | 0x00040000;
    private const int ObsidianSanctumMask = WvWMask | 0x00050000;
    private const int EdgeOfTheMistsMask = WvWMask | 0x00060000;
    private const int ArmisticeBastionMask = WvWMask | 0x00070000;
    private const int GildedHollowMask = WvWMask | 0x00080000;
    private const int LostPrecipiceMask = WvWMask | 0x00090000;
    private const int WindsweptHavenMask = WvWMask | 0x000A0000;
    private const int IsleOfReflectionMask = WvWMask | 0x000B0000;

    private static int _wvwCount = 0;
    public static readonly int Mech_KillingBlowPlayer = WvWMask | ++_wvwCount;
    public static readonly int Mech_KillingBlowEnemy = WvWMask | ++_wvwCount;
    #endregion WVW
}
