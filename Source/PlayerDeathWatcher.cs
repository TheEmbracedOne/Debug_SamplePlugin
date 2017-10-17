using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamplePlugin
{
    public static class PlayerDeathWatcher
    {
        private static bool playerDead;

        public static void Reset()
        {
            playerDead = false;
        }

        public static bool PlayerDied()
        {
            return (!playerDead && PlayerData.instance.health <= 0 && GUIController.gm.IsGameplayScene());
        }

        public static void LogDeathDetails()
        {
            playerDead = true;
            Console.AddLine(string.Concat(new string[]
            {
                "Hero death detected. Game playtime: ",
                PlayerData.instance.playTime.ToString(),
                " Shade Zone: ",
                PlayerData.instance.shadeMapZone.ToString(),
                " Shade Geo: ",
                PlayerData.instance.geoPool.ToString(),
                " Respawn scene: ",
                PlayerData.instance.respawnScene.ToString()
            }));
        }
    }
}
