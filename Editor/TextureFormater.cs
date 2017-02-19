using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tool
{
    class TextureFormater
    {
        [MenuItem("MyMenu/TextureFormater")]
        static void FormatTexture()
        {
            Object[] texturesArray = GetSelectedTextureArray();
            foreach(Texture2D texture in texturesArray)
            {
                string path = AssetDatabase.GetAssetPath(texture);
                //Debug.LogError(path);
                TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
                textureImporter.textureType = TextureImporterType.Sprite;
                textureImporter.mipmapEnabled = false;
                AssetDatabase.ImportAsset(path);
            }
        }


        static Object[] GetSelectedTextureArray()
        {
            return Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        }
    }
}
