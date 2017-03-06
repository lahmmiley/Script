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
        public static void Generate(string jsonPath)
        {
            string fileName = FileTool.GetFileName(jsonPath);

            string spriteDir = Application.dataPath + "/Resources/Sprite/" + fileName + "/";
            FileTool.CreateDirectory(spriteDir);
            spriteDir = FileTool.AllPath2AssetPath(spriteDir);

            string imageDir = FileTool.RemovePostfix(jsonPath.Replace("Data", "Image"));
            DirectoryInfo dirInfo = new DirectoryInfo(imageDir);
            foreach (FileInfo pngFile in dirInfo.GetFiles("*.png", SearchOption.TopDirectoryOnly))
            {
                string assetPath = FileTool.AllPath2AssetPath(pngFile.FullName);
                FormatTexture(assetPath, fileName);
                CreateRelativePrefab(spriteDir, assetPath);
            }
        }

        private static void FormatTexture(string assetPath, string fileName)
        {
            TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.mipmapEnabled = false;
            textureImporter.spritePackingTag = fileName.ToLower().Substring(10);
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
