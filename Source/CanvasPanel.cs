using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SamplePlugin
{
    public class CanvasPanel
    {
        private readonly CanvasImage _background;
        private readonly GameObject _canvas;
        private Vector2 _position;
        private readonly Vector2 _size;
        private readonly Dictionary<string, CanvasButton> _buttons = new Dictionary<string, CanvasButton>();
        private readonly Dictionary<string, CanvasPanel> _panels = new Dictionary<string, CanvasPanel>();
        private readonly Dictionary<string, CanvasImage> _images = new Dictionary<string, CanvasImage>();
        private readonly Dictionary<string, CanvasText> _texts = new Dictionary<string, CanvasText>();

        public bool Active;

        public CanvasPanel(GameObject parent, Texture2D tex, Vector2 pos, Vector2 sz, Rect bgSubSection)
        {
            if (parent == null) return;

            _position = pos;
            _size = sz;
            _canvas = parent;
            _background = new CanvasImage(parent, tex, pos, sz, bgSubSection);

            Active = true;
        }

        public void AddButton(string name, Texture2D tex, Vector2 pos, Vector2 sz, UnityAction<string> func, Rect bgSubSection, Font font = null, string text = null, int fontSize = 13)
        {
            var button = new CanvasButton(_canvas, name, tex, _position + pos, _size + sz, bgSubSection, font, text, fontSize);
            button.AddClickEvent(func);

            _buttons.Add(name, button);
        }

        public void AddPanel(string name, Texture2D tex, Vector2 pos, Vector2 sz, Rect bgSubSection)
        {
            var panel = new CanvasPanel(_canvas, tex, _position + pos, sz, bgSubSection);

            _panels.Add(name, panel);
        }

        public void AddImage(string name, Texture2D tex, Vector2 pos, Vector2 size, Rect subSprite)
        {
            var image = new CanvasImage(_canvas, tex, _position + pos, size, subSprite);

            _images.Add(name, image);
        }

        public void AddText(string name, string text, Vector2 pos, Vector2 sz, Font font, int fontSize = 13, FontStyle style = FontStyle.Normal, TextAnchor alignment = TextAnchor.UpperLeft)
        {
            var t = new CanvasText(_canvas, _position + pos, sz, font, text, fontSize, style, alignment);

            _texts.Add(name, t);
        }

        public CanvasButton GetButton(string buttonName, string panelName = null)
        {
            if (panelName != null && _panels.ContainsKey(panelName))
            {
                return _panels[panelName].GetButton(buttonName);
            }

            return _buttons.ContainsKey(buttonName) ? _buttons[buttonName] : null;
        }

        public CanvasImage GetImage(string imageName, string panelName = null)
        {
            if (panelName != null && _panels.ContainsKey(panelName))
            {
                return _panels[panelName].GetImage(imageName);
            }

            return _images.ContainsKey(imageName) ? _images[imageName] : null;
        }

        public CanvasPanel GetPanel(string panelName)
        {
            return _panels.ContainsKey(panelName) ? _panels[panelName] : null;
        }

        public CanvasText GetText(string textName, string panelName = null)
        {
            if (panelName != null && _panels.ContainsKey(panelName))
            {
                return _panels[panelName].GetText(textName);
            }

            return _texts.ContainsKey(textName) ? _texts[textName] : null;
        }

        public void UpdateBackground(Texture2D tex, Rect subSection)
        {
            _background.UpdateImage(tex, subSection);
        }

        public void ResizeBg(Vector2 sz)
        {
            _background.SetWidth(sz.x);
            _background.SetHeight(sz.y);
            _background.SetPosition(_position);
        }

        public void SetPosition(Vector2 pos)
        {
            _background.SetPosition(pos);

            var deltaPos = _position - pos;
            _position = pos;

            foreach (var button in _buttons.Values)
            {
                button.SetPosition(button.GetPosition() - deltaPos);
            }

            foreach (var text in _texts.Values)
            {
                text.SetPosition(text.GetPosition() - deltaPos);
            }

            foreach (var panel in _panels.Values)
            {
                panel.SetPosition(panel.GetPosition() - deltaPos);
            }
        }

        public void TogglePanel(string name)
        {
            if (Active && _panels.ContainsKey(name))
            {
                _panels[name].ToggleActive();
            }
        }

        public void ToggleActive()
        {
            Active = !Active;
            SetActive(Active, false);
        }

        public void SetActive(bool b, bool panel)
        {
            _background.SetActive(b);

            foreach (var button in _buttons.Values)
            {
                button.SetActive(b);
            }

            foreach (var image in _images.Values)
            {
                image.SetActive(b);
            }

            foreach (var t in _texts.Values)
            {
                t.SetActive(b);
            }

            if (panel)
            {
                foreach (var p in _panels.Values)
                {
                    p.SetActive(b, false);
                }
            }

            Active = b;
        }

        public Vector2 GetPosition()
        {
            return _position;
        }

        public void FixRenderOrder()
        {
            foreach (var t in _texts.Values)
            {
                t.MoveToTop();
            }

            foreach (var button in _buttons.Values)
            {
                button.MoveToTop();
            }

            foreach (var image in _images.Values)
            {
                image.SetRenderIndex(0);
            }

            foreach (var panel in _panels.Values)
            {
                panel.FixRenderOrder();
            }

            _background.SetRenderIndex(0);
        }

        public void Destroy()
        {
            _background.Destroy();

            foreach (var button in _buttons.Values)
            {
                button.Destroy();
            }

            foreach (var image in _images.Values)
            {
                image.Destroy();
            }

            foreach (var t in _texts.Values)
            {
                t.Destroy();
            }

            foreach (var p in _panels.Values)
            {
                p.Destroy();
            }
        }
    }
}
