using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{

    /// <summary>
    /// 「要素名」「Sf:要素名;」
    /// の２つの表記を持つ要素名。System Function。
    /// </summary>
    public interface SfName
    {



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 属性名。書式「Sf:☆;」。
        /// </summary>
        string Sf
        {
            get;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 属性名。「Sf:☆;」の「☆」の部分。
        /// </summary>
        string Plain
        {
            get;
        }

        //────────────────────────────────────────
        #endregion



    }
}
