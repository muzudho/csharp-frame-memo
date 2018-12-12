using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Xenon.Syntax
{

    /// <summary>
    /// 文字をセットできます。（ツリー構造の葉）
    /// 
    /// 旧名:Expression_Leaf_String
    /// </summary>
    public interface Expr_LeafString : Expr_String
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// 新しいインスタンスを作ります。
        /// </summary>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        Expr_LeafString NewInstance(
            Conf_String parent_Conf,
            Log_Reports log_Reports
            );

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 内容を文字列型でセットします。
        /// </summary>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        void SetString(
            string sHumanInput,
            Log_Reports log_Reports
            );

        //────────────────────────────────────────
        #endregion


    
    }
}
