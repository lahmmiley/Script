﻿using LitJson;
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
        public static void Generate(string jsonPath)
        {
            string jsonFileName = FileUtility.GetFileName(jsonPath);
            string imageFolderName = jsonFileName;

            string spriteDir = FileUtility.RESOURCE_SPRITE_DIR + imageFolderName + "/";
            FileUtility.RecreateDirectory(spriteDir);

            string imageDir = FileUtility.UI_IMAGE_DIR + FileUtility.RemovePostfix(jsonFileName);
            DirectoryInfo dirInfo = new DirectoryInfo(imageDir);
            foreach (FileInfo pngFile in dirInfo.GetFiles("*" + FileUtility.PNG_POSTFIX, SearchOption.TopDirectoryOnly))
            {
                string assetPath = FileUtility.AllPath2AssetPath(pngFile.FullName);
                CreateRelativePrefab(spriteDir, assetPath);
            }
            AssetDatabase.Refresh();
        }

        private static void CreateRelativePrefab(string spriteDir, string assetPath)
        {
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
            GameObject go = new GameObject(sprite.name);
            go.AddComponent<SpriteRenderer>().sprite = sprite;
            string prefabPath = spriteDir + sprite.name + FileUtility.PREFAB_POSTFIX;
            PrefabUtility.CreatePrefab(prefabPath, go);
            GameObject.DestroyImmediate(go);
        }
    }
}
