using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Psd2UGUI
{
    public class TextNode : BaseNode
    {
        public override void Build(Transform parent)
        {
            GameObject go = CreateGameObject();
            Transform transform = go.transform;
            transform.SetParent(parent, false);
            Text text = go.AddComponent<Text>();
            text.font = Resources.Load("Font/arial") as Font;
            text.text = "测试测试";

            RectTransform parentRect = parent.GetComponent<RectTransform>();
            Vector3 parentAnchoredPosition = parentRect.anchoredPosition3D;
            RectTransform rect = go.GetComponent<RectTransform>();
            rect.anchoredPosition3D = rect.anchoredPosition3D - parentAnchoredPosition;
        }
    }
}
