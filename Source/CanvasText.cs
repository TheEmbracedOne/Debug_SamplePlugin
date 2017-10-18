using UnityEngine;
using UnityEngine.UI;

namespace SamplePlugin
{
    public class CanvasText
    {
        private readonly GameObject _textObj;
        private Vector2 _size;

        public bool Active;

        public CanvasText(GameObject parent, Vector2 pos, Vector2 sz, Font font, string text, int fontSize = 13, FontStyle style = FontStyle.Normal, TextAnchor alignment = TextAnchor.UpperLeft)
        {
            if (sz.x == 0 || sz.y == 0)
            {
                _size = new Vector2(1920f, 1080f);
            }
            else
            {
                _size = sz;
            }

            _textObj = new GameObject();
            _textObj.AddComponent<CanvasRenderer>();
            var textTransform = _textObj.AddComponent<RectTransform>();
            textTransform.sizeDelta = _size;

            var group = _textObj.AddComponent<CanvasGroup>();
            group.interactable = false;
            group.blocksRaycasts = false;

            var t = _textObj.AddComponent<Text>();
            t.text = text;
            t.font = font;
            t.fontSize = fontSize;
            t.fontStyle = style;
            t.alignment = alignment;

            _textObj.transform.SetParent(parent.transform, false);

            var position = new Vector2((pos.x + _size.x / 2f) / 1920f, (1080f - (pos.y + _size.y / 2f)) / 1080f);
            textTransform.anchorMin = position;
            textTransform.anchorMax = position;

            Object.DontDestroyOnLoad(_textObj.transform.root);

            Active = true;
        }

        public void SetPosition(Vector2 pos)
        {
            if (_textObj == null) return;
            var textTransform = _textObj.GetComponent<RectTransform>();

            var position = new Vector2((pos.x + _size.x / 2f) / 1920f, (1080f - (pos.y + _size.y / 2f)) / 1080f);
            textTransform.anchorMin = position;
            textTransform.anchorMax = position;
        }

        public Vector2 GetPosition()
        {
            if (_textObj == null) return Vector2.zero;
            var anchor = _textObj.GetComponent<RectTransform>().anchorMin;

            return new Vector2(anchor.x * 1920f - _size.x / 2f, 1080f - anchor.y * 1080f - _size.y / 2f);
        }

        public void UpdateText(string text)
        {
            if (_textObj != null)
            {
                _textObj.GetComponent<Text>().text = text;
            }
        }

        public void SetActive(bool a)
        {
            Active = a;

            if (_textObj != null)
            {
                _textObj.SetActive(Active);
            }
        }

        public void MoveToTop()
        {
            if (_textObj != null)
            {
                _textObj.transform.SetAsLastSibling();
            }
        }

        public void SetTextColor(Color color)
        {
            if (_textObj == null) return;
            var t = _textObj.GetComponent<Text>();
            t.color = color;
        }

        public void Destroy()
        {
            Object.Destroy(_textObj);
        }
    }
}
