using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

namespace Psd2UGUI
{
    public class ScrollViewNode : ContainerNode
    {
        public ScrollViewNode(JsonData jsonData)
        {
        }

        public override void ProcessStruct(JsonData jsonData)
        {
            JsonData mask = null;
            int maskIndex = -1;
            for (int i = 0; i < jsonData[BaseNode.FIELD_CHILDREN].Count; i++)
            {
                JsonData child = jsonData[BaseNode.FIELD_CHILDREN][i];
                // mask TODO
                if(child[BaseNode.FIELD_NAME].ToString().ToLower() == "mask")
                {
                    mask = child;
                    maskIndex = i;
                }
            }

            JsonData jArray = new JsonData();
            for (int i = 0; i < jsonData[BaseNode.FIELD_CHILDREN].Count; i++)
            {
                JsonData child = jsonData[BaseNode.FIELD_CHILDREN][i];
                if(i != maskIndex)
                {
                    jArray.Add(child);
                }
            }
            mask[BaseNode.FIELD_CHILDREN] = jArray;
            mask[BaseNode.FIELD_TYPE] = "container";

            jsonData[BaseNode.FIELD_CHILDREN].Clear();
            jsonData[BaseNode.FIELD_CHILDREN].Add(mask);
        }


        public override void Build(Transform parent)
        {
            base.Build(parent);

            Transform transform = gameObject.GetComponent<Transform>(); 
            //TODO
            GameObject goMask = transform.FindChild("mask").gameObject;
            GameObject goContent = transform.FindChild("mask/Content").gameObject;
            AddScrollRect(goMask, goContent);
            AddMaskImage(goMask);
            AddLayoutComponent(goContent);
        }

        private void AddScrollRect(GameObject goMask, GameObject goContent)
        {
            ScrollRect scrollRect = gameObject.AddComponent<ScrollRect>();
            scrollRect.content = goContent.GetComponent<RectTransform>();
            scrollRect.viewport = goMask.GetComponent<RectTransform>();
        }

        private void AddMaskImage(GameObject goMask)
        {
            goMask.AddComponent<Image>();
            Mask mask = goMask.AddComponent<Mask>();
            mask.showMaskGraphic = false;
        }

        private void AddLayoutComponent(GameObject goContent)
        {
            goContent.AddComponent<HorizontalLayoutGroup>();
            goContent.AddComponent<ContentSizeFitter>();
        }
    }
}
