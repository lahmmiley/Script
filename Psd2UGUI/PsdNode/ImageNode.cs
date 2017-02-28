using LitJson;
using PsdRebuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Psd2UGUI
{
    public class ImageNode : AtomNode
    {
        //TODO
        //叫ProcessStruct不是很好，感觉只是处理结构
        public override void ProcessStruct(JsonData jsonData)
        {
            SetState(jsonData);
        }

        public override void Build(Transform parent)
        {
            GameObject go = CreateGameObject(parent);

            Image image = go.AddComponent<Image>();
            Sprite normalSprite = Resources.Load(
                string.Format("IMAGE/{0}/{1}", PanelCreator.Instance.CurrentName, stateDict[STATE_NORMAL]),
                typeof(Sprite)) as Sprite;
            image.sprite = normalSprite;
            if(stateDict.Count > 1)
            {
                Selectable selectable = go.AddComponent<Selectable>();
                selectable.targetGraphic = image;
                selectable.transition = Selectable.Transition.SpriteSwap;

                SpriteState spriteState = new SpriteState();
                selectable.spriteState = spriteState;
                foreach(string state in stateDict.Keys)
                {
                    Sprite sprite = Resources.Load(
                        string.Format("IMAGE/{0}/{1}", PanelCreator.Instance.CurrentName, stateDict[state]),
                        typeof(Sprite)) as Sprite;
                    switch(state)
                    {
                        case STATE_NORMAL:
                            spriteState.highlightedSprite = sprite;
                            break;
                        case STATE_OVER:
                            spriteState.pressedSprite= sprite;
                            break;
                        case STATE_DISABLE:
                            spriteState.disabledSprite= sprite;
                            break;
                    }

                }
            }

            AdjustPosition(go, parent);
        }
    }
}
