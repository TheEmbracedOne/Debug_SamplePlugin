using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GlobalEnums;

namespace SamplePlugin
{
    public static class EnemiesPanel
    {
        private static CanvasPanel _panel;
        private static bool _autoUpdate;
        private static float _lastTime;
        private static readonly List<EnemyData> EnemyPool = new List<EnemyData>();
        private static GameObject _parent;
        private static bool _hpBars;
        private static bool _hitboxes;

        public static bool Visible;

        public static void BuildMenu(GameObject canvas)
        {
            _parent = canvas;

            _panel = new CanvasPanel(canvas, new Texture2D(1, 1),
                new Vector2(1920f - GUIController.Instance.GetImage("EnemiesPBg").width, 481f), Vector2.zero,
                new Rect(0, 0, 1, 1));

            _panel.AddText("Panel Label", "Enemies", new Vector2(125f, -25f), Vector2.zero,
                GUIController.Instance.TrajanBold, 30);

            _panel.AddText("Enemy Names", "", new Vector2(90f, 20f), Vector2.zero, GUIController.Instance.Arial);
            _panel.AddText("Enemy HP", "", new Vector2(300f, 20f), Vector2.zero, GUIController.Instance.Arial);

            _panel.AddPanel("Pause", GUIController.Instance.GetImage("EnemiesPBg"), Vector2.zero, Vector2.zero,
                new Rect(0, 0, GUIController.Instance.GetImage("EnemiesPBg").width,
                    GUIController.Instance.GetImage("EnemiesPBg").height));
            _panel.AddPanel("Play", GUIController.Instance.GetImage("EnemiesBg"), new Vector2(57f, 0f), Vector2.zero,
                new Rect(0f, 0f, GUIController.Instance.GetImage("EnemiesBg").width,
                    GUIController.Instance.GetImage("EnemiesBg").height));

            for (var i = 1; i <= 14; i++)
            {
                _panel.GetPanel("Pause").AddButton("Del" + i, GUIController.Instance.GetImage("ButtonDel"),
                    new Vector2(20f, 20f + (i - 1) * 15f), new Vector2(12f, 12f), DelClicked,
                    new Rect(0, 0, GUIController.Instance.GetImage("ButtonDel").width,
                        GUIController.Instance.GetImage("ButtonDel").height));
                _panel.GetPanel("Pause").AddButton("Clone" + i, GUIController.Instance.GetImage("ButtonPlus"),
                    new Vector2(40f, 20f + (i - 1) * 15f), new Vector2(12f, 12f), CloneClicked,
                    new Rect(0, 0, GUIController.Instance.GetImage("ButtonPlus").width,
                        GUIController.Instance.GetImage("ButtonPlus").height));
                _panel.GetPanel("Pause").AddButton("Inf" + i, GUIController.Instance.GetImage("ButtonInf"),
                    new Vector2(60f, 20f + (i - 1) * 15f), new Vector2(12f, 12f), InfClicked,
                    new Rect(0, 0, GUIController.Instance.GetImage("ButtonInf").width,
                        GUIController.Instance.GetImage("ButtonInf").height));
            }

            _panel.GetPanel("Pause").AddButton("Collision", GUIController.Instance.GetImage("ButtonRect"),
                new Vector2(30f, 250f), Vector2.zero, CollisionClicked,
                new Rect(0, 0, GUIController.Instance.GetImage("ButtonRect").width,
                    GUIController.Instance.GetImage("ButtonRect").height), GUIController.Instance.TrajanBold,
                "Collision");
            _panel.GetPanel("Pause").AddButton("HP Bars", GUIController.Instance.GetImage("ButtonRect"),
                new Vector2(125f, 250f), Vector2.zero, HpBarsClicked,
                new Rect(0, 0, GUIController.Instance.GetImage("ButtonRect").width,
                    GUIController.Instance.GetImage("ButtonRect").height), GUIController.Instance.TrajanBold, "HP Bars");
            _panel.GetPanel("Pause").AddButton("Auto", GUIController.Instance.GetImage("ButtonRect"),
                new Vector2(220f, 250f), Vector2.zero, AutoClicked,
                new Rect(0, 0, GUIController.Instance.GetImage("ButtonRect").width,
                    GUIController.Instance.GetImage("ButtonRect").height), GUIController.Instance.TrajanBold, "Auto");
            _panel.GetPanel("Pause").AddButton("Scan", GUIController.Instance.GetImage("ButtonRect"),
                new Vector2(315f, 250f), Vector2.zero, ScanClicked,
                new Rect(0, 0, GUIController.Instance.GetImage("ButtonRect").width,
                    GUIController.Instance.GetImage("ButtonRect").height), GUIController.Instance.TrajanBold, "Scan");

            _panel.FixRenderOrder();
        }

        private static void DelClicked(string buttonName)
        {
            var num = Convert.ToInt32(buttonName.Substring(3));
            var dat = EnemyPool.FindAll(ed => ed.GameObject != null && ed.GameObject.activeSelf)[num - 1];

            Console.AddLine("Destroying enemy: " + dat.GameObject.name);
            UnityEngine.Object.DestroyImmediate(dat.GameObject);
        }

        private static void CloneClicked(string buttonName)
        {
            var num = Convert.ToInt32(buttonName.Substring(5));
            var dat = EnemyPool.FindAll(ed => ed.GameObject != null && ed.GameObject.activeSelf)[num - 1];

            var gameObject2 = UnityEngine.Object.Instantiate(dat.GameObject, dat.GameObject.transform.position,
                dat.GameObject.transform.rotation) as GameObject;
            if (gameObject2 != null)
            {
                Component component = gameObject2.GetComponent<tk2dSprite>();
                var playMakerFsm2 = FSMUtility.LocateFSM(gameObject2, dat.Fsm.FsmName);
                var value8 = playMakerFsm2.FsmVariables.GetFsmInt("HP").Value;
                EnemyPool.Add(new EnemyData(value8, playMakerFsm2, component, _parent, gameObject2));
            }
            if (gameObject2 != null) Console.AddLine("Cloning enemy as: " + gameObject2.name);
        }

        private static void InfClicked(string buttonName)
        {
            var num = Convert.ToInt32(buttonName.Substring(3));
            var dat = EnemyPool.FindAll(ed => ed.GameObject != null && ed.GameObject.activeSelf)[num - 1];

            dat.SetHp(9999);
            Console.AddLine("HP for enemy: " + dat.GameObject.name + " is now 9999");
        }

        private static void CollisionClicked(string buttonName)
        {
            _hitboxes = !_hitboxes;

            Console.AddLine(_hitboxes ? "Enabled hitboxes" : "Disabled hitboxes");
        }

        private static void HpBarsClicked(string buttonName)
        {
            _hpBars = !_hpBars;

            Console.AddLine(_hpBars ? "Enabled HP bars" : "Disabled HP bars");
        }

        private static void AutoClicked(string buttonName)
        {
            _autoUpdate = !_autoUpdate;

            Console.AddLine(_autoUpdate ? "Enabled auto-scan (May impact performance)" : "Disabled auto-scan");
        }

        private static void ScanClicked(string buttonName)
        {
            EnemyUpdate(200f);
            Console.AddLine("Refreshing collider data...");
        }

        private static UIManager UI
        {
            get
            {
                try
                {
                    return UIManager.instance;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static void Update()
        {
            if (_panel == null)
            {
                return;
            }

            if (UI == null)
                return;

            if (Visible && !_panel.Active)
            {
                _panel.SetActive(true, false);
            }
            else if (!Visible && _panel.Active)
            {
                _panel.SetActive(false, true);
            }

            if (Visible && UI?.uiState == UIState.PLAYING &&
                (_panel.GetPanel("Pause").Active || !_panel.GetPanel("Play").Active))
            {
                _panel.GetPanel("Pause").SetActive(false, true);
                _panel.GetPanel("Play").SetActive(true, false);
            }
            else if (Visible && UI?.uiState == UIState.PAUSED &&
                     (!_panel.GetPanel("Pause").Active || _panel.GetPanel("Play").Active))
            {
                _panel.GetPanel("Pause").SetActive(true, false);
                _panel.GetPanel("Play").SetActive(false, true);


                if (!_panel.Active && EnemyPool.Count > 0)
                {
                    Reset();
                }
            }

            if (!_panel.Active) return;
            if (Input.GetKeyUp(KeyCode.F10))
            {
                SelfDamage();
            }

            CheckForAutoUpdate();

            var enemyNames = "";
            var enemyHp = "";
            var enemyCount = 0;

            for (var i = 0; i < EnemyPool.Count; i++)
            {
                var dat = EnemyPool[i];
                var obj = dat.GameObject;

                if (obj == null || !obj.activeSelf)
                {
                    if (obj == null)
                    {
                        dat.HpBar.Destroy();
                        dat.Hitbox.Destroy();
                        EnemyPool.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        dat.HpBar.SetPosition(new Vector2(-1000f, -1000f));
                    }
                }
                else
                {
                    var hp = dat.Fsm.FsmVariables.GetFsmInt("HP").Value;

                    if (hp != dat.Hp)
                    {
                        dat.SetHp(hp);

                        if (_hpBars)
                        {
                            var tex = new Texture2D(120, 40);

                            for (var x = 0; x < 120; x++)
                            {
                                for (var y = 0; y < 40; y++)
                                {
                                    if (x < 3 || x > 116 || y < 3 || y > 36)
                                    {
                                        tex.SetPixel(x, y, Color.black);
                                    }
                                    else
                                    {
                                        tex.SetPixel(x, y,
                                            hp / (float) dat.MaxHp >= (x - 2f) / 117f
                                                ? Color.red
                                                : new Color(255f, 255f, 255f, 0f));
                                    }
                                }
                            }

                            tex.Apply();

                            dat.HpBar.UpdateBackground(tex, new Rect(0, 0, 120, 40));
                        }
                    }

                    if (_hitboxes)
                    {
                        if ((dat.Spr as tk2dSprite).boxCollider2D != null)
                        {
                            var boxCollider2D = (dat.Spr as tk2dSprite).boxCollider2D;
                            var bounds2 = boxCollider2D.bounds;

                            var width = Math.Abs(bounds2.max.x - bounds2.min.x);
                            var height = Math.Abs(bounds2.max.y - bounds2.min.y);
                            Vector2 position = Camera.main.WorldToScreenPoint(
                                boxCollider2D.transform.position + new Vector3(boxCollider2D.offset.x,
                                    boxCollider2D.offset.y, 0f));
                            Vector2 size =
                                Camera.main.WorldToScreenPoint(
                                    boxCollider2D.transform.position + new Vector3(width, height, 0));
                            size -= position;

                            var rot = boxCollider2D.transform.rotation;
                            rot.eulerAngles = new Vector3(Mathf.Round(rot.eulerAngles.x / 90) * 90,
                                Mathf.Round(rot.eulerAngles.y / 90) * 90, Mathf.Round(rot.eulerAngles.z / 90) * 90);
                            Vector2 pivot = Camera.main.WorldToScreenPoint(boxCollider2D.transform.position);
                            Vector2 pointA =
                                Camera.main.WorldToScreenPoint(
                                    (Vector2) boxCollider2D.transform.position + boxCollider2D.offset);
                            pointA.x -= size.x / 2f;
                            pointA.y -= size.y / 2f;
                            var pointB = pointA + size;

                            pointA = (Vector2) (rot * (pointA - pivot)) + pivot;
                            pointB = (Vector2) (rot * (pointB - pivot)) + pivot;

                            position = new Vector2(pointA.x < pointB.x ? pointA.x : pointB.x,
                                pointA.y > pointB.y ? pointA.y : pointB.y);
                            size = new Vector2(Math.Abs(pointA.x - pointB.x), Math.Abs(pointA.y - pointB.y));

                            size.x *= 1920f / Screen.width;
                            size.y *= 1080f / Screen.height;

                            position.x *= 1920f / Screen.width;
                            position.y *= 1080f / Screen.height;
                            position.y = 1080f - position.y;

                            dat.Hitbox.SetPosition(position);
                            dat.Hitbox.ResizeBg(size);
                        }

                        if (!dat.Hitbox.Active)
                        {
                            dat.Hitbox.SetActive(true, true);
                        }
                    }

                    if (_hpBars)
                    {
                        Vector2 enemyPos = Camera.main.WorldToScreenPoint(obj.transform.position);
                        enemyPos.x *= 1920f / Screen.width;
                        enemyPos.y *= 1080f / Screen.height;

                        enemyPos.y = 1080f - enemyPos.y;

                        var bounds = (dat.Spr as tk2dSprite).GetBounds();
                        enemyPos.y -= (Camera.main.WorldToScreenPoint(bounds.max).y * (1080f / Screen.height) -
                                       Camera.main.WorldToScreenPoint(bounds.min).y * (1080f / Screen.height)) / 2f;
                        enemyPos.x -= 60;

                        dat.HpBar.SetPosition(enemyPos);
                        dat.HpBar.GetText("HP")
                            .UpdateText(dat.Fsm.FsmVariables.GetFsmInt("HP").Value + "/" + dat.MaxHp);

                        if (!dat.HpBar.Active)
                        {
                            dat.HpBar.SetActive(true, true);
                        }
                    }

                    if (!_hpBars && dat.HpBar.Active)
                    {
                        dat.HpBar.SetActive(false, true);
                    }

                    if (!_hitboxes && dat.Hitbox.Active)
                    {
                        dat.Hitbox.SetActive(false, true);
                    }

                    if (++enemyCount > 14) continue;
                    enemyNames += obj.name + "\n";
                    enemyHp += dat.Fsm.FsmVariables.GetFsmInt("HP").Value + "/" + dat.MaxHp + "\n";
                }
            }

            if (_panel.GetPanel("Pause").Active)
            {
                for (var i = 1; i <= 14; i++)
                {
                    if (i <= enemyCount)
                    {
                        _panel.GetPanel("Pause").GetButton("Del" + i).SetActive(true);
                        _panel.GetPanel("Pause").GetButton("Clone" + i).SetActive(true);
                        _panel.GetPanel("Pause").GetButton("Inf" + i).SetActive(true);
                    }
                    else
                    {
                        _panel.GetPanel("Pause").GetButton("Del" + i).SetActive(false);
                        _panel.GetPanel("Pause").GetButton("Clone" + i).SetActive(false);
                        _panel.GetPanel("Pause").GetButton("Inf" + i).SetActive(false);
                    }
                }

                _panel.GetPanel("Pause").GetButton("Collision")
                    .SetTextColor(_hitboxes ? new Color(244f / 255f, 127f / 255f, 32f / 255f) : Color.white);
                _panel.GetPanel("Pause").GetButton("HP Bars")
                    .SetTextColor(_hpBars ? new Color(244f / 255f, 127f / 255f, 32f / 255f) : Color.white);
                _panel.GetPanel("Pause").GetButton("Auto")
                    .SetTextColor(_autoUpdate ? new Color(244f / 255f, 127f / 255f, 32f / 255f) : Color.white);
            }

            if (enemyCount > 14)
            {
                enemyNames += "And " + (enemyCount - 14) + " more";
            }

            _panel.GetText("Enemy Names").UpdateText(enemyNames);
            _panel.GetText("Enemy HP").UpdateText(enemyHp);
        }

        public static void Reset()
        {
            foreach (var dat in EnemyPool)
            {
                dat.Hitbox.Destroy();
                dat.HpBar.Destroy();
            }
            EnemyPool.Clear();
        }

        public static bool Ignore(string name)
        {
            if (name.IndexOf("Hornet Barb", StringComparison.OrdinalIgnoreCase) >= 0) return true;
            if (name.IndexOf("Needle Tink", StringComparison.OrdinalIgnoreCase) >= 0) return true;
            if (name.IndexOf("worm", StringComparison.OrdinalIgnoreCase) >= 0) return true;
            if (name.IndexOf("Laser Turret", StringComparison.OrdinalIgnoreCase) >= 0) return true;
            return name.IndexOf("Deep Spikes", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static void RefreshEnemyList()
        {
            if (!Visible) return;
            var rootGameObjects = UnityEngine.SceneManagement.SceneManager.GetSceneByName(GUIController.GetSceneName())
                .GetRootGameObjects();
            if (rootGameObjects != null)
            {
                foreach (var gameObject in rootGameObjects)
                {
                    if ((gameObject.layer == 11 || gameObject.layer == 17) && !Ignore(gameObject.name))
                    {
                        var playMakerFsm = FSMUtility.LocateFSM(gameObject, "health_manager_enemy");
                        Component component = gameObject.GetComponent<tk2dSprite>();
                        if (playMakerFsm == null)
                        {
                            playMakerFsm = FSMUtility.LocateFSM(gameObject, "health_manager");
                        }
                        var num3 = gameObject.name.IndexOf("grass", StringComparison.OrdinalIgnoreCase);
                        var num2 = gameObject.name.IndexOf("hopper", StringComparison.OrdinalIgnoreCase);
                        if (num3 >= 0 && num2 >= 0)
                        {
                            component = gameObject.transform.FindChild("Sprite").gameObject.gameObject
                                .GetComponent<tk2dSprite>();
                        }
                        if (playMakerFsm != null)
                        {
                            var value = playMakerFsm.FsmVariables.GetFsmInt("HP").Value;
                            EnemyPool.Add(new EnemyData(value, playMakerFsm, component, _parent, gameObject));
                        }
                    }
                    EnemyDescendants(gameObject.transform);
                }
            }
            if (EnemyPool.Count > 0)
            {
                Console.AddLine("Enemy data filled, entries added: " + EnemyPool.Count);
            }
            EnemyUpdate(200f);
        }

        private static void SelfDamage()
        {
            if (PlayerData.instance.health <= 0 || HeroController.instance.cState.dead ||
                !GameManager.instance.IsGameplayScene() || GameManager.instance.IsGamePaused() ||
                HeroController.instance.cState.recoiling || HeroController.instance.cState.invulnerable)
            {
                Console.AddLine(string.Concat("Unacceptable conditions for selfDamage(",
                    PlayerData.instance.health.ToString(), ",", HeroController.instance.cState.dead.ToString(), ",",
                    GameManager.instance.IsGameplayScene().ToString(), ",",
                    HeroController.instance.cState.recoiling.ToString(), ",",
                    GameManager.instance.IsGamePaused().ToString(), ",",
                    HeroController.instance.cState.invulnerable.ToString(), ").", " Pressed too many times at once?"));
                return;
            }
            if (!Visible)
            {
                Console.AddLine("Enable EnemyPanel for self-damage");
                return;
            }
            if (EnemyPool.Count < 1)
            {
                Console.AddLine("Unable to locate a single enemy in the scene.");
                return;
            }
            var random = new System.Random();
            if (HeroController.instance.cState.facingRight)
            {
                HeroController.instance.TakeDamage(EnemyPool.ElementAt(random.Next(0, EnemyPool.Count)).GameObject,
                    CollisionSide.right);
                Console.AddLine("Attempting self-damage, right side");
                return;
            }
            HeroController.instance.TakeDamage(EnemyPool.ElementAt(random.Next(0, EnemyPool.Count)).GameObject,
                CollisionSide.left);
            Console.AddLine("Attempting self-damage, left side");
        }

        private static void CheckForAutoUpdate()
        {
            if (!_autoUpdate) return;

            var deltaTime = Time.realtimeSinceStartup - _lastTime;

            if (!(deltaTime >= 2f)) return;
            _lastTime = Time.realtimeSinceStartup;
            EnemyUpdate(0f);
        }

        public static void EnemyUpdate(float boxSize)
        {
            if (_autoUpdate)
            {
                boxSize = 50f;
            }

            if (Visible && HeroController.instance != null && !HeroController.instance.cState.transitioning &&
                GUIController.Gm.IsGameplayScene())
            {
                var count = EnemyPool.Count;
                const int layerMask = 133120;
                var array = Physics2D.OverlapBoxAll(GUIController.RefKnight.transform.position,
                    new Vector2(boxSize, boxSize), 1f, layerMask);
                if (array == null) return;
                foreach (var col in array)
                {
                    var playMakerFsm = FSMUtility.LocateFSM(col.gameObject, "health_manager_enemy");
                    if (playMakerFsm == null)
                    {
                        FSMUtility.LocateFSM(col.gameObject, "health_manager");
                    }
                    if (!playMakerFsm || EnemyPool.Any(ed => ed.GameObject == col.gameObject) ||
                        Ignore(col.gameObject.name)) continue;
                    Component component = col.gameObject.GetComponent<tk2dSprite>();
                    var value = playMakerFsm.FsmVariables.GetFsmInt("HP").Value;
                    EnemyPool.Add(new EnemyData(value, playMakerFsm, component, _parent, col.gameObject));
                }
                if (EnemyPool.Count <= count) return;
                Console.AddLine("EnemyList updated: +" + (EnemyPool.Count - count));
            }
            else if (_autoUpdate &&
                     (!Visible || !GameManager.instance.IsGameplayScene() || HeroController.instance == null))
            {
                _autoUpdate = false;
                Console.AddLine("Cancelling enemy auto-scan due to weird conditions");
            }
        }

        private static void EnemyDescendants(Transform transform)
        {
            var list = new List<Transform>();
            foreach (var obj in transform)
            {
                var transform2 = (Transform) obj;
                if ((transform2.gameObject.layer == 11 || transform2.gameObject.layer == 17) &&
                    EnemyPool.All(ed => ed.GameObject != transform2.gameObject) && !Ignore(transform2.gameObject.name))
                {
                    var playMakerFsm = FSMUtility.LocateFSM(transform2.gameObject, "health_manager_enemy") ??
                                       FSMUtility.LocateFSM(GUIController.Instance.gameObject, "health_manager");
                    Component component = transform2.gameObject.GetComponent<tk2dSprite>();
                    if (playMakerFsm)
                    {
                        var value = playMakerFsm.FsmVariables.GetFsmInt("HP").Value;
                        EnemyPool.Add(new EnemyData(value, playMakerFsm, component, _parent, transform2.gameObject));
                    }
                }
                list.Add(transform2);
            }
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i].childCount <= 0) continue;
                foreach (var obj2 in list[i])
                {
                    var transform3 = (Transform) obj2;
                    if ((transform3.gameObject.layer == 11 || transform3.gameObject.layer == 17) &&
                        EnemyPool.All(ed => ed.GameObject != transform3.gameObject) &&
                        !Ignore(transform3.gameObject.name))
                    {
                        var playMakerFsm2 = FSMUtility.LocateFSM(transform3.gameObject, "health_manager_enemy") ??
                                            FSMUtility.LocateFSM(GUIController.Instance.gameObject, "health_manager");
                        Component component2 = transform3.gameObject.GetComponent<tk2dSprite>();
                        if (playMakerFsm2)
                        {
                            var value2 = playMakerFsm2.FsmVariables.GetFsmInt("HP").Value;
                            EnemyPool.Add(new EnemyData(value2, playMakerFsm2, component2, _parent,
                                transform3.gameObject));
                        }
                    }
                    list.Add(transform3);
                }
            }
        }
    }
}