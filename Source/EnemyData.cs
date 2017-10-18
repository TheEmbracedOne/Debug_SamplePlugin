using UnityEngine;

namespace SamplePlugin
{
    public struct EnemyData
    {
        public int Hp;
        public int MaxHp;
        public PlayMakerFSM Fsm;
        public Component Spr;
        public CanvasPanel HpBar;
        public CanvasPanel Hitbox;
        public GameObject GameObject;

        public EnemyData(int hp, PlayMakerFSM fsm, Component spr, GameObject parent = null, GameObject go = null)
        {
            Hp = hp;
            MaxHp = hp;
            Fsm = fsm;
            Spr = spr;
            GameObject = go;

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
                        tex.SetPixel(x, y, Color.red);
                    }
                }
            }

            tex.Apply();

            var tex2 = new Texture2D(1, 1);
            var yellow = Color.yellow;
            yellow.a = .7f;
            tex2.SetPixel(1, 1, yellow);

            HpBar = new CanvasPanel(parent, tex, Vector2.zero, Vector2.zero, new Rect(0, 0, 120, 40));
            HpBar.AddText("HP", "", Vector2.zero, new Vector2(120, 40), GUIController.Instance.Arial, 20, FontStyle.Normal, TextAnchor.MiddleCenter);
            HpBar.FixRenderOrder();

            Hitbox = new CanvasPanel(parent, tex2, Vector2.zero, Vector2.zero, new Rect(0, 0, 1, 1));
        }

        public void SetHp(int health)
        {
            Hp = health;
            Fsm.FsmVariables.GetFsmInt("HP").Value = health;
        }
    }
}
