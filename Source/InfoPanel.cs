using System.Globalization;
using UnityEngine;
using InControl;

namespace SamplePlugin
{
    public static class InfoPanel
    {
        private static CanvasPanel _panel;

        public static bool Visible;

        public static void BuildMenu(GameObject canvas)
        {
            _panel = new CanvasPanel(canvas, GUIController.Instance.GetImage("StatusPanelBG"), new Vector2(0f, 223f), Vector2.zero, new Rect(0f, 0f, GUIController.Instance.GetImage("StatusPanelBG").width, GUIController.Instance.GetImage("StatusPanelBG").height));

            //Labels
            _panel.AddText("Hero State Label", "Hero State", new Vector2(10f, 20f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Velocity Label", "Velocity", new Vector2(10f, 40f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Nail Damage Label", "Naildmg", new Vector2(10f, 60f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("HP Label", "HP", new Vector2(10f, 80f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("MP Label", "MP", new Vector2(10f, 100f), Vector2.zero, GUIController.Instance.Arial, 15);

            _panel.AddText("Completion Label", "Completion", new Vector2(10f, 178f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Grubs Label", "Grubs", new Vector2(10f, 198f), Vector2.zero, GUIController.Instance.Arial, 15);
            
            _panel.AddText("isInvuln Label", "isInvuln", new Vector2(10f, 276f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Invincible Label", "Invincible", new Vector2(10f, 296f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Invincitest Label", "invinciTest", new Vector2(10f, 316f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Damage State Label", "Damage State", new Vector2(10f, 336f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Dead State Label", "Dead State", new Vector2(10f, 356f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Hazard Death Label", "Hazard Death", new Vector2(10f, 376f), Vector2.zero, GUIController.Instance.Arial, 15);

            _panel.AddText("Scene Name Label", "Scene Name", new Vector2(10f, 454), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Transition Label", "Transition", new Vector2(10f, 474f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Trans State Label", "Trans State", new Vector2(10f, 494f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("is Gameplay Label", "Is Gameplay", new Vector2(10f, 514f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Game State Label", "Game State", new Vector2(10f, 534f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("UI State Label", "UI State", new Vector2(10f, 554f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Hero Paused Label", "Hero Paused", new Vector2(10f, 574f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Camera Mode Label", "Camera Mode", new Vector2(10f, 594f), Vector2.zero, GUIController.Instance.Arial, 15);

            _panel.AddText("Accept Input Label", "Accept Input", new Vector2(300f, 30f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Relinquished Label", "Relinquished", new Vector2(300f, 50f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("atBench Label", "atBench", new Vector2(300f, 70f), Vector2.zero, GUIController.Instance.Arial, 15);

            _panel.AddText("Dashing Label", "Dashing", new Vector2(300f, 120f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Jumping Label", "Jumping", new Vector2(300f, 140f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Superdashing Label", "Superdashing", new Vector2(300f, 160f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Falling Label", "Falling", new Vector2(300f, 180f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Hardland Label", "Hardland", new Vector2(300f, 200f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Swimming Label", "Swimming", new Vector2(300f, 220f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Recoiling Label", "Recoiling", new Vector2(300f, 240f), Vector2.zero, GUIController.Instance.Arial, 15);

            _panel.AddText("Wall lock Label", "Wall lock", new Vector2(300f, 290f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Wall jumping Label", "Wall jumping", new Vector2(300f, 310f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Wall touching Label", "Wall touching", new Vector2(300f, 330f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Wall sliding Label", "Wall sliding", new Vector2(300f, 350f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Wall left Label", "Wall left", new Vector2(300f, 370f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("Wall right Label", "Wall right", new Vector2(300f, 390f), Vector2.zero, GUIController.Instance.Arial, 15);

            _panel.AddText("Attacking Label", "Attacking", new Vector2(300f, 440f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("canCast Label", "canCast", new Vector2(300f, 460f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("canSuperdash Label", "canSuperdash", new Vector2(300f, 480f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("canQuickmap Label", "canQuickmap", new Vector2(300f, 500f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("canInventory Label", "canInventory", new Vector2(300f, 520f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("canWarp Label", "canWarp", new Vector2(300f, 540f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("canDGate Label", "canDGate", new Vector2(300f, 560f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddText("gateAllow Label", "gateAllow", new Vector2(300f, 580f), Vector2.zero, GUIController.Instance.Arial, 15);

            //Values
            _panel.AddText("Hero State", "", new Vector2(150f, 20f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Velocity", "", new Vector2(150f, 40f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Nail Damage", "", new Vector2(150f, 60f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("HP", "", new Vector2(150f, 80f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("MP", "", new Vector2(150f, 100f), Vector2.zero, GUIController.Instance.TrajanNormal);

            _panel.AddText("Completion", "", new Vector2(150f, 178f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Grubs", "", new Vector2(150f, 198f), Vector2.zero, GUIController.Instance.TrajanNormal);

            _panel.AddText("isInvuln", "", new Vector2(150f, 276f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Invincible", "", new Vector2(150f, 296f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Invincitest", "", new Vector2(150f, 316f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Damage State", "", new Vector2(150f, 336f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Dead State", "", new Vector2(150f, 356f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Hazard Death", "", new Vector2(150f, 376f), Vector2.zero, GUIController.Instance.TrajanNormal);

            _panel.AddText("Scene Name", "", new Vector2(150f, 454), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Transition", "", new Vector2(150f, 474f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Trans State", "", new Vector2(150f, 494f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("is Gameplay", "", new Vector2(150f, 514f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Game State", "", new Vector2(150f, 534f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("UI State", "", new Vector2(150f, 554f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Hero Paused", "", new Vector2(150f, 574f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Camera Mode", "", new Vector2(150f, 594f), Vector2.zero, GUIController.Instance.TrajanNormal);

            _panel.AddText("Accept Input", "", new Vector2(440f, 30f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Relinquished", "", new Vector2(440f, 50f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("atBench", "", new Vector2(440f, 70f), Vector2.zero, GUIController.Instance.TrajanNormal);

            _panel.AddText("Dashing", "", new Vector2(440f, 120f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Jumping", "", new Vector2(440f, 140f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Superdashing", "", new Vector2(440f, 160f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Falling", "", new Vector2(440f, 180f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Hardland", "", new Vector2(440f, 200f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Swimming", "", new Vector2(440f, 220f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Recoiling", "", new Vector2(440f, 240f), Vector2.zero, GUIController.Instance.TrajanNormal);

            _panel.AddText("Wall lock", "", new Vector2(440f, 290f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Wall jumping", "", new Vector2(440f, 310f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Wall touching", "", new Vector2(440f, 330f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Wall sliding", "", new Vector2(440f, 350f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Wall left", "", new Vector2(440f, 370f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("Wall right", "", new Vector2(440f, 390f), Vector2.zero, GUIController.Instance.TrajanNormal);

            _panel.AddText("Attacking", "", new Vector2(440f, 440f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("canCast", "", new Vector2(440f, 460f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("canSuperdash", "", new Vector2(440f, 480f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("canQuickmap", "", new Vector2(440f, 500f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("canInventory", "", new Vector2(440f, 520f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("canWarp", "", new Vector2(440f, 540f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("canDGate", "", new Vector2(440f, 560f), Vector2.zero, GUIController.Instance.TrajanNormal);
            _panel.AddText("gateAllow", "", new Vector2(440f, 580f), Vector2.zero, GUIController.Instance.TrajanNormal);

            //Bottom right info
            _panel.AddText("Right1 Label", "Session Time\nLoad\nHero Pos\nMove Raw", new Vector2(1285, 747), Vector2.zero, GUIController.Instance.Arial);
            _panel.AddText("Right1", "", new Vector2(1385, 747), Vector2.zero, GUIController.Instance.TrajanNormal);

            _panel.AddText("Right2 Label", "Move Vector\nKey Pressed\nMove Pressed\nInput X", new Vector2(1500, 747), Vector2.zero, GUIController.Instance.Arial);
            _panel.AddText("Right2", "", new Vector2(1600, 747), Vector2.zero, GUIController.Instance.TrajanNormal);

            _panel.FixRenderOrder();
        }

        private static HeroController Hc
        {
            get
            {
                try
                {
                    return HeroController.instance;
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

            if (Hc == null)
                return;

            if (Visible && !_panel.Active)
            {
                _panel.SetActive(true, false);
            }
            else if (!Visible && _panel.Active)
            {
                _panel.SetActive(false, true);
            }

            if (!_panel.Active) return;
            PlayerData.instance.CountGameCompletion();

            _panel.GetText("Hero State").UpdateText(Hc?.hero_state.ToString());
            _panel.GetText("Velocity").UpdateText(Hc?.current_velocity.ToString());
            _panel.GetText("Nail Damage").UpdateText(GUIController.RefKnightSlash.FsmVariables.GetFsmInt("damageDealt").Value + " (Flat " + PlayerData.instance.nailDamage + ", x" + GUIController.RefKnightSlash.FsmVariables.GetFsmFloat("Multiplier").Value + ")");
            _panel.GetText("HP").UpdateText(PlayerData.instance.health + " / " + PlayerData.instance.maxHealth);
            _panel.GetText("MP").UpdateText((PlayerData.instance.MPCharge + PlayerData.instance.MPReserve).ToString());

            _panel.GetText("Completion").UpdateText(PlayerData.instance.completionPercentage.ToString(CultureInfo.InvariantCulture));
            _panel.GetText("Grubs").UpdateText(PlayerData.instance.grubsCollected + " / 46");

            _panel.GetText("isInvuln").UpdateText(GetStringForBool(Hc.cState.invulnerable));
            _panel.GetText("Invincible").UpdateText(GetStringForBool(PlayerData.instance.isInvincible));
            _panel.GetText("Invincitest").UpdateText(GetStringForBool(PlayerData.instance.invinciTest));
            _panel.GetText("Damage State").UpdateText(Hc?.damageMode.ToString());
            _panel.GetText("Dead State").UpdateText(GetStringForBool(Hc.cState.dead));
            _panel.GetText("Hazard Death").UpdateText(Hc?.cState.hazardDeath.ToString());

            _panel.GetText("Scene Name").UpdateText(GUIController.GetSceneName());
            _panel.GetText("Transition").UpdateText(GetStringForBool(Hc.cState.transitioning));

            var transState = Hc?.transitionState.ToString();
            if (transState == "WAITING_TO_ENTER_LEVEL") transState = "LOADING";
            if (transState == "WAITING_TO_TRANSITION") transState = "WAITING";

            _panel.GetText("Trans State").UpdateText(transState);
            _panel.GetText("is Gameplay").UpdateText(GetStringForBool(GUIController.Gm.IsGameplayScene()));
            _panel.GetText("Game State").UpdateText(GameManager.instance.gameState.ToString());
            _panel.GetText("UI State").UpdateText(UIManager.instance.uiState.ToString());
            _panel.GetText("Hero Paused").UpdateText(GetStringForBool(Hc.cState.isPaused));
            _panel.GetText("Camera Mode").UpdateText(GUIController.RefCamera.mode.ToString());

            _panel.GetText("Accept Input").UpdateText(GetStringForBool(Hc.acceptingInput));
            _panel.GetText("Relinquished").UpdateText(GetStringForBool(Hc.controlReqlinquished));
            _panel.GetText("atBench").UpdateText(GetStringForBool(PlayerData.instance.atBench));

            _panel.GetText("Dashing").UpdateText(GetStringForBool(Hc.cState.dashing));
            _panel.GetText("Jumping").UpdateText(GetStringForBool((Hc.cState.jumping || Hc.cState.doubleJumping)));
            _panel.GetText("Superdashing").UpdateText(GetStringForBool(Hc.cState.superDashing));
            _panel.GetText("Falling").UpdateText(GetStringForBool(Hc.cState.falling));
            _panel.GetText("Hardland").UpdateText(GetStringForBool(Hc.cState.willHardLand));
            _panel.GetText("Swimming").UpdateText(GetStringForBool(Hc.cState.swimming));
            _panel.GetText("Recoiling").UpdateText(GetStringForBool(Hc.cState.recoiling));

            _panel.GetText("Wall lock").UpdateText(GetStringForBool(Hc.wallLocked));
            _panel.GetText("Wall jumping").UpdateText(GetStringForBool(Hc.cState.wallJumping));
            _panel.GetText("Wall touching").UpdateText(GetStringForBool(Hc.cState.touchingWall));
            _panel.GetText("Wall sliding").UpdateText(GetStringForBool(Hc.cState.wallSliding));
            _panel.GetText("Wall left").UpdateText(GetStringForBool(Hc.touchingWallL));
            _panel.GetText("Wall right").UpdateText(GetStringForBool(Hc.touchingWallR));

            _panel.GetText("Attacking").UpdateText(GetStringForBool(Hc.cState.attacking));
            _panel.GetText("canCast").UpdateText(GetStringForBool(Hc.CanCast()));
            _panel.GetText("canSuperdash").UpdateText(GetStringForBool(Hc.CanSuperDash()));
            _panel.GetText("canQuickmap").UpdateText(GetStringForBool(Hc.CanQuickMap()));
            _panel.GetText("canInventory").UpdateText(GetStringForBool(Hc.CanOpenInventory()));
            _panel.GetText("canWarp").UpdateText(GetStringForBool(GUIController.RefDreamNail.FsmVariables.GetFsmBool("Dream Warp Allowed").Value));
            _panel.GetText("canDGate").UpdateText(GetStringForBool(GUIController.RefDreamNail.FsmVariables.GetFsmBool("Can Dream Gate").Value));
            _panel.GetText("gateAllow").UpdateText(GetStringForBool(GUIController.RefDreamNail.FsmVariables.GetFsmBool("Dream Gate Allowed").Value));

            var time1 = Mathf.FloorToInt(Time.realtimeSinceStartup / 60f);
            var time2 = Mathf.FloorToInt(Time.realtimeSinceStartup - time1 * 60);

            _panel.GetText("Right1").UpdateText($"{time1:00}:{time2:00}" + "\n" + GUIController.GetLoadTime() + "s\n" + (Vector2)GUIController.RefKnight.transform.position + "\n" + $"L: {GUIController.Ih.inputActions.left.RawValue} R: {GUIController.Ih.inputActions.right.RawValue}");
            _panel.GetText("Right2").UpdateText(GUIController.Ih.inputActions.moveVector.Vector.x + ", " + GUIController.Ih.inputActions.moveVector.Vector.y + "\n" + GetStringForBool(InputManager.AnyKeyIsPressed) + "\n" + GetStringForBool(GUIController.Ih.inputActions.left.IsPressed || GUIController.Ih.inputActions.right.IsPressed) + "\n" + GUIController.Ih.inputX);
        }

        private static string GetStringForBool(bool b)
        {
            return b ? "✓" : "X";
        }
    }
}
