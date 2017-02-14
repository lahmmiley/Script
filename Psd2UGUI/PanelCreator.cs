using LitJson;
using Psd2UGUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

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

        public void Create(string path)
        {
            StreamReader sr = new StreamReader("D:/code/PsdParser/ArtProject/UIGenerator/Assets/Resources/Data/Button.json");
            string content = sr.ReadToEnd();
            JsonData jsonData = JsonMapper.ToObject(content);
            BaseNode root = CreateNodeTree(jsonData);
            GameObject goParent = GameObject.Find("Layer");
            root.Build(goParent.transform);
        }

        private BaseNode CreateNodeTree(JsonData jsonData)
        {
            BaseNode node = NodeFactory.Create(jsonData);
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
