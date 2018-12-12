using System.Drawing;
using System.Windows.Forms;

namespace Grayscale.FrameMemo.Commons
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ImageHelper
    {
        /// <summary>
        /// 保存する画像の作成。Save1、Save3で共通。
        /// </summary>
        public static Bitmap CreateSaveImage(
            Usercontrolview_Infodisplay infodisplay,
            CheckBox pcchkInfo,
            UcCanvas uc_FrameMemo
            )
        {
            Bitmap bm;

            if (null != infodisplay.MemorySprite.Bitmap)
            {

                // 情報領域
                int infoHeight;
                {
                    int infoRows = infodisplay.InfoRows;
                    int nHeightMargin = 8 + 4;
                    int nFontSize = 16;
                    infoHeight = infoRows * nFontSize + nHeightMargin;
                }


                {
                    // 情報欄のサイズ
                    SizeF infoSizeF;
                    if (pcchkInfo.Checked)
                    {
                        //
                        // 情報表示時
                        //

                        bm = new Bitmap(1, 1);
                        //ダミーのGraphicsオブジェクトを取得
                        Graphics dammy_g = Graphics.FromImage(bm);

                        infoSizeF = dammy_g.MeasureString(infodisplay.ToString_FileName(), infodisplay.Font);
                        // すぐ、Graphicsを廃棄。
                        dammy_g.Dispose();
                        // 横幅を 4px 大きく取る。
                        infoSizeF.Width += 4;
                    }
                    else
                    {
                        infoSizeF = new SizeF();
                    }



                    // 新規画像サイズ。
                    int w;
                    int h;
                    if (infodisplay.MemorySprite.IsCrop)
                    {
                        w = (int)infodisplay.MemorySprite.WidthcellResult;
                        h = (int)infodisplay.MemorySprite.HeightcellResult;
                    }
                    else
                    {
                        w = infodisplay.MemorySprite.Bitmap.Width;
                        h = infodisplay.MemorySprite.Bitmap.Height;
                    }

                    if (pcchkInfo.Checked)
                    {
                        // 横幅の最低値
                        int minW = (int)infoSizeF.Width;
                        if (w < minW)
                        {
                            w = minW;
                        }
                    }

                    // 横幅の上限（画像の横幅、または画像の横幅が300未満の場合、300）
                    {
                        int maxW;
                        if (300 <= infodisplay.MemorySprite.Bitmap.Width)
                        {
                            maxW = infodisplay.MemorySprite.Bitmap.Width;
                        }
                        else
                        {
                            maxW = 300;
                        }

                        if (maxW < w)
                        {
                            w = maxW;
                        }
                    }

                    // 縦幅
                    if (pcchkInfo.Checked)
                    {
                        h += infoHeight;
                    }

                    bm = new Bitmap(w, h);
                }

                //imgのGraphicsオブジェクトを取得
                Graphics g = Graphics.FromImage(bm);

                try
                {
                    // 背景色（自動の場合は、塗りつぶさない）
                    if (uc_FrameMemo.BackColor != SystemColors.Control)
                    {
                        g.FillRectangle(new SolidBrush(uc_FrameMemo.BackColor), 0, 0, bm.Width, bm.Height);
                    }

                    float baseX = 0;
                    float baseY = 0;
                    if (pcchkInfo.Checked)
                    {
                        baseY += infoHeight;
                    }

                    uc_FrameMemo.PaintSprite(
                        g,
                        false,
                        baseX,
                        baseY,
                        1.0F
                        );//等倍
                }
                finally
                {
                    g.Dispose();
                }
            }
            else
            {
                bm = null;
            }


            return bm;
        }

        /// <summary>
        /// 切抜きフレームの描画。
        /// </summary>
        /// <param name="g"></param>
        /// <param name="isOnWindow"></param>
        /// <param name="memorySprite"></param>
        /// <param name="xBase">ベースX</param>
        /// <param name="yBase">ベースY</param>
        /// <param name="scale"></param>
        /// <param name="imgOpaque"></param>
        /// <param name="isImageGrid"></param>
        /// <param name="isInfodisplayVisible"></param>
        /// <param name="infodisplay"></param>
        public static void DrawCrop(
            Graphics g,
            bool isOnWindow,
            MemorySpriteImpl memorySprite,
            float xBase,
            float yBase,
            float scale,
            float imgOpaque,
            bool isImageGrid,
            bool isInfodisplayVisible,
            Usercontrolview_Infodisplay infodisplay
            )
        {
            // ビットマップ画像の不透明度を指定します。
            System.Drawing.Imaging.ImageAttributes ia;
            {
                System.Drawing.Imaging.ColorMatrix cm =
                    new System.Drawing.Imaging.ColorMatrix();
                cm.Matrix00 = 1;
                cm.Matrix11 = 1;
                cm.Matrix22 = 1;
                cm.Matrix33 = imgOpaque;//α値。0～1か？
                cm.Matrix44 = 1;

                //ImageAttributesオブジェクトの作成
                ia = new System.Drawing.Imaging.ImageAttributes();
                //ColorMatrixを設定する
                ia.SetColorMatrix(cm);
            }
            float dstX = 0;
            float dstY = 0;
            if (isOnWindow)
            {
                dstX += memorySprite.Lefttop.X;
                dstY += memorySprite.Lefttop.Y;
            }


            // 表示する画像の横幅、縦幅。
            float viWidth = (float)memorySprite.Bitmap.Width / memorySprite.CountcolumnResult;
            float viHeight = (float)memorySprite.Bitmap.Height / memorySprite.CountrowResult;

            // 横幅、縦幅の上限。
            if (memorySprite.WidthcellResult < viWidth)
            {
                viWidth = memorySprite.WidthcellResult;
            }

            if (memorySprite.HeightcellResult < viHeight)
            {
                viHeight = memorySprite.HeightcellResult;
            }



            // 枠を考慮しない画像サイズ
            Rectangle dstR = new Rectangle(
                (int)(dstX + xBase),
                (int)(dstY + yBase),
                (int)viWidth,
                (int)viHeight
                );
            Rectangle dstRScaled = new Rectangle(
                (int)(dstX + xBase),
                (int)(dstY + yBase),
                (int)(scale * viWidth),
                (int)(scale * viHeight)
                );

            // 太さ2pxの枠が収まるサイズ（Border Rectangle）
            int borderWidth = 2;
            Rectangle dstBr = new Rectangle(
                (int)dstX + borderWidth,
                (int)dstY + borderWidth,
                (int)viWidth - 2 * borderWidth,
                (int)viHeight - 2 * borderWidth);
            Rectangle dstBrScaled = new Rectangle(
                (int)dstX + borderWidth,
                (int)dstY + borderWidth,
                (int)(scale * viWidth) - 2 * borderWidth,
                (int)(scale * viHeight) - 2 * borderWidth);

            // 切り抜く位置。
            PointF srcL = memorySprite.GetCropXy();

            float gridX = memorySprite.GridLefttop.X;
            float gridY = memorySprite.GridLefttop.Y;



            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;//ドット絵のまま拡縮するように。しかし、この指定だと半ピクセル左上にずれるバグ。
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;//半ピクセル左上にずれるバグに対応。
            g.DrawImage(
                memorySprite.Bitmap,
                dstRScaled,
                srcL.X,
                srcL.Y,
                viWidth,
                viHeight,
                GraphicsUnit.Pixel,
                ia
                );

            // 枠線
            if (isImageGrid)
            {
                //
                // 枠線：影
                //
                // X,Yを、1ドット右下にずらします。
                dstRScaled.Offset(1, 1);
                // 最初の状態だと、右辺、下辺が外に1px出ています。
                // X,Yをずらした分と合わせて、縦幅、横幅を2ドット狭くします。
                dstRScaled.Width -= 2;
                dstRScaled.Height -= 2;
                g.DrawRectangle(Pens.Black, dstRScaled);
                //
                //
                dstRScaled.Offset(-1, -1);// 元の位置に戻します。
                dstRScaled.Width += 2;// 元のサイズに戻します。
                dstRScaled.Height += 2;

                //
                // 格子線は引かない。
                //

                // 枠線：緑
                // 最初から1ドット出ている分と、X,Yをずらした分と合わせて、
                // 縦幅、横幅を2ドット狭くします。
                dstRScaled.Width -= 2;
                dstRScaled.Height -= 2;
                g.DrawRectangle(Pens.Green, dstRScaled);
            }

            //━━━━━
            // 情報欄の描画
            //━━━━━
            if (isInfodisplayVisible)
            {
                int dy;
                if (isOnWindow)
                {
                    dy = 200;
                }
                else
                {
                    dy = 4;// 16;
                }
                infodisplay.Paint(g, isOnWindow, dy, scale);
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// 全体図の描画。
        /// </summary>
        /// <param name="g"></param>
        /// <param name="isOnWindow"></param>
        /// <param name="memorySprite"></param>
        /// <param name="xBase">ベースX</param>
        /// <param name="yBase">ベースY</param>
        /// <param name="scale"></param>
        /// <param name="imgOpaque"></param>
        /// <param name="isImageGrid"></param>
        /// <param name="isVisible_Infodisplay"></param>
        /// <param name="infoDisplay"></param>
        public static void DrawWhole(
            Graphics g,
            bool isOnWindow,
            MemorySpriteImpl memorySprite,
            float xBase,
            float yBase,
            float scale,
            float imgOpaque,
            bool isImageGrid,
            bool isVisible_Infodisplay,
            PartnumberconfigImpl partnumberconf,
            Usercontrolview_Infodisplay infoDisplay
            )
        {
            // ビットマップ画像の不透明度を指定します。
            System.Drawing.Imaging.ImageAttributes ia;
            {
                System.Drawing.Imaging.ColorMatrix cm =
                    new System.Drawing.Imaging.ColorMatrix();
                cm.Matrix00 = 1;
                cm.Matrix11 = 1;
                cm.Matrix22 = 1;
                cm.Matrix33 = imgOpaque;//α値。0～1か？
                cm.Matrix44 = 1;

                //ImageAttributesオブジェクトの作成
                ia = new System.Drawing.Imaging.ImageAttributes();
                //ColorMatrixを設定する
                ia.SetColorMatrix(cm);
            }
            float leftSprite = 0;
            float topSprite = 0;
            if (isOnWindow)
            {
                leftSprite += memorySprite.Lefttop.X;
                topSprite += memorySprite.Lefttop.Y;
            }

            //
            // 表示画像の長方形（Image rectangle）
            RectangleF dstIrScaled = new RectangleF(
                leftSprite + xBase,
                topSprite + yBase,
                scale * (float)memorySprite.Bitmap.Width,
                scale * (float)memorySprite.Bitmap.Height
                );
            // グリッド枠の長方形（Grid frame rectangle）
            RectangleF dstGrScaled;
            {
                float col = memorySprite.CountcolumnResult;
                float row = memorySprite.CountrowResult;
                if (col < 1)
                {
                    col = 1;
                }

                if (row < 1)
                {
                    row = 1;
                }

                float cw = memorySprite.WidthcellResult;
                float ch = memorySprite.HeightcellResult;

                //グリッドのベース
                dstGrScaled = new RectangleF(
                                scale * memorySprite.GridLefttop.X + leftSprite + xBase,
                                scale * memorySprite.GridLefttop.Y + topSprite + yBase,
                                scale * col * cw,
                                scale * row * ch
                                );
            }

            // 太さ2pxの枠が収まるサイズ
            float borderWidth = 2.0f;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;//ドット絵のまま拡縮するように。しかし、この指定だと半ピクセル左上にずれるバグ。
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;//半ピクセル左上にずれるバグに対応。

            //
            // 画像描画
            g.DrawImage(
                memorySprite.Bitmap,
                new Rectangle((int)dstIrScaled.X, (int)dstIrScaled.Y, (int)dstIrScaled.Width, (int)dstIrScaled.Height),
                0,
                0,
                memorySprite.Bitmap.Width,
                memorySprite.Bitmap.Height,
                GraphicsUnit.Pixel,
                ia
                );

            // 枠線
            if (isImageGrid)
            {

                //
                // 枠線：影
                //
                // オフセット 0、0　だと、上辺、左辺の緑線、黒線が保存画像から見切れます。
                // オフセット 1、1　だと、上辺、左辺の緑線が保存画像から見切れます。
                // オフセット 2、2　だと、上辺、左辺の緑線、黒線が保存画像に入ります。
                //
                // X,Yを、2ドット右下にずらします。
                dstGrScaled.Offset(2, 2);
                // X,Yの起点をずらした分だけ、縦幅、横幅を小さくします。
                dstGrScaled.Width -= 2;
                dstGrScaled.Height -= 2;
                g.DrawRectangle(Pens.Black, dstGrScaled.X, dstGrScaled.Y, dstGrScaled.Width, dstGrScaled.Height);
                //
                //
                dstGrScaled.Offset(-1, -1);// 元の位置に戻します。
                dstGrScaled.Width += 2;// 元のサイズに戻します。
                dstGrScaled.Height += 2;


                // 格子：横線
                {
                    float h2 = infoDisplay.MemorySprite.HeightcellResult * scale;

                    for (int row = 1; row < infoDisplay.MemorySprite.CountrowResult; row++)
                    {
                        g.DrawLine(infoDisplay.GridPen,//Pens.Black,
                            dstGrScaled.X + borderWidth,
                            (float)row * h2 + dstGrScaled.Y,
                            dstGrScaled.Width + dstGrScaled.X - borderWidth - 1,
                            (float)row * h2 + dstGrScaled.Y
                            );
                    }
                }

                // 格子：影:縦線
                {
                    float w2 = infoDisplay.MemorySprite.WidthcellResult * scale;

                    for (int column = 1; column < infoDisplay.MemorySprite.CountcolumnResult; column++)
                    {
                        g.DrawLine(infoDisplay.GridPen,//Pens.Black,
                            (float)column * w2 + dstGrScaled.X,
                            dstGrScaled.Y + borderWidth - 1,//上辺の枠と隙間を空けないように-1で調整。
                            (float)column * w2 + dstGrScaled.X,
                            dstGrScaled.Height + dstGrScaled.Y - borderWidth - 1
                            );
                    }
                }



                //
                // 枠線：緑
                //
                // 上辺、左辺の 0、0　と、
                // 右辺、下辺の -2、 -2 に線を引きます。
                //
                // 右辺、下辺が 0、0　だと画像外、
                // 右辺、下辺が -1、-1　だと影線の位置になります。
                dstGrScaled.Width -= 2;
                dstGrScaled.Height -= 2;
                g.DrawRectangle(Pens.Green, dstGrScaled.X, dstGrScaled.Y, dstGrScaled.Width, dstGrScaled.Height);
            }


            // 部品番号の描画
            if (partnumberconf.Visibled)
            {
                //
                // 数字は桁が多くなると横幅が伸びます。「0」「32」「105」
                // 特例で１桁は２桁扱いとして、「横幅÷桁数」が目安です。
                //


                // 最終部品番号
                int numberLast = (int)(infoDisplay.MemorySprite.CountrowResult * infoDisplay.MemorySprite.CountcolumnResult - 1) + partnumberconf.FirstIndex;
                // 最終部品番号の桁数
                int digit = numberLast.ToString().Length;
                if (1 == digit)
                {
                    digit = 2;//特例で１桁は２桁扱いとします。
                }
                float fontPtScaled = scale * memorySprite.WidthcellResult / digit;

                //partnumberconf.Font = new Font("MS ゴシック", fontPt);
                partnumberconf.Font = new Font("メイリオ", fontPtScaled);


                for (int row = 0; row < infoDisplay.MemorySprite.CountrowResult; row++)
                {
                    for (int column = 0; column < infoDisplay.MemorySprite.CountcolumnResult; column++)
                    {
                        int number = (int)(row * infoDisplay.MemorySprite.CountcolumnResult + column) + partnumberconf.FirstIndex;
                        string text = number.ToString();
                        SizeF stringSizeScaled = g.MeasureString(text, partnumberconf.Font);

                        g.DrawString(text, partnumberconf.Font, partnumberconf.Brush,
                            new PointF(
                                scale * (column * memorySprite.WidthcellResult + memorySprite.WidthcellResult / 2) - stringSizeScaled.Width / 2 + dstGrScaled.X,
                                scale * (row * memorySprite.HeightcellResult + memorySprite.HeightcellResult / 2) - stringSizeScaled.Height / 2 + dstGrScaled.Y
                                ));
                    }
                }
            }


            //━━━━━
            // 情報欄の描画
            //━━━━━
            if (isVisible_Infodisplay)
            {
                int dy;
                if (isOnWindow)
                {
                    dy = 200;
                }
                else
                {
                    dy = 4;// 16;
                }
                infoDisplay.Paint(g, isOnWindow, dy, scale);
            }
        }

        ImageHelper()
        {
        }
    }
}
