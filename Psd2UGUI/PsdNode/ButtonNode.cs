using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Psd2UGUI
{
    public class ButtonNode : ContainerNode
    {
        public override void Build(Transform parent)
        {
            base.Build(parent);

            Button button = this.gameObject.AddComponent<Button>();
            button.targetGraphic = this.gameObject.transform.GetComponentInChildren<Image>();
            //button.transition = Selectable.Transition.None;
            button.onClick.AddListener(ButtonOnClick);
        }

        private void ButtonOnClick()
        {
            Debug.LogError("你好，我是阿弟的啊嫲");
        }
    }
}
