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
        public override void ProcessStruct(JsonData jsonData)
        {
            int maskIndex = FindIndexByName(jsonData, MaskNode.MASK);
            JsonData mask = jsonData[BaseNode.FIELD_CHILDREN][maskIndex];
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
            mask[BaseNode.FIELD_TYPE] = MaskNode.MASK;

            jsonData[BaseNode.FIELD_CHILDREN].Clear();
            jsonData[BaseNode.FIELD_CHILDREN].Add(mask);
        }

        public override void Build(Transform parent)
        {
            base.Build(parent);

            Transform transform = gameObject.GetComponent<Transform>(); 
            //TODO
            GameObject goMask = transform.FindChild("mask").gameObject;
            GameObject goContent = null;
            //TODO
            if(transform.FindChild("mask/content") != null)
            {
                goContent = transform.FindChild("mask/content").gameObject;
            }
            else
            {
                goContent = transform.FindChild("mask/Content").gameObject;
            }
            AddScrollRect(goMask, goContent);
            AddLayoutComponent(goContent);
        }

        private void AddScrollRect(GameObject goMask, GameObject goContent)
        {
            ScrollRect scrollRect = gameObject.AddComponent<ScrollRect>();
            scrollRect.content = goContent.GetComponent<RectTransform>();
            scrollRect.viewport = goMask.GetComponent<RectTransform>();
        }


        private void AddLayoutComponent(GameObject goContent)
        {
            goContent.AddComponent<HorizontalLayoutGroup>();
            goContent.AddComponent<ContentSizeFitter>();
        }

        private int FindIndexByName(JsonData jsonData, string name)
        {
            int result = -1;
            for (int i = 0; i < jsonData[BaseNode.FIELD_CHILDREN].Count; i++)
            {
                JsonData child = jsonData[BaseNode.FIELD_CHILDREN][i];
                if(child[BaseNode.FIELD_NAME].ToString().ToLower() == MaskNode.MASK)
                {
                    result = i;
                    break;
                }
            }
            return result;
        }
    }
}
