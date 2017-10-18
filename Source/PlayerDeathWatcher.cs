namespace SamplePlugin
{
    public static class PlayerDeathWatcher
    {
        private static bool _playerDead;

        public static void Reset()
        {
            _playerDead = false;
        }

        public static bool PlayerDied()
        {
            return (!_playerDead && PlayerData.instance.health <= 0 && GUIController.Gm.IsGameplayScene());
        }

        public static void LogDeathDetails()
        {
            _playerDead = true;
            Console.AddLine(string.Concat("Hero death detected. Game playtime: ", PlayerData.instance.playTime.ToString(), " Shade Zone: ", PlayerData.instance.shadeMapZone, " Shade Geo: ", PlayerData.instance.geoPool.ToString(), " Respawn scene: ", PlayerData.instance.respawnScene));
        }
    }
}
