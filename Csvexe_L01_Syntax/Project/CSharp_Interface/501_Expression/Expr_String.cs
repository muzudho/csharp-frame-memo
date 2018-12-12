using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Xenon.Syntax
{

    public delegate void DELEGATE_Expression_Nodes(Expr_String expr_Node, ref bool isRemove, ref bool isBreak);
    public delegate void DELEGATE_Expression_Attributes(string attrName, Expr_String eAttr, ref bool isBreak);

    /// <summary>
    /// ツリー構造の葉以外の節。
    /// 
    /// サブクラスは
    /// ・L01_Cushion:Ev_Elem、
    /// ・L03_Opyopyo:Ev_4ASelectRecord
    /// 
    /// 【方針】読み取り専用にしたい。
    /// 
    /// 旧名：Expression_Node_String
    /// </summary>
    public interface Expr_String
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 内容を確認するのに使います。
        /// </summary>
        void ToText_Snapshot(Log_TextIndented s);

        //────────────────────────────────────────

        /// <summary>
        /// 属性を取得します。
        /// </summary>
        /// <param name="out_Expression_Result">検索結果。</param>
        /// <param name="sName"></param>
        /// <param name="bRequired"></param>
        /// <param name="hits"></param>
        /// <param name="log_Reports"></param>
        /// <returns>検索結果が1件以上あれば真。</returns>
        bool TrySelectAttribute_ExpressionFilepath(
            out Expr_Filepath out_Expression_Result,
            string sName,
            EnumHitcount hits,
            Log_Reports log_Reports
            );

        /// <summary>
        /// 属性を取得します。
        /// </summary>
        /// <param name="out_Expression_Result">検索結果。</param>
        /// <param name="sName"></param>
        /// <param name="bRequired"></param>
        /// <param name="hits"></param>
        /// <param name="log_Reports"></param>
        /// <returns>検索結果が1件以上あれば真。</returns>
        bool TrySelectAttribute(
            out Expr_String out_Expression_Result,
            string sName,
            EnumHitcount hits,
            Log_Reports log_Reports
            );

        /// <summary>
        /// 属性を取得します。
        /// </summary>
        /// <param name="out_Result">検索結果。</param>
        /// <param name="sName"></param>
        /// <param name="bRequired"></param>
        /// <param name="hits"></param>
        /// <param name="log_Reports"></param>
        /// <returns>検索結果が1件以上あれば真。</returns>
        bool TrySelectAttribute(
            out string out_SResult,
            string sName,
            EnumHitcount hits,
            Log_Reports log_Reports
            );

        //────────────────────────────────────────

        /// <summary>
        /// 内部実装用。
        /// 
        /// 旧名：Execute5_Main
        /// </summary>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        string Lv5_Implement(
            Log_Reports log_Reports
            );

        /// <summary>
        /// ユーザー定義プログラムを実行。
        /// 
        /// ・「Execute_」系は、デバッグ出力のために使ってはいけません。
        /// 
        /// 旧名：Execute4_OnExpressionString
        /// </summary>
        /// <param name="request">どういう結果が欲しいかの指定。</param>
        /// <param name="log_Reports"></param>
        /// <returns>処理結果の結合文字列。</returns>
        string Lv4Execute_OnImplement(
            EnumHitcount request,
            Log_Reports log_Reports
            );

        /// <summary>
        /// 旧名：Execute4_OnExpressionString_AsFilepath
        /// </summary>
        /// <param name="request"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        Expr_Filepath Lv4Execute_OnImplement_AsFilepath(
            EnumHitcount request,
            Log_Reports log_Reports
            );

        /// <summary>
        /// 親E_Stringを遡って検索。一致するものがなければヌル。
        /// </summary>
        /// <returns></returns>
        Expr_String GetParentExpressionOrNull(string sName_Node);

        /// <summary>
        /// 例えば　"data"　と指定すれば、
        /// 直下の子要素の中で　＜ｄａｔａ　＞ といったノード名を持つものはヒットする。
        /// 
        /// 読み取った要素をリストから削除するなら、bRemove=true とします。
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="sExpectedValue"></param>
        /// <param name="request"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        List<Expr_String> SelectDirectchildByNodename(
            string sName_ExpectedNode, bool bRemove, EnumHitcount request, Log_Reports log_Reports);

        /// <summary>
        /// 文字列を、子要素として追加。
        /// </summary>
        /// <param name="sHumaninput"></param>
        /// <param name="parent_Conf"></param>
        /// <param name="log_Reports"></param>
        void AppendTextNode(
            string sHumaninput,
            Conf_String parent_Conf,
            Log_Reports log_Reports
            );

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 属性="" マップ。
        /// </summary>
        ExprStringMap Attributes
        {
            get;
        }

        /// <summary>
        /// 属性を上書きします。
        /// </summary>
        /// <param name="name_Attribute"></param>
        /// <param name="expr_Attribute"></param>
        /// <param name="log_Reports"></param>
        void SetAttribute(
            string name_Attribute,
            Expr_String expr_Attribute,
            Log_Reports log_Reports
            );

        //────────────────────────────────────────

        /// <summary>
        /// 親要素。
        /// 
        /// コンストラクターだけではなく、タイミングを遅らせて、後付けで設定されることもあります。
        /// 
        /// 旧名：Parent_Expression
        /// </summary>
        Expr_String Parent
        {
            get;
            set;
        }

        /// <summary>
        /// 設定場所のヒント。
        /// 
        /// コンストラクターだけではなく、タイミングを遅らせて、後付けで設定されることもあります。
        /// 
        /// 旧名：Cur_Ｃonfiguration
        /// </summary>
        Conf_String Conf
        {
            get;
            set;
        }

        /// <summary>
        /// 子要素リスト。
        /// </summary>
        Expr_StringList ChildNodes
        {
            get;
        }

        //────────────────────────────────────────
        #endregion



    }


}
