using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Xenon.Syntax;//WarningReports, HumanInputFilePath

using Xenon.Table;//OTableImpl


namespace Xenon.Table
{
    /// <summary>
    /// テーブルを作成します。
    /// 
    /// 他の Readメソッドの説明文参照。
    /// </summary>
    public class CsvTo_TableImpl
    {



        #region 用意
        //────────────────────────────────────────

        public const string S_WRITE_ONLY = "WriteOnly";

        //────────────────────────────────────────
        #endregion





        #region 生成と破棄
        //────────────────────────────────────────

        public CsvTo_TableImpl()
        {
            this.charSeparator = ',';
        }

        //────────────────────────────────────────
        #endregion




        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// パーサーのハブ。
        /// 
        /// </summary>
        /// <param name="request_ReadsTable">テーブルに付けたい名前や、ファイルパスの要求。</param>
        /// <param name="tableFormat_puts">テーブルの行列が逆になっているなどの、設定。</param>
        /// <param name="isRequired">テーブルが無かった場合、エラーとするなら真。</param>
        /// <param name="out_sErrorMsg"></param>
        /// <returns></returns>
        public Table_Humaninput Read(
            Request_ReadsTable request_ReadsTable,
            Format_Table tableFormat_puts,
            bool isRequired,
            Encoding encodingCsv,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Table.Name_Library, this, "Read",log_Reports);


            // 読み取ったテーブル
            Table_Humaninput xTable;

            //絶対ファイルパス
            string csvAbs = request_ReadsTable.Expression_Filepath.Lv4Execute_OnImplement(
                EnumHitcount.Unconstraint, log_Reports);
            if (!log_Reports.Successful)
            {
                // 既エラー。
                xTable = null;
                goto gt_EndMethod;
            }

            // CSVテキスト
            string contents;
            Exception error_Excp;
            if (CsvTo_TableImpl.S_WRITE_ONLY!=request_ReadsTable.Use)
            {
                // 書き出し専用でなければ。
                // ファイル読取を実行します。

                try
                {
                    if (!System.IO.File.Exists(csvAbs))
                    {
                        // ファイルが存在しない場合。
                        xTable = null;
                        goto gt_Error_NotExistsFile;
                    }

                    // TODO:IOException 別スレッドで開いているときなど。

                    contents = System.IO.File.ReadAllText(csvAbs, encodingCsv);
                    //log_Method.WriteDebug_ToConsole(string_Csv);
                }
                catch (System.IO.IOException e)
                {
                    // エラー処理。
                    xTable = null;
                    contents = "";
                    error_Excp = e;
                    goto gt_Error_FileOpen;
                }
                catch (Exception e)
                {
                    // エラー処理。
                    xTable = null;
                    contents = "";
                    error_Excp = e;
                    goto gt_Error_Exception;
                }
            }
            else
            {
                contents = "";
            }

            xTable = this.Read(
                contents,
                request_ReadsTable,
                tableFormat_puts,
                log_Reports
                );
            if (!log_Reports.Successful)
            {
                // 既エラー。
                goto gt_EndMethod;
            }

            // NOフィールドの値を 0からの連番に振りなおします。
            xTable.RenumberingNoField();

            if (isRequired && null == xTable)
            {
                goto gt_Error_NullTable;
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_FileOpen:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("Er:201;", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("ファイルの読取りに失敗しました。");
                s.Newline();
                s.Newline();

                s.Append("　ファイル=[");
                s.Append(csvAbs);
                s.Append("]");
                s.Newline();
                s.Newline();

                s.Append("もしかして？");
                s.Newline();

                s.Append("　・ファイルの有無、ファイル名、ファイル パスを確認してください。");
                s.Newline();
                s.Append("　・別アプリケーションで　ファイルを開いていれば、閉じてください。");
                s.Newline();
                s.Newline();

                //
                // ヒント
                request_ReadsTable.Expression_Filepath.Conf.ToText_Locationbreadcrumbs(s);
                s.Append(error_Excp.Message);

                r.Message = s.ToString();

                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_NotExistsFile:
            if(log_Reports.CanCreateReport)
            {
                if ("" == request_ReadsTable.Expression_Filepath.Directory_Base)
                {
                    Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                    r.SetTitle("Er:202;", log_Method);

                    Log_TextIndented s = new Log_TextIndentedImpl();
                    s.Append("指定されたファイルはありませんでした。CSVファイルを読み込もうとしたとき。");
                    s.Newline();
                    s.Newline();

                    s.AppendI(1, "指定されたファイルパス=[");
                    s.Append(csvAbs);
                    s.Append("]");
                    s.Newline();

                    {
                        s.AppendI(1, "ベース・ディレクトリは指定されていません。");
                        s.Newline();
                        s.AppendI(2, "もし相対パスが指定されていた場合、実行した.exeファイルからの相対パスとします。");
                        s.Newline();
                        s.Newline();
                    }

                    s.Append("　ヒント：ファイルの有無、ファイル名、ファイル パスを確認してください。");
                    s.Newline();

                    // ヒント
                    s.Append(r.Message_Conf(
                        request_ReadsTable.Expression_Filepath.Conf));
                    r.Message = s.ToString();
                }
                else
                {
                    Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                    r.SetTitle("▲エラー235！", log_Method);

                    Log_TextIndented s = new Log_TextIndentedImpl();
                    s.Append("指定されたファイルはありませんでした。CSVファイルを読み込もうとしたとき。");
                    s.Newline();
                    s.Newline();

                    s.AppendI(1, "指定されたファイルパス=[");
                    s.Append(csvAbs);
                    s.Append("]");
                    s.Newline();

                    {
                        s.AppendI(1, "指定されたベース・ディレクトリ=[");
                        s.Append(request_ReadsTable.Expression_Filepath.Directory_Base);
                        s.Append("]");
                        s.Newline();
                        s.Newline();
                    }

                    s.Append("　ヒント：ファイルの有無、ファイル名、ファイル パスを確認してください。");
                    s.Newline();

                    // ヒント
                    s.Append(r.Message_Conf(
                        request_ReadsTable.Expression_Filepath.Conf));
                    r.Message = s.ToString();
                }


                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_Exception:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー104！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("▲エラー4030！(" + Info_Table.Name_Library + ")");
                s.Newline();
                s.Append("CSV読み取り中にエラーが発生しました。");
                s.Append(Environment.NewLine);
                s.Append(Environment.NewLine);
                s.Append("指定CSVファイル=[");
                s.Append(csvAbs);
                s.Append("]");
                s.Append(Environment.NewLine);
                s.Append(Environment.NewLine);

                //
                // ヒント
                request_ReadsTable.Expression_Filepath.Conf.ToText_Locationbreadcrumbs(s);


                s.Append("エラーの種類：");
                s.Append(error_Excp.GetType().Name);
                s.Append(Environment.NewLine);
                s.Append(Environment.NewLine);
                s.Append("エラーメッセージ：");
                s.Append(error_Excp.Message);

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_NullTable:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー105！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Append("▲エラー131！");
                s.Newline();
                s.Append("[");
                s.Append(request_ReadsTable.Name_PutToTable);
                s.Append("]テーブルがありませんでした。");
                s.Append(Environment.NewLine);
                s.Append(Environment.NewLine);

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
            return xTable;
        }

        //────────────────────────────────────────
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contents">CSVテキスト</param>
        /// <param name="request">テーブル読取り時の要求。</param>
        /// <param name="formatPuts">設定するフォーマット。</param>
        /// <param name="out_SErrorMsg"></param>
        /// <returns></returns>
        public Table_Humaninput Read(
            string contents,
            Request_ReadsTable request,
            Format_Table formatPuts,
            Log_Reports log_Reports
            )
        {
            // 読み取ったテーブル
            Table_Humaninput resultTable;

            if (formatPuts.IsRowcolumnreverse)
            {
                //
                // 縦、横がひっくりかえっているCSVテーブルを読み込みます。
                //

                if (formatPuts.IsAllintfieldsActivated)
                {
                    //
                    // 型定義のレコードがなく、全てのフィールドがint型のCSVテーブルを読み込みます。
                    //

                    CsvTo_Table_ReverseAllIntsImpl_ csvTo = new CsvTo_Table_ReverseAllIntsImpl_();
                    csvTo.CharSeparator = this.CharSeparator;

                    resultTable = csvTo.Read(
                        contents,
                        request,
                        formatPuts,
                        log_Reports
                        );
                    if (!log_Reports.Successful)
                    {
                        // 既エラー。
                        resultTable = null;
                        goto gt_EndMethod;
                    }
                }
                else
                {
                    CsvTo_Table_ReverseImpl_ csvTo = new CsvTo_Table_ReverseImpl_();
                    csvTo.CharSeparator = this.CharSeparator;

                    resultTable = csvTo.Read(
                        contents,
                        request,
                        formatPuts,
                        log_Reports
                        );
                    if (!log_Reports.Successful)
                    {
                        // 既エラー。
                        resultTable = null;

                        goto gt_EndMethod;
                    }
                }
            }
            else
            {
                //
                // 縦、横そのままのCSVテーブルを読み込みます。
                //
                CsvTo_Table_RegularImpl_ csvTo = new CsvTo_Table_RegularImpl_();
                csvTo.CharSeparator = this.CharSeparator;

                resultTable = csvTo.Read(
                    contents,
                    request,
                    formatPuts,
                    log_Reports
                    );
                if (!log_Reports.Successful)
                {
                    // 既エラー。
                    resultTable = null;

                    goto gt_EndMethod;
                }
            }

            goto gt_EndMethod;

            //
            //
            //
            //
        gt_EndMethod:
            return resultTable;
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private char charSeparator;

        /// <summary>
        /// 区切り文字。初期値は「,」
        /// </summary>
        public char CharSeparator
        {
            get
            {
                return charSeparator;
            }
            set
            {
                charSeparator = value;
            }
        }

        //────────────────────────────────────────
        #endregion


    }
}
