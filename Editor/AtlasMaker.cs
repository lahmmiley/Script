using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Tool
{
    public class AtlasMaker
    {
        [MenuItem("MyMenu/AtlasMaker")]
        static private void MakeAtlas()
        {
            string spriteDir = Application.dataPath + "/Resources/Sprite";

            if (!Directory.Exists(spriteDir))
            {
                Directory.CreateDirectory(spriteDir);
            }

            DirectoryInfo rootDirInfo = new DirectoryInfo(Application.dataPath + "/Atlas");
            foreach (DirectoryInfo dirInfo in rootDirInfo.GetDirectories())
            {
                foreach (FileInfo pngFile in dirInfo.GetFiles("*.png", SearchOption.AllDirectories))
                {
                    string allPath = pngFile.FullName;
                    string assetPath = allPath.Substring(allPath.IndexOf("Assets"));
                    Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
                    Debug.LogError(assetPath);
                    GameObject go = new GameObject(sprite.name);
                    go.AddComponent<SpriteRenderer>().sprite = sprite;
                    allPath = spriteDir + "/" + sprite.name + ".prefab";
                    string prefabPath = allPath.Substring(allPath.IndexOf("Assets"));
                    PrefabUtility.CreatePrefab(prefabPath, go);
                    GameObject.DestroyImmediate(go);
                }
            }
        }
    }
}
