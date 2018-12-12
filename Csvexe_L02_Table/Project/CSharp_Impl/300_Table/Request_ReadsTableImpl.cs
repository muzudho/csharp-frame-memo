using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xenon.Syntax;//WarningReports, HumanInputFilePath

namespace Xenon.Table
{


    /// <summary>
    /// 
    /// </summary>
    public class Request_ReadsTableImpl : Request_ReadsTable
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public Request_ReadsTableImpl()
        {
            this.name_PutToTable = "";
            this.use = "";

            {
                Conf_String s_ParentNode = new Conf_StringImpl("!ハードコーディング_Request_TableReadsImpl#<init>", null);
                Conf_Filepath s_fpath = new Conf_FilepathImpl("ファイルパス出典未指定L02_1", s_ParentNode);
                this.expression_Filepath = new Expr_FilepathImpl(s_fpath);
            }
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private string name_PutToTable;

        /// <summary>
        /// テーブルに付けたい名前。
        /// </summary>
        public string Name_PutToTable
        {
            get
            {
                return name_PutToTable;
            }
            set
            {
                name_PutToTable = value;
            }
        }

        //────────────────────────────────────────

        private string tableunit;

        /// <summary>
        /// テーブル_ユニット名。
        /// </summary>
        public string Tableunit
        {
            get
            {
                return tableunit;
            }
            set
            {
                tableunit = value;
            }
        }

        //────────────────────────────────────────

        private string typedata;

        /// <summary>
        /// 「TYPE_DATA」フィールド値。
        /// 「T:～;」
        /// </summary>
        public string Typedata
        {
            get
            {
                return typedata;
            }
            set
            {
                typedata = value;
            }
        }

        //────────────────────────────────────────

        private Expr_Filepath expression_Filepath;

        /// <summary>
        /// CSVファイル等へのパス。
        /// </summary>
        public Expr_Filepath Expression_Filepath
        {
            get
            {
                return expression_Filepath;
            }
            set
            {
                expression_Filepath = value;
            }
        }

        //────────────────────────────────────────

        private bool isDatebackupActivated;

        /// <summary>
        /// 「日別バックアップ」するなら真。
        /// </summary>
        public bool IsDatebackupActivated
        {
            get
            {
                return isDatebackupActivated;
            }
            set
            {
                isDatebackupActivated = value;
            }
        }

        //────────────────────────────────────────

        private string use;

        /// <summary>
        /// 用途。／「」指定なし。／「WriteOnly」データの読取を行わない。ログ出力先を登録しているだけなど。
        /// </summary>
        public string Use
        {
            get
            {
                return use;
            }
            set
            {
                use = value;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
