using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{

    /// <summary>
    /// 
    /// 旧名：Expression_Node_StringImpl
    /// </summary>
    public class Expr_StringImpl : Expr_String
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="parent_Expr"></param>
        /// <param name="cur_Conf">生成時に指定できないものもある。</param>
        public Expr_StringImpl(Expr_String parent_Expr, Conf_String cur_Conf)
        {
            this.parent_Expression = parent_Expr;
            this.conf_ = cur_Conf;

            enumHitcount = EnumHitcount.Unconstraint;
            this.eChildNodes = new Expr_StringListImpl(this);
            this.attributes = new ExprStringMapImpl(this.Conf);
        }

        /// <summary>
        /// コンストラクター。
        /// node_Ｃonfigurationtree を後で InitializeBeforeuse を使って指定する必要がある。
        /// </summary>
        /// <param name="eParent"></param>
        public Expr_StringImpl(Expr_String eParent)
            : this(eParent, null)
        {
        }

        //────────────────────────────────────────

        /// <summary>
        /// コンストラクターで指定していれば、必要ない。
        /// </summary>
        /// <param name="cur_Cf"></param>
        public void InitializeBeforeuse(Conf_String cur_Cf)
        {
            this.conf_ = cur_Cf;
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


            // ノード名
            s.AppendI(0,"L01(文):"+"「E■[");
            s.Append(this.Conf.Name);
            s.Append("]　");

            // クラス名
            s.Append("[");
            s.Append(this.GetType().Name);
            s.Append("]クラス　");

            s.Append("子値＝[");
            s.Append(this.Lv4Execute_OnImplement(EnumHitcount.Unconstraint, log_Reports_ForSnapshot));
            s.Append("]");

            s.Append("」");
            s.Newline();

            // 属性リスト
            this.Attributes.ToText_Snapshot(s);

            // 子リスト
            if (this.ChildNodes.Count < 1)
            {
                s.AppendI(0, "子無し");
                s.Newline();
            }
            else
            {
                s.AppendI(0, "┌────────┐子数＝[");
                s.Append(this.ChildNodes.Count);
                s.Append("]");
                s.Newline();
                this.ChildNodes.ForEach(delegate(Expr_String eChild, ref bool bRemove, ref bool bBreak)
                {
                    eChild.ToText_Snapshot(s);
                });
                s.AppendI(0, "└────────┘");
                s.Newline();
            }

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
        /// E_Elm属性。
        /// </summary>
        /// <param name="out_E_Result">検索結果。</param>
        /// <param name="sName"></param>
        /// <param name="bRequired"></param>
        /// <param name="hits"></param>
        /// <param name="log_Reports"></param>
        /// <returns>検索結果が1件以上あれば真。</returns>
        public bool TrySelectAttribute_ExpressionFilepath(
            out Expr_Filepath out_eResult,
            string sName,
            EnumHitcount hits,
            Log_Reports log_Reports
            )
        {
            return this.Attributes.TrySelect_ExpressionFilepath(
                out out_eResult, sName, hits, log_Reports);
        }

        /// <summary>
        /// E_Elm属性。
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
            return this.Attributes.TrySelect(
                out out_eResult, sName, hits, log_Reports);
        }

        public bool TrySelectAttribute(
            out string out_result,
            string sName,
            EnumHitcount hits,
            Log_Reports log_Reports
            )
        {

            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "TrySelectAttribute", log_Reports);

            //
            //

            bool bResult = this.Attributes.TrySelect2(out out_result, sName, hits, log_Reports);

            goto gt_EndMethod;
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return bResult;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 文字列を、子要素として追加。
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="s_ParentNode"></param>
        /// <param name="log_Reports"></param>
        public void AppendTextNode(
            string contents,
            Conf_String parent_Conf,
            Log_Reports log_Reports
            )
        {
            Expr_LeafStringImpl eChild = new Expr_LeafStringImpl(null, parent_Conf);
            eChild.SetString(contents, log_Reports);

            this.ChildNodes.Add(eChild, log_Reports);
        }

        //────────────────────────────────────────

        public virtual string Lv5_Implement(
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Lv5_Implement", log_Reports);

            //
            //

            StringBuilder sb_Result = new StringBuilder();

            List<Expr_String> eChildList = this.ChildNodes.SelectList(EnumHitcount.Unconstraint, log_Reports);

            switch (this.enumHitcount)
            {
                case EnumHitcount.First_Exist:
                    {
                        //
                        // 最初の１件のみ。存在しない場合エラー。
                        //
                        if (0 < eChildList.Count)
                        {
                            Expr_String eChild = eChildList[0];
                            string s = eChild.Lv4Execute_OnImplement(this.enumHitcount, log_Reports);

                            sb_Result.Append(s);
                        }
                        else
                        {
                            //
                            // エラー
                            goto gt_Error_NotFoundOne;
                        }
                    }
                    break;

                case EnumHitcount.First_Exist_Or_Zero:
                    {
                        //
                        // 最初の１件のみ。存在しない場合、空文字列。
                        //
                        if (0 < eChildList.Count)
                        {
                            Expr_String eChild = eChildList[0];
                            string s = eChild.Lv4Execute_OnImplement(this.enumHitcount, log_Reports);

                            sb_Result.Append(s);
                        }
                        else
                        {
                            //
                            // 存在しないので、空文字列。
                            //

                            // そのままスルー。
                        }
                    }
                    break;

                case EnumHitcount.Unconstraint:
                    {
                        //
                        // 制限なし
                        //

                        foreach (Expr_String eChild in eChildList)
                        {
                            string s = eChild.Lv4Execute_OnImplement(this.enumHitcount, log_Reports);

                            sb_Result.Append(s);
                        }

                    }
                    break;

                default:
                    {
                        //
                        // エラー
                        goto gt_Error_UndefinedEnum;
                    }
            }

            goto gt_EndMethod;
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_NotFoundOne:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー111！", log_Method);

                StringBuilder sb = new StringBuilder();
                sb.Append("必ず、最初の１件を取得する指定でしたが、１件も存在しませんでした。");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);

                // ヒント

                r.Message = sb_Result.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_UndefinedEnum:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー113！", log_Method);

                StringBuilder sb = new StringBuilder();
                sb.Append("this.requestItems.VolumeConstraintEnum=[");
                sb.Append(this.enumHitcount.ToString());
                sb.Append("]には、プログラム側でまだ未対応です。");

                // ヒント

                r.Message = sb_Result.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return sb_Result.ToString();
        }

        //────────────────────────────────────────

        /// <summary>
        /// 内容を文字列型で返します。
        /// </summary>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public virtual string Lv4Execute_OnImplement(
            EnumHitcount request,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Lv4Execute_OnImplement①", log_Reports);
            //
            //

            string result = this.Lv5_Implement(log_Reports);

            //
            //
            //
            //

            // もとに戻す
            this.enumHitcount = EnumHitcount.Unconstraint;

            goto gt_EndMethod;
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
        /// このデータは、ファイルパス型だ、と想定して、ファイルパスを取得します。
        /// </summary>
        /// <returns></returns>
        public static Expr_Filepath Execute4_OnExpressionString_AsFilepath_Impl(
            Expr_String eCaller,
            EnumHitcount request,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Syntax.Name_Library, eCaller, "Execute4_OnExpressionString_AsFilepath_Impl", log_Reports);
            //
            //
            //
            //

            Expr_Filepath reulst_eRel;

            //
            // ファイルパス。
            string sFpath = eCaller.Lv5_Implement(log_Reports);
            {
                reulst_eRel = Expr_FilepathImpl.Init2(
                    "",
                    sFpath,
                    "ファイルパス出典未指定L01_1",
                    eCaller.Conf,
                    log_Reports
                    );

                if (!log_Reports.Successful)
                {
                    // 既エラー。
                    reulst_eRel = null;
                    goto gt_EndMethod;
                }
            }

            goto gt_EndMethod;
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return reulst_eRel;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 親E_Stringを遡って検索。一致するものがなければヌル。
        /// </summary>
        /// <param name="nodeName">ノード名</param>
        /// <returns></returns>
        public Expr_String GetParentExpressionOrNull(string nodeName)
        {
            return Expr_StringImpl.GetParentEOrNull_(this, nodeName);
        }

        //────────────────────────────────────────

        /// <summary>
        /// 親E_Stringを遡って検索。一致するものがなければヌル。
        /// </summary>
        /// <param name="nodeName">ノード名</param>
        /// <returns></returns>
        public static Expr_String GetParentEOrNull_(Expr_String eMe, string nodeName)
        {
            Expr_String result;


            if (eMe.Parent == null)
            {
                result = null;
            }
            else if (eMe.Parent.Conf.Name == nodeName)
            {
                result = eMe.Parent;
            }
            else
            {
                result = eMe.Parent.GetParentExpressionOrNull(nodeName);
            }

            return result;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 例えば　"data"　と指定すれば、
        /// 直下の子要素の中で　＜ｄａｔａ　＞ といったノード名を持つものはヒットする。
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="sExpectedValue"></param>
        /// <param name="request"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public List<Expr_String> SelectDirectchildByNodename(
            string sExpectedNodeName, bool bRemove, EnumHitcount request, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Divide3Blocks",log_Reports);

            List<Expr_String> result = new List<Expr_String>();

            this.ChildNodes.ForEach(delegate(Expr_String eChild, ref bool bRemove2, ref bool bBreak2)
            {
                if (log_Reports.Successful)
                {
                    if (eChild.Conf.Name == sExpectedNodeName)
                    {
                        result.Add(eChild);

                        if (bRemove)
                        {
                            // 削除要求1があるとき、削除要求2を出します。
                            bRemove2 = true;
                        }


                        if (EnumHitcount.First_Exist == request ||
                            EnumHitcount.First_Exist_Or_Zero == request)
                        {
                            // 最初の１件で削除は終了。複数件ヒットするかどうかは判定しない。
                            bBreak2 = true;
                        }
                    }
                }
            });

            if (EnumHitcount.One == request)
            {
                // 必ず１件だけヒットする想定。

                if (result.Count != 1)
                {
                    goto gt_errorNotOne;
                }
            }
            else if (EnumHitcount.First_Exist == request)
            {
                // 必ずヒットする。複数件あれば、最初の１件だけ取得。

                if (0 == result.Count)
                {
                    goto gt_errorNoHit;
                }
                else if (1 < result.Count)
                {
                    result.RemoveRange(1, result.Count - 1);
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
        gt_errorNoHit:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー102！", log_Method);

                StringBuilder sb = new StringBuilder();
                sb.Append("必ず、１件以上ヒットする指定でしたが、[");
                sb.Append(result.Count);
                sb.Append("]件ヒットしました。");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);

                // ヒント

                r.Message = sb.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //
        //
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

        /// <summary>
        /// 例えば　"data"　と指定すれば、
        /// 直下の子要素の中で　＜ｄａｔａ　＞ といったノード名を持つものはヒットする。
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="sExpectedValue"></param>
        /// <param name="request"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public static List<Expr_String> SelectItemByNodeName(
            List<Expr_String> eItems, string sName_ExpectedNode, bool bRemove, EnumHitcount request, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, "Expression_Node_StringImpl", "SelectItemByNodeName",log_Reports);

            List<Expr_String> result = new List<Expr_String>();


            for (int nI = 0; nI < eItems.Count; nI++)
            {
                Expr_String eItem = eItems[nI];

                if (log_Reports.Successful)
                {
                    if (eItem.Conf.Name == sName_ExpectedNode)
                    {
                        result.Add(eItem);

                        if (bRemove)
                        {
                            // 削除を要求します。
                            eItems.RemoveAt(nI);
                            nI--;
                        }


                        if (EnumHitcount.First_Exist == request ||
                            EnumHitcount.First_Exist_Or_Zero == request)
                        {
                            // 最初の１件で終了。複数件ヒットするかどうかは判定しない。
                            break;
                        }
                    }
                }
            }


            if (EnumHitcount.One == request)
            {
                // 必ず１件だけヒットする想定。

                if (result.Count != 1)
                {
                    goto gt_errorNotOne;
                }
            }
            else if (EnumHitcount.First_Exist == request)
            {
                // 必ずヒットする。複数件あれば、最初の１件だけ取得。

                if (0 == result.Count)
                {
                    goto gt_errorNoHit;
                }
                else if (1 < result.Count)
                {
                    result.RemoveRange(1, result.Count - 1);
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
        gt_errorNoHit:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー102！", log_Method);

                StringBuilder sb = new StringBuilder();
                sb.Append("必ず、１件以上ヒットする指定でしたが、[");
                sb.Append(result.Count);
                sb.Append("]件ヒットしました。");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);

                // ヒント

                r.Message = sb.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //
        //
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
        /// </summary>
        /// <param name="request"></param>
        public void SetEnumHitcount(
            EnumHitcount request
            )
        {
            enumHitcount = request;
        }

        //────────────────────────────────────────

        private Expr_StringList eChildNodes;

        /// <summary>
        /// 子＜●●＞要素リスト。
        /// </summary>
        public Expr_StringList ChildNodes
        {
            get
            {
                return eChildNodes;
            }
        }

        //────────────────────────────────────────

        private ExprStringMap attributes;

        /// <summary>
        /// 属性="" マップ。
        /// </summary>
        public ExprStringMap Attributes
        {
            get
            {
                return attributes;
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
        #endregion



    }
}
