using System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using Grayscale.FrameMemo.Commons;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using Grayscale.FrameMemo.Commons;


namespace Grayscale.FrameMemo.Actions.SavingAllFrames
{
    /// <summary>
    /// 全フレームの画像を保存。
    /// </summary>
    public sealed class Action
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public static void Perfrom(ContextModel context, InputModel input, OutputModel output)
        {
            if(context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (output == null)
            {
                throw new ArgumentNullException("output");
            }

            if (null != input.InfoDisplay.MemorySprite.Bitmap)
            {

                // 列数と行数。
                int nCols = (int)input.InfoDisplay.MemorySprite.CountcolumnResult;
                int nRows = (int)input.InfoDisplay.MemorySprite.CountrowResult;

                // ファイル名の頭。
                StringBuilder s1 = new StringBuilder();
                {
                    s1.Append(Application.StartupPath);
                    s1.Append("\\ScreenShot\\");

                    DateTime now = System.DateTime.Now;
                    s1.Append(now.Year);
                    s1.Append("_");
                    s1.Append(now.Month);
                    s1.Append("_");
                    s1.Append(now.Day);
                    s1.Append("_");
                    s1.Append(now.Hour);
                    s1.Append("_");
                    s1.Append(now.Minute);
                    s1.Append("_");
                    s1.Append(now.Second);
                    s1.Append("_");
                    s1.Append(now.Millisecond);
                }


                for (int nRow = 1; nRow <= nRows; nRow++)
                {
                    for (int nCol = 1; nCol <= nCols; nCol++)
                    {
                        int nCell = (nRow - 1) * nCols + nCol;
                        System.Console.WriteLine("r" + nRow + " c" + nCol + " nCell" + nCell + "  nRows" + nRows + " nCols" + nCols);


                        context.UcCanvas.CropForceTxt.Text = nCell.ToString();

                        Bitmap bm = ImageHelper.CreateSaveImage(
                            input.InfoDisplay,
                            input.InfoCheckBox,
                            context.UcCanvas
                            );



                        // ファイル名を適当に作成。
                        StringBuilder s = new StringBuilder();
                        {
                            s.Append(s1.ToString());
                            s.Append("_c");
                            s.Append(nCell.ToString());
                            s.Append(".png");
                        }

                        string file = s.ToString();
                        if (!Directory.Exists(Directory.GetParent(file).Name))
                        {
                            // ScreenShot フォルダーがなければ、作ります。
                            Directory.CreateDirectory(Directory.GetParent(file).Name);
                        }

                        bm.Save(file, System.Drawing.Imaging.ImageFormat.Png);

                    }
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        Action()
        {
        }
    }
}
