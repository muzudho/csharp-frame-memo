using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Xenon.Syntax;

namespace Xenon.Table
{
    public class CsvWriterImpl
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contents">CSVテキスト</param>
        /// <param name="csvAbs">絶対ファイルパス</param>
        /// <param name="isSuccessfulDialogPopup"></param>
        public void Write(
            string contents,
            string csvAbs,
            bool isSuccessfulDialogPopup,
            Encoding encodingCsv
            )
        {


            try
            {
                System.IO.File.WriteAllText(csvAbs, contents, encodingCsv);

                if (isSuccessfulDialogPopup)
                {
                    Log_TextIndented s = new Log_TextIndentedImpl();

                    s.Append("ファイルに書き込みました。");
                    s.Append(Environment.NewLine);
                    s.Append("[");
                    s.Append(csvAbs);
                    s.Append("]");

                    MessageBox.Show(s.ToString(), "▲実行結果！（L02）");
                }
            }
            catch (Exception ex)
            {
                // 異常時は必ずポップアップが出る。
                MessageBox.Show(
                    ex.Message,
                    "▲エラー201！(" + Info_Table.Name_Library + ") " + this.GetType().Name + "#Write"
                    );
            }


        }

        //────────────────────────────────────────
        #endregion



    }
}
