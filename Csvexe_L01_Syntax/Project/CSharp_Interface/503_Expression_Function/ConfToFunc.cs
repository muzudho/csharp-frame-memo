using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{

    /// <summary>
    /// 旧名：ＣonfigurationtreeToFunction
    /// </summary>
    public interface ConfToFunc
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s_Action"></param>
        /// <param name="isRequired"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        Expr_Function Translate(
            Conf_String cSystemFunction,
            bool isRequired,
            Log_Reports log_Reports
        );

        //────────────────────────────────────────
        #endregion



    }
}
