using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{
    /// <summary>
    /// 名前。 GetElementByName(o_Name)のような引数として使う。
    /// 
    /// 例：バリデーターの引数、イベント名、トゥゲザーの名前、変数名。
    /// </summary>
    public interface XenonName
    {



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 名前の文字列。
        /// </summary>
        string SValue
        {
            get;
        }

        /// <summary>
        /// 旧名：Cur_Ｃonfiguration
        /// </summary>
        Conf_String Conf
        {
            get;
        }

        //────────────────────────────────────────
        #endregion



    }
}
