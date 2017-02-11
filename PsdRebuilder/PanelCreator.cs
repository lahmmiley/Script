using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.PsdRebuilder
{
    public class PanelCreator
    {
        private static PanelCreator _instance;
        public static PanelCreator Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new PanelCreator();
                }
                return _instance;
            }
        }
        private PanelCreator(){ }

        public void Create(PsdNode node)
        {
            GameObject parent = GameObject.Find("Layer");
            RectTransform rect = parent.AddComponent<RectTransform>();
            rect.localScale = Vector3.one;
            rect.pivot = new Vector2(0, 1);
            rect.anchorMax = new Vector2(0, 1);
            rect.anchorMin = new Vector2(0, 1);
            rect.sizeDelta = new Vector2(1280, 720);
            rect.anchoredPosition = Vector3.zero;
            //TODO
            int length = node.Children.Length;
            for (int i = length - 1; i >= 0; i--)
            {
                CreateRecursion(parent, node.Children[i]);
            }
        }

        public void CreateRecursion(GameObject parent, PsdNode node)
        {
            GameObject go = new GameObject();
            go.name = node.Name;
            RectTransform rect = go.AddComponent<RectTransform>();
            go.transform.SetParent(parent.transform, false);
            //TODO toLower
            switch(node.Type.ToLower())
            {
                case "container":
                    break;
                case "Button":
                    break;
                case "image":
                    //TODO
                    if(node.Name != "Image")
                    {
                        RawImage rawImage = go.AddComponent<RawImage>();
                        rawImage.texture = Resources.Load("IMAGE/" + node.Name) as Texture;
                    }
                    break;
                case "text":
                    Text text = go.AddComponent<Text>();
                    text.font = Resources.Load("FONT/arial") as Font;
                    text.text = node.Name;
                    break;
            }
            rect.localScale = Vector3.one;
            rect.pivot = new Vector2(0, 1);
            rect.anchorMax = new Vector2(0, 1);
            rect.anchorMin = new Vector2(0, 1);
            rect.sizeDelta = new Vector2(node.Width, node.Height);
            rect.anchoredPosition3D = new Vector3(node.X, -node.Y, 0);
            int length = node.Children.Length;
            for (int i = length - 1; i >= 0; i--)
            {
                CreateRecursion(go, node.Children[i]);
            }
            RectTransform parentRect = parent.GetComponent<RectTransform>();
            Vector3 parentAnchoredPosition = parentRect.anchoredPosition3D;
            rect.anchoredPosition3D = rect.anchoredPosition3D - parentAnchoredPosition;
        }
    }
}
