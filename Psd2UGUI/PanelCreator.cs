using LitJson;
using Psd2UGUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
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

        public string CurrentName;

        public void Create(string name)
        {
            CurrentName = name;

            //FormatSprite();
            
            StreamReader sr = new StreamReader(string.Format("Assets/Resources/Data/{0}.json", name));
            string content = sr.ReadToEnd();
            JsonData jsonData = JsonMapper.ToObject(content);
            BaseNode root = CreateNodeTree(jsonData);
            Debug.LogError(root.GetJson());
            GameObject goParent = GameObject.Find("Layer");
            root.Build(goParent.transform);
        }

        private void FormatSprite()
        {
            string folderPath = "Assets/Resources/Image/" + CurrentName + "/";
            DirectoryInfo direction = new DirectoryInfo(folderPath);
            FileInfo[] files = direction.GetFiles("*.png", SearchOption.AllDirectories);
            for(int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i];
                string path = "Assets/Resources/Image/" + CurrentName + "/" + file.Name;
                TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
                textureImporter.textureType = TextureImporterType.Sprite;
                textureImporter.mipmapEnabled = false;
                AssetDatabase.ImportAsset(path);
            }
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
