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
            GameObject go = CreateGameObject(parent);
            Text text = go.AddComponent<Text>();
            text.font = Resources.Load("Font/arial") as Font;
            text.text = "测试测试";

            AdjustPosition(go, parent);
        }
    }
}
