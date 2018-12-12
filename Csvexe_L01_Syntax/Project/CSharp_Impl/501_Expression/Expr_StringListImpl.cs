using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Xenon.Syntax
{

    /// <summary>
    /// 
    /// </summary>
    public class Expr_StringListImpl : Expr_StringList
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public Expr_StringListImpl(Expr_String owner)
        {
            this.owner_Expression = owner;
            this.items = new List<Expr_String>();
        }

        //────────────────────────────────────────

        public void Clear(Log_Reports log_Reports)
        {
            this.items.Clear();
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 
        /// </summary>
        public void ToText_Snapshot(Log_TextIndented txt)
        {
            txt.Increment();


            txt.Append("<" + this.GetType().Name + "クラス>");
            txt.Newline();

            foreach (Expr_String eItem in this.items)
            {
                eItem.Conf.ToText_Content(txt);
            }

            txt.Append("</" + this.GetType().Name + "クラス>");
            txt.Newline();


            txt.Decrement();
        }

        //────────────────────────────────────────

        /// <summary>
        /// 追加。
        /// </summary>
        /// <param name="nItems"></param>
        /// <param name="request"></param>
        /// <param name="log_Reports"></param>
        public void Add(
            Expr_String eChild,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Add",log_Reports);


            if (eChild is Expr_StringImpl)
            {
                ((Expr_StringImpl)eChild).Parent = this.owner_Expression;
            }
            else if (eChild is Expr_LeafStringImpl)
            {
                ((Expr_LeafStringImpl)eChild).Parent = this.owner_Expression;
            }
            else if (eChild is Expr_TexttemplateP1pImpl)
            {
                ((Expr_TexttemplateP1pImpl)eChild).Parent = this.owner_Expression;
            }
            else
            {
                log_Method.WriteWarning_ToConsole(" 想定外のクラス=[" + eChild.GetType().Name + "]");
            }
            this.items.Add(eChild);

            log_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────

        public void SetList(
            List<Expr_String> eItems,
            Log_Reports log_Reports
            )
        {
            this.items = eItems;
        }

        //────────────────────────────────────────

        public void AddList(
            List<Expr_String> eItems,
            Log_Reports log_Reports
            )
        {
            this.items.AddRange(eItems);
        }

        //────────────────────────────────────────

        /// <summary>
        /// デバッグするのに使う内容を取得します。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="log_Reports"></param>
        public void ToText_Debug(Log_TextIndented s, Log_Reports log_Reports)
        {
            s.Append(this.GetType().Name + "#DebugWrite:E_String項目数＝[" + this.items.Count + "]");
            s.Newline();
            s.Append(this.GetType().Name + "#DebugWrite:──────────ここから");
            s.Newline();
            foreach (Expr_String e_Str in this.items)
            {
                s.Append("E_String=[" + e_Str.Lv4Execute_OnImplement(EnumHitcount.Unconstraint, log_Reports) + "]");
                s.Newline();
            }
            s.Append(this.GetType().Name + "#DebugWrite:──────────ここまで");
            s.Newline();
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 親要素。
        /// </summary>
        private Expr_String owner_Expression;

        //────────────────────────────────────────

        /// <summary>
        /// 子＜●●＞要素のリスト。
        /// </summary>
        private List<Expr_String> items;

        public List<Expr_String> SelectList(
            EnumHitcount request,
            Log_Reports log_Reports
            )
        {
            return this.items;
        }

        //────────────────────────────────────────

        public void ForEach(DELEGATE_Expression_Nodes dlgt1)
        {
            bool bBreak = false;
            bool bRemove = false;
            for (int nI = 0; nI < this.items.Count; nI++)
            {
                Expr_String cur_Expression = this.items[nI];

                dlgt1(cur_Expression, ref bRemove, ref bBreak);

                if (bRemove)
                {
                    this.items.RemoveAt(nI);
                    nI--;
                    bRemove = false;
                }

                if (bBreak)
                {
                    break;
                }
            }
        }

        //────────────────────────────────────────

        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
