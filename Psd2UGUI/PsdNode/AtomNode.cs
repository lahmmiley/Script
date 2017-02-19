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

        //public enum State
        //{
        //    Normal = 1,
        //    Over = 2,
        //    Disable = 3,
        //}

        protected Dictionary<string, string> stateDict;
        protected void SetState(JsonData jsonData)
        {
            stateDict = new Dictionary<string, string>();
            if(jsonData.Keys.Contains(FIELD_CHILDREN))
            {
                for(int i = 0; i < jsonData[FIELD_CHILDREN].Count; i++)
                {
                    JsonData child = jsonData[FIELD_CHILDREN][i];
                    string state = child[FIELD_NAME].ToString().ToLower();
                    string name = child[FIELD_CHILDREN][0][FIELD_NAME].ToString();
                    stateDict.Add(state, name);
                }
            }
            else
            {
                stateDict.Add(STATE_NORMAL, jsonData[FIELD_NAME].ToString());
            }
            
        }
    }
}
