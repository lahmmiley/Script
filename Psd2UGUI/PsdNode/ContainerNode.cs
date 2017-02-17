using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Psd2UGUI
{
    public class ContainerNode : BaseNode
    {
        public override void Build(Transform parent)
        {
            GameObject go = CreateGameObject();
            this.gameObject = go;
            Transform transform = go.transform;
            transform.SetParent(parent, false);
            int length = Children.Length;
            for (int i = length - 1; i >= 0; i--)
            {
                Children[i].Build(transform);
            }

            //重新计算坐标
            RectTransform parentRect = parent.GetComponent<RectTransform>();
            //TODO
            if (parentRect != null)
            {
                Vector3 parentAnchoredPosition = parentRect.anchoredPosition3D;
                RectTransform rect = go.GetComponent<RectTransform>();
                rect.anchoredPosition3D = rect.anchoredPosition3D - parentAnchoredPosition;
            }
        }
    }
}
