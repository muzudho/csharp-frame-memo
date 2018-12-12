using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{

    public delegate void DELEGATE_Conf_Nodes(Conf_String expr_Child, ref bool bBreak);


    /// <summary>
    /// 記述ファイル、記述要素をたどれる仕組み。
    /// 
    /// これはツリー構造なので、テーブルには向かない。
    /// 
    /// 読み取り専用。
    /// 
    /// 旧名：Ｃonfigurationtree_Node
    /// 旧名：Ｃonfiguration_Node
    /// </summary>
    public interface Conf_String
    {


        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// new された直後の内容に戻します。
        /// </summary>
        void Clear(string name, Conf_String parent_OrNull, Log_Reports log_Reports);

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────


        /// <summary>
        /// 位置型のパンくずリスト。
        /// この設定の書かれているファイル名、要素等を示す文字列。
        /// 
        /// 無限ループを防ぐために、このメソッドの中で参照できるのは、
        /// 親元のオブジェクトのみです。
        /// </summary>
        void ToText_Locationbreadcrumbs(Log_TextIndented s);

        /// <summary>
        /// 問題が起こったときに、設定ファイル等で、どのような内容だったのかを示す説明などに利用。
        /// 
        /// 無限ループを防ぐために、このメソッドの中では、
        /// 親を参照してはいけません。
        /// 
        /// 同じインスタンスの、ToText_Content の中で使うことができます。
        /// </summary>
        void ToText_Content(Log_TextIndented s);

        /// <summary>
        /// 要素名を指定して、直近の親ノードを取得します。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isRequired"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        Conf_String GetParentByName(string name, bool isRequired, Log_Reports log_Reports);


        //────────────────────────────────────────
        
        /// <summary>
        /// 直近の１件の子要素を返します。
        /// 該当がなければヌルを返します。
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="isRequired">該当がない場合にエラー扱いにするなら真</param>
        /// <returns></returns>
        Conf_String GetFirstChildByName(
            string sExpected,
            bool isRequired,
            Log_Reports log_Reports);

        /// <summary>
        /// 直近の１件の子要素を返します。
        /// 該当がなければヌルを返します。
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="isRequired">該当がない場合にエラー扱いにするなら真</param>
        /// <returns></returns>
        Conf_String GetFirstChildByAttr(
            PmName sExpectedName,
            string sExpectedValue,
            bool isRequired,
            Log_Reports log_Reports);

        /// <summary>
        /// 要素名を指定して、子ノードを取得します。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isRequired">偽を指定した時は、要素数0のリストを返す。</param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        List<Conf_String> GetChildrenByName(string name, bool isRequired, Log_Reports log_Reports);

        //────────────────────────────────────────
        #endregion




        #region プロパティー
        //────────────────────────────────────────


        /// <summary>
        /// 要素名。関数なら「Sf:Cell;」といった関数名。
        /// </summary>
        string Name
        {
            get;
        }


        /// <summary>
        /// 親。なければヌル。
        /// </summary>
        Conf_String Parent
        {
            get;
        }


        //────────────────────────────────────────

        /// <summary>
        /// 属性＝””。
        /// 
        /// 旧名：Dictionary_Attribute
        /// </summary>
        ConfStringMap Attributes
        {
            get;
        }

        /// <summary>
        /// 子のリスト。
        /// </summary>
        ConfStringList ChildNodes
        {
            get;
        }

        //────────────────────────────────────────
        #endregion



    }



}
