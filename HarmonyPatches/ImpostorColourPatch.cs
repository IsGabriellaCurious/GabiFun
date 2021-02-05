using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnhollowerBaseLib;
using UnityEngine;

namespace GabiFun.HarmonyPatches
{
    public static class ImpostorColourPatch
    {
        [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
        public static class HudPatch
        {
            public static void Postfix(HudManager __instance)
            {
                if (GabiFun.impostors.Count > 0)
                {
                    GabiFun.UpdateImpostorColours();
                }
            }
        }

        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.SetInfected))]
        public static class SetInfectedPatch
        {
            public static void Postfix(Il2CppStructArray<byte> JPGEIBIBJPJ)
            {
                GabiFun.impostors.Clear();
                GabiFun.displayImpostors = false;
                PlayerControl player = PlayerControl.LocalPlayer;
                foreach (byte b in JPGEIBIBJPJ)
                {
                    if (player.PlayerId == b)
                    {
                        GabiFun.displayImpostors = true;
                        break;
                    }
                }

                foreach (PlayerControl p in PlayerControl.AllPlayerControls)
                {
                    if (JPGEIBIBJPJ.Contains(p.PlayerId))
                    {
                        ImpostorData data = new ImpostorData();
                        data.playerControl = p;
                        data.id = p.PlayerId;
                        data.name = p.name;
                        GabiFun.impostors.Add(data);
                    }
                }
                GabiFun.UpdateImpostorColours();
            }
        }

        [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnGameEnd))]
        public static class GameEndPatch
        {
            public static void Postfix(GameOverReason DOAMFOGADEF, bool EMAKAHIFLDE)
            {
                GabiFun.displayImpostors = false;
                GabiFun.impostors.Clear();
            }
        }

        [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.ExitGame))]
        public static class GameExitPatch
        {
            public static void Postfix(DisconnectReasons OECOPGMHMKC)
            {
                GabiFun.displayImpostors = false;
                GabiFun.impostors.Clear();
            }
        }

        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Update))]
        public static class MeetingHudPatch
        {
            public static void Postfix(MeetingHud __instance)
            {
                if (!GabiFun.displayImpostors)
                    return;

                foreach (PlayerVoteArea player in __instance.playerStates)
                {
                    if (GabiFun.IsImpostor(player.TargetPlayerId))
                    {
                        player.NameText.Color = GabiFun.impostorColour;
                    }
                }
            }
        }

        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.MurderPlayer))]
        public static class PlayerDeathPatch
        {
            public static void Postfix(PlayerControl __instance, PlayerControl CAKODNGLPDF) // __instance = killer, CAKODNGLPDF = player
            {
                if (CAKODNGLPDF == null || __instance == null)
                    return;

                if (CAKODNGLPDF.PlayerId == PlayerControl.LocalPlayer.PlayerId) // Player is dead
                {
                    GabiFun.displayImpostors = true;
                    GabiFun.UpdateImpostorColours();
                }
            }
        }

        [HarmonyPatch(typeof(ExileController), nameof(ExileController.Begin))]
        public static class PlayerExiledPatch
        {
            public static void Postfix(GameData.PlayerInfo LNMDIKCFBAK, bool BBJAEDLJIED)
            {
                if (LNMDIKCFBAK == null)
                    return;

                if (LNMDIKCFBAK.PlayerId == PlayerControl.LocalPlayer.PlayerId)
                {
                    GabiFun.displayImpostors = true;
                    GabiFun.UpdateImpostorColours();
                }
            }
        }
    }
}
