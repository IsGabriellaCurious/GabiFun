using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GabiFun.HarmonyPatches
{
    [HarmonyPatch(typeof(IntroCutscene.CoBegin__d), nameof(IntroCutscene.CoBegin__d.MoveNext))]
    public static class IntroCutScenePatch
    {
        public static void Postfix(IntroCutscene.CoBegin__d __instance)
        {
            if (__instance.isImpostor)
            {
                __instance.__this.Title.Text = "whore";
                __instance.__this.Title.Color = GabiFun.impostorColour;
                __instance.__this.BackgroundBar.material.color = GabiFun.impostorColour;
            }
            else
            {
                __instance.__this.Title.Text = "bruh";
                __instance.__this.Title.Color = GabiFun.crewColour;
                __instance.__this.ImpostorText.Text = "theres [ff5ccbff]" + GabiFun.impostors.Count + " [ffffffff]whore" + (GabiFun.impostors.Count != 1 ? "s" : "") + " in this house";
                __instance.__this.BackgroundBar.material.color = GabiFun.crewColour;
            }
        }
    }
}
