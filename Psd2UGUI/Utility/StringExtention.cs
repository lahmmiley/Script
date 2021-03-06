﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Psd2UGUI
{
    public static class StringExtention
    {
        public static Color ToColor(this string colorStr)
        {
            int r = Convert.ToInt32(colorStr.Substring(2, 2), 16);
            int g = Convert.ToInt32(colorStr.Substring(4, 2), 16);
            int b = Convert.ToInt32(colorStr.Substring(6, 2), 16);
            return new Color(r / 255f, g / 255f, b / 255f);
        }
    }
}
