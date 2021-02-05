using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GabiFun.HarmonyPatches
{
    public static class AudioPatch
    {
        [HarmonyPatch(typeof(SoundManager), nameof(SoundManager.PlaySound))]
        public static class ReactorSoundPatch
        {
            public static void Postfix(AudioClip HJPONHPGBFE, bool APGACMFAAAH, float HFOMPLGMDGO)
            {
                if (HJPONHPGBFE == null)
                    return;

                if (HJPONHPGBFE == HudManager.Instance.SabotageSound)
                {
                    if (HudManager.Instance.FullScreen.enabled)
                    {
                        SoundManager.Instance.StopSound(HudManager.Instance.SabotageSound);
                        SoundManager.Instance.PlaySoundImmediate(HJPONHPGBFE, APGACMFAAAH, HFOMPLGMDGO, 0.5f);
                    }
                }
            }
        }
    }
}
