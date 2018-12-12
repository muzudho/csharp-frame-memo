﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Xenon.Syntax
{

    /// <summary>
    /// 旧名：List_Expression_Node_String
    /// </summary>
    public interface Expr_StringList
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// new された直後の内容に戻します。
        /// </summary>
        /// <param name="log_Reports"></param>
        void Clear(Log_Reports log_Reports);

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 
        /// </summary>
        void ToText_Snapshot(Log_TextIndented s);

        /// <summary>
        /// 要素の追加。
        /// </summary>
        /// <param name="eItem"></param>
        /// <param name="log_Reports"></param>
        void Add(
            Expr_String eItem,
            Log_Reports log_Reports
            );

        /// <summary>
        /// 要素の追加。
        /// </summary>
        /// <param name="eItems"></param>
        /// <param name="log_Reports"></param>
        void AddList(
            List<Expr_String> eItems,
            Log_Reports log_Reports
            );

        /// <summary>
        /// リストの上書き。
        /// </summary>
        /// <param name="eItems"></param>
        /// <param name="log_Reports"></param>
        void SetList(
            List<Expr_String> listExpression_Item,
            Log_Reports log_Reports
            );

        /// <summary>
        /// 子要素のリスト。
        /// </summary>
        List<Expr_String> SelectList(
            EnumHitcount request,
            Log_Reports log_Reports
            );

        /// <summary>
        /// デバッグするのに使う内容を取得します。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="log_Reports"></param>
        void ToText_Debug(Log_TextIndented s, Log_Reports log_Reports);

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// それぞれの要素。
        /// </summary>
        /// <param name="dlgt1"></param>
        void ForEach(DELEGATE_Expression_Nodes dlgt1);

        /// <summary>
        /// 要素の個数。
        /// </summary>
        int Count
        {
            get;
        }

        //────────────────────────────────────────
        #endregion



    }
}
