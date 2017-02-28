using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

namespace Psd2UGUI
{
    public class TextNode : AtomNode
    {
        public const string FIELD_SIZE = "Size";
        public const string FIELD_TEXT = "Text";

        private string _content = "未定义";
        private int _size;
        private TextAnchor _anchor = TextAnchor.UpperLeft;

        public override void ProcessStruct(JsonData jsonData)
        {
            if (jsonData.Keys.Contains(FIELD_SIZE))
            {
                _size = (int)jsonData[FIELD_SIZE];
            }

            if(jsonData.Keys.Contains(FIELD_TEXT))
            {
                _content = jsonData[FIELD_TEXT].ToString();
            }

            if(jsonData.Keys.Contains(FIELD_PARAM))
            {
                string param = jsonData[FIELD_PARAM].ToString();
                if(param.IndexOf("Left") != -1)
                {
                    _anchor = TextAnchor.UpperLeft;
                }
                else if(param.IndexOf("Center") != -1)
                {
                    _anchor = TextAnchor.UpperCenter;
                }
                else if(param.IndexOf("Right") != -1)
                {
                    _anchor = TextAnchor.UpperRight;
                }
            }
        }

        public override void Build(Transform parent)
        {
            GameObject go = CreateGameObject(parent);
            Text text = go.AddComponent<Text>();
            text.font = Resources.Load("Font/arial") as Font;
            text.fontSize = _size;
            text.text = _content;
            //TODO 如果只有一行，就用overflow
            //如果有多行，缺少的宽度就没有影响了
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.alignment = _anchor;

            AdjustPosition(go, parent);
        }
    }
}
