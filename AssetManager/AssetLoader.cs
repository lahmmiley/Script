using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AssetManager
{
    public class AssetLoader
    {
        //LoadIcon 加载图集的图标

        //LoadSprite 加载面板上的固定资源
        private static char[] _splitChar = new char[] { '.' };
        public static Sprite LoadSprite(string panelName, string spriteName)
        {
            //string[] splits = name.Split(_splitChar);
            //string panelName = splits[0];
            //string spriteName = splits[1];
            return Resources.Load<GameObject>(string.Format("Sprite/{0}/{1}", panelName, spriteName)).GetComponent<SpriteRenderer>().sprite;
        }
    }
}
