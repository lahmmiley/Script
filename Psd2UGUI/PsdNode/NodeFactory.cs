using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Psd2UGUI
{
    public class NodeFactory
    {
        public static BaseNode Create(JsonData jsonData)
        {
            BaseNode result;
            string typeStr = jsonData["Type"].ToString().ToLower();
            switch(typeStr)
            {
                case "text":
                    result = new TextNode();
                    break;
                case "image":
                    result = new ImageNode(jsonData);
                    break;
                case "scrollview":
                    result = new ScrollViewNode(jsonData);
                    break;
                default:
                    result = new ContainerNode();
                    break;
            }
            result.Name = jsonData[BaseNode.FIELD_NAME].ToString();
            result.Width = (int)jsonData[BaseNode.FIELD_WIDTH];
            result.Height = (int)jsonData[BaseNode.FIELD_HEIGHT];
            result.X = (int)jsonData[BaseNode.FIELD_X];
            result.Y = (int)jsonData[BaseNode.FIELD_Y];
            return result;
        }
    }
}
