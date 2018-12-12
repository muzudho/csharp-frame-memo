using System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using Grayscale.FrameMemo.Commons;


namespace Grayscale.FrameMemo.Actions.SavingImage
{
    /// <summary>
    /// [画像を保存]ボタンを押したときの内容。
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

                Bitmap bm = ImageHelper.CreateSaveImage(
                    input.InfoDisplay,
                    input.InfoCheckBox,
                    context.UcCanvas
                    );



                // ファイル名を適当に作成。
                StringBuilder s = new StringBuilder();
                {
                    s.Append(Application.StartupPath);
                    s.Append("\\ScreenShot\\");

                    DateTime now = System.DateTime.Now;
                    s.Append(now.Year);
                    s.Append("_");
                    s.Append(now.Month);
                    s.Append("_");
                    s.Append(now.Day);
                    s.Append("_");
                    s.Append(now.Hour);
                    s.Append("_");
                    s.Append(now.Minute);
                    s.Append("_");
                    s.Append(now.Second);
                    s.Append("_");
                    s.Append(now.Millisecond);
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

        /// <summary>
        /// 
        /// </summary>
        Action()
        {
        }
    }
}
