using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SamplePlugin
{
    public static class Console
    {
        public static bool Visible;

        private static CanvasPanel _panel;
        private static string _guiString = "";
        private static float _alpha = 1f;
        private static readonly List<string> History = new List<string>();
        private static Vector2 _scrollPosition = Vector2.zero;
        private static float _lastTime;

        public static void BuildMenu(GameObject canvas)
        {
            _panel = new CanvasPanel(canvas, GUIController.Instance.GetImage("ConsoleBg"), new Vector2(1275, 800), Vector2.zero, new Rect(0, 0, GUIController.Instance.GetImage("ConsoleBg").width, GUIController.Instance.GetImage("ConsoleBg").height));

            _panel.AddText("Console", "", new Vector2(10f, 25f), Vector2.zero, GUIController.Instance.Arial);
            _panel.AddText("NoConsole", "", new Vector2(10f, 180f), Vector2.zero, GUIController.Instance.Arial);

            _panel.FixRenderOrder();

            GUIController.Instance.Arial.RequestCharactersInTexture("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890`~!@#$%^&*()-_=+[{]}\\|;:'\",<.>/? ", 13);
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

            if (_panel.Active)
            {
                var consoleString = "";
                var lineCount = 0;

                for (var i = History.Count - 1; i >= 0; i--)
                {
                    if (lineCount >= 8) break;
                    consoleString = History[i] + "\n" + consoleString;
                    lineCount++;
                }

                _panel.GetText("Console").UpdateText(consoleString);
            }

            if (_panel.Active) return;
            var delta = Time.realtimeSinceStartup - _lastTime;
            _lastTime = Time.realtimeSinceStartup;

            _alpha -= delta * .5f;

            if (_alpha > 0 && GUIController.Gm.IsGameplayScene())
            {
                var c = Color.white;
                c.a = _alpha;

                _panel.GetText("NoConsole").SetActive(true);
                _panel.GetText("NoConsole").UpdateText(_guiString);
                _panel.GetText("NoConsole").SetTextColor(c);
            }
            else
            {
                _panel.GetText("NoConsole").SetActive(false);
            }
        }

        public static void Reset()
        {
            History.Clear();
            _alpha = 1f;
            _guiString = "";
            _lastTime = Time.realtimeSinceStartup;
            _scrollPosition = Vector2.zero;
        }

        public static void AddLine(string chatLine)
        {
            while (History.Count > 1000)
            {
                History.RemoveAt(0);
            }

            var wrap = WrapIndex(GUIController.Instance.Arial, 13, chatLine);

            while (wrap != -1)
            {
                var index = chatLine.LastIndexOf(' ', wrap, wrap);

                if (index != -1)
                {
                    History.Add(chatLine.Substring(0, index));
                    chatLine = chatLine.Substring(index + 1);
                    wrap = WrapIndex(GUIController.Instance.Arial, 13, chatLine);
                }
                else
                {
                    break;
                }
            }

            History.Add(chatLine);

            _scrollPosition.y = _scrollPosition.y + 50f;
            _alpha = 1f;
            _lastTime = Time.realtimeSinceStartup;

            if (!Visible)
            {
                _guiString = chatLine;
            }
        }

        public static void SaveHistory()
        {
            try
            {
                File.WriteAllLines("console.txt", History.ToArray());
                AddLine("Written history to console.txt");
            }
            catch (Exception arg)
            {
               // Modding.ModHooks.ModLog("[DEBUG MOD] [CONSOLE] Unable to write console history: " + arg);
                AddLine("Unable to write console history");
            }
        }

        private static int WrapIndex(Font font, int fontSize, string message)
        {
            var totalLength = 0;

            var arr = message.ToCharArray();

            for (var i = 0; i < arr.Length; i++)
            {
                var c = arr[i];
                CharacterInfo characterInfo;
                font.GetCharacterInfo(c, out characterInfo, fontSize);
                totalLength += characterInfo.advance;

                if (totalLength >= 564) return i;
            }

            return -1;
        }
    }
}
