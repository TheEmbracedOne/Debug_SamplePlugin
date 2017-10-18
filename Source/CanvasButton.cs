using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace SamplePlugin
{
    public class CanvasButton
    {
        private readonly GameObject _buttonObj;
        private readonly GameObject _textObj;
        private Button _button;
        private UnityAction<string> _clicked;
        private readonly string _buttonName;

        public bool Active;

        public CanvasButton(GameObject parent, string name, Texture2D tex, Vector2 pos, Vector2 size, Rect bgSubSection, Font font = null, string text = null, int fontSize = 13)
        {
            if (size.x == 0 || size.y == 0)
            {
                size = new Vector2(bgSubSection.width, bgSubSection.height);
            }

            _buttonName = name;

            _buttonObj = new GameObject();
            _buttonObj.AddComponent<CanvasRenderer>();
            var buttonTransform = _buttonObj.AddComponent<RectTransform>();
            buttonTransform.sizeDelta = new Vector2(bgSubSection.width, bgSubSection.height);
            _buttonObj.AddComponent<Image>().sprite = Sprite.Create(tex, new Rect(bgSubSection.x, tex.height - bgSubSection.height, bgSubSection.width, bgSubSection.height), Vector2.zero);
            _button = _buttonObj.AddComponent<Button>();

            _buttonObj.transform.SetParent(parent.transform, false);

            buttonTransform.SetScaleX(size.x / bgSubSection.width);
            buttonTransform.SetScaleY(size.y / bgSubSection.height);

            var position = new Vector2((pos.x + ((size.x / bgSubSection.width) * bgSubSection.width) / 2f) / 1920f, (1080f - (pos.y + ((size.y / bgSubSection.height) * bgSubSection.height) / 2f)) / 1080f);
            buttonTransform.anchorMin = position;
            buttonTransform.anchorMax = position;

            Object.DontDestroyOnLoad(_buttonObj.transform.root);

            if (font != null && text != null)
            {
                _textObj = new GameObject();
                _textObj.AddComponent<RectTransform>().sizeDelta = new Vector2(bgSubSection.width, bgSubSection.height);
                var t = _textObj.AddComponent<Text>();
                t.text = text;
                t.font = font;
                t.fontSize = fontSize;
                t.alignment = TextAnchor.MiddleCenter;
                _textObj.transform.SetParent(_buttonObj.transform, false);

                Object.DontDestroyOnLoad(_textObj.transform.root);
            }

            Active = true;
        }

        public void UpdateSprite(Texture2D tex, Rect bgSubSection)
        {
            if (_buttonObj != null)
            {
                _buttonObj.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(bgSubSection.x, tex.height - bgSubSection.height, bgSubSection.width, bgSubSection.height), Vector2.zero);
            }
        }

        public void AddClickEvent(UnityAction<string> action)
        {
            if (_buttonObj == null) return;
            _clicked = action;
            _buttonObj.GetComponent<Button>().onClick.AddListener(ButtonClicked);
        }

        private void ButtonClicked()
        {
            if (_clicked != null && _buttonName != null) _clicked(_buttonName);
        }

        public void UpdateText(string text)
        {
            if (_textObj != null)
            {
                _textObj.GetComponent<Text>().text = text;
            }
        }

        public void SetWidth(float width)
        {
            if (_buttonObj != null)
            {
                _buttonObj.GetComponent<RectTransform>().SetScaleX(width / _buttonObj.GetComponent<RectTransform>().sizeDelta.x);
            }
        }

        public void SetHeight(float height)
        {
            if (_buttonObj != null)
            {
                _buttonObj.GetComponent<RectTransform>().SetScaleY(height / _buttonObj.GetComponent<RectTransform>().sizeDelta.y);
            }
        }

        public void SetPosition(Vector2 pos)
        {
            if (_buttonObj == null) return;
            var sz = _buttonObj.GetComponent<RectTransform>().sizeDelta;
            var position = new Vector2((pos.x + sz.x / 2f) / 1920f, (1080f - (pos.y + sz.y / 2f)) / 1080f);
            _buttonObj.GetComponent<RectTransform>().anchorMin = position;
            _buttonObj.GetComponent<RectTransform>().anchorMax = position;
        }

        public void SetActive(bool b)
        {
            if (_buttonObj == null) return;
            _buttonObj.SetActive(b);
            Active = b;
        }


        public void SetRenderIndex(int idx)
        {
            _buttonObj.transform.SetSiblingIndex(idx);
        }

        public Vector2 GetPosition()
        {
            if (_buttonObj == null) return Vector2.zero;
            var anchor = _buttonObj.GetComponent<RectTransform>().anchorMin;
            var sz = _buttonObj.GetComponent<RectTransform>().sizeDelta;

            return new Vector2(anchor.x * 1920f - sz.x / 2f, 1080f - anchor.y * 1080f - sz.y / 2f);
        }

        public void MoveToTop()
        {
            if (_buttonObj != null)
            {
                _buttonObj.transform.SetAsLastSibling();
            }
        }

        public void SetTextColor(Color color)
        {
            if (_textObj == null) return;
            var t = _textObj.GetComponent<Text>();
            t.color = color;
        }

        public string GetText()
        {
            return _textObj != null ? _textObj.GetComponent<Text>().text : null;
        }

        public void Destroy()
        {
            Object.Destroy(_buttonObj);
            Object.Destroy(_textObj);
        }
    }
}
