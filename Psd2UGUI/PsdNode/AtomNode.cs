using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Psd2UGUI
{
    public abstract class AtomNode : BaseNode
    {
        public const string STATE_NORMAL = "normal";
        public const string STATE_OVER = "over";
        public const string STATE_DISABLE = "disable";

        protected Dictionary<string, string> stateDict;
        protected void SetState(JsonData jsonData)
        {
            stateDict = new Dictionary<string, string>();
            if(jsonData.Keys.Contains(NodeField.CHILDREN))
            {
                for(int i = 0; i < jsonData[NodeField.CHILDREN].Count; i++)
                {
                    JsonData child = jsonData[NodeField.CHILDREN][i];
                    string state = child[NodeField.NAME].ToString().ToLower();
                    string name = child[NodeField.CHILDREN][0][NodeField.NAME].ToString();
                    stateDict.Add(state, name);
                }
            }
            else
            {
                stateDict.Add(STATE_NORMAL, jsonData[NodeField.NAME].ToString());
            }
            
        }
    }
}
