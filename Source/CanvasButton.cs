﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace DebugMod
{
    public class CanvasButton
    {
        private GameObject buttonObj;
        private GameObject textObj;
        private Button button;
        private UnityAction<string> clicked;
        private string buttonName;

        public bool active;

        public CanvasButton(GameObject parent, string name, Texture2D tex, Vector2 pos, Vector2 size, Rect bgSubSection, Font font = null, string text = null, int fontSize = 13)
        {
            if (size.x == 0 || size.y == 0)
            {
                size = new Vector2(bgSubSection.width, bgSubSection.height);
            }

            buttonName = name;

            buttonObj = new GameObject();
            buttonObj.AddComponent<CanvasRenderer>();
            RectTransform buttonTransform = buttonObj.AddComponent<RectTransform>();
            buttonTransform.sizeDelta = new Vector2(bgSubSection.width, bgSubSection.height);
            buttonObj.AddComponent<Image>().sprite = Sprite.Create(tex, new Rect(bgSubSection.x, tex.height - bgSubSection.height, bgSubSection.width, bgSubSection.height), Vector2.zero);
            button = buttonObj.AddComponent<Button>();

            buttonObj.transform.SetParent(parent.transform, false);

            buttonTransform.SetScaleX(size.x / bgSubSection.width);
            buttonTransform.SetScaleY(size.y / bgSubSection.height);

            Vector2 position = new Vector2((pos.x + ((size.x / bgSubSection.width) * bgSubSection.width) / 2f) / 1920f, (1080f - (pos.y + ((size.y / bgSubSection.height) * bgSubSection.height) / 2f)) / 1080f);
            buttonTransform.anchorMin = position;
            buttonTransform.anchorMax = position;

            GameObject.DontDestroyOnLoad(buttonObj);

            if (font != null && text != null)
            {
                textObj = new GameObject();
                textObj.AddComponent<RectTransform>().sizeDelta = new Vector2(bgSubSection.width, bgSubSection.height);
                Text t = textObj.AddComponent<Text>();
                t.text = text;
                t.font = font;
                t.fontSize = fontSize;
                t.alignment = TextAnchor.MiddleCenter;
                textObj.transform.SetParent(buttonObj.transform, false);

                GameObject.DontDestroyOnLoad(textObj);
            }

            active = true;
        }

        public void UpdateSprite(Texture2D tex, Rect bgSubSection)
        {
            if (buttonObj != null)
            {
                buttonObj.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(bgSubSection.x, tex.height - bgSubSection.height, bgSubSection.width, bgSubSection.height), Vector2.zero);
            }
        }

        public void AddClickEvent(UnityAction<string> action)
        {
            if (buttonObj != null)
            {
                clicked = action;
                buttonObj.GetComponent<Button>().onClick.AddListener(ButtonClicked);
            }
        }

        private void ButtonClicked()
        {
            if (clicked != null && buttonName != null) clicked(buttonName);
        }

        public void UpdateText(string text)
        {
            if (textObj != null)
            {
                textObj.GetComponent<Text>().text = text;
            }
        }

        public void SetWidth(float width)
        {
            if (buttonObj != null)
            {
                buttonObj.GetComponent<RectTransform>().SetScaleX(width / buttonObj.GetComponent<RectTransform>().sizeDelta.x);
            }
        }

        public void SetHeight(float height)
        {
            if (buttonObj != null)
            {
                buttonObj.GetComponent<RectTransform>().SetScaleY(height / buttonObj.GetComponent<RectTransform>().sizeDelta.y);
            }
        }

        public void SetPosition(Vector2 pos)
        {
            if (buttonObj != null)
            {
                Vector2 sz = buttonObj.GetComponent<RectTransform>().sizeDelta;
                Vector2 position = new Vector2((pos.x + sz.x / 2f) / 1920f, (1080f - (pos.y + sz.y / 2f)) / 1080f);
                buttonObj.GetComponent<RectTransform>().anchorMin = position;
                buttonObj.GetComponent<RectTransform>().anchorMax = position;
            }
        }

        public void SetActive(bool b)
        {
            if (buttonObj != null)
            {
                buttonObj.SetActive(b);
                active = b;
            }
        }


        public void SetRenderIndex(int idx)
        {
            buttonObj.transform.SetSiblingIndex(idx);
        }

        public Vector2 GetPosition()
        {
            if (buttonObj != null)
            {
                Vector2 anchor = buttonObj.GetComponent<RectTransform>().anchorMin;
                Vector2 sz = buttonObj.GetComponent<RectTransform>().sizeDelta;

                return new Vector2(anchor.x * 1920f - sz.x / 2f, 1080f - anchor.y * 1080f - sz.y / 2f);
            }

            return Vector2.zero;
        }

        public void MoveToTop()
        {
            if (buttonObj != null)
            {
                buttonObj.transform.SetAsLastSibling();
            }
        }

        public void SetTextColor(Color color)
        {
            if (textObj != null)
            {
                Text t = textObj.GetComponent<Text>();
                t.color = color;
            }
        }

        public void Destroy()
        {
            GameObject.Destroy(buttonObj);
            GameObject.Destroy(textObj);
        }
    }
}
