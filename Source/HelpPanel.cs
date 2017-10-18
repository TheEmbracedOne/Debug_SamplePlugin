using UnityEngine;

namespace SamplePlugin
{
    public static class HelpPanel
    {
        private static CanvasPanel _panel;
        private static int _page = 1;

        public static bool Visible;

        public static void BuildMenus(GameObject canvas)
        {
            _panel = new CanvasPanel(canvas, GUIController.Instance.GetImage("HelpBG"), new Vector2(1123, 456), Vector2.zero, new Rect(0, 0, GUIController.Instance.GetImage("HelpBG").width, GUIController.Instance.GetImage("HelpBG").height));
            _panel.AddText("Label", "Help", new Vector2(130f, -25f), Vector2.zero, GUIController.Instance.TrajanBold, 30);

            _panel.AddText("Help", "F1 - Toggle All\nF2 - Toggle info\nF3 - Toggle top right\nF4 - Toggle console\nF5 - Force pause\nF6 - Hazard respawn\nF7 - Set respawn\nF8 - Force camera follow\nF9 - Toggle enemy panel\nF10 - Self damage\n` - Toggle Help", new Vector2(75f, 50f), Vector2.zero, GUIController.Instance.Arial, 15);
            _panel.AddButton("Next", GUIController.Instance.GetImage("ButtonRect"), new Vector2(125, 250), Vector2.zero, NextClicked, new Rect(0, 0, GUIController.Instance.GetImage("ButtonRect").width, GUIController.Instance.GetImage("ButtonRect").height), GUIController.Instance.TrajanBold, "Next");
        }

        private static void NextClicked(string buttonName)
        {
            if (_page == 1)
            {
                _page = 2;

                _panel.GetButton("Next").UpdateText("Prev");
                _panel.GetText("Help").UpdateText("Plus - Nail damage +4\nMinus - Nail damage -4\nNumpad+ - Timescale up\nNumpad- - Timescale down\nHome - Toggle hero light\nInsert - Toggle vignette\nPageUP - Zoom in\nPageDN - Zoom out\nEnd - Reset zoom\nDelete - Toggle HUD\nBackspc - Hide hero");
            }
            else
            {
                _page = 1;

                _panel.GetButton("Next").UpdateText("Next");
                _panel.GetText("Help").UpdateText("F1 - Toggle All\nF2 - Toggle info\nF3 - Toggle top right\nF4 - Toggle console\nF5 - Force pause\nF6 - Hazard respawn\nF7 - Set respawn\nF8 - Force camera follow\nF9 - Toggle enemy panel\nF10 - Self damage\n` - Toggle Help");

            }
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
        }
    }
}
