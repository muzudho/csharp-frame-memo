using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{

    /// <summary>
    /// 設定ファイル等から読み込んだデータを保持するオブジェクトは、これを継承することになる。
    /// 
    /// 旧名：Ｃonfiguration_NodeImpl
    /// 旧名：Ｃonfigurationtree_NodeImpl
    /// </summary>
    public class Conf_StringImpl : Conf_String
    {


        #region 生成と破棄
        //────────────────────────────────────────

        public Conf_StringImpl(string name, Conf_String cParent_OrNull)
        {
            this.childNodes = new ConfStringListImpl(this);
            this.attributes = new ConfStringMapImpl(this);

            this.name = name;
            this.parent = cParent_OrNull;
        }

        //────────────────────────────────────────

        /// <summary>
        /// new された直後の内容に戻します。
        /// </summary>
        public void Clear(string name, Conf_String cParent_OrNull, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "Clear", log_Reports);

            //
            //
            //
            // 親
            //
            //
            //
            this.Parent = cParent_OrNull;


            //
            //
            //
            // 自
            //
            //
            //
            this.Name = name;


            //
            //
            //
            // 属性
            //
            //
            //
            this.Attributes.Clear(this, log_Reports);


            //
            //
            //
            // 子
            //
            //
            //
            this.childNodes.Clear(log_Reports);


            //
            //
            //
            // 親への連結は維持。
            //
            //
            //

            //
            //
            log_Method.EndMethod(log_Reports);
        }
        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        public virtual void ToText_Content(Log_TextIndented s)
        {
            s.Increment();

            // ノード名
            s.AppendI(0, "<");
            s.Append(this.Name);
            s.Append(" ");

            //
            // 属性
            //
            this.Attributes.ForEach(delegate(string sKey, string sValue, ref bool bBreak)
            {
                s.Append(sKey);
                s.Append("=[");
                s.Append(sValue);
                s.Append("] ");
            });


            if (0 < this.childNodes.Count)
            {
                s.Append(">");
                s.Newline();

                // 子要素
                this.childNodes.ToText_Content(s);

                s.AppendI(0, "</");
                s.Append(this.Name);
                s.Append(">");
                s.Newline();
            }
            else
            {
                s.Append("/>");
                s.Newline();
            }


            s.Decrement();
        }

        //────────────────────────────────────────

        /// <summary>
        /// 問題箇所ヒント。
        /// 
        /// 問題が起こったときに、設定ファイル等の修正箇所を示す説明などに利用。
        /// </summary>
        public virtual void ToText_Locationbreadcrumbs(Log_TextIndented s)
        {
            s.Increment();

            // 親のノード名を追加。
            if (null != this.Parent)
            {
                this.Parent.ToText_Locationbreadcrumbs(s);
                s.Append("/");
            }
            else
            {
                // このクラスがトップ・ノードだった場合。
                s.Append("問題個所ヒント（トップノード）：");
            }

            // 自分のノード名を追加。
            s.Append(this.Name);

            s.Decrement();
        }

        //────────────────────────────────────────

        /// <summary>
        /// ノード名を指定して、直近の親ノードを取得したい。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isRequired">偽を指定した時は、不一致の時ヌルを返す。</param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public virtual Conf_String GetParentByName(
            string name, bool isRequired, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0, Log_ReportsImpl.BDebugmode_Static);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "GetParentByNodename", log_Reports);
            //
            //
            Conf_String result;

            Conf_String err_cParent;
            if (log_Reports.Successful)
            {
                if (null != this.Parent)
                {
                    // 親要素があるとき

                    if (name == this.Parent.Name)
                    {
                        // ノード名が一致
                        result = this.Parent;
                    }
                    else
                    {
                        // ノード名が一致しないとき
                        result = this.Parent.GetParentByName(name, isRequired, log_Reports);
                    }
                }
                else
                {
                    // 親要素がないとき

                    result = null;
                    err_cParent = null;
                    goto gt_Error_NotFoundParent;
                }
            }
            else
            {
                // 既にエラーが出ているとき

                result = null;
            }

            if (!(result is Conf_String))
            {
                //エラー
                goto gt_Error_AnotherClass;
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_NotFoundParent:
            if (log_Reports.CanCreateReport)
            {
                if (isRequired)
                {
                    Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                    r.SetTitle("▲エラー501！", log_Method);

                    Log_TextIndented s = new Log_TextIndentedImpl();
                    s.Append("親要素の取得に失敗しました。");
                    s.Newline();


                    s.Append("指定ノード名[");
                    s.Append(name);
                    s.Append("]");
                    s.Newline();

                    s.Append("親要素はヌルです。");
                    s.Newline();

                    if (null != err_cParent)
                    {
                        s.Append("親要素ノード名[");
                        s.Append(err_cParent.Name);
                        s.Append("]");
                        s.Newline();
                    }

                    // ヒント
                    s.Append(r.Message_Conf(this));

                    r.Message = s.ToString();
                    log_Reports.EndCreateReport();
                }
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_AnotherClass:
            if (log_Reports.CanCreateReport)
            {
                if (isRequired)
                {
                    Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                    r.SetTitle("▲エラー502！", log_Method);

                    Log_TextIndented s = new Log_TextIndentedImpl();
                    s.Append("（内部プログラム・エラー）取得した親要素は、指定のクラスとは異なりました。");
                    s.Newline();

                    s.Append("取得した親要素のクラス名[");
                    s.Append(result.GetType().Name);
                    s.Append("]");
                    s.Newline();

                    // ヒント
                    s.Append(r.Message_Conf(this));

                    r.Message = s.ToString();
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
        /// 直近の１件の子要素を返します。
        /// 該当がなければヌルを返します。
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="isRequired">該当がない場合にエラー扱いにするなら真</param>
        /// <returns></returns>
        public Conf_String GetFirstChildByName(
            string sExpected,
            bool isRequired,
            Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0, Log_ReportsImpl.BDebugmode_Static);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "GetFirstChildByName", log_Reports);
            //
            //

            Conf_String cResult = null;

            if (log_Reports.Successful)
            {
                bool isHit = false;

                this.ChildNodes.ForEach(delegate(Conf_String item2, ref bool isBreak2)
                {
                    if (item2.Name == sExpected)
                    {
                        isHit = true;
                        cResult = item2;
                        isBreak2 = true;
                    }
                });

                if (!isHit)
                {
                    cResult = null;

                    if (isRequired)
                    {
                        // エラーとして扱います。
                        goto gt_Error_NotFound;
                    }

                }
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_NotFound:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー002！", log_Method);

                StringBuilder s = new StringBuilder();
                s.Append("指定された要素は存在しませんでした。");
                s.Append(Environment.NewLine);
                s.Append(Environment.NewLine);

                s.Append("要素名=[");
                s.Append(sExpected);
                s.Append("]");
                s.Append(Environment.NewLine);

                s.Append(r.Message_Conf(this));

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
            return cResult;
        }

        /// <summary>
        /// 直近の１件の子要素を返します。
        /// 該当がなければヌルを返します。
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="isRequired">該当がない場合にエラー扱いにするなら真</param>
        /// <returns></returns>
        public Conf_String GetFirstChildByAttr(
            PmName expectedName,
            string sExpectedValue,
            bool isRequired,
            Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0, Log_ReportsImpl.BDebugmode_Static);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "GetFirstChildByAttr", log_Reports);
            //
            //

            Conf_String cResult = null;

            if (log_Reports.Successful)
            {
                bool isHit = false;

                this.ChildNodes.ForEach(delegate(Conf_String item2, ref bool isBreak2)
                {
                    string value;
                    item2.Attributes.TryGetValue(expectedName, out value, false, log_Reports);

                    if (value == sExpectedValue)
                    {
                        isHit = true;
                        cResult = item2;
                        isBreak2 = true;
                    }
                });

                if (!isHit)
                {
                    cResult = null;

                    if (isRequired)
                    {
                        // エラーとして扱います。
                        goto gt_Error_NotFound;
                    }

                }
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_NotFound:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー002！", log_Method);

                StringBuilder s = new StringBuilder();
                s.Append("指定された要素は存在しませんでした。");
                s.Append(Environment.NewLine);
                s.Append(Environment.NewLine);

                s.Append("指定属性=[");
                s.Append(expectedName.Pm);
                s.Append("]");
                s.Append(Environment.NewLine);

                s.Append("指定値=[");
                s.Append(sExpectedValue);
                s.Append("]");
                s.Append(Environment.NewLine);

                //s.Append("┌──────────┐");
                //this.ChildNodes.ForEach(delegate(Conf_String item2, ref bool isBreak2)
                //{
                //    string value;
                //    item2.Attributes.TryGetValue(expectedName, out value, false, log_Reports);

                //    if (value == sExpectedValue)
                //    {
                //        isHit = true;
                //        cResult = item2;
                //        isBreak2 = true;
                //    }
                //});
                //s.Append("└──────────┘");

                // ヒント
                s.Append(r.Message_Conf(this));

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
            return cResult;
        }

        //────────────────────────────────────────

        /// <summary>
        /// 要素名を指定して、子ノードを取得したい。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isRequired">偽を指定した時は、要素数0のリストを返す。</param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        public List<Conf_String> GetChildrenByName(string name, bool isRequired, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0, Log_ReportsImpl.BDebugmode_Static);
            log_Method.BeginMethod(Info_Syntax.Name_Library, this, "GetChildrenByNodename", log_Reports);
            //
            //
            List<Conf_String> result = new List<Conf_String>();

            if (log_Reports.Successful)
            {
                this.childNodes.ForEach(delegate(Conf_String child_Conf, ref bool bBreak)
                {
                    if (name == child_Conf.Name)
                    {
                        // ノード名が一致
                        result.Add(child_Conf);
                    }
                    else
                    {
                        // ノード名が一致しないとき

                    }
                });
            }
            else
            {
                // 既にエラーが出ているとき
                goto gt_EndMethod;
            }

            if (result.Count < 1 && isRequired)
            {
                if (isRequired)
                {
                    goto gt_Error_EmptyHitChild;
                }
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_EmptyHitChild:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー502！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("該当した子要素がありませんでした。");
                s.Newline();


                s.Append("指定ノード名[");
                s.Append(name);
                s.Append("]");
                s.Newline();

                // ヒント
                s.Append(r.Message_Conf(this));

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
            return result;
        }

        //────────────────────────────────────────
        #endregion




        #region プロパティー
        //────────────────────────────────────────

        private string name;

        /// <summary>
        /// ノード（要素、属性）の名前。fncや arg など。
        /// 要素名検索で使われます。
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        //────────────────────────────────────────

        private Conf_String parent;

        /// <summary>
        /// 親要素。なければヌル。
        /// </summary>
        public Conf_String Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        //────────────────────────────────────────

        private ConfStringList childNodes;

        /// <summary>
        /// 子要素のリスト。（格納順序を保つこと）
        /// </summary>
        public ConfStringList ChildNodes
        {
            get
            {
                return childNodes;
            }
        }

        //────────────────────────────────────────

        private ConfStringMap attributes;

        public ConfStringMap Attributes
        {
            get
            {
                return this.attributes;
            }
        }

        //────────────────────────────────────────
        #endregion


    }
}
