
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadAtlasImage : MonoBehaviour
{

    private string _name = "gogo.assetbundle";
	IEnumerator Start ()
    {
        string assetBundlePath = "file://" + Application.dataPath + "/AssetBundles/";
        //Debug.LogError(assetBundlePath);
        string manifestPath = assetBundlePath + "AssetBundles";
        WWW wwwManifest = WWW.LoadFromCacheOrDownload(manifestPath, 0);
        yield return wwwManifest;
        AssetBundle asssetBundle = null;
        if (wwwManifest.error == null)
        {
            AssetBundle manifestBundle = wwwManifest.assetBundle;
            AssetBundleManifest manifest = (AssetBundleManifest)manifestBundle.LoadAsset("AssetBundleManifest");
            manifestBundle.Unload(false);

            WWW www2 = WWW.LoadFromCacheOrDownload(assetBundlePath + _name, 0);
            yield return www2;
            asssetBundle = www2.assetBundle;
        }
        else
        {
            Debug.LogError("error");
        }


        UnityEngine.Object[] list = asssetBundle.LoadAllAssets();
        GameObject go = GameObject.Find("Image");
        Image image = go.GetComponent<Image>();
        for(int i = 0;i < list.Length; i++)
        {
            UnityEngine.Object obj = list[i];
            Debug.LogError(obj.name);
            if(obj.name.StartsWith("gg"))
            {
                image.sprite = obj as Sprite;
                image.SetNativeSize();
                yield return new WaitForSeconds(1);
            }
        }



        //Sprite normalSprite = Resources.Load(string.Format("IMAGE/Button/Background.png"),
        //        typeof(Sprite)) as Sprite;
        //Debug.LogError(normalSprite);
        //image.sprite = normalSprite;
	}

}
