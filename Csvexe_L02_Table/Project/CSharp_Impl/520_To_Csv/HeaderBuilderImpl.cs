using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Table
{

    /// <summary>
    /// 
    /// NO,ID,NAME,Expl,EOL
    /// int,int,string,string,
    /// ver1.09改1,識別子,名前,解説,
    /// 
    /// といった文字列を作ります。
    /// </summary>
    public class HeaderBuilderImpl
    {


        #region 生成と破棄
        //────────────────────────────────────────

        public HeaderBuilderImpl()
        {
            this.name_ = new List<string>();
            this.type_ = new List<string>();
            this.comment_ = new List<string>();
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 文字列エスケープされます。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="comment"></param>
        public void AddWithEscape(string name, string type, string comment)
        {
            this.name_.Add(CsvLineParserImpl.EscapeCell(name));
            this.type_.Add(CsvLineParserImpl.EscapeCell(type));
            this.comment_.Add(CsvLineParserImpl.EscapeCell(comment));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            //フィールド名
            foreach(string name in this.name_)
            {
                sb.Append(name);
                sb.Append(",");
            }
            sb.AppendLine("EOL");

            //タイプ
            foreach (string type in this.type_)
            {
                sb.Append(type);
                sb.Append(",");
            }
            sb.AppendLine();

            //コメント
            foreach (string comment in this.comment_)
            {
                sb.Append(comment);
                sb.Append(",");
            }
            sb.AppendLine();

            return sb.ToString();
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private List<string> name_;

        //public List<string> Name
        //{
        //    get
        //    {
        //        return name_;
        //    }
        //    set
        //    {
        //        name_ = value;
        //    }
        //}

        private List<string> type_;

        //public List<string> Type
        //{
        //    get
        //    {
        //        return type_;
        //    }
        //    set
        //    {
        //        type_ = value;
        //    }
        //}

        private List<string> comment_;

        //public List<string> Comment
        //{
        //    get
        //    {
        //        return comment_;
        //    }
        //    set
        //    {
        //        comment_ = value;
        //    }
        //}

        //────────────────────────────────────────
        #endregion



    }
}
