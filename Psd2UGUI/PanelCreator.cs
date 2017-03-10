using LitJson;
using Psd2UGUI;
using System.IO;
using UnityEngine;

namespace PsdRebuilder
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

        public string CurrentName;

        public void Create(string name)
        {
            CurrentName = name;

            StreamReader sr = new StreamReader(string.Format("Assets/UI/Data/{0}.json", name));
            string content = sr.ReadToEnd();
            JsonData jsonData = JsonMapper.ToObject(content);
            BaseNode root = CreateNodeTree(jsonData);
            //Debug.LogError(root.GetJson());
            GameObject goParent = GameObject.Find("Layer");
            root.Build(goParent.transform);
            //TODO
            //居中
            GameObject goRoot = goParent.transform.FindChild("root").gameObject;
            goRoot.name = name;
            RectTransform rectRoot = goRoot.GetComponent<RectTransform>();
            rectRoot.anchorMin = Vector2.one * 0.5f;
            rectRoot.anchorMax = Vector2.one * 0.5f;
            rectRoot.pivot = Vector2.one * 0.5f;
            rectRoot.anchoredPosition = Vector2.zero;
        }

        private BaseNode CreateNodeTree(JsonData jsonData)
        {
            BaseNode node = NodeFactory.Create(jsonData);
            node.ProcessStruct(jsonData);
            if(jsonData.Keys.Contains(BaseNode.FIELD_CHILDREN))
            {
                int length = jsonData[BaseNode.FIELD_CHILDREN].Count;
                node.Children = new BaseNode[length];
                JsonData children = jsonData[BaseNode.FIELD_CHILDREN];
                for(int i = 0; i < length; i++)
                {
                    node.Children[i] =  CreateNodeTree(children[i]);
                }
            }
            return node;
        }
    }
}
