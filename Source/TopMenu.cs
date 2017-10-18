using System;
using UnityEngine;

namespace SamplePlugin
{
    public static class TopMenu
    {
        private static CanvasPanel _panel;

        public static bool Visible;

        public static void BuildMenu(GameObject canvas)
        {
            _panel = new CanvasPanel(canvas, GUIController.Instance.GetImage("ButtonsMenuBG"), new Vector2(1092f, 25f), Vector2.zero, new Rect(0f, 0f, GUIController.Instance.GetImage("ButtonsMenuBG").width, GUIController.Instance.GetImage("ButtonsMenuBG").height));

            var buttonRect = new Rect(0, 0, GUIController.Instance.GetImage("ButtonRect").width, GUIController.Instance.GetImage("ButtonRect").height);
            
            //Main buttons
            _panel.AddButton("Hide Menu", GUIController.Instance.GetImage("ButtonRect"), new Vector2(46f, 28f), Vector2.zero, HideMenuClicked, buttonRect, GUIController.Instance.TrajanBold, "Hide Menu");
            _panel.AddButton("Kill All", GUIController.Instance.GetImage("ButtonRect"), new Vector2(146f, 28f), Vector2.zero, KillAllClicked, buttonRect, GUIController.Instance.TrajanBold, "Kill All");
            _panel.AddButton("Set Spawn", GUIController.Instance.GetImage("ButtonRect"), new Vector2(246f, 28f), Vector2.zero, SetSpawnClicked, buttonRect, GUIController.Instance.TrajanBold, "Set Spawn");
            _panel.AddButton("Respawn", GUIController.Instance.GetImage("ButtonRect"), new Vector2(346f, 28f), Vector2.zero, RespawnClicked, buttonRect, GUIController.Instance.TrajanBold, "Respawn");
            _panel.AddButton("Dump Log", GUIController.Instance.GetImage("ButtonRect"), new Vector2(446f, 28f), Vector2.zero, DumpLogClicked, buttonRect, GUIController.Instance.TrajanBold, "Dump Log");
            _panel.AddButton("Cheats", GUIController.Instance.GetImage("ButtonRect"), new Vector2(46f, 68f), Vector2.zero, CheatsClicked, buttonRect, GUIController.Instance.TrajanBold, "Cheats");
            _panel.AddButton("Charms", GUIController.Instance.GetImage("ButtonRect"), new Vector2(146f, 68f), Vector2.zero, CharmsClicked, buttonRect, GUIController.Instance.TrajanBold, "Charms");
            _panel.AddButton("Skills", GUIController.Instance.GetImage("ButtonRect"), new Vector2(246f, 68f), Vector2.zero, SkillsClicked, buttonRect, GUIController.Instance.TrajanBold, "Skills");
            _panel.AddButton("Items", GUIController.Instance.GetImage("ButtonRect"), new Vector2(346f, 68f), Vector2.zero, ItemsClicked, buttonRect, GUIController.Instance.TrajanBold, "Items");
            _panel.AddButton("Bosses", GUIController.Instance.GetImage("ButtonRect"), new Vector2(446f, 68f), Vector2.zero, BossesClicked, buttonRect, GUIController.Instance.TrajanBold, "Bosses");
            _panel.AddButton("DreamGate", GUIController.Instance.GetImage("ButtonRect"), new Vector2(546f, 68f), Vector2.zero, DreamGatePanelClicked, buttonRect, GUIController.Instance.TrajanBold, "DreamGate");

            //Dropdown panels
            _panel.AddPanel("Cheats Panel", GUIController.Instance.GetImage("DropdownBG"), new Vector2(45f, 75f), Vector2.zero, new Rect(0, 0, GUIController.Instance.GetImage("DropdownBG").width, 180f));
            _panel.AddPanel("Charms Panel", GUIController.Instance.GetImage("DropdownBG"), new Vector2(145f, 75f), Vector2.zero, new Rect(0, 0, GUIController.Instance.GetImage("DropdownBG").width, 180f));
            _panel.AddPanel("Skills Panel", GUIController.Instance.GetImage("DropdownBG"), new Vector2(245f, 75f), Vector2.zero, new Rect(0, 0, GUIController.Instance.GetImage("DropdownBG").width, GUIController.Instance.GetImage("DropdownBG").height));
            _panel.AddPanel("Items Panel", GUIController.Instance.GetImage("DropdownBG"), new Vector2(345f, 75f), Vector2.zero, new Rect(0, 0, GUIController.Instance.GetImage("DropdownBG").width, GUIController.Instance.GetImage("DropdownBG").height));
            _panel.AddPanel("Bosses Panel", GUIController.Instance.GetImage("DropdownBG"), new Vector2(445f, 75f), Vector2.zero, new Rect(0, 0, GUIController.Instance.GetImage("DropdownBG").width, 170f));
            _panel.AddPanel("DreamGate Panel", GUIController.Instance.GetImage("DreamGateDropdownBG"), new Vector2(545f, 75f), Vector2.zero, new Rect(0, 0, GUIController.Instance.GetImage("DreamGateDropdownBG").width, GUIController.Instance.GetImage("DreamGateDropdownBG").height));

            //Cheats panel
            _panel.GetPanel("Cheats Panel").AddButton("Infinite Jump", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 30f), Vector2.zero, InfiniteJumpClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "Infinite Jump", 10);
            _panel.GetPanel("Cheats Panel").AddButton("Infinite Soul", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 60f), Vector2.zero, InfiniteSoulClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "Infinite Soul", 10);
            _panel.GetPanel("Cheats Panel").AddButton("Infinite HP", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 90f), Vector2.zero, InfiniteHpClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "Infinite HP", 10);
            _panel.GetPanel("Cheats Panel").AddButton("Invincibility", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 120f), Vector2.zero, InvincibilityClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "Invincibility", 10);
            _panel.GetPanel("Cheats Panel").AddButton("Noclip", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 150f), Vector2.zero, NoclipClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "Noclip", 10);


            //Charms panel
            _panel.GetPanel("Charms Panel").AddButton("All Charms", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 30f), Vector2.zero, AllCharmsClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "All Charms", 10);
            _panel.GetPanel("Charms Panel").AddButton("Kingsoul", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 60f), Vector2.zero, KingsoulClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "Kingsoul: " + PlayerData.instance.royalCharmState, 10);
            _panel.GetPanel("Charms Panel").AddButton("fHeart fix", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 90f), Vector2.zero, FragileHeartFixClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "fHeart fix", 10);
            _panel.GetPanel("Charms Panel").AddButton("fGreed fix", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 120f), Vector2.zero, FragileGreedFixClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "fGreed fix", 10);
            _panel.GetPanel("Charms Panel").AddButton("fStrength fix", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 150f), Vector2.zero, FragileStrengthFixClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "fStrength fix", 10);

            //Skills panel buttons
            _panel.GetPanel("Skills Panel").AddButton("All Skills", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 30f), Vector2.zero, AllSkillsClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "All Skills", 10);
            _panel.GetPanel("Skills Panel").AddButton("Scream", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 60f), Vector2.zero, ScreamClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "Scream: " + PlayerData.instance.screamLevel, 10);
            _panel.GetPanel("Skills Panel").AddButton("Fireball", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 90f), Vector2.zero, FireballClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "Fireball: " + PlayerData.instance.fireballLevel, 10);
            _panel.GetPanel("Skills Panel").AddButton("Quake", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 120f), Vector2.zero, QuakeClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "Quake: " + PlayerData.instance.quakeLevel, 10);
            _panel.GetPanel("Skills Panel").AddButton("Mothwing Cloak", GUIController.Instance.GetImage("MothwingCloak"), new Vector2(5f, 150f), new Vector2(37f, 34f), MothwingCloakClicked, new Rect(0f, 0f, GUIController.Instance.GetImage("MothwingCloak").width, GUIController.Instance.GetImage("MothwingCloak").height));
            _panel.GetPanel("Skills Panel").AddButton("Mantis Claw", GUIController.Instance.GetImage("MantisClaw"), new Vector2(43f, 150f), new Vector2(37f, 34f), MantisClawClicked, new Rect(0f, 0f, GUIController.Instance.GetImage("MantisClaw").width, GUIController.Instance.GetImage("MantisClaw").height));
            _panel.GetPanel("Skills Panel").AddButton("Monarch Wings", GUIController.Instance.GetImage("MonarchWings"), new Vector2(5f, 194f), new Vector2(37f, 33f), MonarchWingsClicked, new Rect(0f, 0f, GUIController.Instance.GetImage("MonarchWings").width, GUIController.Instance.GetImage("MonarchWings").height));
            _panel.GetPanel("Skills Panel").AddButton("Crystal Heart", GUIController.Instance.GetImage("CrystalHeart"), new Vector2(43f, 194f), new Vector2(37f, 34f), CrystalHeartClicked, new Rect(0f, 0f, GUIController.Instance.GetImage("CrystalHeart").width, GUIController.Instance.GetImage("CrystalHeart").height));
            _panel.GetPanel("Skills Panel").AddButton("Isma's Tear", GUIController.Instance.GetImage("IsmasTear"), new Vector2(5f, 238f), new Vector2(37f, 40f), IsmasTearClicked, new Rect(0f, 0f, GUIController.Instance.GetImage("IsmasTear").width, GUIController.Instance.GetImage("IsmasTear").height));
            _panel.GetPanel("Skills Panel").AddButton("Dream Nail", GUIController.Instance.GetImage("DreamNail1"), new Vector2(43f, 251f), new Vector2(37f, 59f), DreamNailClicked, new Rect(0f, 0f, GUIController.Instance.GetImage("DreamNail1").width, GUIController.Instance.GetImage("DreamNail1").height));
            _panel.GetPanel("Skills Panel").AddButton("Dream Gate", GUIController.Instance.GetImage("DreamGate"), new Vector2(5f, 288f), new Vector2(37f, 36f), DreamGateClicked, new Rect(0f, 0f, GUIController.Instance.GetImage("DreamGate").width, GUIController.Instance.GetImage("DreamGate").height));
            _panel.GetPanel("Skills Panel").AddButton("Great Slash", GUIController.Instance.GetImage("NailArt_GreatSlash"), new Vector2(5f, 329f), new Vector2(23f, 23f), GreatSlashClicked, new Rect(0f, 0f, GUIController.Instance.GetImage("NailArt_GreatSlash").width, GUIController.Instance.GetImage("NailArt_GreatSlash").height));
            _panel.GetPanel("Skills Panel").AddButton("Dash Slash", GUIController.Instance.GetImage("NailArt_DashSlash"), new Vector2(33f, 329f), new Vector2(23f, 23f), DashSlashClicked, new Rect(0f, 0f, GUIController.Instance.GetImage("NailArt_DashSlash").width, GUIController.Instance.GetImage("NailArt_DashSlash").height));
            _panel.GetPanel("Skills Panel").AddButton("Cyclone Slash", GUIController.Instance.GetImage("NailArt_CycloneSlash"), new Vector2(61f, 329f), new Vector2(23f, 23f), CycloneSlashClicked, new Rect(0f, 0f, GUIController.Instance.GetImage("NailArt_CycloneSlash").width, GUIController.Instance.GetImage("NailArt_CycloneSlash").height));

            //Skills panel button glow
            _panel.GetPanel("Skills Panel").AddImage("Mothwing Cloak Glow", GUIController.Instance.GetImage("BlueGlow"), new Vector2(0f, 145f), new Vector2(47f, 44f), new Rect(0f, 0f, GUIController.Instance.GetImage("BlueGlow").width, GUIController.Instance.GetImage("BlueGlow").height));
            _panel.GetPanel("Skills Panel").AddImage("Mantis Claw Glow", GUIController.Instance.GetImage("BlueGlow"), new Vector2(38f, 145f), new Vector2(47f, 44f), new Rect(0f, 0f, GUIController.Instance.GetImage("BlueGlow").width, GUIController.Instance.GetImage("BlueGlow").height));
            _panel.GetPanel("Skills Panel").AddImage("Monarch Wings Glow", GUIController.Instance.GetImage("BlueGlow"), new Vector2(0f, 189f), new Vector2(47f, 43f), new Rect(0f, 0f, GUIController.Instance.GetImage("BlueGlow").width, GUIController.Instance.GetImage("BlueGlow").height));
            _panel.GetPanel("Skills Panel").AddImage("Crystal Heart Glow", GUIController.Instance.GetImage("BlueGlow"), new Vector2(38f, 189f), new Vector2(47f, 44f), new Rect(0f, 0f, GUIController.Instance.GetImage("BlueGlow").width, GUIController.Instance.GetImage("BlueGlow").height));
            _panel.GetPanel("Skills Panel").AddImage("Isma's Tear Glow", GUIController.Instance.GetImage("BlueGlow"), new Vector2(0f, 233f), new Vector2(47f, 50f), new Rect(0f, 0f, GUIController.Instance.GetImage("BlueGlow").width, GUIController.Instance.GetImage("BlueGlow").height));
            _panel.GetPanel("Skills Panel").AddImage("Dream Nail Glow", GUIController.Instance.GetImage("BlueGlow"), new Vector2(38f, 246f), new Vector2(47f, 69f), new Rect(0f, 0f, GUIController.Instance.GetImage("BlueGlow").width, GUIController.Instance.GetImage("BlueGlow").height));
            _panel.GetPanel("Skills Panel").AddImage("Dream Gate Glow", GUIController.Instance.GetImage("BlueGlow"), new Vector2(0f, 283f), new Vector2(47f, 46f), new Rect(0f, 0f, GUIController.Instance.GetImage("BlueGlow").width, GUIController.Instance.GetImage("BlueGlow").height));
            _panel.GetPanel("Skills Panel").AddImage("Great Slash Glow", GUIController.Instance.GetImage("BlueGlow"), new Vector2(0f, 324f), new Vector2(33f, 33f), new Rect(0f, 0f, GUIController.Instance.GetImage("BlueGlow").width, GUIController.Instance.GetImage("BlueGlow").height));
            _panel.GetPanel("Skills Panel").AddImage("Dash Slash Glow", GUIController.Instance.GetImage("BlueGlow"), new Vector2(28f, 324f), new Vector2(33f, 33f), new Rect(0f, 0f, GUIController.Instance.GetImage("BlueGlow").width, GUIController.Instance.GetImage("BlueGlow").height));
            _panel.GetPanel("Skills Panel").AddImage("Cyclone Slash Glow", GUIController.Instance.GetImage("BlueGlow"), new Vector2(56f, 324f), new Vector2(33f, 33f), new Rect(0f, 0f, GUIController.Instance.GetImage("BlueGlow").width, GUIController.Instance.GetImage("BlueGlow").height));

            //Items panel
            _panel.GetPanel("Items Panel").AddButton("Pale Ore", GUIController.Instance.GetImage("PaleOre"), new Vector2(5f, 30f), new Vector2(23f, 22f), PaleOreClicked, new Rect(0, 0, GUIController.Instance.GetImage("PaleOre").width, GUIController.Instance.GetImage("PaleOre").height));
            _panel.GetPanel("Items Panel").AddButton("Simple Key", GUIController.Instance.GetImage("SimpleKey"), new Vector2(33f, 30f), new Vector2(23f, 23f), SimpleKeyClicked, new Rect(0, 0, GUIController.Instance.GetImage("SimpleKey").width, GUIController.Instance.GetImage("SimpleKey").height));
            _panel.GetPanel("Items Panel").AddButton("Rancid Egg", GUIController.Instance.GetImage("RancidEgg"), new Vector2(61f, 30f), new Vector2(23f, 30f), RancidEggClicked, new Rect(0, 0, GUIController.Instance.GetImage("RancidEgg").width, GUIController.Instance.GetImage("RancidEgg").height));
            _panel.GetPanel("Items Panel").AddButton("Geo", GUIController.Instance.GetImage("Geo"), new Vector2(5f, 63f), new Vector2(23f, 23f), GeoClicked, new Rect(0, 0, GUIController.Instance.GetImage("Geo").width, GUIController.Instance.GetImage("Geo").height));
            _panel.GetPanel("Items Panel").AddButton("Essence", GUIController.Instance.GetImage("Essence"), new Vector2(33f, 63f), new Vector2(23f, 23f), EssenceClicked, new Rect(0, 0, GUIController.Instance.GetImage("Essence").width, GUIController.Instance.GetImage("Essence").height));
            _panel.GetPanel("Items Panel").AddButton("Lantern", GUIController.Instance.GetImage("Lantern"), new Vector2(5f, 96f), new Vector2(37f, 41f), LanternClicked, new Rect(0, 0, GUIController.Instance.GetImage("Lantern").width, GUIController.Instance.GetImage("Lantern").height));
            _panel.GetPanel("Items Panel").AddButton("Tram Pass", GUIController.Instance.GetImage("TramPass"), new Vector2(43f, 96f), new Vector2(37f, 27f), TramPassClicked, new Rect(0, 0, GUIController.Instance.GetImage("TramPass").width, GUIController.Instance.GetImage("TramPass").height));
            _panel.GetPanel("Items Panel").AddButton("Map & Quill", GUIController.Instance.GetImage("MapQuill"), new Vector2(5f, 147f), new Vector2(37f, 30f), MapQuillClicked, new Rect(0, 0, GUIController.Instance.GetImage("MapQuill").width, GUIController.Instance.GetImage("MapQuill").height));
            _panel.GetPanel("Items Panel").AddButton("City Crest", GUIController.Instance.GetImage("CityKey"), new Vector2(43f, 147f), new Vector2(37f, 50f), CityKeyClicked, new Rect(0, 0, GUIController.Instance.GetImage("CityKey").width, GUIController.Instance.GetImage("CityKey").height));
            _panel.GetPanel("Items Panel").AddButton("Sly Key", GUIController.Instance.GetImage("SlyKey"), new Vector2(5f, 207f), new Vector2(37f, 39f), SlyKeyClicked, new Rect(0, 0, GUIController.Instance.GetImage("SlyKey").width, GUIController.Instance.GetImage("SlyKey").height));
            _panel.GetPanel("Items Panel").AddButton("Elegant Key", GUIController.Instance.GetImage("ElegantKey"), new Vector2(43f, 207f), new Vector2(37f, 36f), ElegantKeyClicked, new Rect(0, 0, GUIController.Instance.GetImage("ElegantKey").width, GUIController.Instance.GetImage("ElegantKey").height));
            _panel.GetPanel("Items Panel").AddButton("Love Key", GUIController.Instance.GetImage("LoveKey"), new Vector2(5f, 256f), new Vector2(37f, 36f), LoveKeyClicked, new Rect(0, 0, GUIController.Instance.GetImage("LoveKey").width, GUIController.Instance.GetImage("LoveKey").height));
            _panel.GetPanel("Items Panel").AddButton("King's Brand", GUIController.Instance.GetImage("Kingsbrand"), new Vector2(43f, 256f), new Vector2(37f, 35f), KingsbrandClicked, new Rect(0, 0, GUIController.Instance.GetImage("Kingsbrand").width, GUIController.Instance.GetImage("Kingsbrand").height));
            _panel.GetPanel("Items Panel").AddButton("Bullshit Flower", GUIController.Instance.GetImage("Flower"), new Vector2(5f, 302f), new Vector2(37f, 35f), FlowerClicked, new Rect(0, 0, GUIController.Instance.GetImage("Flower").width, GUIController.Instance.GetImage("Flower").height));
            
            //Items panel button glow
            _panel.GetPanel("Items Panel").AddImage("Lantern Glow", GUIController.Instance.GetImage("BlueGlow"), new Vector2(0f, 91f), new Vector2(47f, 51f), new Rect(0f, 0f, GUIController.Instance.GetImage("BlueGlow").width, GUIController.Instance.GetImage("BlueGlow").height));
            _panel.GetPanel("Items Panel").AddImage("Tram Pass Glow", GUIController.Instance.GetImage("BlueGlow"), new Vector2(38f, 91f), new Vector2(47f, 37f), new Rect(0f, 0f, GUIController.Instance.GetImage("BlueGlow").width, GUIController.Instance.GetImage("BlueGlow").height));
            _panel.GetPanel("Items Panel").AddImage("Map & Quill Glow", GUIController.Instance.GetImage("BlueGlow"), new Vector2(0f, 142f), new Vector2(47f, 40f), new Rect(0f, 0f, GUIController.Instance.GetImage("BlueGlow").width, GUIController.Instance.GetImage("BlueGlow").height));
            _panel.GetPanel("Items Panel").AddImage("City Crest Glow", GUIController.Instance.GetImage("BlueGlow"), new Vector2(38f, 142f), new Vector2(47f, 60f), new Rect(0f, 0f, GUIController.Instance.GetImage("BlueGlow").width, GUIController.Instance.GetImage("BlueGlow").height));
            _panel.GetPanel("Items Panel").AddImage("Sly Key Glow", GUIController.Instance.GetImage("BlueGlow"), new Vector2(0f, 202f), new Vector2(47f, 49f), new Rect(0f, 0f, GUIController.Instance.GetImage("BlueGlow").width, GUIController.Instance.GetImage("BlueGlow").height));
            _panel.GetPanel("Items Panel").AddImage("Elegant Key Glow", GUIController.Instance.GetImage("BlueGlow"), new Vector2(38f, 202f), new Vector2(47f, 46f), new Rect(0f, 0f, GUIController.Instance.GetImage("BlueGlow").width, GUIController.Instance.GetImage("BlueGlow").height));
            _panel.GetPanel("Items Panel").AddImage("Love Key Glow", GUIController.Instance.GetImage("BlueGlow"), new Vector2(0f, 251f), new Vector2(47f, 46f), new Rect(0f, 0f, GUIController.Instance.GetImage("BlueGlow").width, GUIController.Instance.GetImage("BlueGlow").height));
            _panel.GetPanel("Items Panel").AddImage("King's Brand Glow", GUIController.Instance.GetImage("BlueGlow"), new Vector2(38f, 251f), new Vector2(47f, 45f), new Rect(0f, 0f, GUIController.Instance.GetImage("BlueGlow").width, GUIController.Instance.GetImage("BlueGlow").height));
            _panel.GetPanel("Items Panel").AddImage("Bullshit Flower Glow", GUIController.Instance.GetImage("BlueGlow"), new Vector2(0f, 297f), new Vector2(47f, 45f), new Rect(0f, 0f, GUIController.Instance.GetImage("BlueGlow").width, GUIController.Instance.GetImage("BlueGlow").height));

            //Boss panel
            _panel.GetPanel("Bosses Panel").AddButton("Respawn Boss", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 30f), Vector2.zero, RespawnBossClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "Respawn Boss", 10);
            _panel.GetPanel("Bosses Panel").AddButton("Respawn Ghost", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 50f), Vector2.zero, RespawnGhostClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "Respawn Ghost", 9);

            _panel.GetPanel("Bosses Panel").AddButton("Failed Champ", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 110f), Vector2.zero, FailedChampClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "Failed Champ", 10);
            _panel.GetPanel("Bosses Panel").AddButton("Soul Tyrant", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 130f), Vector2.zero, SoulTyrantClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "Soul Tyrant", 10);
            _panel.GetPanel("Bosses Panel").AddButton("Lost Kin", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 150f), Vector2.zero, LostKinClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "Lost Kin", 10);

            //Dream gate left panel
            _panel.GetPanel("DreamGate Panel").AddButton("Read Data", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 30f), Vector2.zero, ReadDataClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "Read Data", 10);
            _panel.GetPanel("DreamGate Panel").AddButton("Save Data", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 50f), Vector2.zero, SaveDataClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "Save Data", 10);
            _panel.GetPanel("DreamGate Panel").AddButton("Delete Item", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 70f), Vector2.zero, DeleteItemClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "Delete Item", 10);
            _panel.GetPanel("DreamGate Panel").AddButton("Add Item", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(5f, 90f), Vector2.zero, AddItemClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.TrajanNormal, "Add Item", 10);

            //Dream gate right panel
            _panel.GetPanel("DreamGate Panel").AddButton("Right1", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(90f, 30f), Vector2.zero, SetWarpClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.Arial, "");
            _panel.GetPanel("DreamGate Panel").AddButton("Right2", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(90f, 50f), Vector2.zero, SetWarpClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.Arial, "");
            _panel.GetPanel("DreamGate Panel").AddButton("Right3", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(90f, 70f), Vector2.zero, SetWarpClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.Arial, "");
            _panel.GetPanel("DreamGate Panel").AddButton("Right4", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(90f, 90f), Vector2.zero, SetWarpClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.Arial, "");
            _panel.GetPanel("DreamGate Panel").AddButton("Right5", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(90f, 110f), Vector2.zero, SetWarpClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.Arial, "");
            _panel.GetPanel("DreamGate Panel").AddButton("Right6", GUIController.Instance.GetImage("ButtonRectEmpty"), new Vector2(90f, 130f), Vector2.zero, SetWarpClicked, new Rect(0f, 0f, 80f, 20f), GUIController.Instance.Arial, "");

            //Dream gate scroll
            _panel.GetPanel("DreamGate Panel").AddButton("Scroll Up", GUIController.Instance.GetImage("ScrollBarArrowUp"), new Vector2(180f, 30f), Vector2.zero, ScrollUpClicked, new Rect(0f, 0f, GUIController.Instance.GetImage("ScrollBarArrowUp").width, GUIController.Instance.GetImage("ScrollBarArrowUp").height));
            _panel.GetPanel("DreamGate Panel").AddButton("Scroll Down", GUIController.Instance.GetImage("ScrollBarArrowDown"), new Vector2(180f, 130f), Vector2.zero, ScrollDownClicked, new Rect(0f, 0f, GUIController.Instance.GetImage("ScrollBarArrowDown").width, GUIController.Instance.GetImage("ScrollBarArrowDown").height));

            _panel.FixRenderOrder();
        }

        private static void NoclipClicked(string buttonName)
        {
            GUIController.Noclip = !GUIController.Noclip;

            if (GUIController.Noclip)
            {
                Console.AddLine("Enabled noclip");
                GUIController.NoclipPos = GUIController.RefKnight.transform.position;
            }
            else
            {
                Console.AddLine("Disabled noclip");
            }

            _panel.GetButton("Noclip", "Cheats Panel").SetTextColor(GUIController.Noclip ? new Color(244f / 255f, 127f / 255f, 32f / 255f) : Color.white);
        }

        public static void Update()
        {
            if (_panel == null)
            {
                return;
            }

            if (Visible && !_panel.Active)
            {
                _panel.SetActive(true, false);
            }
            else if (!Visible && _panel.Active)
            {
                _panel.SetActive(false, true);
            }

            if (_panel.GetPanel("Skills Panel").Active) RefreshSkillsMenu();

            if (_panel.GetPanel("Items Panel").Active) RefreshItemsMenu();

            if (_panel.GetPanel("Cheats Panel").Active) _panel.GetButton("Infinite Jump", "Cheats Panel").SetTextColor(PlayerData.instance.infiniteAirJump ? new Color(244f / 255f, 127f / 255f, 32f / 255f) : Color.white);

            if (_panel.GetPanel("Bosses Panel").Active) _panel.GetButton("Failed Champ", "Bosses Panel").SetTextColor(PlayerData.instance.falseKnightDreamDefeated ? new Color(244f / 255f, 127f / 255f, 32f / 255f) : Color.white);
            if (_panel.GetPanel("Bosses Panel").Active) _panel.GetButton("Soul Tyrant", "Bosses Panel").SetTextColor(PlayerData.instance.mageLordDreamDefeated ? new Color(244f / 255f, 127f / 255f, 32f / 255f) : Color.white);
            if (_panel.GetPanel("Bosses Panel").Active) _panel.GetButton("Lost Kin", "Bosses Panel").SetTextColor(PlayerData.instance.infectedKnightDreamDefeated ? new Color(244f / 255f, 127f / 255f, 32f / 255f) : Color.white);

            if (!_panel.GetPanel("DreamGate Panel").Active) return;
            _panel.GetPanel("DreamGate Panel").GetButton("Delete Item").SetTextColor(DreamGate.DelMenu ? new Color(244f / 255f, 127f / 255f, 32f / 255f) : Color.white);

            _panel.GetPanel("DreamGate Panel").GetButton("Right1").UpdateText("");
            _panel.GetPanel("DreamGate Panel").GetButton("Right2").UpdateText("");
            _panel.GetPanel("DreamGate Panel").GetButton("Right3").UpdateText("");
            _panel.GetPanel("DreamGate Panel").GetButton("Right4").UpdateText("");
            _panel.GetPanel("DreamGate Panel").GetButton("Right5").UpdateText("");
            _panel.GetPanel("DreamGate Panel").GetButton("Right6").UpdateText("");

            var i = 0;
            var buttonNum = 1;

            foreach (var entryName in DreamGate.DgData.Keys)
            {
                if (i >= DreamGate.ScrollPosition)
                {
                    _panel.GetPanel("DreamGate Panel").GetButton("Right" + buttonNum).UpdateText(entryName);
                    buttonNum++;
                    if (buttonNum > 6)
                    {
                        break;
                    }
                }

                i++;
            }
        }

        private static void RefreshItemsMenu()
        {
            _panel.GetImage("Lantern Glow", "Items Panel").SetActive(true);
            _panel.GetImage("Tram Pass Glow", "Items Panel").SetActive(true);
            _panel.GetImage("Map & Quill Glow", "Items Panel").SetActive(true);
            _panel.GetImage("City Crest Glow", "Items Panel").SetActive(true);
            _panel.GetImage("Sly Key Glow", "Items Panel").SetActive(true);
            _panel.GetImage("Elegant Key Glow", "Items Panel").SetActive(true);
            _panel.GetImage("Love Key Glow", "Items Panel").SetActive(true);
            _panel.GetImage("King's Brand Glow", "Items Panel").SetActive(true);
            _panel.GetImage("Bullshit Flower Glow", "Items Panel").SetActive(true);

            if (!PlayerData.instance.hasLantern) _panel.GetImage("Lantern Glow", "Items Panel").SetActive(false);
            if (!PlayerData.instance.hasTramPass) _panel.GetImage("Tram Pass Glow", "Items Panel").SetActive(false);
            if (!PlayerData.instance.hasQuill) _panel.GetImage("Map & Quill Glow", "Items Panel").SetActive(false);
            if (!PlayerData.instance.hasCityKey) _panel.GetImage("City Crest Glow", "Items Panel").SetActive(false);
            if (!PlayerData.instance.hasSlykey) _panel.GetImage("Sly Key Glow", "Items Panel").SetActive(false);
            if (!PlayerData.instance.hasWhiteKey) _panel.GetImage("Elegant Key Glow", "Items Panel").SetActive(false);
            if (!PlayerData.instance.hasLoveKey) _panel.GetImage("Love Key Glow", "Items Panel").SetActive(false);
            if (!PlayerData.instance.hasKingsBrand) _panel.GetImage("King's Brand Glow", "Items Panel").SetActive(false);
            if (!PlayerData.instance.hasXunFlower || PlayerData.instance.xunFlowerBroken) _panel.GetImage("Bullshit Flower Glow", "Items Panel").SetActive(false);
        }

        private static void RefreshSkillsMenu()
        {
            if (PlayerData.instance.dreamNailUpgraded) _panel.GetButton("Dream Nail", "Skills Panel").UpdateSprite(GUIController.Instance.GetImage("DreamNail2"), new Rect(0f, 0f, GUIController.Instance.GetImage("DreamNail2").width, GUIController.Instance.GetImage("DreamNail2").height));
            else _panel.GetButton("Dream Nail", "Skills Panel").UpdateSprite(GUIController.Instance.GetImage("DreamNail1"), new Rect(0f, 0f, GUIController.Instance.GetImage("DreamNail1").width, GUIController.Instance.GetImage("DreamNail1").height));
            if (PlayerData.instance.hasShadowDash) _panel.GetButton("Mothwing Cloak", "Skills Panel").UpdateSprite(GUIController.Instance.GetImage("ShadeCloak"), new Rect(0f, 0f, GUIController.Instance.GetImage("ShadeCloak").width, GUIController.Instance.GetImage("ShadeCloak").height));
            else _panel.GetButton("Mothwing Cloak", "Skills Panel").UpdateSprite(GUIController.Instance.GetImage("MothwingCloak"), new Rect(0f, 0f, GUIController.Instance.GetImage("MothwingCloak").width, GUIController.Instance.GetImage("MothwingCloak").height));

            _panel.GetImage("Mothwing Cloak Glow", "Skills Panel").SetActive(true);
            _panel.GetImage("Mantis Claw Glow", "Skills Panel").SetActive(true);
            _panel.GetImage("Monarch Wings Glow", "Skills Panel").SetActive(true);
            _panel.GetImage("Crystal Heart Glow", "Skills Panel").SetActive(true);
            _panel.GetImage("Isma's Tear Glow", "Skills Panel").SetActive(true);
            _panel.GetImage("Dream Gate Glow", "Skills Panel").SetActive(true);
            _panel.GetImage("Dream Nail Glow", "Skills Panel").SetActive(true);
            _panel.GetImage("Great Slash Glow", "Skills Panel").SetActive(true);
            _panel.GetImage("Dash Slash Glow", "Skills Panel").SetActive(true);
            _panel.GetImage("Cyclone Slash Glow", "Skills Panel").SetActive(true);

            if (!PlayerData.instance.hasDash && !PlayerData.instance.hasShadowDash) _panel.GetImage("Mothwing Cloak Glow", "Skills Panel").SetActive(false);
            if (!PlayerData.instance.hasWalljump) _panel.GetImage("Mantis Claw Glow", "Skills Panel").SetActive(false);
            if (!PlayerData.instance.hasDoubleJump) _panel.GetImage("Monarch Wings Glow", "Skills Panel").SetActive(false);
            if (!PlayerData.instance.hasSuperDash) _panel.GetImage("Crystal Heart Glow", "Skills Panel").SetActive(false);
            if (!PlayerData.instance.hasAcidArmour) _panel.GetImage("Isma's Tear Glow", "Skills Panel").SetActive(false);
            if (!PlayerData.instance.hasDreamGate) _panel.GetImage("Dream Gate Glow", "Skills Panel").SetActive(false);
            if (!PlayerData.instance.hasDreamNail && !PlayerData.instance.dreamNailUpgraded) _panel.GetImage("Dream Nail Glow", "Skills Panel").SetActive(false);
            if (!PlayerData.instance.hasDashSlash) _panel.GetImage("Great Slash Glow", "Skills Panel").SetActive(false);
            if (!PlayerData.instance.hasUpwardSlash) _panel.GetImage("Dash Slash Glow", "Skills Panel").SetActive(false);
            if (!PlayerData.instance.hasCyclone) _panel.GetImage("Cyclone Slash Glow", "Skills Panel").SetActive(false);

            _panel.GetButton("Scream", "Skills Panel").UpdateText("Scream: " + PlayerData.instance.screamLevel);
            _panel.GetButton("Fireball", "Skills Panel").UpdateText("Fireball: " + PlayerData.instance.fireballLevel);
            _panel.GetButton("Quake", "Skills Panel").UpdateText("Quake: " + PlayerData.instance.quakeLevel);
        }

        private static void HideMenuClicked(string buttonName)
        {
            GUIController.Instance.SetMenusActive(false);
        }

        private static void KillAllClicked(string buttonName)
        {
            PlayMakerFSM.BroadcastEvent("INSTA KILL");
            Console.AddLine("INSTA KILL broadcasted!");
        }

        private static void SetSpawnClicked(string buttonName)
        {
            HeroController.instance.SetHazardRespawn(GUIController.RefKnight.transform.position, false);
            Console.AddLine("Manual respawn point on this map set to" + GUIController.RefKnight.transform.position);
        }

        private static void RespawnClicked(string buttonName)
        {
            if (!GameManager.instance.IsGameplayScene() || HeroController.instance.cState.dead ||
                PlayerData.instance.health <= 0) return;
            switch (UIManager.instance.uiState.ToString())
            {
                case "PAUSED":
                    UIManager.instance.TogglePauseGame();
                    GameManager.instance.HazardRespawn();
                    Console.AddLine("Closing Pause Menu and respawning...");
                    return;
                case "PLAYING":
                    HeroController.instance.RelinquishControl();
                    GameManager.instance.HazardRespawn();
                    HeroController.instance.RegainControl();
                    Console.AddLine("Respawn signal sent");
                    return;
                default:
                    Console.AddLine("Respawn requested in some weird conditions, abort, ABORT");
                    return;
            }
        }

        private static void DumpLogClicked(string buttonName)
        {
            Console.AddLine("Saving console log...");
            Console.SaveHistory();
        }

        private static void CheatsClicked(string buttonName)
        {
            _panel.TogglePanel("Cheats Panel");
        }

        private static void CharmsClicked(string buttonName)
        {
            _panel.TogglePanel("Charms Panel");
        }

        private static void SkillsClicked(string buttonName)
        {
            _panel.TogglePanel("Skills Panel");
            if (_panel.GetPanel("Skills Panel").Active) RefreshSkillsMenu();
        }

        private static void ItemsClicked(string buttonName)
        {
            _panel.TogglePanel("Items Panel");
        }

        private static void BossesClicked(string buttonName)
        {
            _panel.TogglePanel("Bosses Panel");
        }

        private static void DreamGatePanelClicked(string buttonName)
        {
            _panel.TogglePanel("DreamGate Panel");
        }

        private static void InfiniteJumpClicked(string buttonName)
        {
            PlayerData.instance.infiniteAirJump = !PlayerData.instance.infiniteAirJump;
            Console.AddLine("Infinite Jump set to " + PlayerData.instance.infiniteAirJump.ToString().ToUpper());

            _panel.GetButton("Infinite Jump", "Cheats Panel").SetTextColor(PlayerData.instance.infiniteAirJump ? new Color(244f / 255f, 127f / 255f, 32f / 255f) : Color.white);
        }

        private static void InfiniteSoulClicked(string buttonName)
        {
            GUIController.InfiniteSoul = !GUIController.InfiniteSoul;
            Console.AddLine("Infinite SOUL set to " + GUIController.InfiniteSoul.ToString().ToUpper());

            _panel.GetButton("Infinite Soul", "Cheats Panel").SetTextColor(GUIController.InfiniteSoul ? new Color(244f / 255f, 127f / 255f, 32f / 255f) : Color.white);
        }

        private static void InfiniteHpClicked(string buttonName)
        {
            GUIController.InfiniteHp = !GUIController.InfiniteHp;
            Console.AddLine("Infinite HP set to " + GUIController.InfiniteHp.ToString().ToUpper());

            _panel.GetButton("Infinite HP", "Cheats Panel").SetTextColor(GUIController.InfiniteHp ? new Color(244f / 255f, 127f / 255f, 32f / 255f) : Color.white);
        }

        private static void InvincibilityClicked(string buttonName)
        {
            PlayerData.instance.isInvincible = !PlayerData.instance.isInvincible;
            Console.AddLine("Invincibility set to " + PlayerData.instance.isInvincible.ToString().ToUpper());

            _panel.GetButton("Invincibility", "Cheats Panel").SetTextColor(PlayerData.instance.isInvincible ? new Color(244f / 255f, 127f / 255f, 32f / 255f) : Color.white);

            GUIController.PlayerInvincible = PlayerData.instance.isInvincible;
        }

        private static void AllCharmsClicked(string buttonName)
        {
            for (var i = 1; i < 37; i++)
            {
                PlayerData.instance.GetType().GetField("gotCharm_" + i).SetValue(PlayerData.instance, true);
            }

            PlayerData.instance.charmSlots = 10;
            PlayerData.instance.hasCharm = true;
            PlayerData.instance.charmsOwned = 36;
            PlayerData.instance.royalCharmState = 4;
            PlayerData.instance.gotKingFragment = true;
            PlayerData.instance.gotQueenFragment = true;
            PlayerData.instance.notchShroomOgres = true;
            PlayerData.instance.notchFogCanyon = true;
            PlayerData.instance.salubraNotch1 = true;
            PlayerData.instance.salubraNotch2 = true;
            PlayerData.instance.salubraNotch3 = true;
            PlayerData.instance.salubraNotch4 = true;

            _panel.GetButton("Kingsoul", "Charms Panel").UpdateText("Kingsoul: " + PlayerData.instance.royalCharmState);

            Console.AddLine("Added all charms to inventory");
        }

        private static void KingsoulClicked(string buttonName)
        {
            if (!PlayerData.instance.gotCharm_36)
            {
                PlayerData.instance.gotCharm_36 = true;
            }

            PlayerData.instance.royalCharmState++;

            if (PlayerData.instance.royalCharmState >= 5)
            {
                PlayerData.instance.royalCharmState = 0;
            }

            _panel.GetButton("Kingsoul", "Charms Panel").UpdateText("Kingsoul: " + PlayerData.instance.royalCharmState);
        }

        private static void FragileHeartFixClicked(string buttonName)
        {
            if (!PlayerData.instance.brokenCharm_23) return;
            PlayerData.instance.brokenCharm_23 = false;
            Console.AddLine("Fixed fragile heart");
        }

        private static void FragileGreedFixClicked(string buttonName)
        {
            if (!PlayerData.instance.brokenCharm_24) return;
            PlayerData.instance.brokenCharm_24 = false;
            Console.AddLine("Fixed fragile greed");
        }

        private static void FragileStrengthFixClicked(string buttonName)
        {
            if (!PlayerData.instance.brokenCharm_25) return;
            PlayerData.instance.brokenCharm_25 = false;
            Console.AddLine("Fixed fragile strength");
        }

        private static void AllSkillsClicked(string buttonName)
        {
            PlayerData.instance.screamLevel = 2;
            PlayerData.instance.fireballLevel = 2;
            PlayerData.instance.quakeLevel = 2;

            PlayerData.instance.hasDash = true;
            PlayerData.instance.canDash = true;
            PlayerData.instance.hasShadowDash = true;
            PlayerData.instance.canShadowDash = true;
            PlayerData.instance.hasWalljump = true;
            PlayerData.instance.canWallJump = true;
            PlayerData.instance.hasDoubleJump = true;
            PlayerData.instance.hasSuperDash = true;
            PlayerData.instance.canSuperDash = true;
            PlayerData.instance.hasAcidArmour = true;

            PlayerData.instance.hasDreamNail = true;
            PlayerData.instance.dreamNailUpgraded = true;
            PlayerData.instance.hasDreamGate = true;

            PlayerData.instance.hasNailArt = true;
            PlayerData.instance.hasCyclone = true;
            PlayerData.instance.hasDashSlash = true;
            PlayerData.instance.hasUpwardSlash = true;

            Console.AddLine("Giving player all skills");

            RefreshSkillsMenu();
        }

        private static void ScreamClicked(string buttonName)
        {
            if (PlayerData.instance.screamLevel >= 2)
            {
                PlayerData.instance.screamLevel = 0;
            }
            else
            {
                PlayerData.instance.screamLevel++;
            }

            RefreshSkillsMenu();
        }

        private static void FireballClicked(string buttonName)
        {
            if (PlayerData.instance.fireballLevel >= 2)
            {
                PlayerData.instance.fireballLevel = 0;
            }
            else
            {
                PlayerData.instance.fireballLevel++;
            }

            RefreshSkillsMenu();
        }

        private static void QuakeClicked(string buttonName)
        {
            if (PlayerData.instance.quakeLevel >= 2)
            {
                PlayerData.instance.quakeLevel = 0;
            }
            else
            {
                PlayerData.instance.quakeLevel++;
            }

            RefreshSkillsMenu();
        }

        private static void MothwingCloakClicked(string buttonName)
        {
            if (!PlayerData.instance.hasDash && !PlayerData.instance.hasShadowDash)
            {
                PlayerData.instance.hasDash = true;
                PlayerData.instance.canDash = true;
                Console.AddLine("Giving player Mothwing Cloak");
            }
            else if (PlayerData.instance.hasDash && !PlayerData.instance.hasShadowDash)
            {
                PlayerData.instance.hasShadowDash = true;
                PlayerData.instance.canShadowDash = true;
                Console.AddLine("Giving player Shade Cloak");
            }
            else
            {
                PlayerData.instance.hasDash = false;
                PlayerData.instance.canDash = false;
                PlayerData.instance.hasShadowDash = false;
                PlayerData.instance.canShadowDash = false;
                Console.AddLine("Taking away both dash upgrades");
            }

            RefreshSkillsMenu();
        }

        private static void MantisClawClicked(string buttonName)
        {
            if (!PlayerData.instance.hasWalljump)
            {
                PlayerData.instance.hasWalljump = true;
                PlayerData.instance.canWallJump = true;
                Console.AddLine("Giving player Mantis Claw");
            }
            else
            {
                PlayerData.instance.hasWalljump = false;
                PlayerData.instance.canWallJump = false;
                Console.AddLine("Taking away Mantis Claw");
            }

            RefreshSkillsMenu();
        }

        private static void MonarchWingsClicked(string buttonName)
        {
            if (!PlayerData.instance.hasDoubleJump)
            {
                PlayerData.instance.hasDoubleJump = true;
                Console.AddLine("Giving player Monarch Wings");
            }
            else
            {
                PlayerData.instance.hasDoubleJump = false;
                Console.AddLine("Taking away Monarch Wings");
            }

            RefreshSkillsMenu();
        }

        private static void CrystalHeartClicked(string buttonName)
        {
            if (!PlayerData.instance.hasSuperDash)
            {
                PlayerData.instance.hasSuperDash = true;
                PlayerData.instance.canSuperDash = true;
                Console.AddLine("Giving player Crystal Heart");
            }
            else
            {
                PlayerData.instance.hasSuperDash = false;
                PlayerData.instance.canSuperDash = false;
                Console.AddLine("Taking away Crystal Heart");
            }

            RefreshSkillsMenu();
        }

        private static void IsmasTearClicked(string buttonName)
        {
            if (!PlayerData.instance.hasAcidArmour)
            {
                PlayerData.instance.hasAcidArmour = true;
                Console.AddLine("Giving player Isma's Tear");
            }
            else
            {
                PlayerData.instance.hasAcidArmour = false;
                Console.AddLine("Taking away Isma's Tear");
            }

            RefreshSkillsMenu();
        }

        private static void DreamNailClicked(string buttonName)
        {
            if (!PlayerData.instance.hasDreamNail && !PlayerData.instance.dreamNailUpgraded)
            {
                PlayerData.instance.hasDreamNail = true;
                Console.AddLine("Giving player Dream Nail");
            }
            else if (PlayerData.instance.hasDreamNail && !PlayerData.instance.dreamNailUpgraded)
            {
                PlayerData.instance.dreamNailUpgraded = true;
                Console.AddLine("Giving player Awoken Dream Nail");
            }
            else
            {
                PlayerData.instance.hasDreamNail = false;
                PlayerData.instance.dreamNailUpgraded = false;
                Console.AddLine("Taking away both Dream Nail upgrades");
            }

            RefreshSkillsMenu();
        }

        private static void DreamGateClicked(string buttonName)
        {
            if (!PlayerData.instance.hasDreamNail && !PlayerData.instance.hasDreamGate)
            {
                PlayerData.instance.hasDreamNail = true;
                PlayerData.instance.hasDreamGate = true;
                FSMUtility.LocateFSM(GUIController.RefKnight, "Dream Nail").FsmVariables.GetFsmBool("Dream Warp Allowed").Value = true;
                Console.AddLine("Giving player both Dream Nail and Dream Gate");
            }
            else if (PlayerData.instance.hasDreamNail && !PlayerData.instance.hasDreamGate)
            {
                PlayerData.instance.hasDreamGate = true;
                FSMUtility.LocateFSM(GUIController.RefKnight, "Dream Nail").FsmVariables.GetFsmBool("Dream Warp Allowed").Value = true;
                Console.AddLine("Giving player Dream Gate");
            }
            else
            {
                PlayerData.instance.hasDreamGate = false;
                FSMUtility.LocateFSM(GUIController.RefKnight, "Dream Nail").FsmVariables.GetFsmBool("Dream Warp Allowed").Value = false;
                Console.AddLine("Taking away Dream Gate");
            }

            RefreshSkillsMenu();
        }

        private static void GreatSlashClicked(string buttonName)
        {
            if (!PlayerData.instance.hasDashSlash)
            {
                PlayerData.instance.hasDashSlash = true;
                PlayerData.instance.hasNailArt = true;
                Console.AddLine("Giving player Great Slash");
            }
            else
            {
                PlayerData.instance.hasDashSlash = false;
                Console.AddLine("Taking away Great Slash");
            }

            if (!PlayerData.instance.hasUpwardSlash && !PlayerData.instance.hasDashSlash && !PlayerData.instance.hasCyclone) PlayerData.instance.hasNailArt = false;

            RefreshSkillsMenu();
        }

        private static void DashSlashClicked(string buttonName)
        {
            if (!PlayerData.instance.hasUpwardSlash)
            {
                PlayerData.instance.hasUpwardSlash = true;
                PlayerData.instance.hasNailArt = true;
                Console.AddLine("Giving player Dash Slash");
            }
            else
            {
                PlayerData.instance.hasUpwardSlash = false;
                Console.AddLine("Taking away Dash Slash");
            }

            if (!PlayerData.instance.hasUpwardSlash && !PlayerData.instance.hasDashSlash && !PlayerData.instance.hasCyclone) PlayerData.instance.hasNailArt = false;

            RefreshSkillsMenu();
        }

        private static void CycloneSlashClicked(string buttonName)
        {
            if (!PlayerData.instance.hasCyclone)
            {
                PlayerData.instance.hasCyclone = true;
                PlayerData.instance.hasNailArt = true;
                Console.AddLine("Giving player Cyclone Slash");
            }
            else
            {
                PlayerData.instance.hasCyclone = false;
                Console.AddLine("Taking away Cyclone Slash");
            }

            if (!PlayerData.instance.hasUpwardSlash && !PlayerData.instance.hasDashSlash && !PlayerData.instance.hasCyclone) PlayerData.instance.hasNailArt = false;

            RefreshSkillsMenu();
        }

        private static void RespawnGhostClicked(string buttonName)
        {
            BossHandler.RespawnGhost();
        }

        private static void RespawnBossClicked(string buttonName)
        {
            BossHandler.RespawnBoss();
        }

        private static void FailedChampClicked(string buttonName)
        {
            PlayerData.instance.falseKnightDreamDefeated = !PlayerData.instance.falseKnightDreamDefeated;

            Console.AddLine("Set Failed Champion killed: " + PlayerData.instance.falseKnightDreamDefeated);
        }

        private static void SoulTyrantClicked(string buttonName)
        {
            PlayerData.instance.mageLordDreamDefeated = !PlayerData.instance.mageLordDreamDefeated;

            Console.AddLine("Set Soul Tyrant killed: " + PlayerData.instance.mageLordDreamDefeated);
        }

        private static void LostKinClicked(string buttonName)
        {
            PlayerData.instance.infectedKnightDreamDefeated = !PlayerData.instance.infectedKnightDreamDefeated;

            Console.AddLine("Set Lost Kin killed: " + PlayerData.instance.infectedKnightDreamDefeated);
        }

        private static void PaleOreClicked(string buttonName)
        {
            PlayerData.instance.ore = 6;
            Console.AddLine("Set player pale ore to 6");
        }

        private static void SimpleKeyClicked(string buttonName)
        {
            PlayerData.instance.simpleKeys = 3;
            Console.AddLine("Set player simple keys to 3");
        }

        private static void RancidEggClicked(string buttonName)
        {
            PlayerData.instance.rancidEggs += 10;
            Console.AddLine("Giving player 10 rancid eggs");
        }

        private static void GeoClicked(string buttonName)
        {
            HeroController.instance.AddGeo(1000);
            Console.AddLine("Giving player 1000 geo");
        }

        private static void EssenceClicked(string buttonName)
        {
            PlayerData.instance.dreamOrbs += 100;
            Console.AddLine("Giving player 100 essence");
        }

        private static void LanternClicked(string buttonName)
        {
            if (!PlayerData.instance.hasLantern)
            {
                PlayerData.instance.hasLantern = true;
                Console.AddLine("Giving player lantern");
            }
            else
            {
                PlayerData.instance.hasLantern = false;
                Console.AddLine("Taking away lantern");
            }
        }

        private static void TramPassClicked(string buttonName)
        {
            if (!PlayerData.instance.hasTramPass)
            {
                PlayerData.instance.hasTramPass = true;
                Console.AddLine("Giving player tram pass");
            }
            else
            {
                PlayerData.instance.hasTramPass = false;
                Console.AddLine("Taking away tram pass");
            }
        }

        private static void MapQuillClicked(string buttonName)
        {
            if (!PlayerData.instance.hasQuill)
            {
                PlayerData.instance.hasQuill = true;
                Console.AddLine("Giving player map quill");
            }
            else
            {
                PlayerData.instance.hasQuill = false;
                Console.AddLine("Taking away map quill");
            }
        }

        private static void CityKeyClicked(string buttonName)
        {
            if (!PlayerData.instance.hasCityKey)
            {
                PlayerData.instance.hasCityKey = true;
                Console.AddLine("Giving player city crest");
            }
            else
            {
                PlayerData.instance.hasCityKey = false;
                Console.AddLine("Taking away city crest");
            }
        }

        private static void SlyKeyClicked(string buttonName)
        {
            if (!PlayerData.instance.hasSlykey)
            {
                PlayerData.instance.hasSlykey = true;
                Console.AddLine("Giving player shopkeeper's key");
            }
            else
            {
                PlayerData.instance.hasSlykey = false;
                Console.AddLine("Taking away shopkeeper's key");
            }
        }

        private static void ElegantKeyClicked(string buttonName)
        {
            if (!PlayerData.instance.hasWhiteKey)
            {
                PlayerData.instance.hasWhiteKey = true;
                PlayerData.instance.usedWhiteKey = false;
                Console.AddLine("Giving player elegant key");
            }
            else
            {
                PlayerData.instance.hasWhiteKey = false;
                Console.AddLine("Taking away elegant key");
            }
        }

        private static void LoveKeyClicked(string buttonName)
        {
            if (!PlayerData.instance.hasLoveKey)
            {
                PlayerData.instance.hasLoveKey = true;
                Console.AddLine("Giving player love key");
            }
            else
            {
                PlayerData.instance.hasLoveKey = false;
                Console.AddLine("Taking away love key");
            }
        }

        private static void KingsbrandClicked(string buttonName)
        {
            if (!PlayerData.instance.hasKingsBrand)
            {
                PlayerData.instance.hasKingsBrand = true;
                Console.AddLine("Giving player kingsbrand");
            }
            else
            {
                PlayerData.instance.hasKingsBrand = false;
                Console.AddLine("Taking away kingsbrand");
            }
        }

        private static void FlowerClicked(string buttonName)
        {
            if (!PlayerData.instance.hasXunFlower || PlayerData.instance.xunFlowerBroken)
            {
                PlayerData.instance.hasXunFlower = true;
                PlayerData.instance.xunFlowerBroken = false;
                Console.AddLine("Giving player delicate flower");
            }
            else
            {
                PlayerData.instance.hasLantern = false;
                Console.AddLine("Taking away delicate flower");
            }
        }

        private static void ReadDataClicked(string buttonName)
        {
            DreamGate.AddMenu = false;
            DreamGate.DelMenu = false;
            if (DreamGate.DataBusy) return;
            Console.AddLine("Updating DGdata from the file...");
            DreamGate.ReadData(true);
        }

        private static void SaveDataClicked(string buttonName)
        {
            DreamGate.AddMenu = false;
            DreamGate.DelMenu = false;
            if (DreamGate.DataBusy) return;
            Console.AddLine("Writing DGdata to the file...");
            DreamGate.WriteData();
        }

        private static void DeleteItemClicked(string buttonName)
        {
            DreamGate.AddMenu = false;
            DreamGate.DelMenu = !DreamGate.DelMenu;
        }

        private static void AddItemClicked(string buttonName)
        {
            DreamGate.AddMenu = true;
            DreamGate.DelMenu = false;

            var entryName = GUIController.Gm.GetSceneNameString();
            var i = 1;

            if (entryName.Length > 5) entryName = entryName.Substring(0, 5);

            while (DreamGate.DgData.ContainsKey(entryName))
            {
                entryName = GUIController.Gm.GetSceneNameString() + i;
                i++;
            }

            DreamGate.AddEntry(entryName);
        }

        private static void SetWarpClicked(string buttonName)
        {
            var text = _panel.GetPanel("DreamGate Panel").GetButton(buttonName).GetText();

            if (!string.IsNullOrEmpty(text))
            {
                DreamGate.ClickedEntry(text);
            }
        }

        private static void ScrollUpClicked(string buttonName)
        {
            if (DreamGate.ScrollPosition > 0)
            {
                DreamGate.ScrollPosition--;
            }
        }

        private static void ScrollDownClicked(string buttonName)
        {
            if (DreamGate.ScrollPosition + 6 < DreamGate.DgData.Count)
            {
                DreamGate.ScrollPosition++;
            }
        }
    }
}
