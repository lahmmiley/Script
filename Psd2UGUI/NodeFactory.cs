using LitJson;

namespace Psd2UGUI
{
    public class NodeFactory
    {
        public static BaseNode Create(JsonData jsonData)
        {
            BaseNode result;
            string typeStr = jsonData[NodeField.TYPE].ToString().ToLower();
            switch(typeStr)
            {
                case "text":
                    result = new TextNode();
                    break;
                case "image":
                    result = new ImageNode();
                    break;
                case "mask":
                    result = new MaskNode();
                    break;
                case "button":
                    result = new ButtonNode();
                    break;
                case "scrollview":
                    result = new ScrollViewNode();
                    break;
                case "togglegroup":
                    result = new ToggleGroupNode();
                    break;
                case "toggle":
                    result = new ToggleNode();
                    break;
                case "list":
                    result = new ListNode();
                    break;
                default:
                    result = new ContainerNode();
                    break;
            }
            result.Name = jsonData[NodeField.NAME].ToString();
            result.Width = (int)jsonData[NodeField.WIDTH];
            result.Height = (int)jsonData[NodeField.HEIGHT];
            result.X = (int)jsonData[NodeField.X];
            result.Y = (int)jsonData[NodeField.Y];
            return result;
        }
    }
}
