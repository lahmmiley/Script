using LitJson;
using Psd2UGUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Tool
{
    public class TextureProcessor : AssetPostprocessor
    {
        private static Dictionary<string, Dictionary<string, Vector4>> _jsonDict = new Dictionary<string, Dictionary<string, Vector4>>();

        void OnPreprocessSprites()
        {
            LogError("Sprites:" + assetPath);
        }

        void OnPreprocessTexture()
        {
            if (assetPath.StartsWith(FileUtility.UI_IMAGE_DIR))
            {
                FormatUITexture(assetPath);
            }
        }

        private void FormatUITexture(string assetPath)
        {
            string folderName = FileUtility.GetFolderName(assetPath);
            ReadSilce(folderName);
            string fileName = FileUtility.GetFileName(assetPath);
            TextureImporter textureImporter = (TextureImporter)assetImporter;
            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.mipmapEnabled = false;
            textureImporter.spritePackingTag = folderName;
            Dictionary<string, Vector4> sliceDict = _jsonDict[folderName];
            if(sliceDict.Keys.Contains(fileName))
            {
                textureImporter.spriteBorder = sliceDict[fileName];
            }
        }

        private void ReadSilce(string folderName)
        {
            if(_jsonDict.ContainsKey(folderName))
            {
                return;
            }
            Dictionary<string, Vector4> sliceDict = new Dictionary<string, Vector4>();
            _jsonDict.Add(folderName, sliceDict);
            string jsonPath = FileUtility.GetJsonPath(folderName);
            StreamReader sr = new StreamReader(jsonPath);
            string content = sr.ReadToEnd();
            JsonData jsonData = JsonMapper.ToObject(content);
            TraversalTree(jsonData, sliceDict);
        }

        private static void TraversalTree(JsonData jsonData, Dictionary<string, Vector4> sliceDict)
        {
            string typeStr = jsonData[BaseNode.FIELD_TYPE].ToString().ToLower();
            if ((typeStr == "image") && (jsonData.Keys.Contains(BaseNode.FIELD_PARAM)))
            {
                string name = jsonData[BaseNode.FIELD_NAME].ToString();
                if(!sliceDict.ContainsKey(name))
                {
                    string param = jsonData[BaseNode.FIELD_PARAM].ToString();
                    string[] splitArray = param.Split(',');
                    Vector4 v4 = new Vector4(float.Parse(splitArray[2]), float.Parse(splitArray[1]), float.Parse(splitArray[3]), float.Parse(splitArray[0]));//0:上 1:下 2:左 3:右
                    sliceDict.Add(name, v4);

                }
            }
            if (jsonData.Keys.Contains(BaseNode.FIELD_CHILDREN))
            {
                int length = jsonData[BaseNode.FIELD_CHILDREN].Count;
                JsonData children = jsonData[BaseNode.FIELD_CHILDREN];
                for (int i = 0; i < length; i++)
                {
                    TraversalTree(children[i], sliceDict);
                }
            }
        }
    }
}
