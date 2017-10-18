using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PluginManager.Plugin;

namespace SamplePlugin
{
    [OnGameInit]
    public class GUIController : MonoBehaviour
    {
        private static PlayMakerFSM _refKnightSlash;
        private static CameraController _refCamera;
        private static PlayMakerFSM _refDreamNail;

        private static float _loadTime;
        private static float _unloadTime;
        private static bool _loadingChar;

        public static bool InfiniteHp;
        public static bool InfiniteSoul;
        public static bool PlayerInvincible;

        public static bool Noclip;
        public static Vector3 NoclipPos;
        public static bool LevelLoading;

        public void Awake()
        {
            //if (images != null && images.Count > 0) return;
            PlayerInvincible = false;
            InfiniteHp = false;
            InfiniteSoul = false;
            Noclip = false;

            File.WriteAllText(Application.persistentDataPath + "/debug.txt", "Alive");

            //PatchUp.Initialize();

            //ModHooks.ModLog("Initializing debug mod");
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += LevelActivated;
            //UnityEngine.GameObject UIObj = new UnityEngine.GameObject();
            //UIObj.AddComponent<GUIController>();
            //UnityEngine.GameObject.DontDestroyOnLoad(UIObj.transform.root);

            UnityEngine.SceneManagement.SceneManager.sceneUnloaded += Test;
            //ModHooks.Instance.SavegameLoadHook += LoadCharacter;
            //ModHooks.Instance.NewGameHook += NewCharacter;
            //ModHooks.Instance.BeforeSceneLoadHook += OnLevelUnload;

            BossHandler.PopulateBossLists();
            Instance.BuildMenus();

            Console.AddLine("New session started " + DateTime.Now.ToString(CultureInfo.InvariantCulture));

            HazardLocation = PlayerData.instance.hazardRespawnLocation;
            RespawnSceneWatch = PlayerData.instance.respawnScene;
        }

        public void NewCharacter()
        {
            LoadCharacter(0);
        }

        public void LoadCharacter(int saveId)
        {
            Console.Reset();
            EnemiesPanel.Reset();
            DreamGate.Reset();

            LevelLoading = false;

            _loadingChar = true;
        }

        public static string GetSceneName()
        {
            return Gm?.GetSceneNameString();
        }

        public static float GetLoadTime()
        {
            return (float) Math.Round(_loadTime - _unloadTime, 2);
        }

        public static GameManager Gm
        {
            get
            {
                try
                {
                    return GameManager.instance;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static InputHandler Ih => Gm?.inputHandler;

        public static GameObject RefKnight => HeroController.instance?.gameObject;

        public static PlayMakerFSM RefKnightSlash => _refKnightSlash ?? (_refKnightSlash =
                                                         RefKnight.transform.Find("Attacks/Slash")
                                                             .GetComponent<PlayMakerFSM>());

        public static CameraController RefCamera =>
            _refCamera ?? (_refCamera = GameObject.Find("tk2dCamera").GetComponent<CameraController>());

        public static PlayMakerFSM RefDreamNail =>
            _refDreamNail ?? (_refDreamNail = FSMUtility.LocateFSM(RefKnight, "Dream Nail"));

        public void Test(Scene sceneFrom)
        {
            _unloadTime = Time.realtimeSinceStartup;
            if (sceneFrom.name != "Menu_Title") return;
            Console.Reset();
            DreamGate.Reset();
            EnemiesPanel.Reset();
            _loadingChar = true;
        }

        public string OnLevelUnload(string toScene)
        {
            LevelLoading = true;
            _unloadTime = Time.realtimeSinceStartup;

            return toScene;
        }

        public Font TrajanBold;
        public Font TrajanNormal;
        public Font Arial;
        private readonly Dictionary<string, Texture2D> _images = new Dictionary<string, Texture2D>();
        public Vector3 HazardLocation;
        public Vector3 ManualRespawn;
        public bool CameraFollow;
        public string RespawnSceneWatch;

        private GameObject _canvas;
        private static GUIController _instance;
        
        private readonly string[] _imageExtensions = { ".png", ".jpg" };
        private const string AssetFolder = "DebugMod";

        public Texture2D GetImage(string textureName)
        {
            var lcName = textureName.ToLower();

            if (_images.ContainsKey(lcName))
                return _images[lcName];
            
            // attempt to load the image
            if (Directory.Exists(AssetFolder))
                foreach (var extension in _imageExtensions)
            {
                var path = Path.Combine(AssetFolder, $"{textureName}{extension}");

                if (!File.Exists(path)) continue;
                
                var texture = new Texture2D(1, 1);
                texture.LoadImage(File.ReadAllBytes(path));

                _images[lcName] = texture;

                return texture;
            }
            
            ModHooks.ModLog($"Texture not found: {textureName}");
            return new Texture2D(1, 1);
        }

        public void LevelActivated(Scene sceneFrom, Scene sceneTo)
        {
            LevelLoading = false;
            var sceneName = sceneTo.name;

            if (_loadingChar)
            {
                var timeSpan = TimeSpan.FromSeconds(PlayerData.instance.playTime);
                var text = $"{Math.Floor(timeSpan.TotalHours):00}.{timeSpan.Minutes:00}";
                var profileId = PlayerData.instance.profileID;
                var saveFilename = GameManager.instance.GetSaveFilename(profileId);
                var lastWriteTime = File.GetLastWriteTime(Application.persistentDataPath + saveFilename);
                Console.AddLine("New savegame loaded. Profile playtime " + text + " Completion: " +
                                PlayerData.instance.completionPercentage + " Save slot: " + profileId +
                                " Game Version: " + PlayerData.instance.version + " Last Written: " + lastWriteTime);

                Instance.SetMenusActive(true);

                _loadingChar = false;
            }

            if (Gm == null || !Gm)
            {
                return;
            }

            if (Gm.IsGameplayScene())
            {
                _loadTime = Time.realtimeSinceStartup;
                Console.AddLine("New scene loaded: " + sceneName);
                EnemiesPanel.Reset();
                PlayerDeathWatcher.Reset();
                BossHandler.LookForBoss(sceneName);
            }

            if (sceneName == "Menu_Title")
            {
                Instance.SetMenusActive(false);
            }
        }

        public void BuildMenus()
        {
            LoadResources();

            _canvas = new GameObject();
            _canvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            var scaler = _canvas.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            _canvas.AddComponent<GraphicRaycaster>();

            InfoPanel.BuildMenu(_canvas);
            TopMenu.BuildMenu(_canvas);
            EnemiesPanel.BuildMenu(_canvas);
            Console.BuildMenu(_canvas);
            HelpPanel.BuildMenus(_canvas);

            SetMenusActive(false);

            DontDestroyOnLoad(_canvas.transform.root);
        }

        public void SetMenusActive(bool active)
        {
            TopMenu.Visible = active;
            InfoPanel.Visible = active;
            EnemiesPanel.Visible = active;
            Console.Visible = active;
            HelpPanel.Visible = active;
        }

        private void LoadResources()
        {
            foreach (var f in Resources.FindObjectsOfTypeAll<Font>())
            {
                if (f != null && f.name == "TrajanPro-Bold")
                {
                    TrajanBold = f;
                }

                if (f != null && f.name == "TrajanPro-Regular")
                {
                    TrajanNormal = f;
                }

                //Just in case for some reason the computer doesn't have arial
                if (f != null && f.name == "Perpetua")
                {
                    Arial = f;
                }

                foreach (var font in Font.GetOSInstalledFontNames())
                {
                    if (!font.ToLower().Contains("arial")) continue;
                    Arial = Font.CreateDynamicFontFromOSFont(font, 13);
                    break;
                }
            }

            if (TrajanBold == null || TrajanNormal == null || Arial == null)
                ModHooks.ModLog("[DEBUG MOD] Could not find game fonts");

            if (Directory.Exists(AssetFolder))
            {
                foreach (var file in Directory.GetFiles(AssetFolder))
                {
                    if (!_imageExtensions.Contains(Path.GetExtension(file)?.ToLower()))
                    {
                        ModHooks.ModLog($"[DEBUG MOD] Non-image file in asset folder: {Path.GetFileName(file)}");
                        continue;
                    }
                    
                    var textureName = Path.GetFileNameWithoutExtension(file);
                    GetImage(textureName); // load image
                    ModHooks.ModLog($"[DEBUG MOD] Loaded image: {textureName}");
                }
            }
            else
            {
                ModHooks.ModLog("[DEBUG MOD] Could not find asset folder");
            }
        }

        public void Respawn()
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

        public void Update()
        {
            InfoPanel.Update();
            TopMenu.Update();
            EnemiesPanel.Update();
            if (Gm != null) Console.Update();
            HelpPanel.Update();

            if (GetSceneName() == "Menu_Title") return;

            if (InfiniteSoul && PlayerData.instance.MPCharge < 100 && PlayerData.instance.health > 0 &&
                !HeroController.instance.cState.dead && GameManager.instance.IsGameplayScene())
            {
                PlayerData.instance.MPCharge = 100;
            }

            if (Gm != null)
            if (InfiniteHp && !HeroController.instance.cState.dead && Gm.IsGameplayScene() &&
                PlayerData.instance.health < PlayerData.instance.maxHealth)
            {
                var amount = PlayerData.instance.maxHealth - PlayerData.instance.health;
                PlayerData.instance.health = PlayerData.instance.maxHealth;
                HeroController.instance.AddHealth(amount);
            }

            if (PlayerInvincible && PlayerData.instance != null)
            {
                PlayerData.instance.isInvincible = true;
            }

            if (Noclip)
            {
                if (Ih.inputActions.left.IsPressed)
                {
                    NoclipPos = new Vector3(NoclipPos.x - Time.deltaTime * 20f, NoclipPos.y, NoclipPos.z);
                }

                if (Ih.inputActions.right.IsPressed)
                {
                    NoclipPos = new Vector3(NoclipPos.x + Time.deltaTime * 20f, NoclipPos.y, NoclipPos.z);
                }

                if (Ih.inputActions.up.IsPressed)
                {
                    NoclipPos = new Vector3(NoclipPos.x, NoclipPos.y + Time.deltaTime * 20f, NoclipPos.z);
                }

                if (Ih.inputActions.down.IsPressed)
                {
                    NoclipPos = new Vector3(NoclipPos.x, NoclipPos.y - Time.deltaTime * 20f, NoclipPos.z);
                }

                if (HeroController.instance.transitionState.ToString() == "WAITING_TO_TRANSITION")
                {
                    RefKnight.transform.position = NoclipPos;
                }
                else
                {
                    NoclipPos = RefKnight.transform.position;
                }
            }

            if (Gm != null && Input.GetKeyUp(KeyCode.Escape) && Gm.IsGamePaused())
            {
                UIManager.instance.TogglePauseGame();
            }

            if (Input.GetKeyUp(KeyCode.Equals))
            {
                var num = 4;
                if (PlayerData.instance != null && PlayerData.instance.nailDamage == 0)
                {
                    num = 5;
                }
                if (PlayerData.instance != null) PlayerData.instance.nailDamage = PlayerData.instance.nailDamage + num;
                PlayMakerFSM.BroadcastEvent("UPDATE NAIL DAMAGE");
                Console.AddLine("Increased base nailDamage by " + num);
            }
            if (Input.GetKeyUp(KeyCode.Minus))
            {
                if (PlayerData.instance != null)
                {
                    var num2 = PlayerData.instance.nailDamage - 4;
                    if (num2 >= 0)
                    {
                        PlayerData.instance.nailDamage = num2;
                        PlayMakerFSM.BroadcastEvent("UPDATE NAIL DAMAGE");
                        Console.AddLine("Decreased base nailDamage by 4");
                    }
                    else
                    {
                        Console.AddLine("Cannot set base nailDamage less than 0 therefore forcing 0 value");
                        PlayerData.instance.nailDamage = 0;
                        PlayMakerFSM.BroadcastEvent("UPDATE NAIL DAMAGE");
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.BackQuote))
            {
                HelpPanel.Visible = !HelpPanel.Visible;
            }
            if (Input.GetKeyUp(KeyCode.F1))
            {
                SetMenusActive(!(HelpPanel.Visible || InfoPanel.Visible || EnemiesPanel.Visible ||
                                 TopMenu.Visible || Console.Visible));
            }

            if (EnemiesPanel.Visible)
            {
                EnemiesPanel.RefreshEnemyList();
            }
            if (Input.GetKeyUp(KeyCode.F2))
            {
                InfoPanel.Visible = !InfoPanel.Visible;
            }
            if (Input.GetKeyUp(KeyCode.F3))
            {
                TopMenu.Visible = !TopMenu.Visible;
            }
            if (Input.GetKeyUp(KeyCode.F4))
            {
                Console.Visible = !Console.Visible;
            }
            if (Gm != null && Input.GetKeyUp(KeyCode.F5))
            {
                if (PlayerData.instance.disablePause && GetSceneName() != "Menu_Title" && Gm.IsGameplayScene() &&
                    !HeroController.instance.cState.recoiling)
                {
                    PlayerData.instance.disablePause = false;
                    UIManager.instance.TogglePauseGame();
                    Console.AddLine("Forcing Pause Menu because pause is disabled");
                }
                else
                {
                    Console.AddLine("Game does not report that Pause is disabled, requesting it normally.");
                    UIManager.instance.TogglePauseGame();
                }
            }
            if (Input.GetKeyUp(KeyCode.F6))
            {
                Respawn();
            }
            if (Input.GetKeyUp(KeyCode.F7))
            {
                ManualRespawn = RefKnight.transform.position;
                HeroController.instance.SetHazardRespawn(ManualRespawn, false);
                Console.AddLine("Manual respawn point on this map set to" + ManualRespawn);
            }
            if (Input.GetKeyUp(KeyCode.F8))
            {
                var text = RefCamera.mode.ToString();
                if (!CameraFollow && text != "FOLLOWING")
                {
                    Console.AddLine("Setting Camera Mode to FOLLOW. Previous mode: " + text);
                    CameraFollow = true;
                }
                else if (CameraFollow)
                {
                    CameraFollow = false;
                    Console.AddLine("Camera Mode is no longer forced");
                }
            }
            if (Input.GetKeyUp(KeyCode.F9))
            {
                EnemiesPanel.Visible = !EnemiesPanel.Visible;
                if (EnemiesPanel.Visible)
                {
                    EnemiesPanel.EnemyUpdate(200f);
                }
            }
            if (Input.GetKeyUp(KeyCode.Insert))
            {
                HeroController.instance.vignette.enabled = !HeroController.instance.vignette.enabled;
            }
            if (Input.GetKeyUp(KeyCode.Home))
            {
                var go = RefKnight.transform.Find("HeroLight").gameObject;
                var color = go.GetComponent<SpriteRenderer>().color;
                if (color.a < 0f || color.a > 0f)
                {
                    color.a = 0f;
                    go.GetComponent<SpriteRenderer>().color = color;
                    Console.AddLine("Rendering HeroLight invisible...");
                }
                else
                {
                    color.a = 0.7f;
                    go.GetComponent<SpriteRenderer>().color = color;
                    Console.AddLine("Rendering HeroLight visible...");
                }
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyUp(KeyCode.Home))
            {
                RefKnight.transform.Find("HeroLight").gameObject.SetActive(false);
                Console.AddLine("Object HeroLight DISABLED until reload!");
            }
            if (Input.GetKeyUp(KeyCode.Delete))
            {
                if (GameCameras.instance.hudCanvas.gameObject.activeInHierarchy)
                {
                    GameCameras.instance.hudCanvas.gameObject.SetActive(false);
                    Console.AddLine("Disabling HUD...");
                }
                else
                {
                    GameCameras.instance.hudCanvas.gameObject.SetActive(true);
                    Console.AddLine("Enabling HUD...");
                }
            }
            if (Input.GetKeyUp(KeyCode.End))
            {
                GameCameras.instance.tk2dCam.ZoomFactor = 1f;
                Console.AddLine("Zoom factor was reset");
            }
            if (Input.GetKeyUp(KeyCode.PageUp))
            {
                var zoomFactor = GameCameras.instance.tk2dCam.ZoomFactor;
                GameCameras.instance.tk2dCam.ZoomFactor = zoomFactor + zoomFactor * 0.05f;
                Console.AddLine("Zoom level increased to: " + GameCameras.instance.tk2dCam.ZoomFactor);
            }
            if (Input.GetKeyUp(KeyCode.PageDown))
            {
                var zoomFactor2 = GameCameras.instance.tk2dCam.ZoomFactor;
                GameCameras.instance.tk2dCam.ZoomFactor = zoomFactor2 - zoomFactor2 * 0.05f;
                Console.AddLine("Zoom level decreased to: " + GameCameras.instance.tk2dCam.ZoomFactor);
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyUp(KeyCode.PageUp))
            {
                var zoomFactor3 = GameCameras.instance.tk2dCam.ZoomFactor;
                GameCameras.instance.tk2dCam.ZoomFactor = zoomFactor3 + zoomFactor3 * 0.2f;
                Console.AddLine("Zoom level increased to: " + GameCameras.instance.tk2dCam.ZoomFactor);
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyUp(KeyCode.PageDown))
            {
                var zoomFactor4 = GameCameras.instance.tk2dCam.ZoomFactor;
                GameCameras.instance.tk2dCam.ZoomFactor = zoomFactor4 - zoomFactor4 * 0.2f;
                Console.AddLine("Zoom level decreased to: " + GameCameras.instance.tk2dCam.ZoomFactor);
            }
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyUp(KeyCode.PageUp))
            {
                GameCameras.instance.tk2dCam.ZoomFactor = GameCameras.instance.tk2dCam.ZoomFactor + 0.05f;
                Console.AddLine("Zoom level increased to: " + GameCameras.instance.tk2dCam.ZoomFactor);
            }
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyUp(KeyCode.PageDown))
            {
                GameCameras.instance.tk2dCam.ZoomFactor = GameCameras.instance.tk2dCam.ZoomFactor - 0.05f;
                Console.AddLine("Zoom level decreased to: " + GameCameras.instance.tk2dCam.ZoomFactor);
            }
            if (Input.GetKeyUp(KeyCode.Backspace))
            {
                var component = RefKnight.GetComponent<tk2dSprite>();
                var color2 = component.color;
                if (color2.a < 0f || color2.a > 0f)
                {
                    color2.a = 0f;
                    component.color = color2;
                    Console.AddLine("Rendering Hero sprite invisible...");
                }
                else
                {
                    color2.a = 1f;
                    component.color = color2;
                    Console.AddLine("Rendering Hero sprite visible...");
                }
            }
            if (Input.GetKeyUp(KeyCode.KeypadMinus))
            {
                var timeScale = Time.timeScale;
                var num3 = timeScale - 0.1f;
                if (num3 > 0f)
                {
                    Time.timeScale = num3;
                    Console.AddLine(string.Concat("New TimeScale value: ", num3, " Old value: ", timeScale));
                }
                else
                {
                    Console.AddLine("Cannot set TimeScale equal or lower than 0");
                }
            }
            if (Input.GetKeyUp(KeyCode.KeypadPlus))
            {
                var timeScale2 = Time.timeScale;
                var num4 = timeScale2 + 0.1f;
                if (num4 < 2f)
                {
                    Time.timeScale = num4;
                    Console.AddLine(string.Concat("New TimeScale value: ", num4, " Old value: ", timeScale2));
                }
                else
                {
                    Console.AddLine("Cannot set TimeScale greater than 2.0");
                }
            }
            if (CameraFollow && RefCamera.mode != CameraController.CameraMode.FOLLOWING)
            {
                RefCamera.SetMode(CameraController.CameraMode.FOLLOWING);
            }

            if (PlayerDeathWatcher.PlayerDied())
            {
                PlayerDeathWatcher.LogDeathDetails();
            }

            if (PlayerData.instance.hazardRespawnLocation != HazardLocation)
            {
                HazardLocation = PlayerData.instance.hazardRespawnLocation;
                Console.AddLine("Hazard Respawn location updated: " + HazardLocation);

                if (EnemiesPanel.Visible)
                    EnemiesPanel.RefreshEnemyList();
            }
            if (string.IsNullOrEmpty(RespawnSceneWatch) ||
                RespawnSceneWatch == PlayerData.instance.respawnScene) return;
            RespawnSceneWatch = PlayerData.instance.respawnScene;
            Console.AddLine(string.Concat("Save Respawn updated, new scene: ", PlayerData.instance.respawnScene,
                ", Map Zone: ", GameManager.instance.GetCurrentMapZone(), ", Respawn Marker: ",
                PlayerData.instance.respawnMarkerName));
        }

        public static GUIController Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = FindObjectOfType<GUIController>();
                return _instance;
            }
        }
    }
}
