﻿using System.Text.RegularExpressions;

namespace GW2EIBuilders;

public class HTMLAssets
{
    internal readonly string EIJavascriptCode;

    internal readonly string EICRJavascriptCode;
    internal readonly string EIHealingExtJavascriptCode;
    private static readonly string[] separatorTemplate = ["<template>"];
    private static readonly string[] separatorScript = ["<script>"];
    private static readonly string[] separatorTemplateEnd = ["</template>"];
    private static readonly string[] separatorScriptENd = ["</script>"];

    public HTMLAssets()
    {
        var orderedScripts = new List<string>()
        {
            Properties.Resources.global,
            Properties.Resources.mixins,
            Properties.Resources.functions,
            Properties.Resources.main_js,
        };
        string scriptContent = orderedScripts[0];
        for (int i = 1; i < orderedScripts.Count; i++)
        {
            scriptContent += orderedScripts[i];
        }
        List<string> templates = BuildTemplates();
        EIJavascriptCode = scriptContent.Replace("TEMPLATE_COMPILE", string.Join("\n", templates));
        //
        var orderedCRScripts = new List<string>()
        {
            Properties.Resources.animator,
            Properties.Resources.actors,
            Properties.Resources.decorations,
        };
        string scriptCRContent = orderedCRScripts[0];
        for (int i = 1; i < orderedCRScripts.Count; i++)
        {
            scriptCRContent += orderedCRScripts[i];
        }
        List<string> templatesCR = BuildCRTemplates();
        EICRJavascriptCode = scriptCRContent.Replace("TEMPLATE_CR_COMPILE", string.Join("\n", templatesCR));
        //
        var orderedHealingExtScripts = new List<string>()
        {
            Properties.Resources.healingExtGlobals,
            Properties.Resources.healingExtFunctions,
        };
        string scriptHealingExtContent = orderedHealingExtScripts[0];
        for (int i = 1; i < orderedHealingExtScripts.Count; i++)
        {
            scriptHealingExtContent += orderedHealingExtScripts[i];
        }
        List<string> templateHealingExt = BuildHealingExtensionTemplates();
        EIHealingExtJavascriptCode = scriptHealingExtContent.Replace("TEMPLATE_HEALING_EXT_COMPILE", string.Join("\n", templateHealingExt));
    }
    private static string PrepareTemplate(string template)
    {
        if (!template.Contains("<template>") || !template.Contains("<script>") || !template.Contains("${template}"))
        {
            throw new InvalidDataException("Not a template");
        }
        string html = template.Split(separatorTemplate, StringSplitOptions.None)[1].Split(separatorTemplateEnd, StringSplitOptions.None)[0];
        string js = template.Split(separatorScript, StringSplitOptions.None)[1].Split(separatorScriptENd, StringSplitOptions.None)[0];
        js = js.Replace("${template}", Regex.Replace(html, @"\t|\n|\r", ""));
        js = "{" + js + "}";
        return js;
    }

    private static List<string> BuildTemplates()
    {
        var templates = new List<string>
        {
            Properties.Resources.tmplGraph,
            //
            Properties.Resources.tmplBuffStats,
            Properties.Resources.tmplBuffStatsPlayer,
            Properties.Resources.tmplBuffStatsTarget,
            Properties.Resources.tmplBuffTable,
            Properties.Resources.tmplBuffTables,
            Properties.Resources.tmplGearBuffTable,
            Properties.Resources.tmplNourishmentBuffTable,
            Properties.Resources.tmplEnhancementBuffTable,
            Properties.Resources.tmplOtherConsumableBuffTable,
            Properties.Resources.tmplDebuffTable,
            Properties.Resources.tmplConditionsTable,
            Properties.Resources.tmplPersonalBuffTable,
            //
            Properties.Resources.tmplBuffVolumeStats,
            Properties.Resources.tmplBuffVolumeStatsPlayer,
            Properties.Resources.tmplBuffVolumeStatsTarget,
            Properties.Resources.tmplBuffVolumeTable,
            Properties.Resources.tmplBuffVolumeTables,
            Properties.Resources.tmplGearBuffVolumeTable,
            Properties.Resources.tmplDebuffVolumeTable,
            Properties.Resources.tmplConditionsVolumeTable,
            Properties.Resources.tmplPersonalBuffVolumeTable,
            //
            Properties.Resources.tmplDamageDistPlayer,
            Properties.Resources.tmplDamageTakenDistPlayer,
            Properties.Resources.tmplDamageDistTable,
            Properties.Resources.tmplDamageDistTarget,
            Properties.Resources.tmplDamageTakenDistTarget,
            Properties.Resources.tmplDamageModifierTable,
            Properties.Resources.tmplDamageModifierStatsContainer,
            Properties.Resources.tmplDamageModifierStats,
            Properties.Resources.tmplDamageModifierPersStats,
            //
            Properties.Resources.tmplDamageTable,
            Properties.Resources.tmplDeathRecap,
            Properties.Resources.tmplDefenseTable,
            Properties.Resources.tmplEncounter,
            Properties.Resources.tmplFood,
            Properties.Resources.tmplGameplayTable,
            Properties.Resources.tmplOffensiveTable,
            Properties.Resources.tmplBuffTab,
            Properties.Resources.tmplStatTables,
            Properties.Resources.tmplMechanicsStats,
            Properties.Resources.tmplMechanicsTable,
            Properties.Resources.tmplPhase,
            Properties.Resources.tmplPlayers,
            Properties.Resources.tmplPlayerStats,
            Properties.Resources.tmplPlayerTab,
            Properties.Resources.tmplSimpleRotation,
            Properties.Resources.tmplAdvancedRotation,
            Properties.Resources.tmplSupportTable,
            Properties.Resources.tmplTargets,
            Properties.Resources.tmplTargetStats,
            Properties.Resources.tmplTargetTab,
            Properties.Resources.tmplDPSGraph,
            Properties.Resources.tmplDPSGraphModeSelector,
            Properties.Resources.tmplGraphStats,
            Properties.Resources.tmplPlayerBuffTab,
            Properties.Resources.tmplPlayerTabGraph,
            Properties.Resources.tmplPlayersRotation,
            Properties.Resources.tmplPlayersRotationTab,
            Properties.Resources.tmplRotationLegend,
            Properties.Resources.tmplTargetBuffTab,
            Properties.Resources.tmplTargetTabGraph,
            Properties.Resources.tmplTargetTabPerPlayerGraph,
            Properties.Resources.tmplTargetData,
            Properties.Resources.tmplMainView,
        };
        var res = new List<string>();
        foreach (string template in templates)
        {
            res.Add(PrepareTemplate(template));
        }
        return res;
    }

    private static List<string> BuildCRTemplates()
    {
        var templates = new List<string>
        {
            Properties.Resources.tmplCombatReplayDamageData,
            Properties.Resources.tmplCombatReplayStatusData,
            Properties.Resources.tmplCombatReplayDamageTable,
            Properties.Resources.tmplCombatReplayActorBuffStats,
            Properties.Resources.tmplCombatReplayPlayerStats,
            Properties.Resources.tmplCombatReplayPlayerStatus,
            Properties.Resources.tmplCombatReplayActorRotation,
            Properties.Resources.tmplCombatReplayTargetStats,
            Properties.Resources.tmplCombatReplayTargetStatus,
            Properties.Resources.tmplCombatReplayTargetsStats,
            Properties.Resources.tmplCombatReplayPlayersStats,
            Properties.Resources.tmplCombatReplayUI,
            Properties.Resources.tmplCombatReplayPlayerSelect,
            Properties.Resources.tmplCombatReplayTargetSelect,
            Properties.Resources.tmplCombatReplayExtraDecorations,
            Properties.Resources.tmplCombatReplayAnimationControl,
            Properties.Resources.tmplCombatReplayMechanicsList
        };
        var res = new List<string>();
        foreach (string template in templates)
        {
            res.Add(PrepareTemplate(template));
        }
        return res;
    }

    private static List<string> BuildHealingExtensionTemplates()
    {
        var templates = new List<string>
        {
            Properties.Resources.tmplHealingExtensionView,
            Properties.Resources.tmplTargetPlayers,
            Properties.Resources.tmplIncomingHealingTable,
            Properties.Resources.tmplHealingStatTables,
            Properties.Resources.tmplOutgoingHealingTable,
            Properties.Resources.tmplHPSGraphModeSelector,
            Properties.Resources.tmplHPSGraph,
            Properties.Resources.tmplHealingGraphStats,
            Properties.Resources.tmplHealingDistPlayer,
            Properties.Resources.tmplHealingDistTable,
            Properties.Resources.tmplHealingTakenDistPlayer,
            Properties.Resources.tmplPlayerHealingStats,
            Properties.Resources.tmplPlayerHealingTab,
            Properties.Resources.tmplPlayerHealingTabGraph,
            Properties.Resources.tmplBarrierDistPlayer,
            Properties.Resources.tmplBarrierDistTable,
            Properties.Resources.tmplBarrierTakenDistPlayer
        };
        var res = new List<string>();
        foreach (string template in templates)
        {
            res.Add(PrepareTemplate(template));
        }
        return res;
    }
}
