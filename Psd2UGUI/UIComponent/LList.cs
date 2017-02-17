using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIComponent
{
    public class LList : UIBehaviour
    {
        private const string TEMPLET_NAME = "item";
        private GameObject _templet;

        public LList()
        {
            _templet = transform.FindChild(TEMPLET_NAME).gameObject;
        }


        public class Item
        {
        }
    }


}
