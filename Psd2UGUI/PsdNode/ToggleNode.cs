using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
using UnityEngine;
using UnityEngine.UI;
using PsdRebuilder;

namespace Psd2UGUI
{
    //TODO
    //这里假设Toggle只有一张图片，如果要多张图片，需要学俊钦做法
    public class ToggleNode : ContainerNode
    {
        private string _normalName;

        public override void ProcessStruct(JsonData jsonData)
        {
            //TODO 优化
            JsonData jsonImage = jsonData[FIELD_CHILDREN][0];
            _normalName = jsonImage[FIELD_CHILDREN][1][FIELD_CHILDREN][0][FIELD_NAME].ToString();
            //Debug.LogError(_normalName);
            JsonData normalChild = jsonImage[FIELD_CHILDREN][0];
            //TODO 删除的方法不好
            jsonImage[FIELD_CHILDREN].Clear();
            jsonImage[FIELD_CHILDREN].Add(normalChild);
            //Debug.LogError(jsonImage[FIELD_CHILDREN].Count);
        }

        public override void Build(Transform parent)
        {
            base.Build(parent);

            GameObject go = this.gameObject;
            Toggle toggle = go.AddComponent<Toggle>();
            toggle.transition = Selectable.Transition.None;
            toggle.graphic = go.transform.GetChild(0).GetComponent<Image>();

            Image image = this.gameObject.AddComponent<Image>();
            Sprite normalSprite = Resources.Load(
                string.Format("IMAGE/{0}/{1}", PanelCreator.Instance.CurrentName, _normalName),
                typeof(Sprite)) as Sprite;
            image.sprite = normalSprite;

            //Debug.LogError(go.transform.GetSiblingIndex());
            if(this.gameObject.transform.GetSiblingIndex() == 0)
            {
                toggle.isOn = true;
            }
            else
            {
                toggle.isOn = false;
            }
        }
    }
}
