using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Xenon.Table;

namespace Xenon.Lib
{

    /// <summary>
    /// フォント。
    /// </summary>
    public class FontText
    {


        #region 生成と破棄
        //────────────────────────────────────────

        private FontText()
        {
            this.Name = "";
            this.Size = 12.0f;
        }

        public static FontText FromFont(Font font1)
        {
            FontText result = new FontText();

            if (null != font1)
            {
                result.Name = font1.Name;

                result.Size = font1.Size;

                result.Bold = font1.Bold;
                result.Italic = font1.Italic;
                result.Strikeout = font1.Strikeout;
                result.Underline = font1.Underline;
            }

            return result;
        }

        /// <summary>
        /// フォントの一行表記を読取り。
        /// "ＭＳ ゴシック", "12.25", "Bold, Italic"
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static FontText FromTextbox(string textbox, out string errMsg)
        {
            errMsg = "";
            FontText result = new FontText();

            CsvLineParserImpl p = new CsvLineParserImpl();
            List<string> tokens1 =  p.UnescapeLineToFieldList(textbox,',');

            if (tokens1.Count!=3)
            {
                errMsg = "カンマ区切りの3要素でない。";
                goto gt_EndMethod;
            }

            //━━━━━
            //フォント名
            //━━━━━
            result.Name = tokens1[0].Trim();

            //━━━━━
            //フォントサイズ
            //━━━━━
            float size;
            if (float.TryParse(tokens1[1].Trim(), out size))
            {
                result.Size = size;
            }
            else
            {
                //エラー
                errMsg = "サイズがおかしい。";
                goto gt_EndMethod;
            }

            //━━━━━
            //フォントスタイル
            //━━━━━
            List<string> tokens2 = p.UnescapeLineToFieldList(tokens1[2].Trim(), ',');

            foreach (string token in tokens2)
            {
                switch (token.Trim().ToUpper())
                {
                    case "BOLD":
                        result.Bold = true;
                        break;
                    case "ITALIC":
                        result.Italic = true;
                        break;
                    case "STRIKEOUT":
                        result.Strikeout = true;
                        break;
                    case "UNDERLINE":
                        result.Underline = true;
                        break;
                }
            }

            goto gt_EndMethod;

        gt_EndMethod:
            return result;
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────


        /// <summary>
        /// TODO:現状、返却値は常に真だが、偽を返して欲しいことはあるか？
        /// </summary>
        /// <param name="rFont1Name"></param>
        /// <param name="rFont1Size"></param>
        /// <param name="rFont1Style"></param>
        /// <param name="font1"></param>
        /// <returns></returns>
        public static bool TryFont(string rFont1Name, string rFont1Size, string rFont1Style, out Font font1)
        {
            float size;
            if (!float.TryParse(rFont1Size, out size))
            {
                size = 12.0f;// UcMain.DEFAULT_FONT.Size;
            }

            //ystem.Console.WriteLine("rFont1Style=["+rFont1Style+"]");
            FontStyle fs = FontStyle.Regular;
            {
                CsvLineParserImpl p = new CsvLineParserImpl();
                List<string> tokens = p.UnescapeLineToFieldList(rFont1Style, ',');
                foreach (string token in tokens)
                {
                    string token2 = token.Trim();
                    //ystem.Console.WriteLine("token2=[" + token2 + "]");

                    // 指定をつなげています。
                    switch (token2)
                    {
                        case "Bold":
                            fs |= FontStyle.Bold;
                            //ystem.Console.WriteLine("ボールド");
                            break;
                        case "Italic":
                            fs |= FontStyle.Italic;
                            //ystem.Console.WriteLine("イタリック");
                            break;
                        case "Strikeout":
                            fs |= FontStyle.Strikeout;
                            //ystem.Console.WriteLine("ストライクアウト");
                            break;
                        case "Underline":
                            fs |= FontStyle.Underline;
                            //ystem.Console.WriteLine("アンダーライン");
                            break;
                        default:
                            //無視。
                            //fs = FontStyle.Regular;
                            break;
                    }

                }
            }

            font1 = new Font(rFont1Name, size, fs);
            return true;
        }

        //────────────────────────────────────────

        public bool ToFont(out Font result, out string errMsg)
        {
            errMsg = "";
            bool isSuccess = true;

            FontStyle fs = FontStyle.Regular;

            if(this.Bold)
            {
                fs |= FontStyle.Bold;
            }

            if (this.Italic)
            {
                fs |= FontStyle.Italic;
            }

            if (this.Strikeout)
            {
                fs |= FontStyle.Strikeout;
            }

            if (this.Underline)
            {
                fs |= FontStyle.Underline;
            }

            try
            {
                result = new Font(this.Name, this.Size, fs);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("FontText#ToFont: エラー。 this.Name（" + this.Name + "）　this.Size（" + this.Size + "）");

                // テキスト入力中に　引数エラーになる場合もある。
                isSuccess = false;
                result = null;
                errMsg = e.Message;
            }

            return isSuccess;
        }

        /// <summary>
        /// フォントの一行表記。
        /// "ＭＳ ゴシック", "12.25", "Bold, Italic"
        /// </summary>
        public string ToTextbox()
        {
            StringBuilder sb = new StringBuilder();

            //━━━━━
            //フォント名
            //━━━━━
            CsvLineParserImpl p = new CsvLineParserImpl();


            sb.Append(CsvLineParserImpl.EscapeCell(this.Name));

            sb.Append(",");//カンマの前後に無用に空白を入れないこと。

            //━━━━━
            //サイズ
            //━━━━━
            sb.Append(CsvLineParserImpl.EscapeCell(this.Size.ToString()));

            sb.Append(",");//カンマの前後に無用に空白を入れないこと。

            //━━━━━
            //フォントスタイル
            //━━━━━
            {
                StringBuilder sb2 = new StringBuilder();

                if (!this.Bold && !this.Italic)
                {
                    sb2.Append("Regular");
                }
                else if (this.Bold && this.Italic)
                {
                    sb2.Append("Bold, Italic");
                }
                else if (this.Bold)
                {
                    sb2.Append("Bold");
                }
                else if (this.Italic)
                {
                    sb2.Append("Italic");
                }
                else
                {
                }

                if (this.Strikeout)
                {
                    sb.Append(", Strikeout");
                }

                if (this.Strikeout)
                {
                    sb.Append(", Underline");
                }

                sb.Append(CsvLineParserImpl.EscapeCell(sb2.ToString()));
            }

            return sb.ToString();
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private bool bold_;

        public bool Bold
        {
            get
            {
                return bold_;
            }
            set
            {
                bold_ = value;
            }
        }

        //────────────────────────────────────────

        private string name_;

        public string Name
        {
            get
            {
                return name_;
            }
            set
            {
                name_ = value;
            }
        }

        //────────────────────────────────────────

        private bool italic_;

        public bool Italic
        {
            get
            {
                return italic_;
            }
            set
            {
                italic_ = value;
            }
        }

        //────────────────────────────────────────

        private float size_;

        public float Size
        {
            get
            {
                return size_;
            }
            set
            {
                size_ = value;
            }
        }

        //────────────────────────────────────────

        private bool strikeout_;

        public bool Strikeout
        {
            get
            {
                return strikeout_;
            }
            set
            {
                strikeout_ = value;
            }
        }

        //────────────────────────────────────────

        private bool underline_;

        public bool Underline
        {
            get
            {
                return underline_;
            }
            set
            {
                underline_ = value;
            }
        }

        //────────────────────────────────────────
        #endregion



    }

}
