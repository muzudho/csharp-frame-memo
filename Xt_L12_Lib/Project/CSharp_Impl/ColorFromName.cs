using System;
using System.Collections.Generic;
using System.Drawing;//Color
using System.Linq;
using System.Text;

namespace Xenon.Lib
{
    public class ColorFromName
    {



        #region アクション
        //────────────────────────────────────────

        public Color FromName(string name)
        {
            Color clr;

            if ("白" == name)
            {
                clr = Color.White;
            }
            else if ("灰色" == name)
            {
                clr = Color.Gray;
            }
            else if ("赤" == name)
            {
                clr = Color.Red;
            }
            else if ("黄" == name)
            {
                clr = Color.Yellow;
            }
            else if ("緑" == name)
            {
                clr = Color.Green;
            }
            else if ("青" == name)
            {
                clr = Color.Blue;
            }
            else if ("黒" == name)
            {
                clr = Color.Black;
            }
            else
            {
                // 未定義の名前は黒。
                clr = Color.Black;
            }

            return clr;
        }

        //────────────────────────────────────────
        #endregion



    }

}
