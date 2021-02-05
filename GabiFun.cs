using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using HarmonyLib;
using Reactor;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GabiFun
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public class GabiFun : BasePlugin
    {
        public const string Id = "moe.gabriella.gabifun";

        public Harmony Harmony { get; } = new Harmony(Id);

        public ConfigEntry<string> Name { get; private set; }

        public static ManualLogSource log;

        public static List<ImpostorData> impostors;
        public static bool displayImpostors;

        public static Color impostorColour;
        public static Color crewColour;

        public string[] randomText;
        public static string selectedRandomText;

        public override void Load()
        {
            log = base.Log;
            Harmony.PatchAll();

            displayImpostors = false;
            impostors = new List<ImpostorData>();

            impostorColour = new Color(1f, 0.36f, 0.8f, 1f);
            crewColour = new Color(0.36f, 0.8f, 1f, 1f);
            Palette.ImpostorRed = impostorColour;
            Palette.CrewmateBlue = crewColour;

            LoadRandomText();
            
            new Task(() =>
            {
                while (true)
                {
                    System.Random random = new System.Random();
                    selectedRandomText = randomText[random.Next(0, randomText.Length)];
                    Task.Delay(10000).Wait();
                }                
            }).Start();

            Log.LogInfo("Loaded GabiFun~!!");
        }

        private void LoadRandomText()
        {
            randomText = new string[]
            {
                "[5ccbffff]t[ff5ccbff]r[ffffffff]a[ff5ccbff]n[5ccbffff]s [ffffffff]rights!",
                "[ff5ccbff]how do i girl?",
                "[e3e63dff]if you sat in a puddle of your \n[e3e63dff]own piss for long enough\n[e3e63dff]would your legs disintergrate?",
                "[86e63dff]hi my name is chelease, what's \nyour favourite dinner food?",
                "[6d3de6ff]IS A KITTY!",
                "[e63d3dff]bruh.",
                "[b51717ff]time to DIE!",
                "[1db1e2ff]beat[e53838ff]saber [ffffffff]is fun!",
                "[385be5ff]moonlight [ffffffff]by [ce38e5ff]geoxor [ffffffff]is\na good song!",
                "[ff5ccbff]gabriella.moe",
                "[e59238ff]just win already!",
                "[b01717ff]bet you'll loose",
                "[196ebeff]i wanna sob",
                "[ff5ccbff]I NEED THE E!",
                "[ff5ccbff]still can't find the e...",
                "[e39426ff]gingers are cool, i guess",
                "[45d91cff]mmm, olive oil",
                "[e3ca26ff]suddenly, pineapples!",
                "[88e326ff]somebody come get her, shes \nhot WAIT THATS MY SISTER",
                "[e38b26ff]no homo ;)",
                "[a4e326ff]jesus fuck me sideways",
                "[45d91cff]WHEEEEEZE",
                "[45d91cff]PEANUTTTT",
                "[4b34e5ff]i'm so gay",
                "[4b34e5ff]imagine being straight...\ncould never",
                "[34cae5ff]facts!",
                "[e22222ff]personally i wouldn't take that",
		        "[ff5ccbff]things are about to get kinkey!",
                "[6d3de6ff]DADDY",
                "[e39426ff]iM fInE",
                "[6d3de6ff]sorry gab i love you really",
                "[e39426ff]school is so fucking shit",
                "[e39426ff]grrrr",
		        "[e39426ff]I HATE MUSICALS *PERIOD*"
            };
        }

        public static void UpdateImpostorColours()
        {
            if (displayImpostors)
            {
                foreach (ImpostorData impostor in impostors)
                {
                    impostor.playerControl.nameText.Color = impostorColour;
                }
            }
        }

        public static bool IsImpostor(int id)
        {
            foreach (ImpostorData impostor in impostors)
            {
                if (impostor.id == id)
                    return true;
            }
            return false;
        }

        public static bool IsImpostor(string name)
        {
            foreach (ImpostorData impostor in impostors)
            {
                if (impostor.name.Equals(name)) 
                    return true;
            }
            return false;
        }

        public static PlayerControl GetPlayerControl(string name)
        {
            foreach (PlayerControl p in PlayerControl.AllPlayerControls)
            {
                if (p.name.Equals(name))
                    return p;
            }
            return null;
        }
    }
}
