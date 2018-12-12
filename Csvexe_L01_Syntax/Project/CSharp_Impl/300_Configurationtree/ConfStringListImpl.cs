using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{

    /// <summary>
    /// 主に、子要素を入れるのに使う。
    /// 
    /// 旧名：List_Ｃonfigurationtree_NodeImpl
    /// 旧名：List_Ｃonf_NodeImpl
    /// </summary>
    public class ConfStringListImpl : ConfStringList
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public ConfStringListImpl(Conf_String cOwner)
        {
            this.owner = cOwner;
            this.items = new List<Conf_String>();
        }

        //────────────────────────────────────────

        /// <summary>
        /// リストを空にする。
        /// </summary>
        /// <param name="log_Reports"></param>
        public void Clear(
            Log_Reports log_Reports
            )
        {
            this.items.Clear();
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        public virtual void ToText_Content(Log_TextIndented s)
        {
            s.Increment();


            //
            // 子要素
            foreach (Conf_String cElm in this.items)
            {
                cElm.ToText_Content(s);
            }

            // 子要素しかありません。

            s.Decrement();
        }

        //────────────────────────────────────────

        /// <summary>
        /// 追加。
        /// </summary>
        public void Add(
            Conf_String cElm,
            Log_Reports log_Reports
            )
        {
            this.items.Add(cElm);
        }

        //────────────────────────────────────────

        /// <summary>
        /// @Deprecated
        /// 一覧。
        /// </summary>
        public List<Conf_String> SelectList(
            EnumHitcount request,
            Log_Reports log_Reports
            )
        {
            return items;
        }

        //────────────────────────────────────────

        /// <summary>
        /// ノードを、リストのindexで指定して、取得します。
        /// 該当がなければヌルを返します。
        /// </summary>
        /// <param select="index">リストのインデックス</param>
        /// <param select="bRequired">該当するデータがない場合、エラー</param>
        /// <param select="log_Reports">警告メッセージ</param>
        /// <returns></returns>
        public Conf_String GetNodeByIndex(
            int index,
            bool isRequired,
            Log_Reports log_Reports
            )
        {

            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "GetNodeByIndex",log_Reports);
            //
            //
            //
            //

            Conf_String gcav_FoundItem;

            if (0 <= index && index < this.items.Count)
            {
                gcav_FoundItem = this.items[index];
            }
            else
            {
                gcav_FoundItem = null;

                if (isRequired)
                {
                    // エラーとして扱います。
                    goto gt_Error_BadIndex;
                }
            }

            goto gt_EndMethod;
        //
        //
        #region 異常系
        //────────────────────────────────────────
        gt_Error_BadIndex:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー097！!", log_Method);

                StringBuilder sb = new StringBuilder();
                sb.Append("指定されたノードは存在しませんでした。");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append("リストのインデックス=[");
                sb.Append(index);
                sb.Append("]");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);

                // ヒント

                r.Message = sb.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        #endregion
        //
        //
    gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return gcav_FoundItem;
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private Conf_String owner;

        public Conf_String Owner
        {
            get
            {
                return owner;
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// ＜f-●●＞要素のリスト。
        /// </summary>
        private List<Conf_String> items;

        public void ForEach(DELEGATE_Conf_Nodes dlgt1)
        {
            bool isBreak = false;
            foreach (Conf_String cElm in this.items)
            {
                dlgt1(cElm, ref isBreak);

                if (isBreak)
                {
                    break;
                }
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// 個数。
        /// </summary>
        public int Count
        {
            get
            {
                return items.Count;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
