using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Psd2UGUI
{
    public class ImageNode : BaseNode
    {
        public string NormalName = string.Empty;
        public string OverName = string.Empty;

        public ImageNode(JsonData jsonData)
        {
            if(MultiplceState(jsonData))
            {
                this.NormalName = jsonData[FIELD_CHILDREN][0][FIELD_CHILDREN][0][FIELD_NAME].ToString();
                this.OverName = jsonData[FIELD_CHILDREN][1][FIELD_CHILDREN][0][FIELD_NAME].ToString();
            }
            else
            {
                this.NormalName = jsonData[FIELD_NAME].ToString();
            }
        }

        private bool MultiplceState(JsonData jsonData)
        {
            if(jsonData.Keys.Contains(FIELD_CHILDREN))
            {
                return true;
            }
            return false;
        }



        public override void Build(Transform parent)
        {
            GameObject go = CreateGameObject();
            Transform transform = go.transform;
            transform.SetParent(parent, false);

            Image image = go.AddComponent<Image>();
            Texture2D texture = Resources.Load("IMAGE/" + this.NormalName) as Texture2D;
            RectTransform rect = go.GetComponent<RectTransform>();
            Sprite normalSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 1));
            image.sprite = normalSprite;
            if(this.OverName != string.Empty)
            {
                Selectable selectable = go.AddComponent<Selectable>();
                selectable.targetGraphic = image;
                selectable.transition = Selectable.Transition.SpriteSwap;
                SpriteState spriteState = new SpriteState();
                spriteState.highlightedSprite = normalSprite;

                //Texture2D overTexture = Resources.Load("IMAGE/" + this.OverName) as Texture2D;
                //Sprite overSprite = Sprite.Create(overTexture, new Rect(10, 10, overTexture.width - 10, overTexture.height - 10), new Vector2(0, 1));
                Sprite overSprite = Resources.Load("IMAGE/" + this.OverName, typeof(Sprite)) as Sprite;
                spriteState.pressedSprite = overSprite;
                selectable.spriteState = spriteState;
            }

            RectTransform parentRect = parent.GetComponent<RectTransform>();
            Vector3 parentAnchoredPosition = parentRect.anchoredPosition3D;
            rect.anchoredPosition3D = rect.anchoredPosition3D - parentAnchoredPosition;
        }
    }
}
