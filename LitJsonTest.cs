using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using Assets.Script.PsdRebuilder;
using System;
using UnityEngine.UI;
using UnityEditor;

public class LitJsonTest : MonoBehaviour {

	// Use this for initialization
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
        StreamReader sr = new StreamReader("D:/code/PsdParser/ArtProject/UIGenerator/Assets/Resources/Data/Button.json");
        string content = sr.ReadToEnd();
        PsdNode root = JsonMapper.ToObject<PsdNode>(content);
        PanelCreator.Instance.Create(root);
        //string rootContent = RebuildJson(root, 0);
        //Debug.LogError(rootContent);
    }

    private string RebuildJson(PsdNode root, int depth)
    {
        string prefix = string.Empty;
        for(int i = 0; i < depth; i++)
        {
            prefix += "\t";
        }
        string content = prefix + root.Name + "\n";
        if(root.Children != null)
        {
            for(int i = 0; i < root.Children.Length; i++)
            {
                content += RebuildJson(root.Children[i], depth + 1);
            }
        }
        return content;
    }
}
