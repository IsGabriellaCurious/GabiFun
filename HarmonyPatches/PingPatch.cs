using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace GabiFun.HarmonyPatches
{
    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public static class PingPatch
    {
        public static void Postfix(PingTracker __instance)
        {
            __instance.text.Text = GabiFun.selectedRandomText;
        }
    }
}
