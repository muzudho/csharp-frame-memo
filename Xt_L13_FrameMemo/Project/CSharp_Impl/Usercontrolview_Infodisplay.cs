using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

namespace Grayscale.FrameMemo
{

    /// <summary>
    /// width,height,ファイル名等表示です。
    /// </summary>
    public class Usercontrolview_Infodisplay : Usercontrolview
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public Usercontrolview_Infodisplay()
        {
            this.font = new Font("ＭＳ ゴシック", 12, FontStyle.Bold);

            int dy = 0;

            int nLastIndex = 7;
            this.locationAA = new Point[nLastIndex + 1][];

            for (int nIndex = 1; nIndex <= nLastIndex; nIndex++)
            {
                this.locationAA[nIndex] = new Point[nLastIndex + 1];
                this.locationAA[nIndex][1] = new Point(0, dy + 0);
                this.locationAA[nIndex][2] = new Point(4, dy + 4);
                this.locationAA[nIndex][3] = new Point(0, dy + 4);
                this.locationAA[nIndex][4] = new Point(4, dy + 0);
                this.locationAA[nIndex][5] = new Point(2, dy + 2);
                dy += 16;
            }

            this.filepath = "";
            this.textBackBrush = new SolidBrush(Color.FromArgb(0x99, 0xff, 0xff, 0xff));
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        public void Refresh()
        {
        }

        //────────────────────────────────────────

        /// <summary>
        /// セルの横幅。等倍。
        /// </summary>
        public void OnChanged_WidthcellResult(float nValue)
        {
        }

        //────────────────────────────────────────

        /// <summary>
        /// セルの縦幅。等倍。
        /// </summary>
        public void OnChanged_HeightcellResult(float nValue)
        {
        }

        //────────────────────────────────────────

        /// <summary>
        /// 行数。
        /// </summary>
        public void OnChanged_CountrowResult(float nValue)
        {
        }

        //────────────────────────────────────────

        /// <summary>
        /// 列数。
        /// </summary>
        public void OnChanged_CountcolumnResult(float nValue)
        {
        }

        //────────────────────────────────────────

        /// <summary>
        /// 切抜き位置／１～。0以下、または範囲外指定で全体図。
        /// </summary>
        public void OnChanged_CropForce(int nValue)
        {
        }

        //────────────────────────────────────────

        /// <summary>
        /// 切抜き位置終値／１～。
        /// </summary>
        public void OnChanged_CropLastResult(int nValue)
        {
        }

        //────────────────────────────────────────

        public string ToString_ColumnRow()
        {
            StringBuilder s = new StringBuilder();
            s.Append("c");
            s.Append(this.MemorySprite.CountcolumnResult);
            s.Append(" r");
            s.Append(this.MemorySprite.CountrowResult);
            s.Append("（列,行）");

            return s.ToString();
        }

        //────────────────────────────────────────

        public string ToString_CellSize()
        {
            StringBuilder s = new StringBuilder();
            s.Append("w");
            s.Append(this.MemorySprite.WidthcellResult);
            s.Append(" h");
            s.Append(this.MemorySprite.HeightcellResult);
            s.Append(" (セル)");

            return s.ToString();
        }

        //────────────────────────────────────────

        public string ToString_FrameSize()
        {
            SizeF size = this.MemorySprite.GetFrameSize();

            StringBuilder s = new StringBuilder();
            s.Append("w");
            s.Append(size.Width);
            s.Append(" h");
            s.Append(size.Height);
            s.Append(" (枠)");

            return s.ToString();
        }

        //────────────────────────────────────────

        public string ToString_FileName()
        {
            StringBuilder s = new StringBuilder();
            s.Append("file=");
            s.Append(System.IO.Path.GetFileName(this.Filepath));

            return s.ToString();
        }

        //────────────────────────────────────────

        public string ToString_Crop()
        {
            StringBuilder s = new StringBuilder();
            s.Append("切抜き=");
            s.Append(this.MemorySprite.FrameCropForce);
            s.Append("番目　x");

            PointF cropL = this.MemorySprite.GetCropXy();
            s.Append(cropL.X);
            s.Append(" y");
            s.Append(cropL.Y);

            return s.ToString();
        }

        //────────────────────────────────────────

        public string ToString_GridXy()
        {
            StringBuilder s = new StringBuilder();
            s.Append("x");
            s.Append(this.MemorySprite.GridLefttop.X);
            s.Append(" y");
            s.Append(this.MemorySprite.GridLefttop.Y);
            s.Append("（グリッド）");

            return s.ToString();
        }

        //────────────────────────────────────────

        public string ToString_ImageSize()
        {
            StringBuilder s = new StringBuilder();
            s.Append("w");
            s.Append(this.MemorySprite.SizeImage.Width);
            s.Append(" h");
            s.Append(this.MemorySprite.SizeImage.Height);
            s.Append(" (画)");

            return s.ToString();
        }

        //────────────────────────────────────────

        public int TextWidth(Graphics g, out string[] textArray)
        {
            int maxTxtWidth = 1;
            int tmpTxtWidth;

            //
            // 文字列行数
            textArray = new string[this.InfoRows + 1];

            int nIndex = 1;

            if ((this.MemorySprite.CountrowResult != 1 || this.MemorySprite.CountcolumnResult != 1) || this.MemorySprite.IsCrop)
            {
                // 1列、1行でなければ [c,r][w,h（個）][w,h（枠）]を表示するので +3 行。
                // または、切り抜いた時。

                // c,r
                textArray[nIndex] = this.ToString_ColumnRow();
                tmpTxtWidth = (int)g.MeasureString(textArray[nIndex], this.Font).Width;
                if (maxTxtWidth < tmpTxtWidth)
                {
                    maxTxtWidth = tmpTxtWidth;
                }
                nIndex++;

                // w,h（個）
                textArray[nIndex] = this.ToString_CellSize();
                tmpTxtWidth = (int)g.MeasureString(textArray[nIndex], this.Font).Width;
                if (maxTxtWidth < tmpTxtWidth)
                {
                    maxTxtWidth = tmpTxtWidth;
                }
                nIndex++;

                // w,h（枠）
                textArray[nIndex] = this.ToString_FrameSize();
                tmpTxtWidth = (int)g.MeasureString(textArray[nIndex], this.Font).Width;
                if (maxTxtWidth < tmpTxtWidth)
                {
                    maxTxtWidth = tmpTxtWidth;
                }
                nIndex++;
            }

            textArray[nIndex] = this.ToString_FileName();
            tmpTxtWidth = (int)g.MeasureString(textArray[nIndex], this.Font).Width;
            if (maxTxtWidth < tmpTxtWidth)
            {
                maxTxtWidth = tmpTxtWidth;
            }
            nIndex++;

            if (this.MemorySprite.IsCrop)
            {
                textArray[nIndex] = this.ToString_Crop();
                tmpTxtWidth = (int)g.MeasureString(textArray[nIndex], this.Font).Width;
                if (maxTxtWidth < tmpTxtWidth)
                {
                    maxTxtWidth = tmpTxtWidth;
                }
                nIndex++;
            }

            if (this.MemorySprite.GridLefttop.X != 0 || this.MemorySprite.GridLefttop.Y != 0)
            {
                textArray[nIndex] = this.ToString_GridXy();
                tmpTxtWidth = (int)g.MeasureString(textArray[nIndex], this.Font).Width;
                if (maxTxtWidth < tmpTxtWidth)
                {
                    maxTxtWidth = tmpTxtWidth;
                }
                nIndex++;
            }

            // 画
            textArray[nIndex] = this.ToString_ImageSize();
            tmpTxtWidth = (int)g.MeasureString(textArray[nIndex], this.Font).Width;
            if (maxTxtWidth < tmpTxtWidth)
            {
                maxTxtWidth = tmpTxtWidth;
            }
            nIndex++;

            return maxTxtWidth;
        }

        //────────────────────────────────────────

        public void Paint(Graphics g, bool bOnWindow, int dy, float scale2)
        {
            string[] textArray;
            int maxTxtWidth = this.TextWidth(g, out textArray);


            // 切抜きの有無。
            bool bCrop = this.MemorySprite.IsCrop;

            // 半透明の白色
            {
                int nRow = this.InfoRows;
                int nFontSize = 16;// +4;
                int nHeightMargin = 8 + 4;
                g.FillRectangle(this.textBackBrush,
                    0,
                    dy - 4,
                    maxTxtWidth + 8,
                    nRow * nFontSize + nHeightMargin//旧：5 * 16 + 8
                    );
            }

            int nIndex = 1;

            if ((this.MemorySprite.CountrowResult != 1 || this.MemorySprite.CountcolumnResult != 1) || this.MemorySprite.IsCrop)
            {
                // 1列、1行でなければ [c,r][w,h（個）][w,h（枠）]を表示するので +3 行。
                // または、切り抜いた時。

                // c,r
                // w,h（個）
                // w,h（枠）
                for (int nJ = 0; nJ < 3; nJ++)
                {
                    string sValue = textArray[nIndex];

                    // 光
                    g.DrawString(
                        sValue,
                        this.font,
                        Brushes.White,
                        this.locationAA[nIndex][1].X,
                        this.locationAA[nIndex][1].Y + dy
                        );
                    // 影
                    g.DrawString(
                        sValue,
                        this.font,
                        Brushes.White,
                        this.locationAA[nIndex][2].X,
                        this.locationAA[nIndex][2].Y + dy
                        );
                    g.DrawString(
                        sValue,
                        this.font,
                        Brushes.White,
                        this.locationAA[nIndex][3].X,
                        this.locationAA[nIndex][3].Y + dy
                        );
                    g.DrawString(
                        sValue,
                        this.font,
                        Brushes.White,
                        this.locationAA[nIndex][4].X,
                        this.locationAA[nIndex][4].Y + dy
                        );
                    //文字
                    g.DrawString(
                        sValue,
                        this.font,
                        Brushes.Black,
                        this.locationAA[nIndex][5].X,
                        this.locationAA[nIndex][5].Y + dy
                        );

                    nIndex++;
                }
            }

            // ファイル名
            {
                string sValue = textArray[nIndex];

                // 光
                g.DrawString(
                    sValue,
                    this.font,
                    Brushes.White,
                    this.locationAA[nIndex][1].X,
                    this.locationAA[nIndex][1].Y + dy
                    );
                // 影
                g.DrawString(
                    sValue,
                    this.font,
                    Brushes.White,
                    this.locationAA[nIndex][2].X,
                    this.locationAA[nIndex][2].Y + dy
                    );
                g.DrawString(
                    sValue,
                    this.font,
                    Brushes.White,
                    this.locationAA[nIndex][3].X,
                    this.locationAA[nIndex][3].Y + dy
                    );
                g.DrawString(
                    sValue,
                    this.font,
                    Brushes.White,
                    this.locationAA[nIndex][4].X,
                    this.locationAA[nIndex][4].Y + dy
                    );
                //文字
                g.DrawString(
                    sValue,
                    this.font,
                    Brushes.Black,
                    this.locationAA[nIndex][5].X,
                    this.locationAA[nIndex][5].Y + dy
                    );

                nIndex++;
            }

            // 切抜き位置。
            if (bCrop)
            {
                string sValue = textArray[nIndex];

                // 光
                g.DrawString(
                    sValue,
                    this.font,
                    Brushes.White,
                    this.locationAA[nIndex][1].X,
                    this.locationAA[nIndex][1].Y + dy
                    );
                // 影
                g.DrawString(
                    sValue,
                    this.font,
                    Brushes.White,
                    this.locationAA[nIndex][2].X,
                    this.locationAA[nIndex][2].Y + dy
                    );
                g.DrawString(
                    sValue,
                    this.font,
                    Brushes.White,
                    this.locationAA[nIndex][3].X,
                    this.locationAA[nIndex][3].Y + dy
                    );
                g.DrawString(
                    sValue,
                    this.font,
                    Brushes.White,
                    this.locationAA[nIndex][4].X,
                    this.locationAA[nIndex][4].Y + dy
                    );
                //文字
                g.DrawString(
                    sValue,
                    this.font,
                    Brushes.Black,
                    this.locationAA[nIndex][5].X,
                    this.locationAA[nIndex][5].Y + dy
                    );

                nIndex++;
            }

            // グリッドXY
            if (this.MemorySprite.GridLefttop.X != 0 || this.MemorySprite.GridLefttop.Y != 0)
            {
                string sValue = textArray[nIndex];

                // 光
                g.DrawString(
                    sValue,
                    this.font,
                    Brushes.White,
                    this.locationAA[nIndex][1].X,
                    this.locationAA[nIndex][1].Y + dy
                    );
                // 影
                g.DrawString(
                    sValue,
                    this.font,
                    Brushes.White,
                    this.locationAA[nIndex][2].X,
                    this.locationAA[nIndex][2].Y + dy
                    );
                g.DrawString(
                    sValue,
                    this.font,
                    Brushes.White,
                    this.locationAA[nIndex][3].X,
                    this.locationAA[nIndex][3].Y + dy
                    );
                g.DrawString(
                    sValue,
                    this.font,
                    Brushes.White,
                    this.locationAA[nIndex][4].X,
                    this.locationAA[nIndex][4].Y + dy
                    );
                //文字
                g.DrawString(
                    sValue,
                    this.font,
                    Brushes.Black,
                    this.locationAA[nIndex][5].X,
                    this.locationAA[nIndex][5].Y + dy
                    );

                nIndex++;
            }

            // 画像w,h（画）
            {
                string sValue = textArray[nIndex];

                // 光
                // bug:切り抜いたとき
                g.DrawString(
                    sValue,
                    this.font,
                    Brushes.White,
                    this.locationAA[nIndex][1].X,
                    this.locationAA[nIndex][1].Y + dy
                    );
                // 影
                g.DrawString(
                    sValue,
                    this.font,
                    Brushes.White,
                    this.locationAA[nIndex][2].X,
                    this.locationAA[nIndex][2].Y + dy
                    );
                g.DrawString(
                    sValue,
                    this.font,
                    Brushes.White,
                    this.locationAA[nIndex][3].X,
                    this.locationAA[nIndex][3].Y + dy
                    );
                g.DrawString(
                    sValue,
                    this.font,
                    Brushes.White,
                    this.locationAA[nIndex][4].X,
                    this.locationAA[nIndex][4].Y + dy
                    );
                //文字
                g.DrawString(
                    sValue,
                    this.font,
                    Brushes.Black,
                    this.locationAA[nIndex][5].X,
                    this.locationAA[nIndex][5].Y + dy
                    );

                nIndex++;
            }
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        protected MemorySpriteImpl memorySprite;

        public MemorySpriteImpl MemorySprite
        {
            get
            {
                return memorySprite;
            }
            set
            {
                memorySprite = value;
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// 座標フォント。
        /// </summary>
        protected Font font;

        public Font Font
        {
            get
            {
                return font;
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// 格子ペン。
        /// </summary>
        protected Pen gridPen;

        public Pen GridPen
        {
            get
            {
                return gridPen;
            }
            set
            {
                gridPen = value;
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// 2次元配列。
        /// [1]…(c r)列数行数　座標XY。 [光][影][影][影][字]
        /// [2]…(w h)セルサイズ（個）。 [光][影][影][影][字]
        /// [3]…(w h)セルサイズ（枠）。 [光][影][影][影][字]
        /// [4]…(file)ファイル名。 [光][影][影][影][字]
        /// [5]…(切抜き)切抜き位置。 [光][影][影][影][字]
        /// [6]…(グリッドxy)ベース位置。 [光][影][影][影][字]
        /// [3]…(w h)画像サイズ（画）。 [光][影][影][影][字]
        /// </summary>
        protected Point[][] locationAA;

        //────────────────────────────────────────

        protected string filepath;

        /// <summary>
        /// 画像の縦幅。等倍。
        /// </summary>
        public string Filepath
        {
            get
            {
                return filepath;
            }
            set
            {
                filepath = value;
            }
        }

        //────────────────────────────────────────

        protected Brush textBackBrush;

        //────────────────────────────────────────

        public int InfoRows
        {
            get
            {
                //
                // 文字列行数
                int infoRows = 2;// [w,h（全）] [file name]

                if ((this.MemorySprite.CountrowResult != 1 || this.MemorySprite.CountcolumnResult != 1) || this.MemorySprite.IsCrop)
                {
                    // 1列、1行でなければ [c,r][w,h（個）][w,h（枠）]を表示するので +2 行。
                    // または、切り抜いた時。
                    infoRows += 3;
                }

                if (this.MemorySprite.IsCrop)
                {
                    // 切抜き画像サイズ（切抜き1番目、等）を表示するなら +1 行。
                    infoRows++;
                }

                if (this.MemorySprite.GridLefttop.X != 0 || this.MemorySprite.GridLefttop.Y != 0)
                {
                    // グリッドベース座標を表示するなら +1 行。
                    infoRows++;
                }

                return infoRows;
            }
        }

        //────────────────────────────────────────
        #endregion



    }

}
