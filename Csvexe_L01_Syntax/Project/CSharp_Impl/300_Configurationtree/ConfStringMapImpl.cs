using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{
    public delegate void DELEGATE_StringAttributes(string key, string value, ref bool isBreak);


    public class ConfStringMapImpl : ConfStringMap
    {



        #region 生成と破棄
        //────────────────────────────────────────

        public ConfStringMapImpl(Conf_String cOwner)
        {
            this.owner = cOwner;
            this.map = new Dictionary<string, string>();
        }

        //────────────────────────────────────────

        /// <summary>
        /// new された直後の内容に戻します。
        /// </summary>
        public void Clear(Conf_String cOwner, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Clear", log_Reports);

            //
            //

            this.owner = null;
            this.map.Clear();

            //
            //
            log_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// attr系要素の追加。
        /// 
        /// 既に追加されている要素は、追加できない。
        /// </summary>
        public void Add(
            string key,
            string value,
            Conf_String cValue,
            bool isRequired,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Add",log_Reports);

            //
            //

            if (!this.map.ContainsKey(key))
            {
                this.map.Add(key, value);
            }
            else
            {
                if (isRequired)
                {
                    // エラー
                    goto gt_Error_Duplicate;
                }
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_Duplicate:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー345！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();

                s.Append("要素<");
                s.Append(this.owner.Name);
                s.Append(">に、同じ名前の属性が重複していました。");
                s.Newline();

                s.Append("入れようとした要素の名前=[");
                s.Append(key);
                s.Append("]");
                s.Newline();

                // ヒント
                s.Append(r.Message_Conf(cValue));

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────

        /// <summary>
        /// attr系要素の追加。
        /// </summary>
        public void Set(
            string key,
            string value,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Set",log_Reports);

            //
            //
            //
            //

            this.map[key] = value;

            //
            //
            //
            //
            log_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">引数名</param>
        /// <param name="value">その値</param>
        /// <param name="cElm">引数のコンフィグ</param>
        /// <param name="eParent">親Expr</param>
        /// <param name="isRequired"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public bool TryGetValue2(
            PmName name,
            out Expr_String value,
            Conf_String cElm,
            Expr_String eParent,
            bool isRequired,
            Log_Reports log_Reports
            )
        {
            bool isHit = false;

            string s;//コントロール名
            this.TryGetValue(name, out s, isRequired, log_Reports);

            if (log_Reports.Successful)
            {
                value = new Expr_LeafStringImpl(s, eParent, cElm);
                isHit = true;
            }
            else
            {
                value = null;
            }

            return isHit;
        }

        /// <summary>
        /// 空白は、無いのと同じに扱う。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="result"></param>
        /// <param name="isRequired"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public bool TryGetValue(
            PmName name,//Pmオブジェクトにしたい。
            out string result,
            bool isRequired,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "TryGetValue",log_Reports);
            //


            bool bHit = this.map.TryGetValue(name.Pm, out result);
            if (!bHit || "" == result)
            {
                if (isRequired)
                {
                    goto gt_Error_NoHit;
                }
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_NoHit:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("Er:004;", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();


                s.Append("name=\"");
                s.Append(name.Plain);
                s.Append("\" 属性か、または <arg name=\"");
                s.Append(name.Pm);
                s.Append("\" ～> 要素のどちらかが必要でしたが、違う方を書いたか、記述されていないか、空文字列でした。");
                s.Newline();
                s.Newline();

                if (null != this.owner)
                {
                    //ヒント
                    s.Append(r.Message_Conf(this.owner));
                }
                else
                {
                    s.Append("どの要素かは不明。");
                    s.Newline();
                }

                // ヒント

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return bHit;
        }

        //────────────────────────────────────────
        #endregion



        #region 判定
        //────────────────────────────────────────

        public bool ContainsKey(string key)
        {
            return this.map.ContainsKey(key);
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// このオブジェクトを所有しているオブジェクト。
        /// </summary>
        private Conf_String owner;

        //────────────────────────────────────────

        private Dictionary<string, string> map;

        public void ForEach(DELEGATE_StringAttributes dlgt1)
        {
            bool isBreak = false;
            foreach (KeyValuePair<string, string> kvp in this.map)
            {
                dlgt1(kvp.Key, kvp.Value, ref isBreak);

                if (isBreak)
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
                return this.map.Count;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
