using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Tool
{
    public class TextureProcessor : AssetPostprocessor
    {
         void OnPostprocessTexture(Texture2D texture)
        {
            //Debug.LogError(typeof(Texture2D));
        }
    }
}
