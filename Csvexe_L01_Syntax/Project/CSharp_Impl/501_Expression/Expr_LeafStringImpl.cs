using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Xenon.Syntax
{

    /// <summary>
    /// 旧名：Expression_Leaf_StringImpl
    /// </summary>
    public class Expr_LeafStringImpl : Expr_LeafString
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public Expr_LeafStringImpl(Expr_String parent_Expr, Conf_String cur_Conf)
            : this("", parent_Expr, cur_Conf)
        {
        }

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public Expr_LeafStringImpl(string sHumanInput, Expr_String parent_Expr, Conf_String cur_Conf)
        {
            this.sHumanInput = sHumanInput;
            this.parent_Expression = parent_Expr;
            this.conf_ = cur_Conf;

            this.enumHitcount = EnumHitcount.Unconstraint;
            this.dictionary_Expression_Attribute = new ExprStringMapImpl(this.Conf);
        }

        //────────────────────────────────────────

        /// <summary>
        /// 新しいインスタンスを作ります。
        /// </summary>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public Expr_LeafString NewInstance(
            Conf_String parent_Expression,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "NewInstance",log_Reports);

            //
            //
            //
            //

            Expr_LeafStringImpl result = new Expr_LeafStringImpl(null, parent_Expression);

            result.SetString(
                this.sHumanInput,
                log_Reports
                );

            //
            //
            log_Method.EndMethod(log_Reports);
            return result;
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 
        /// </summary>
        public void ToText_Snapshot(Log_TextIndented s)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            Log_Reports log_Reports_ForSnapshot = new Log_ReportsImpl(log_Method);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "ToText_Snapshot",log_Reports_ForSnapshot);

            log_Reports_ForSnapshot.BeginCreateReport(EnumReport.Dammy);
            s.Increment();

            s.AppendI(0,"L01(葉):"+"「E■[");
            s.Append(this.Conf.Name);
            s.Append("]　");
            s.Append(this.Lv4Execute_OnImplement(EnumHitcount.Unconstraint, log_Reports_ForSnapshot));
            s.Append("」");
            s.Newline();


            goto gt_EndMethod;
        //
        //
        gt_EndMethod:
            s.Decrement();
            log_Reports_ForSnapshot.EndCreateReport();
            log_Method.EndMethod(log_Reports_ForSnapshot);
        }

        //────────────────────────────────────────

        /// <summary>
        /// 親E_Stringを遡って検索。一致するものがなければヌル。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Expr_String GetParentExpressionOrNull(string name)
        {
            return Expr_StringImpl.GetParentEOrNull_(this, name);
        }

        //────────────────────────────────────────

        /// <summary>
        /// 子要素を追加します。
        /// </summary>
        /// <param name="nItems"></param>
        /// <param name="request"></param>
        /// <param name="log_Reports"></param>
        public void Expression_AddChild(
            Expr_String eChild,
            Log_Reports log_Reports
            )
        {
            //
            // エラー。

            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Expression_AddChild",log_Reports);

            //
            //
            //
            //

            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー101！", log_Method);

                Log_TextIndented t = new Log_TextIndentedImpl();
                t.Append("このメソッド " + this.GetType().Name + "#AddChildN は使わないでください。");

                // ヒント
                t.Append(r.Message_Conf(this.Conf));

                r.Message = t.ToString();
                log_Reports.EndCreateReport();
            }

            //
            //
            log_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────

        /// <summary>
        /// @Deprecated
        /// </summary>
        /// <param name="request"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public List<Expr_String> Expression_GetChildList(
            EnumHitcount request,
            Log_Reports log_Reports
            )
        {
            //
            // エラー。

            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Expression_GetChildList",log_Reports);

            //
            //

            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー101！", log_Method);

                Log_TextIndented t = new Log_TextIndentedImpl();
                t.Append("このメソッド " + this.GetType().Name + "#GetChildNList は使わないんでください。");

                // ヒント
                t.Append(r.Message_Conf(this.Conf));

                r.Message = t.ToString();

                log_Reports.EndCreateReport();
            }

            //
            //
            log_Method.EndMethod(log_Reports);

            return null;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 属性。
        /// </summary>
        /// <param name="out_E_Result">検索結果。</param>
        /// <param name="sName"></param>
        /// <param name="bRequired"></param>
        /// <param name="hits"></param>
        /// <param name="log_Reports"></param>
        /// <returns>検索結果が1件以上あれば真。</returns>
        public bool TrySelectAttribute_ExpressionFilepath(
            out Expr_Filepath ec_Result_Out,
            string sName,
            EnumHitcount hits,
            Log_Reports log_Reports
            )
        {
            // 使いません。

            ec_Result_Out = Expr_FilepathImpl.Init2(
                "",
                "",
                sName,
                this.Conf,
                log_Reports
                );

            return false;
        }

        /// <summary>
        /// 属性。
        /// </summary>
        /// <param name="out_E_Result">検索結果。</param>
        /// <param name="sName"></param>
        /// <param name="bRequired"></param>
        /// <param name="hits"></param>
        /// <param name="log_Reports"></param>
        /// <returns>検索結果が1件以上あれば真。</returns>
        public bool TrySelectAttribute(
            out Expr_String out_eResult,
            string sName,
            EnumHitcount hits,
            Log_Reports log_Reports
            )
        {
            // 使いません。
            out_eResult = new Expr_StringImpl(this, null);
            return false;
        }

        public bool TrySelectAttribute(
            out string sResult_Out,
            string sName,
            EnumHitcount hits,
            Log_Reports log_Reports
            )
        {
            // 使いません。
            sResult_Out = "";
            return false;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 文字列を、子要素として追加。
        /// </summary>
        /// <param name="sHumaninput"></param>
        /// <param name="parent_Conf"></param>
        /// <param name="log_Reports"></param>
        public void AppendTextNode(
            string sHumaninput,
            Conf_String parent_Conf,
            Log_Reports log_Reports
            )
        {
            throw new Exception(Info_Syntax.Name_Library + ":" + this.GetType().Name + "#AppendTextElement:このクラスでは、このメソッドを使わないでください。");
        }

        //────────────────────────────────────────

        public virtual string Lv5_Implement(
            Log_Reports log_Reports
            )
        {
            return this.sHumanInput;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 内容を文字列型で返します。
        /// </summary>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public virtual string Lv4Execute_OnImplement(
            EnumHitcount req_Items,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Execute4_OnExpressionString", log_Reports);
            //
            //

            // もとに戻す
            this.enumHitcount = EnumHitcount.Unconstraint;

            string sResult = this.Lv5_Implement(log_Reports);

            //
            //
            log_Method.EndMethod(log_Reports);
            return sResult;
        }

        //────────────────────────────────────────

        /// <summary>
        /// このデータは、ファイルパス型だ、と想定して、ファイルパスを取得します。
        /// </summary>
        /// <returns></returns>
        public virtual Expr_Filepath Lv4Execute_OnImplement_AsFilepath(
            EnumHitcount request,
            Log_Reports log_Reports
            )
        {
            return Expr_StringImpl.Execute4_OnExpressionString_AsFilepath_Impl(this, request, log_Reports);
        }

        //────────────────────────────────────────

        /// <summary>
        /// 使えません。
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="sExpectedValue"></param>
        /// <param name="request"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public List<Expr_String> SelectDirectchildByNodename(
            string sName_ExpectedNode,
            bool bRemove,
            EnumHitcount request,
            Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "GetDirectChildByNodeName",log_Reports);

            List<Expr_String> result = new List<Expr_String>();

            if (EnumHitcount.One == request)
            {
                // 必ず１件だけヒットする想定。

                if (result.Count != 1)
                {
                    goto gt_errorNotOne;
                }
            }
            else if (EnumHitcount.First_Exist_Or_Zero == request)
            {
                // ヒットすれば最初の１件だけ、ヒットしなければ０件の想定。

                if (1 < result.Count)
                {
                    result.RemoveRange(1, result.Count - 1);
                }
            }
            else
            {
            }

            goto gt_EndMethod;
            //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_errorNotOne:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー101！", log_Method);

                StringBuilder sb = new StringBuilder();
                sb.Append("必ず、１件のみ取得する指定でしたが、[");
                sb.Append(result.Count);
                sb.Append("]件取得しました。");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);

                // ヒント

                r.Message = sb.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return result;
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private Conf_String conf_;

        /// <summary>
        /// 設定場所のヒント。
        /// </summary>
        public Conf_String Conf
        {
            get
            {
                return this.conf_;
            }
            set
            {
                this.conf_ = value;
            }
        }

        //────────────────────────────────────────

        private Expr_String parent_Expression;

        /// <summary>
        /// 設定場所のヒント。
        /// </summary>
        public Expr_String Parent
        {
            get
            {
                return this.parent_Expression;
            }
            set
            {
                this.parent_Expression = value;
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// 子＜●●＞要素リスト。
        /// 使わない。
        /// </summary>
        public Expr_StringList ChildNodes
        {
            get
            {
                return null;
            }
        }

        //────────────────────────────────────────

        private ExprStringMap dictionary_Expression_Attribute;

        /// <summary>
        /// 属性="" マップ。
        /// </summary>
        public ExprStringMap Attributes
        {
            get
            {
                return dictionary_Expression_Attribute;
            }
        }

        /// <summary>
        /// 属性を上書きします。
        /// </summary>
        /// <param name="name_Attribute"></param>
        /// <param name="expr_Attribute"></param>
        /// <param name="log_Reports"></param>
        public void SetAttribute(
            string name_Attribute,
            Expr_String expr_Attribute,
            Log_Reports log_Reports
            )
        {
            this.Attributes.Set(name_Attribute, expr_Attribute, log_Reports);
        }

        //────────────────────────────────────────

        /// <summary>
        /// 値。
        /// </summary>
        private string sHumanInput;

        /// <summary>
        /// 内容を文字列型でセットします。
        /// </summary>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public void SetString(
            string sHumanInput,
            Log_Reports log_Reports
            )
        {
            this.sHumanInput = sHumanInput;
        }

        //────────────────────────────────────────

        /// <summary>
        /// どういう結果が欲しいかの指定。
        /// </summary>
        private EnumHitcount enumHitcount;

        protected EnumHitcount EnumHitcount
        {
            get
            {
                return this.enumHitcount;
            }
        }

        /// <summary>
        /// どういう結果が欲しいかの指定。
        /// 
        /// 旧名：SetValidation
        /// </summary>
        /// <param name="request"></param>
        public void SetEnumHitcount(
            EnumHitcount request
            )
        {
            enumHitcount = request;
        }

        //────────────────────────────────────────
        #endregion



    }
}
