using System.Collections.Generic;
using UnityEngine;

namespace SamplePlugin
{
    public static class BossHandler
    {
        public static bool BossSub;

        public static Dictionary<string, KeyValuePair<bool, string>> BossData;
        public static Dictionary<string, string> GhostData;
        public static bool BossFound;
        public static bool GhostFound;

        public static void LookForBoss(string sceneName)
        {
            BossFound = false;
            GhostFound = false;
            if (BossData != null && BossData.ContainsKey(sceneName))
            {
                Console.AddLine("Found stored Boss in this scene, respawn available");
                BossFound = true;
            }
            if (GhostData == null || !GhostData.ContainsKey(sceneName)) return;
            Console.AddLine("Found stored Ghost Boss in this scene, respawn available");
            GhostFound = true;
        }

        public static void PopulateBossLists()
        {
            if (BossData == null)
            {
                BossData = new Dictionary<string, KeyValuePair<bool, string>>(16);
            }
            if (GhostData == null)
            {
                GhostData = new Dictionary<string, string>(7);
            }
            BossData.Clear();
            GhostData.Clear();
            BossData.Add("Ruins2_03", new KeyValuePair<bool, string>(true, "Battle Control"));
            BossData.Add("Crossroads_09", new KeyValuePair<bool, string>(true, "Battle Scene"));
            BossData.Add("Crossroads_04", new KeyValuePair<bool, string>(true, "Battle Scene"));
            BossData.Add("Fungus1_04", new KeyValuePair<bool, string>(false, "hornet1Defeated"));
            BossData.Add("Crossroads_10", new KeyValuePair<bool, string>(true, "Battle Scene"));
            BossData.Add("Fungus3_archive_02", new KeyValuePair<bool, string>(false, "defeatedMegaJelly"));
            BossData.Add("Fungus2_15", new KeyValuePair<bool, string>(false, "defeatedMantisLords"));
            BossData.Add("Waterways_12", new KeyValuePair<bool, string>(false, "flukeMotherDefeated"));
            BossData.Add("Waterways_05", new KeyValuePair<bool, string>(false, "defeatedDungDefender"));
            BossData.Add("Ruins1_24", new KeyValuePair<bool, string>(false, "mageLordDefeated"));
            BossData.Add("Deepnest_32", new KeyValuePair<bool, string>(true, "Battle Scene"));
            BossData.Add("Mines_18", new KeyValuePair<bool, string>(true, "Battle Scene"));
            BossData.Add("Mines_32", new KeyValuePair<bool, string>(true, "Battle Scene"));
            BossData.Add("Fungus3_23", new KeyValuePair<bool, string>(true, "Battle Scene"));
            BossData.Add("Ruins2_11", new KeyValuePair<bool, string>(true, "Battle Scene"));
            BossData.Add("Deepnest_East_Hornet", new KeyValuePair<bool, string>(false, "hornetOutskirtsDefeated"));
            GhostData.Add("RestingGrounds_02", "xeroDefeated");
            GhostData.Add("Fungus1_35", "noEyesDefeated");
            GhostData.Add("Fungus2_32", "elderHuDefeated");
            GhostData.Add("Deepnest_East_10", "markothDefeated");
            GhostData.Add("Deepnest_40", "galienDefeated");
            GhostData.Add("Fungus3_40", "mumCaterpillarDefeated");
            GhostData.Add("Cliffs_02", "aladarSlugDefeated");
        }

        public static void RespawnBoss()
        {
            if (BossFound)
            {
                if (BossData[GUIController.GetSceneName()].Key)
                {
                    var components = GameObject.Find(BossData[GUIController.GetSceneName()].Value).GetComponents<PlayMakerFSM>();
                    if (components != null)
                    {
                        foreach (var playMakerFsm in components)
                        {
                            if (playMakerFsm.FsmVariables.GetFsmBool("Activated") == null) continue;
                            playMakerFsm.FsmVariables.GetFsmBool("Activated").Value = false;
                            Console.AddLine("Boss control for this scene was reset, re-enter scene or warp");
                        }
                    }
                    else
                    {
                        Console.AddLine("GO does not exist or no FSM on it");
                    }
                }
                else
                {
                    PlayerData.instance.GetType().GetField(BossData[GUIController.GetSceneName()].Value).SetValue(PlayerData.instance, false);
                    Console.AddLine("Boss control for this scene was reset, re-enter scene or warp");
                }
            }
            else
            {
                Console.AddLine("No boss in this scene to respawn");
            }
        }

        public static void RespawnGhost()
        {
            if (GhostFound)
            {
                PlayerData.instance.GetType().GetField(GhostData[GUIController.GetSceneName()]).SetValue(PlayerData.instance, 0);
                Console.AddLine("Ghost Boss for this scene was reset, re-enter scene or warp");
            }
            else
            {
                Console.AddLine("No ghost in this scene to respawn");
            }
        }
    }
}
