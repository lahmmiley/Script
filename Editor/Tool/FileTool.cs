using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tool
{
    public class FileTool
    {
        public static string AllPath2AssetPath(string allPath)
        {
            return allPath.Substring(allPath.IndexOf("Assets"));
        }

        public static string GetFileName(string path)
        {
            int startIndex = path.LastIndexOf('/') + 1;
            int endIndex = path.LastIndexOf('.');
            return path.Substring(startIndex, endIndex - startIndex);
        }

        public static string RemovePostfix(string path)
        {
            return path.Substring(0, path.IndexOf('.'));
        }

        public static void CreateDirectory(string dir)
        {
            if(!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }
}
