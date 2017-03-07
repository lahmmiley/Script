using LitJson;
using Psd2UGUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tool
{
    public class DevelopResourcesGenerator
    {
        private static Dictionary<string, Vector4> _sliceDict = new Dictionary<string, Vector4>();

        public static void Generate(string jsonPath)
        {
            string jsonFileName = FileTool.GetFileName(jsonPath);
            string imageFolderName = jsonFileName;

            Read9SliceParam(jsonPath);

            string spriteDir = Application.dataPath + "/Resources/Sprite/" + imageFolderName + "/";
            FileTool.CreateDirectory(spriteDir);
            spriteDir = FileTool.AllPath2AssetPath(spriteDir);

            string imageDir = FileTool.RemovePostfix(jsonPath.Replace("Data", "Image"));
            DirectoryInfo dirInfo = new DirectoryInfo(imageDir);
            foreach (FileInfo pngFile in dirInfo.GetFiles("*.png", SearchOption.TopDirectoryOnly))
            {
                string assetPath = FileTool.AllPath2AssetPath(pngFile.FullName);
                FormatTexture(FileTool.RemovePostfix(pngFile.Name), assetPath, imageFolderName);
                CreateRelativePrefab(spriteDir, assetPath);
            }
        }

        private static void Read9SliceParam(string jsonPath)
        {
            StreamReader sr = new StreamReader(jsonPath);
            string content = sr.ReadToEnd();
            JsonData jsonData = JsonMapper.ToObject(content);
            TraversalTree(jsonData);
        }

        private static void TraversalTree(JsonData jsonData)
        {
            string typeStr = jsonData[BaseNode.FIELD_TYPE].ToString().ToLower();
            if (typeStr == "image")
            {
                if (jsonData[BaseNode.FIELD_PARAM] != null)
                {
                    string name = jsonData[BaseNode.FIELD_NAME].ToString();
                    string param = jsonData[BaseNode.FIELD_PARAM].ToString();
                    string[] splitArray = param.Split(',');
                    Vector4 v4 = new Vector4(float.Parse(splitArray[3]), float.Parse(splitArray[2]), float.Parse(splitArray[1]), float.Parse(splitArray[0]));//上下左右
                    _sliceDict.Add(name, v4);
                }
            }
            if(jsonData.Keys.Contains(BaseNode.FIELD_CHILDREN))
            {
                int length = jsonData[BaseNode.FIELD_CHILDREN].Count;
                JsonData children = jsonData[BaseNode.FIELD_CHILDREN];
                for(int i = 0; i < length; i++)
                {
                    TraversalTree(children[i]);
                }
            }
        }

        private static void FormatTexture(string pngName, string assetPath, string imageFolderName)
        {
            Debug.LogError(pngName);
            TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.mipmapEnabled = false;
            textureImporter.spritePackingTag = imageFolderName;
            if(_sliceDict.Keys.Contains(pngName))
            {
                textureImporter.spriteBorder = _sliceDict[pngName];
            }
            AssetDatabase.ImportAsset(assetPath);
        }

        private static void CreateRelativePrefab(string spriteDir, string assetPath)
        {
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
            GameObject go = new GameObject(sprite.name);
            go.AddComponent<SpriteRenderer>().sprite = sprite;
            string prefabPath = spriteDir + sprite.name + ".prefab";
            PrefabUtility.CreatePrefab(prefabPath, go);
            GameObject.DestroyImmediate(go);
        }
    }
}
