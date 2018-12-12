using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

namespace Xenon.FrameMemo
{


    /// <summary>
    /// 部品番号の設定。
    /// </summary>
    public class PartnumberconfigImpl
    {



        #region 生成と破棄
        //────────────────────────────────────────

        public PartnumberconfigImpl()
        {
            this.Font = new Font("MS ゴシック", 12);
            this.SetBrushByColor(Color.Black);
            this.Visibled = true;
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        public void SetBrushByColor(Color color)
        {
            this.color = color;
            this.brush = new SolidBrush(color);
            this.alpha = color.A;
        }

        public void SetBrushByAlpha(byte alpha)
        {
            this.color = Color.FromArgb(alpha, this.color.R, this.color.G, this.color.B);
            this.brush = new SolidBrush(this.color);
            this.alpha = alpha;
        }

        //────────────────────────────────────────
        #endregion




        #region プロパティー
        //────────────────────────────────────────

        private int firstIndex;

        public int FirstIndex
        {
            get
            {
                return this.firstIndex;
            }
            set
            {
                this.firstIndex = value;
            }
        }

        //────────────────────────────────────────

        private Color color;

        //────────────────────────────────────────

        /// <summary>
        /// 不透明度。0～255。
        /// </summary>
        private byte alpha;

        /// <summary>
        /// 不透明度。0～255。
        /// </summary>
        public byte Alpha
        {
            get
            {
                return this.alpha;
            }
        }

        //────────────────────────────────────────

        private Brush brush;

        public Brush Brush
        {
            get
            {
                return this.brush;
            }
        }

        //────────────────────────────────────────

        private Font font;

        public Font Font
        {
            get
            {
                return this.font;
            }
            set
            {
                this.font = value;
            }
        }

        //────────────────────────────────────────

        private bool visibled;

        public bool Visibled
        {
            get
            {
                return this.visibled;
            }
            set
            {
                this.visibled = value;
            }
        }

        //────────────────────────────────────────
        #endregion





    }



}
