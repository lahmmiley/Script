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
    public class TextureFormater
    {
        public static void GenerateDebugSources(string path, string fileName)
        {
            Debug.LogError(path);
            path = path.Replace("Data", "Image");
            path = path.Substring(0, path.IndexOf('.'));
            Debug.LogError(path);
            FormatTexture(path, fileName);
        }

        static void FormatTexture(string path, string fileName)
        {
            DirectoryInfo rootDirInfo = new DirectoryInfo(path);
            string spriteDir = Application.dataPath + "/Resources/Sprite/" + fileName + "/";
            //dataPath 到Assets这层
            Debug.LogError(spriteDir);

            if(!Directory.Exists(spriteDir))
            {
                Directory.CreateDirectory(spriteDir);
            }
            spriteDir = spriteDir.Substring(spriteDir.IndexOf("Assets"));

            foreach (FileInfo pngFile in rootDirInfo.GetFiles("*.png", SearchOption.TopDirectoryOnly))
            {
                string allPath = pngFile.FullName;
                string assetPath = allPath.Substring(allPath.IndexOf("Assets"));
                TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
                textureImporter.textureType = TextureImporterType.Sprite;
                textureImporter.mipmapEnabled = false;
                textureImporter.spritePackingTag = fileName.ToLower().Substring(10);
                AssetDatabase.ImportAsset(path);
                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
                GameObject go = new GameObject(sprite.name);
                go.AddComponent<SpriteRenderer>().sprite = sprite;
                string prefabPath = spriteDir + sprite.name + ".prefab";
                PrefabUtility.CreatePrefab(prefabPath, go);
                GameObject.DestroyImmediate(go);
            }
        }


        static Object[] GetSelectedTextureArray()
        {
            return Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        }
    }
}
