using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Windows.Forms;

namespace Xenon.Lib
{


    /// <summary>
    /// 垂直スクロールバー、水平スクロールバーのセットです。
    /// </summary>
    public class ScrollpaneImpl
    {


        #region 生成と破棄
        //────────────────────────────────────────

        public ScrollpaneImpl()
        {
            this.VScrollbar = new ScrollbarImpl();
            this.HScrollbar = new ScrollbarImpl();
            this.HScrollbar.IsHorizontal = true;


            // 先頭行に表示されている項目のインデックス
            this.SetViewBounds(new Rectangle());
        }

        //────────────────────────────────────────
        #endregion


        #region アクション
        //────────────────────────────────────────

        public void OnMouseMove(ref bool isRefreshCoordinate, ref bool isRefresh, object sender, MouseEventArgs e, UserControl uc)
        {
            this.VScrollbar.OnMouseMove(ref isRefreshCoordinate, ref isRefresh, this, sender, e, uc);
            this.HScrollbar.OnMouseMove(ref isRefreshCoordinate, ref isRefresh, this, sender, e, uc);
        }

        //────────────────────────────────────────

        public void OnPaint(object sender, PaintEventArgs e, UserControl uc)
        {
            //すみっこの角。
            {
                Graphics g = e.Graphics;
                g.FillRectangle(
                    SystemBrushes.Control,
                    this.HScrollbar.MovableLength + 2*this.HScrollbar.ArrowboxLength,
                    this.VScrollbar.MovableLength + 2*this.VScrollbar.ArrowboxLength,
                    SystemInformation.VerticalScrollBarWidth,
                    SystemInformation.VerticalScrollBarWidth
                    );
            }
            this.VScrollbar.OnPaint(sender, e, uc);
            this.HScrollbar.OnPaint(sender, e, uc);
        }

        /// <summary>
        /// クライアント領域の横幅、縦幅を指定することで、
        /// スクロールバーの座標とサイズを設定。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetClientareaSize(Size size)
        {
            this.VScrollbar.SetClientareaSize(size);
            this.HScrollbar.SetClientareaSize(size);
        }

        /// <summary>
        /// スライダーボックスの座標とサイズを、表示端座標、表示領域サイズ、データの長さを指定して設定します。
        /// </summary>
        /// <param name="viewHead">表示端座標</param>
        /// <param name="viewLength">表示領域サイズ</param>
        /// <param name="dataLength">データの長さ</param>
        public void SetSliderboxBy(Rectangle viewBounds, Size appearDataSize)
        {
            this.appearDataSize_ = appearDataSize;

            this.VScrollbar.SetSliderboxBy(viewBounds.Y, viewBounds.Height, appearDataSize.Height);
            this.HScrollbar.SetSliderboxBy(viewBounds.X, viewBounds.Width, appearDataSize.Width);
        }

        //────────────────────────────────────────
        #endregion


        #region プロパティー
        //────────────────────────────────────────

        private ScrollbarImpl vScrollbar_;

        /// <summary>
        /// 垂直スクロールバー。
        /// </summary>
        public ScrollbarImpl VScrollbar
        {
            get
            {
                return vScrollbar_;
            }
            set
            {
                vScrollbar_ = value;
            }
        }

        //────────────────────────────────────────

        private ScrollbarImpl hScrollbar_;

        /// <summary>
        /// 水平スクロールバー。
        /// </summary>
        public ScrollbarImpl HScrollbar
        {
            get
            {
                return hScrollbar_;
            }
            set
            {
                hScrollbar_ = value;
            }
        }

        //────────────────────────────────────────

        private Rectangle viewBounds_;

        /// <summary>
        /// 見えているデータの座標があるとき、その中で画面に映っている矩形座標。
        /// </summary>
        public Rectangle ViewBounds
        {
            get
            {
                return viewBounds_;
            }
        }
        //
        public bool SetViewX(int x)
        {
            bool isUpdate = false;

            if (this.viewBounds_.X != x)
            {
                isUpdate = true;
                this.viewBounds_.X = x;
            }

            return isUpdate;
        }
        public bool SetViewY(int y)
        {
            bool isUpdate = false;

            if (this.viewBounds_.Y != y)
            {
                isUpdate = true;
                this.viewBounds_.Y = y;
            }

            return isUpdate;
        }
        public void SetViewSize(int width, int height)
        {
            this.viewBounds_.Width = width;
            this.viewBounds_.Height = height;
        }
        //
        public void SetViewBounds(Rectangle bnds)
        {
            this.viewBounds_ = bnds;
        }

        //────────────────────────────────────────

        private Size appearDataSize_;

        /// <summary>
        /// 折りたたまれていない全てのノードの縦幅を足したもの。
        /// </summary>
        public Size AppearDataSize
        {
            get
            {
                return appearDataSize_;
            }
        }

        //────────────────────────────────────────
        #endregion


    }


}
