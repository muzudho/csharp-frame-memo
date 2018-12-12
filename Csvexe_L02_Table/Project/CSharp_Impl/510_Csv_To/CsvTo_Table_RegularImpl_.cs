using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Xenon.Syntax;


namespace Xenon.Table
{

    /// <summary>
    /// 
    /// </summary>
    class CsvTo_Table_RegularImpl_
    {



        #region 生成と破棄
        //────────────────────────────────────────

        public CsvTo_Table_RegularImpl_()
        {
            this.charSeparator = ',';
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// <summary>
        /// CSVを読取り、テーブルにして返します。
        /// 
        /// 
        /// SRS仕様の実装状況
        /// ここでは、先頭行を[0]行目と数えるものとします。
        /// (1)CSVの[0]行目は列名です。
        /// (2)CSVの[1]行目は型名です。
        /// (3)CSVの[2]行目はコメントです。
        /// 
        /// (4)データ・テーブル部で、0列目に「EOF」と入っていれば終了。大文字・小文字は区別せず。
        ///    それ以降に、コメントのようなデータが入力されていることがあるが、フィールドの型に一致しないことがあるので無視。
        ///    TODO: EOF以降の行も、コメントとして残したい。
        /// 
        /// (5)列名に ”ＥＮＤ”（半角） がある場合、その手前までの列が有効データです。
        ///    ”ＥＮＤ”以降の列は無視します。
        ///    TODO: ”ＥＮＤ”以降の行も、コメントとして残したい。
        /// 
        /// (6)int型として指定されているフィールドのデータ・テーブル部に空欄があった場合、DBNull（データベース用のヌル）とします。
        /// </summary>
        /// <param name="contents">CSVテキスト</param>
        /// <param name="request">要求</param>
        /// <param name="format_puts">設定するフォーマット</param>
        /// <param name="log_Reports"></param>
        /// <returns>列名情報も含むテーブル。列の型は文字列型とします。</returns>
        public Table_Humaninput Read(
            string contents,
            Request_ReadsTable request,
            Format_Table format_puts,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl();
            log_Method.BeginMethod(Info_Table.Name_Library, this, "Read(1)",log_Reports);

            Table_Humaninput xTable = new Table_HumaninputImpl(
                request.Name_PutToTable, request.Expression_Filepath, request.Expression_Filepath.Conf );
            xTable.Tableunit = request.Tableunit;
            xTable.Typedata = request.Typedata;
            xTable.IsDatebackupActivated = request.IsDatebackupActivated;
            xTable.Format_Table_Humaninput = format_puts;


            Exception err_Excp;
            int error_Count_Index;
            string[] error_Fields_Cur;


            //
            // 型定義部
            //
            // （※NO,ID,EXPL,NAME など、フィールドの定義を持つテーブル）
            //
            RecordFielddef rFielddef = new RecordFielddefImpl();

            //
            // データ・テーブル部
            //
            List<List<string>> dataTableRows = new List<List<string>>();

            // CSVテキストを読み込み、型とデータのバッファーを作成します。
            System.IO.StringReader reader = new System.IO.StringReader(contents);
            CsvLineParserImpl csvParser = new CsvLineParserImpl();

            // CSVを解析して、テーブル形式で格納。
            {
                // データとして認識する列の総数です。
                int nDataColumnsCount = 0;

                // 行番地(0開始)
                int nRow = 0;
                string[] fields_Cur;
                while (-1 < reader.Peek())
                {
                    string line = reader.ReadLine();

                    fields_Cur = csvParser.UnescapeLineToFieldList(line, this.charSeparator).ToArray();


                    if (0 == nRow)
                    {
                        // 0行目
                        
                        // 列名の行とします。

                        for (int nColumnIx = 0; nColumnIx < fields_Cur.Length; nColumnIx++)
                        {
                            string sColumnName = fields_Cur[nColumnIx];

                            // 列名を読み込みました。

                            // トリム＆大文字
                            string sCellValueTU = sColumnName.Trim().ToUpper();
                            if (
                                ToCsv_Table_RowColRegularImpl_.S_EOL == sCellValueTU ||
                                ToCsv_Table_RowColRegularImpl_.S_END == sCellValueTU
                                )
                            {
                                // 列名に ”ＥＮＤ” がある場合、その手前までの列が有効データです。
                                // ”ＥＮＤ” 以降の列は無視します。
                                goto field_name_reading_end;
                            }

                            // テーブルのフィールドを追加します。型の既定値は文字列型とします。
                            FielddefImpl fieldDef = new FielddefImpl(sColumnName, EnumTypeFielddef.String);
                            rFielddef.Add(fieldDef);
                            nDataColumnsCount++;
                        }


                        // 0行目は、テーブルのデータとしては持ちません。
                    }
                    else if (1 == nRow)
                    {
                        // 1行目
                        
                        // フィールド型名の行。

                        for (int nColumnIx = 0; nColumnIx < nDataColumnsCount; nColumnIx++)
                        {
                            string name_FieldType_Lower;
                            try
                            {
                                name_FieldType_Lower = fields_Cur[nColumnIx].ToLower();
                            }
                            catch (IndexOutOfRangeException e)
                            {
                                err_Excp = e;
                                goto gt_Error_FdIndexOutOfRangeException;
                            }

                            // 列の型名を読み込みました。

                            // テーブルのフィールドを追加します。型の既定値は文字列型とします。
                            // TODO int型とboolean型にも対応したい。
                            if (FielddefImpl.S_STRING.Equals(name_FieldType_Lower))
                            {
                                rFielddef.ValueAt(nColumnIx).Type_Field = EnumTypeFielddef.String;
                            }
                            else if (FielddefImpl.S_INT.Equals(name_FieldType_Lower))
                            {
                                rFielddef.ValueAt(nColumnIx).Type_Field = EnumTypeFielddef.Int;
                            }
                            else if (FielddefImpl.S_BOOL.Equals(name_FieldType_Lower))
                            {
                                // 2009-11-11修正：SRS仕様では「bool」が正しい。「boolean」は間違い。
                                rFielddef.ValueAt(nColumnIx).Type_Field = EnumTypeFielddef.Bool;
                            }
                            else
                            {
                                // 型が未定義の列は、文字列型として読み取ります。

                                // TODO:警告を出すか？

                                rFielddef.ValueAt(nColumnIx).Type_Field = EnumTypeFielddef.String;
                            }
                        }

                        // 1行目は、テーブルのデータとしては持ちません。
                    }
                    else if (2 == nRow)
                    {
                        // 2行目
                        
                        // フィールドのコメントの行。
                        // TODO: フィールドのコメントの行は省略されることがある。

                        for (int column = 0; column < nDataColumnsCount; column++)
                        {
                            if (fields_Cur.Length<=column)
                            {
                                error_Fields_Cur = fields_Cur;
                                //error_Count_Columns = fields_Cur.Length;
                                error_Count_Index = column;
                                goto gt_Error_CommentFieldCount;
                            }

                            string comment_Field = fields_Cur[column];//todo:bug:境界線エラーをキャッチしてない。

                            rFielddef.ValueAt(column).Comment = comment_Field;
                        }

                        // 2行目は、テーブルのデータとしては持ちません。
                    }
                    else
                    {
                        // 3行目以降のループ。
                        List<string> sList_Column = new List<string>();

                        // データ・テーブル部で、0列目に「EOF」と入っていれば終了。大文字・小文字は区別せず。

                        if (fields_Cur.Length < 1)
                        {
                            // 空行は無視。
                            goto end_recordAdd;
                        }
                        //ystem.Console.WriteLine(InfxenonTable.LibraryName + ":" + this.GetType().Name + "#UnescapeToList: sFields[0]=[" + sFields[0] + "] sLine=[" + sLine + "]");

                        string sCellValueTrimUpper = fields_Cur[0].Trim().ToUpper();
                        if (ToCsv_Table_RowColRegularImpl_.S_EOF == sCellValueTrimUpper)
                        {
                            goto reading_end;
                        }

                        int nColumnCount;
                        if (fields_Cur.Length < nDataColumnsCount)
                        {
                            // 「実際にデータとして存在する列数」
                            nColumnCount = fields_Cur.Length;
                        }
                        else
                        {
                            // 「データとして存在する筈の列数」（これ以降の列は無視）
                            nColumnCount = nDataColumnsCount;
                        }


                        for (int nColumnIx = 0; nColumnIx < nColumnCount; nColumnIx++)
                        {
                            string sValue;

                            sValue = fields_Cur[nColumnIx];

                            if (rFielddef.Count <= nColumnIx)
                            {
                                // 0行目で数えた列数より多い場合。

                                // テーブルのフィールドを追加します。型は文字列型とします。名前は空文字列です。
                                rFielddef.Add(new FielddefImpl("", EnumTypeFielddef.String));
                            }

                            sList_Column.Add(sValue);
                        }

                        dataTableRows.Add(sList_Column);
                    end_recordAdd:
                        ;
                    }
                field_name_reading_end:

                    //essageBox.Show("ttbwIndex=[" + ttbwIndex + "]行目ループ終わり", "TableCsvLibデバッグ");
                    nRow++;
                }
            }
        reading_end:

            // ストリームを閉じます。
            reader.Close();

            //essageBox.Show("CSV読取終わり1 rows.Count=[" + rows.Count + "]", "TableCsvLibデバッグ");


            // テーブルのフィールド定義。
            xTable.CreateTable(rFielddef,log_Reports);
            if(log_Reports.Successful)
            {
                // データ本体のセット。
                xTable.AddRecordList(dataTableRows, rFielddef, log_Reports);
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_CommentFieldCount:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー1356！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();

                s.Append("「フィールド・コメント」行のフィールド数が合いませんでした。");
                s.Append(Environment.NewLine);
                s.Append(Environment.NewLine);
                s.Append("もしかして？");
                s.Append(Environment.NewLine);
                s.Append("　　・１行目の最後のフィールドには「EOL」を入れてください。");
                s.Append(Environment.NewLine);
                s.Append(Environment.NewLine);

                s.Append("index＝[");
                s.Append(error_Count_Index);
                s.Append("]");
                s.Append(Environment.NewLine);

                s.Append("列数＝[");
                s.Append(error_Fields_Cur.Length);
                s.Append("]");
                s.Append(Environment.NewLine);
                s.Append(Environment.NewLine);

                //
                // ヒント
                s.Append(Log_RecordReportsImpl.ToText_Conf(xTable));
                s.Append(Environment.NewLine);

                s.Append(Environment.NewLine);
                s.Append(Environment.NewLine);
                s.Append("──────────fields ここから");
                s.Append(Environment.NewLine);
                foreach (string field in error_Fields_Cur)
                {
                    s.Append("field=[");
                    s.Append(field);
                    s.Append("]");
                    s.Append(Environment.NewLine);
                }
                s.Append("──────────fields ここまで");

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_FdIndexOutOfRangeException:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー132！", log_Method);

                Log_TextIndented s = new Log_TextIndentedImpl();
                s.Newline();
                
                s.Append("フィールド定義の数が合いませんでした。");
                s.Append(Environment.NewLine);
                s.Append(Environment.NewLine);

                string sFpatha = request.Expression_Filepath.Lv4Execute_OnImplement(
                    EnumHitcount.Unconstraint, log_Reports);
                s.Append("ファイルパス＝[");
                s.Append(sFpatha);
                s.Append("]");
                s.Append(Environment.NewLine);
                s.Append(Environment.NewLine);

                //
                // ヒント
                s.Append(err_Excp.Message);

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
