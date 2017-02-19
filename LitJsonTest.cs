using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using PsdRebuilder;
using System;
using UnityEngine.UI;
using UnityEditor;
using Psd2UGUI;

public class LitJsonTest : MonoBehaviour {

	void Start ()
    {
        CreatePanel();
        //LoadImage();
    }

    private void LoadImage()
    {
        GameObject go = GameObject.Find("Image");
        Image image = go.GetComponent<Image>();
        Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Atlas/Button/buttonNormal.png");
        image.sprite = sprite;
    }

    private void CreatePanel()
    {
        PanelCreator.Instance.Create("BattlePreparePanel");
        //PanelCreator.Instance.Create("Button");
    }

}
