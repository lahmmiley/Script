using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Psd2UGUI
{
    public abstract class BaseNode
    {
        public const string FIELD_NAME = "Name";
        public const string FIELD_TYPE = "Type";
        public const string FIELD_X = "X";
        public const string FIELD_Y = "Y";
        public const string FIELD_WIDTH = "Width";
        public const string FIELD_HEIGHT = "Height";
        public const string FIELD_CHILDREN = "Children";

        public string Name;
        public string Type;
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public BaseNode[] Children;

        protected GameObject CreateGameObject()
        {
            GameObject go = new GameObject();
            go.name = Name;
            RectTransform rect = go.AddComponent<RectTransform>();
            rect.localScale = Vector3.one;
            rect.pivot = new Vector2(0, 1);
            rect.anchorMax = new Vector2(0, 1);
            rect.anchorMin = new Vector2(0, 1);
            rect.sizeDelta = new Vector2(Width, Height);
            rect.anchoredPosition3D = new Vector3(X, -Y, 0);
            return go;
        }

        public abstract void Build(Transform parent);
    }
}
