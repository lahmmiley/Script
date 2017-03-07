using AssetManager;
using LitJson;
using PsdRebuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Psd2UGUI
{
    public class ImageNode : AtomNode
    {
        private string _psdName;
        private bool _hasParam = false;

        //TODO
        //叫ProcessStruct不是很好，感觉只是处理结构
        public override void ProcessStruct(JsonData jsonData)
        {
            //这块的处理很差
            if(jsonData.Keys.Contains(FIELD_CHILDREN))
            {
                for(int i = 0; i < jsonData[FIELD_CHILDREN].Count; i++)
                {
                    JsonData child = jsonData[FIELD_CHILDREN][i];
                    _psdName = child[FIELD_CHILDREN][0][BaseNode.FIELD_BELONG_PSD].ToString();
                    if(child[FIELD_CHILDREN][0].Keys.Contains(BaseNode.FIELD_PARAM))
                    {
                        _hasParam = true;
                    }
                    break;
                }
            }
            else
            {
                _psdName = jsonData[BaseNode.FIELD_BELONG_PSD].ToString();
                if(jsonData.Keys.Contains(BaseNode.FIELD_PARAM))
                {
                    _hasParam = true;
                }
            }
            SetState(jsonData);
        }

        public override void Build(Transform parent)
        {
            GameObject go = CreateGameObject(parent);

            Image image = go.AddComponent<Image>();
            image.sprite = AssetLoader.LoadSprite(_psdName, stateDict[STATE_NORMAL]);
            if(_hasParam)
            {
                image.type = Image.Type.Sliced;
            }
            //Sprite normalSprite = Resources.Load(
            //    string.Format("IMAGE/{0}/{1}", PanelCreator.Instance.CurrentName, stateDict[STATE_NORMAL]),
            //    typeof(Sprite)) as Sprite;
            //image.sprite = normalSprite;



            //GameObject go1 = (GameObject)Resources.Load("Image/BattlePreparePanelPrefab");
            //GameObject go1 = (GameObject)Resources.Load("Prefab/UIRoot");
            //Debug.LogError(go1.name);

            //GameObject obj = Resources.Load<GameObject>(string.Format("Image/BattlePreparePanelPrefab"));
            //Debug.LogError(obj.name);
            //Debug.LogError(obj.transform.FindChild(stateDict[STATE_NORMAL]));
            //Debug.LogError(stateDict[STATE_NORMAL]);
            //image.sprite = obj.transform.FindChild(stateDict[STATE_NORMAL]).GetComponent<SpriteRenderer>().sprite;

            //if(stateDict.Count > 1)
            if(false)
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
