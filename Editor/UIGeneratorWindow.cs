using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tool
{
    public class UIGeneratorWindow : EditorWindow
    {
        [MenuItem("Psd2UGUI/Window")]
        static void Init()
        {
            Rect rect = new Rect(100, 0, 500, 200);
            UIGeneratorWindow window = (UIGeneratorWindow)EditorWindow.GetWindowWithRect<UIGeneratorWindow>(rect, true, "yes");
            window.Show();
        }

        private string _tips;
        private Object _jsonFile;

        private void OnGUI()
        {
            _jsonFile = EditorGUILayout.ObjectField("添加Json文件", _jsonFile, typeof(TextAsset), false, null);
            if(_jsonFile != null)
            {
                string path = AssetDatabase.GetAssetPath(_jsonFile);
                if(path.EndsWith(".json"))
                {
                    if(GUILayout.Button("生成", GUILayout.Width(100)))
                    {
                        string fileName = GetFileName(path);
                        TextureFormater.GenerateDebugSources(path, fileName);
                    }

                    //Debug.LogError(fileName);
                }
                else
                {
                    _tips = "<color=#FF0000>选择的文件格式并非Json</color>";
                    _jsonFile = null;
                }
            }
            else
            {
                _tips = "请选择Json文件";
            }

            EditorGUILayout.LabelField(_tips);
        }

        private string GetFileName(string path)
        {
            int startIndex = path.LastIndexOf('/') + 1;
            int endIndex = path.LastIndexOf('.');
            return path.Substring(startIndex, endIndex - startIndex);
        }
    }
}
