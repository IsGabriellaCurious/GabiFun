using HarmonyLib;
using System;
using System.Collections;
using System.Text;
using UnityEngine;

namespace GabiFun.HarmonyPatches
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.ShowTaskComplete))]
    public static class TaskCompletePatch
    {
        public static void Postfix(HudManager __instance)
        {
            Reactor.Coroutines.Start(Animate(__instance));
        }

        public static IEnumerator Animate(HudManager hm)
        {
            while (hm.TaskCompleteOverlay.gameObject.active)
            {
                hm.TaskCompleteOverlay.Rotate(0, 0, 30);
                yield return null;
            }
            yield break;
        }
    }
}
