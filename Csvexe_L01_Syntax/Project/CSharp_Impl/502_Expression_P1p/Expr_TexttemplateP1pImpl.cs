using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Xenon.Syntax
{

    /// <summary>
    /// </summary>
    public class Expr_TexttemplateP1pImpl : Expr_String
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public Expr_TexttemplateP1pImpl(Expr_String eParent, Conf_String cElm)
        {
            this.parent_Expression = eParent;
            this.conf = cElm;

            this.requestItems = EnumHitcount.Unconstraint;

            this.dictionary_P1p = new Dictionary<int, string>();
            this.list_Expression_Child = new Expr_StringListImpl(this);//使いません。
            this.dictionary_Expression_Attribute = new ExprStringMapImpl(this.Conf);
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
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "ToText_Snapshot", log_Reports_ForSnapshot);

            log_Reports_ForSnapshot.BeginCreateReport(EnumReport.Dammy);
            s.Increment();

            s.Append("「E■[");
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
        /// 属性。
        /// </summary>
        /// <param name="out_E_Result">検索結果。</param>
        /// <param name="name"></param>
        /// <param name="bRequired"></param>
        /// <param name="hits"></param>
        /// <param name="log_Reports"></param>
        /// <returns>検索結果が1件以上あれば真。</returns>
        public bool TrySelectAttribute_ExpressionFilepath(
            out Expr_Filepath eResult_Out,
            string name,
            EnumHitcount hits,
            Log_Reports log_Reports
            )
        {
            // 使いません。
            eResult_Out = Expr_FilepathImpl.Init2(
                "",
                "",
                name,
                this.Conf,
                log_Reports
                );

            return false;
        }

        /// <summary>
        /// 属性。
        /// </summary>
        /// <param name="out_E_Result">検索結果。</param>
        /// <param name="name"></param>
        /// <param name="bRequired"></param>
        /// <param name="hits"></param>
        /// <param name="log_Reports"></param>
        /// <returns>検索結果が1件以上あれば真。</returns>
        public bool TrySelectAttribute(
            out Expr_String eResult_Out,
            string name,
            EnumHitcount hits,
            Log_Reports log_Reports
            )
        {
            // 使いません。
            eResult_Out = new Expr_StringImpl(this, this.Conf);
            return false;
        }

        public bool TrySelectAttribute(
            out string sResult_Out,
            string name,
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
        /// <param name="humaninput"></param>
        /// <param name="cParent"></param>
        /// <param name="log_Reports"></param>
        public void AppendTextNode(
            string humaninput,
            Conf_String cParent,
            Log_Reports log_Reports
            )
        {
            Expr_LeafStringImpl eAtom = new Expr_LeafStringImpl(null, cParent);
            eAtom.SetString(
                humaninput,
                log_Reports
                );

            this.ChildNodes.Add(
                eAtom,
                log_Reports
                );
        }

        //────────────────────────────────────────

        public virtual string Lv5_Implement(
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Execute5_Main", log_Reports);

            string result;
            Exception err_Excp;

            try
            {
                result = this.dictionary_P1p[this.numberP1p];
            }
            catch (KeyNotFoundException e)
            {
                // エラー
                err_Excp = e;
                goto gt_Error_KeyNotFound;
            }

            goto gt_EndMethod;
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_KeyNotFound:
            {
                result = "";
                if (log_Reports.CanCreateReport)
                {
                    Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                    r.SetTitle("▲エラー211！", log_Method);

                    Log_TextIndented t = new Log_TextIndentedImpl();
                    t.Append("テキスト_テンプレートの引数 p");
                    t.Append(this.numberP1p);
                    t.Append("p の取得に失敗しました。");
                    t.Newline();

                    // ヒント
                    t.Append(r.Message_SException(err_Excp));

                    r.Message = t.ToString();
                    log_Reports.EndCreateReport();
                }
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return result;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 内容を文字列型で返します。
        /// </summary>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public string Lv4Execute_OnImplement(
            EnumHitcount request,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Execute4_OnExpressionString", log_Reports);

            //
            //
            //
            //

            string result;

            // エラーが出ている時は飛ばしたいが、「エラー報告」として利用されることがある。

            //if (!log_Reports.Successful)
            //{
            //    //エラー
            //    sResult = "＜E_P1pImpl:エラー101＞";
            //    goto gt_EndMethod;
            //}

            result = this.Lv5_Implement(log_Reports);

            // もとに戻す
            this.requestItems = EnumHitcount.Unconstraint;

            goto gt_EndMethod;
        //
        //
        //
        //

        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return result;
        }

        //────────────────────────────────────────

        /// <summary>
        /// このデータは、ファイルパス型だ、と想定して、ファイルパスを取得します。
        /// </summary>
        /// <returns></returns>
        public Expr_Filepath Lv4Execute_OnImplement_AsFilepath(
            EnumHitcount request,
            Log_Reports log_Reports
            )
        {
            return Expr_StringImpl.Execute4_OnExpressionString_AsFilepath_Impl(this, request, log_Reports);
        }

        //────────────────────────────────────────

        /// <summary>
        /// 子要素を追加します。
        /// </summary>
        /// <param name="items"></param>
        /// <param name="request"></param>
        /// <param name="log_Reports"></param>
        public void AddChildElement(
            Expr_String eChild,
            Log_Reports log_Reports
            )
        {
            //
            // エラー。

            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "AddChildElement",log_Reports);

            //
            //
            //
            //

            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);

                r.SetTitle("▲エラー201！", log_Method);

                Log_TextIndented t = new Log_TextIndentedImpl();
                t.Append("このメソッド " + log_Method.Fullname + " は使わないでください。");

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

        public List<Expr_String> GetChildElements(
            EnumHitcount request,
            Log_Reports log_Reports
            )
        {
            //
            // エラー。

            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "GetChildElements",log_Reports);

            //
            //
            //
            //

            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);

                r.SetTitle("▲エラー101！", log_Method);

                Log_TextIndented t = new Log_TextIndentedImpl();
                t.Append("このメソッド " + log_Method.Fullname + " は使わないでください。");

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
        /// 親E_Stringを遡って検索。一致するものがなければヌル。
        /// </summary>
        /// <param name="name">ノード名</param>
        /// <returns></returns>
        public Expr_String GetParentExpressionOrNull(string name)
        {
            return Expr_StringImpl.GetParentEOrNull_(this, name);
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
            string expectedNodeName, bool isRemove, EnumHitcount request, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "SelectDirectchildByNodename", log_Reports);

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
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return result;
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private Conf_String conf;

        /// <summary>
        /// 設定場所のヒント。
        /// </summary>
        public Conf_String Conf
        {
            get
            {
                return this.conf;
            }
            set
            {
                this.conf = value;
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

        private int numberP1p;

        /// <summary>
        /// 「p1p」、「p2p」といった引数名の中の数字。
        /// 
        /// 「p1p」は、「%1%」を名前にしたもの。
        /// </summary>
        public int NumberP1p
        {
            get
            {
                return numberP1p;
            }
            set
            {
                this.numberP1p = value;
            }
        }

        //────────────────────────────────────────

        private Dictionary<int, string> dictionary_P1p;

        /// <summary>
        /// [1]=101
        /// [2]=赤
        /// といったディクショナリー。
        /// 
        /// 数字は %1%や、p1pの名前の中の数字。[1]から始める。
        /// </summary>
        public Dictionary<int, string> Dictionary_P1p
        {
            get
            {
                return dictionary_P1p;
            }
            set
            {
                dictionary_P1p = value;
            }
        }

        //────────────────────────────────────────

        private Expr_StringList list_Expression_Child;

        /// <summary>
        /// 子＜●●＞リスト。
        /// 使いません。
        /// </summary>
        public Expr_StringList ChildNodes
        {
            get
            {
                return list_Expression_Child;
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
        /// どういう結果が欲しいかの指定。
        /// </summary>
        private EnumHitcount requestItems;

        //────────────────────────────────────────

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
            requestItems = request;
        }

        //────────────────────────────────────────
        #endregion




    }
}
