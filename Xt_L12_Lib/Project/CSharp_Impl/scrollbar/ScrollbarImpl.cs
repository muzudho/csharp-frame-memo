using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Windows.Forms;

namespace Xenon.Lib
{


    /// <summary>
    /// スクロールバー
    /// </summary>
    public class ScrollbarImpl
    {



        #region 用意
        //────────────────────────────────────────

        /// <summary>
        /// スクロールバーの矢印のフォント。
        /// </summary>
        public static readonly Font ARROW_FONT = new Font("MS UI Gothic", 10.0f, FontStyle.Regular);

        //────────────────────────────────────────
        #endregion



        #region 生成と破棄
        //────────────────────────────────────────

        public ScrollbarImpl()
        {
            this.arrowboxLength_ = SystemInformation.VerticalScrollBarArrowHeight;//スクロールバーの[▲][▼]部分。
            this.bold_ = SystemInformation.VerticalScrollBarWidth;

            this.BorderBold = 2.0f;
            this.Pen1 = new Pen(Color.Gold, this.BorderBold);

            this.UpBtnBounds = new Rectangle();
            this.DownBtnBounds = new Rectangle();
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        public void OnMouseMove(ref bool isRefreshCoordinate, ref bool isRefresh, ScrollpaneImpl scrollpane, object sender, MouseEventArgs e, UserControl uc)
        {
            if (!this.IsHorizontal)
            {
                //━━━━━
                //垂直スクロールバー
                //━━━━━

                if (uc.Capture)
                {
                    //System.Console.WriteLine("○ツリーボックスは画面外のマウスをキャプチャーしています。（" + e.Location.X + "、" + e.Location.Y+"）");
                    //━━━━━
                    //画面外へマウスドラッグしていれば
                    //━━━━━

                    int pixel = 0;

                    bool isOutScreen = false;
                    if (e.Location.Y < uc.Bounds.Y)
                    {
                        //━━━━━
                        //画面上の外
                        //━━━━━
                        //ystem.Console.WriteLine("○↑ツリーボックスは画面上の外に逃げたマウスをキャプチャーしています。（" + e.Location.X + "、" + e.Location.Y+"）");
                        pixel = (int)((double)scrollpane.ViewBounds.Y - (double)(uc.Bounds.Y - e.Location.Y) / 10.0d);
                        isOutScreen = true;
                    }
                    else if (uc.Bounds.Y + uc.Bounds.Height < e.Location.Y)
                    {
                        //━━━━━
                        //画面下の外
                        //━━━━━
                        //ystem.Console.WriteLine("○↓ツリーボックスは画面下の外に逃げたマウスをキャプチャーしています。（" + e.Location.X + "、" + e.Location.Y + "）");
                        pixel = scrollpane.ViewBounds.Y + (int)(((double)e.Location.Y - (double)(uc.Bounds.Y + uc.Bounds.Height)) / 10.0d);
                        isOutScreen = true;
                    }
                    else
                    {
                    }

                    if (isOutScreen)
                    {
                        if (pixel < 0)
                        {
                            // 上端制限
                            pixel = 0;
                        }
                        else if (scrollpane.AppearDataSize.Height < pixel + uc.ClientSize.Height - scrollpane.HScrollbar.Bold)
                        {
                            // 下端制限
                            pixel = scrollpane.AppearDataSize.Height - uc.ClientSize.Height + scrollpane.HScrollbar.Bold;
                        }

                        if (scrollpane.ViewBounds.Y != pixel)
                        {
                            scrollpane.SetViewY(pixel);

                            isRefreshCoordinate = true;
                            isRefresh = true;
                        }
                    }
                }


                if (0 < this.PressedPosInSbox)
                {
                    //━━━━━
                    // 垂直スクロールボックスのスライダーボックスを動かしている場合。
                    //━━━━━
                    //ystem.Console.WriteLine("スライダーボックスを動かしました。");

                    //
                    //
                    // VisibleBoundsのY座標を設定してください。
                    // ──────────
                    // ここでスライダーボックスの位置を設定しても、
                    // RefreshDataで上書きされます。
                    //
                    //

                    //スライダーボックスの上端の座標
                    double boxTop = (double)(e.Location.Y - this.PressedPosInSbox);

                    //スライダーボックスの移動量
                    double movedHeight = boxTop - (double)this.ToMovableBounds().Y;

                    //スライダーボックスの上端が、何割の位置にあるか。
                    double boxTopPer = movedHeight / (double)this.ToMovableBounds().Height;

                    // スライダーボックスの上端が、データ上、何ピクセルの位置にあたるか。
                    int pixel = (int)((double)scrollpane.AppearDataSize.Height * boxTopPer);

                    if (pixel < 0)
                    {
                        // 上端制限
                        pixel = 0;
                    }
                    else if (scrollpane.AppearDataSize.Height < pixel + uc.ClientSize.Height - scrollpane.HScrollbar.Bold)
                    {
                        // 下端制限
                        pixel = scrollpane.AppearDataSize.Height - uc.ClientSize.Height + scrollpane.HScrollbar.Bold;
                    }

                    scrollpane.SetViewY(pixel);
                }
            }
            else
            {
                //━━━━━
                //水平スクロールバー
                //━━━━━

                if (uc.Capture)
                {
                    //System.Console.WriteLine("○ツリーボックスは画面外のマウスをキャプチャーしています。（" + e.Location.X + "、" + e.Location.Y+"）");
                    //━━━━━
                    //画面外へマウスドラッグしていれば
                    //━━━━━

                    int pixel = 0;

                    bool isOutScreen = false;
                    if (e.Location.X < uc.Bounds.X)
                    {
                        //━━━━━
                        //画面上の外
                        //━━━━━
                        //ystem.Console.WriteLine("○↑ツリーボックスは画面上の外に逃げたマウスをキャプチャーしています。（" + e.Location.X + "、" + e.Location.Y+"）");
                        pixel = (int)((double)scrollpane.ViewBounds.X - (double)(uc.Bounds.X - e.Location.X) / 10.0d);
                        isOutScreen = true;
                    }
                    else if (uc.Bounds.X + uc.Bounds.Width < e.Location.X)
                    {
                        //━━━━━
                        //画面下の外
                        //━━━━━
                        //ystem.Console.WriteLine("○↓ツリーボックスは画面下の外に逃げたマウスをキャプチャーしています。（" + e.Location.X + "、" + e.Location.Y + "）");
                        pixel = scrollpane.ViewBounds.X + (int)(((double)e.Location.X - (double)(uc.Bounds.X + uc.Bounds.Width)) / 10.0d);
                        isOutScreen = true;
                    }
                    else
                    {
                    }

                    if (isOutScreen)
                    {
                        if (pixel < 0)
                        {
                            // 上端制限
                            pixel = 0;
                        }
                        else if (scrollpane.AppearDataSize.Width < pixel + uc.ClientSize.Width - scrollpane.VScrollbar.Bold)
                        {
                            // 下端制限
                            pixel = scrollpane.AppearDataSize.Width - uc.ClientSize.Width + scrollpane.VScrollbar.Bold;
                        }

                        if (scrollpane.ViewBounds.X != pixel)
                        {
                            scrollpane.SetViewX(pixel);

                            isRefreshCoordinate = true;
                            isRefresh = true;
                        }
                    }
                }


                if (0 < this.PressedPosInSbox)
                {
                    //━━━━━
                    // 水平スクロールボックスのスライダーボックスを動かしている場合。
                    //━━━━━

                    //
                    //
                    // VisibleBoundsのX座標を設定してください。
                    // ──────────
                    // ここでスライダーボックスの位置を設定しても、
                    // RefreshDataで上書きされます。
                    //
                    //

                    //スライダーボックスの左端の座標
                    double boxLeft = (double)(e.Location.X - this.PressedPosInSbox);

                    //スライダーボックスの移動量
                    double movedWidth = boxLeft - (double)this.ToMovableBounds().X;

                    //スライダーボックスの左端が、何割の位置にあるか。
                    double boxLeftPer = movedWidth / (double)this.ToMovableBounds().Width;

                    // スライダーボックスの左端が、データ上、何ピクセルの位置にあたるか。
                    int pixel = (int)((double)scrollpane.AppearDataSize.Width * boxLeftPer);

                    if (pixel < 0)
                    {
                        // 左端制限
                        pixel = 0;
                    }
                    else if (scrollpane.AppearDataSize.Width < pixel + uc.ClientSize.Width - scrollpane.VScrollbar.Bold)
                    {
                        // 右端制限
                        pixel = scrollpane.AppearDataSize.Width - uc.ClientSize.Width + scrollpane.VScrollbar.Bold;
                    }

                    scrollpane.SetViewX(pixel);
                }
            }
        }

        //────────────────────────────────────────

        public void OnMouseDown(ref bool isRefreshData, ref bool isRefresh, out bool isHit, ScrollpaneImpl scrollpane, object sender, MouseEventArgs e, UserControl uc)
        {
            isHit = false;

            if (!this.IsHorizontal)
            {
                //━━━━━
                //垂直スクロールバー
                //━━━━━
                if (this.ToMovableBounds().Contains(e.Location))
                {
                    isHit = true;

                    if (this.ToSliderboxBounds().Contains(e.Location))
                    {
                        //━━━━━
                        // スライダーボックス
                        //━━━━━

                        // カメラ・ボックスの中での、押したマウス座標。
                        // この数字をマウス座標から引くと、カメラ・ボックスの上端位置が分かります。
                        this.PressedPosInSbox = e.Location.Y - this.SliderboxLocation.Y;

                        //ystem.Console.WriteLine("スライダーボックスを押しました。 this.MouseYInCamera=[" + this.VScrollbar.PressedPosInSbox + "]");
                    }
                    else
                    {
                        //━━━━━
                        // スライダーボックスでないところをクリックした場合
                        //━━━━━
                        this.PressedPosInSbox = 0;

                        if (e.Location.Y < this.SliderboxLocation.Y)
                        {
                            //ystem.Console.WriteLine("移動可能領域（上）を押しました。１ページ分上にスクロールしたい。　this.MouseYInCamera=[" + this.VScrollbar.PressedPosInSbox + "]");

                            //
                            //
                            // スライダーボックスの縦幅分、スライダーボックスを上に移動します。
                            //
                            // スライダーボックスの座標ではなく、setViewY() で確定します。
                            //
                            //

                            int dstY = this.SliderboxLocation.Y - this.SliderboxLength;
                            if (dstY < this.MoveableY)
                            {
                                dstY = this.MoveableY;
                            }

                            // 移動可能範囲の先端からの移動量。
                            int movedHeight = dstY - this.MoveableY;

                            // スライダーボックスの上辺が、移動可能範囲全体の何割か。
                            double boxTopPer = (double)movedHeight / (double)this.MovableLength;

                            // データの高さの内、指定割合は何ピクセルか。
                            int pixel = (int)((double)scrollpane.AppearDataSize.Height * boxTopPer);

                            if (scrollpane.ViewBounds.Y != pixel)
                            {
                                scrollpane.SetViewY(pixel);
                                isRefreshData = true;
                                isRefresh = true;
                            }
                        }
                        else
                        {
                            //ystem.Console.WriteLine("移動可能領域（下）を押しました。１ページ分下にスクロールしたい。 this.MouseYInCamera=[" + this.VScrollbar.PressedPosInSbox + "]");

                            //
                            //
                            // スライダーボックスの縦幅分、スライダーボックスを下に移動します。
                            //
                            // スライダーボックスの座標ではなく、setViewY() で確定します。
                            //
                            //

                            int dstY = this.SliderboxLocation.Y + this.SliderboxLength;
                            if (this.MoveableY + this.MovableLength < dstY + this.SliderboxLength)
                            {
                                dstY = this.MoveableY + this.MovableLength - this.SliderboxLength;
                            }

                            // 移動可能範囲の先端からの移動量。
                            int movedHeight = dstY - this.MoveableY;

                            // スライダーボックスの上辺が、移動可能範囲全体の何割か。
                            double boxTopPer = (double)movedHeight / (double)this.MovableLength;

                            // データの高さの内、指定割合は何ピクセルか。
                            int pixel = (int)((double)scrollpane.AppearDataSize.Height * boxTopPer);

                            if (scrollpane.ViewBounds.Y != pixel)
                            {
                                scrollpane.SetViewY(pixel);
                                isRefreshData = true;
                                isRefresh = true;
                            }
                        }
                    }
                }
                else
                {
                    this.PressedPosInSbox = 0;
                }
            }
            else
            {
                //━━━━━
                //水平スクロールバー
                //━━━━━
                if (this.ToMovableBounds().Contains(e.Location))
                {
                    isHit = true;

                    if (this.ToSliderboxBounds().Contains(e.Location))
                    {
                        //━━━━━
                        // スライダーボックス
                        //━━━━━

                        // スライダーボックスの中での、押したマウス座標。
                        // この数字をマウス座標から引くと、スライダーボックスの左端位置が分かります。
                        this.PressedPosInSbox = e.Location.X - this.SliderboxLocation.X;

                        //ystem.Console.WriteLine("水平スライダーボックスを押しました。 this.HScrollbar.PressedPosInSbox=[" + this.HScrollbar.PressedPosInSbox + "]");
                    }
                    else
                    {
                        //━━━━━
                        // スライダーボックスでないところをクリックした場合
                        //━━━━━
                        this.PressedPosInSbox = 0;

                        if (e.Location.X < this.SliderboxLocation.X)
                        {
                            //ystem.Console.WriteLine("移動可能領域（左）を押しました。スライダーボックス１個分左にスクロールしたい。　this.HScrollbar.PressedPosInSbox=[" + this.HScrollbar.PressedPosInSbox + "]");

                            //
                            //
                            // スライダーボックスの縦幅分、スライダーボックスを上に移動します。
                            //
                            // スライダーボックスの座標ではなく、setViewX() で確定します。
                            //
                            //

                            int dstX = this.SliderboxLocation.X - this.SliderboxLength;
                            if (dstX < this.MoveableX)
                            {
                                dstX = this.MoveableX;
                            }

                            // 移動可能範囲の左端からの移動量。
                            int movedWidth = dstX - this.MoveableX;

                            // スライダーボックスの左端が、移動可能範囲全体の何割か。
                            double boxLeftPer = (double)movedWidth / (double)this.MovableLength;

                            // データの高さの内、指定割合は何ピクセルか。
                            int pixel = (int)((double)scrollpane.AppearDataSize.Width * boxLeftPer);

                            if (scrollpane.ViewBounds.X != pixel)
                            {
                                scrollpane.SetViewX(pixel);
                                isRefreshData = true;
                                isRefresh = true;
                            }
                        }
                        else
                        {
                            //ystem.Console.WriteLine("移動可能領域（右）を押しました。スライダーボックス１個分右にスクロールしたい。 this.HScrollbar.PressedPosInSbox=[" + this.HScrollbar.PressedPosInSbox + "]");

                            //
                            //
                            // スライダーボックスの縦幅分、スライダーボックスを下に移動します。
                            //
                            // スライダーボックスの座標ではなく、setViewY() で確定します。
                            //
                            //

                            int dstX = this.SliderboxLocation.X + this.SliderboxLength;
                            //ystem.Console.WriteLine("dstX（" + dstX + "）＝this.HScrollbar.SliderboxLocation.X（" + this.HScrollbar.SliderboxLocation.X + "）＋this.HScrollbar.SliderboxLength（" + this.HScrollbar.SliderboxLength + "）");
                            //ystem.Console.WriteLine("this.HScrollbar.MoveableX（" + this.HScrollbar.MoveableX + "）＋this.HScrollbar.MovableLength（" + this.HScrollbar.MovableLength + "）＜dstX（" + dstX + "）");
                            if (this.MoveableX + this.MovableLength < dstX + this.SliderboxLength)
                            {
                                //ystem.Console.WriteLine("dstX（" + dstX + "）＝this.HScrollbar.MoveableX（" + this.HScrollbar.MoveableX + "）＋this.HScrollbar.MovableLength（" + this.HScrollbar.MovableLength + "）");
                                dstX = this.MoveableX + this.MovableLength - this.SliderboxLength;
                            }

                            // 移動可能範囲の先端からの移動量。
                            int movedWidth = dstX - this.MoveableX;
                            //ystem.Console.WriteLine("movedWidth（" + movedWidth + "）＝dstX（" + dstX + "）－this.HScrollbar.MoveableX（" + this.MoveableX + "）");

                            // スライダーボックスの左辺が、移動可能範囲全体の何割か。
                            double boxLeftPer = (double)movedWidth / (double)this.MovableLength;
                            //ystem.Console.WriteLine("boxLeftPer（" + boxLeftPer + "）＝movedWidth（" + movedWidth + "）／this.HScrollbar.MovableLength（" + this.MovableLength + "）");

                            // データの高さの内、指定割合は何ピクセルか。
                            int pixel = (int)((double)scrollpane.AppearDataSize.Width * boxLeftPer);
                            //ystem.Console.WriteLine("pixel（" + pixel + "）＝this.AppearDataSize.Width（" + scrollpane.AppearDataSize.Width + "）×boxLeftPer（" + boxLeftPer + "）");

                            if (scrollpane.ViewBounds.X != pixel)
                            {
                                scrollpane.SetViewX(pixel);
                                isRefreshData = true;
                                isRefresh = true;
                            }
                        }
                    }
                }
                else
                {
                    this.PressedPosInSbox = 0;
                }
            }
        }

        //────────────────────────────────────────

        public void OnPaint(object sender, PaintEventArgs e, UserControl uc)
        {
            Graphics g = e.Graphics;


            //━━━━━
            // バーの背景塗りつぶし
            //━━━━━
            if (this.IsHorizontal)
            {
                g.FillRectangle(
                    SystemBrushes.Control,
                    this.UpBtnBounds.X,
                    this.UpBtnBounds.Y,
                    this.MovableLength + 2 * this.ArrowboxLength,
                    this.Bold
                    );
            }
            else
            {
                g.FillRectangle(
                    SystemBrushes.Control,
                    this.UpBtnBounds.X,
                    this.UpBtnBounds.Y,
                    this.Bold,
                    this.MovableLength + 2 * this.ArrowboxLength
                    );
            }


            //━━━━━
            // バーのスライダーボックスの面積の塗りつぶし
            //━━━━━
            //g.FillRectangle(Brushes.LightYellow, this.ToSliderboxBounds());
            g.FillRectangle(SystemBrushes.ScrollBar, this.ToSliderboxBounds());

            //{
            //    Brush b = Brushes.Black;
            //    int y = 10;
            //    int x1 = 0;
            //    int x2 = 40;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.ActiveBorder, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.ActiveCaption, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.ActiveCaptionText, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.AppWorkspace, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.ButtonFace, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.ButtonHighlight, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.ButtonShadow, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.Control, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.ControlDark, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.ControlDarkDark, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.ControlLight, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.ControlLightLight, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.ControlText, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.Desktop, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.GradientActiveCaption, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.GradientInactiveCaption, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.GrayText, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.Highlight, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.HighlightText, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.HotTrack, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.InactiveBorder, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.InactiveCaption, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.InactiveCaptionText, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.Info, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.InfoText, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.Menu, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.MenuBar, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.MenuHighlight, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.MenuText, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.ScrollBar, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.Window, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.WindowFrame, x2, y, 10, 10);
            //    y += 10;
            //    g.DrawString("y=[" + y + "]", uc.Font, b, new Point(x1, y)); g.FillRectangle(SystemBrushes.WindowText, x2, y, 10, 10);
            //}


            //━━━━━
            // スクロールバーの[▲]の描画
            //━━━━━
            {
                g.DrawRectangle(
                    SystemPens.Control,
                    this.UpBtnBounds);

                string s;
                int paddingTop = 0;
                if (this.IsHorizontal)
                {
                    s = "←";
                    paddingTop = 2;
                }
                else
                {
                    s = "▲";
                    paddingTop = 2;
                }

                g.DrawString(s, ScrollbarImpl.ARROW_FONT,
                    SystemBrushes.WindowFrame,
                    new Point(
                        this.UpBtnBounds.Location.X,
                        this.UpBtnBounds.Location.Y + paddingTop
                        )
                    );
            }


            //━━━━━
            // スクロールバーの[▼]の描画
            //━━━━━
            {
                g.DrawRectangle(
                    SystemPens.Control,
                    this.DownBtnBounds);

                string s;
                int paddingTop = 0;
                if (this.IsHorizontal)
                {
                    s = "→";
                    paddingTop = 2;
                }
                else
                {
                    s = "▼";
                }

                g.DrawString(s, ScrollbarImpl.ARROW_FONT,
                    SystemBrushes.WindowFrame,
                    new Point(
                        this.DownBtnBounds.Location.X,
                        this.DownBtnBounds.Location.Y + paddingTop
                        )
                        );
            }


            //━━━━━
            // スクロールバーの枠の描画
            //━━━━━
            {
                g.DrawRectangle(
                    SystemPens.Control,
                    //this.Pen1,
                    this.ToMovableBounds());
            }


            ////━━━━━
            //// デバッグ用罫線
            ////━━━━━
            //{
            //    int h = 500;

            //    for (int x = 100; x < 1000; x+=100 )
            //    {
            //        g.DrawLine(Pens.Red, x, 0, x, h);
            //    }
            //}
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private bool isHorizontal_;

        /// <summary>
        /// 水平スクロールバーなら真。
        /// </summary>
        public bool IsHorizontal
        {
            get
            {
                return isHorizontal_;
            }
            set
            {
                isHorizontal_ = value;
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// スクロールバーの座標。
        /// </summary>
        /// <returns></returns>
        public Point ToLocation()
        {
            // 上矢印の座標＝スクロールバーの座標。
            Point result = new Point(
                this.UpBtnBounds.X,
                this.UpBtnBounds.Y
                );
            return result;
        }


        /// <summary>
        /// クライアント領域の横幅、縦幅を指定することで、
        /// スクロールバーの座標とサイズを設定。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetClientareaSize(Size size)
        {
            if (this.IsHorizontal)
            {
                //━━━━━
                //水平スクロールバー
                //━━━━━
                int x = 0;
                int y = size.Height - this.Bold;

                //[▲]ボタン。
                this.UpBtnBounds = new Rectangle(
                    x,
                    y,
                    this.Bold, this.ArrowboxLength);

                //━━━━━
                // スクロールバーが移動できる領域
                //━━━━━
                {
                    this.moveableX_ = this.ArrowboxLength;
                    this.moveableY_ = y;

                    //// スライダーボックスが移動できる矩形部分のサイズ。
                    //this.bold_ = this.Bold;
                    //this.movableLength_ = this.MovableLength;
                }

                //[▼]ボタン。
                this.DownBtnBounds = new Rectangle(
                        this.ArrowboxLength + this.MovableLength + (int)(this.BorderBold / 2.0f),
                        y,
                        this.Bold,
                        this.ArrowboxLength - (int)this.BorderBold
                        );

                this.movableLength_ = size.Width - 3 * this.ArrowboxLength;
            }
            else
            {
                //━━━━━
                //垂直スクロールバー
                //━━━━━
                int x = size.Width - (int)(this.BorderBold / 2) - this.Bold;
                int y = 0;

                //[▲]ボタン。
                this.UpBtnBounds = new Rectangle(
                    x,
                    y,
                    this.Bold, this.ArrowboxLength);

                //━━━━━
                // スクロールバーが移動できる領域
                //━━━━━
                {
                    this.moveableX_ = x;
                    this.moveableY_ = this.ArrowboxLength;

                    //// スライダーボックスが移動できる矩形部分のサイズ。
                    //this.bold_ = this.Bold;
                    //this.movableLength_ = this.MovableLength;
                }

                //[▼]ボタン。
                this.DownBtnBounds = new Rectangle(
                        x,
                        this.ArrowboxLength + this.MovableLength + (int)(this.BorderBold / 2.0f),
                        this.Bold,
                        this.ArrowboxLength - (int)this.BorderBold
                        );

                this.movableLength_ = size.Height - 3 * this.ArrowboxLength;
            }
        }

        //────────────────────────────────────────

        private int moveableX_;

        /// <summary>
        /// スライダーバーの移動可能範囲のx。
        /// </summary>
        public int MoveableX
        {
            get
            {
                return moveableX_;
            }
        }

        private int moveableY_;

        /// <summary>
        /// スライダーバーの移動可能範囲のy。
        /// </summary>
        public int MoveableY
        {
            get
            {
                return moveableY_;
            }
        }

        private int bold_;
        /// <summary>
        /// スクロールバーの矩形の厚み。
        /// 垂直スクロールバーの場合、横幅。
        /// </summary>
        public int Bold
        {
            get
            {
                return bold_;
            }
        }


        private int movableLength_;
        /// <summary>
        /// スライダーボックスが移動できる部分の矩形の長さ。
        /// 垂直スクロールバーの場合、縦幅。
        /// </summary>
        public int MovableLength
        {
            get
            {
                return movableLength_;
            }
        }

        /// <summary>
        /// スライダーボックスが移動できる矩形部分の座標とサイズ。
        /// </summary>
        /// <returns></returns>
        public Rectangle ToMovableBounds()
        {
            Rectangle result;

            if (this.IsHorizontal)
            {
                //━━━━━
                //水平スクロールバー
                //━━━━━
                result = new Rectangle(
                    this.moveableX_,
                    this.moveableY_,
                    this.movableLength_,
                    this.bold_
                );
            }
            else
            {
                //━━━━━
                //垂直スクロールバー
                //━━━━━
                result = new Rectangle(
                    this.moveableX_,
                    this.moveableY_,
                    this.bold_,
                    this.movableLength_
                );
            }

            return result;
        }

        //────────────────────────────────────────

        private float borderBold_;

        /// <summary>
        /// 境界線の太さ。
        /// </summary>
        public float BorderBold
        {
            get
            {
                return borderBold_;
            }
            set
            {
                borderBold_ = value;
            }
        }

        //────────────────────────────────────────

        private Pen pen1_;

        /// <summary>
        /// 枠線を引くペンです。
        /// </summary>
        public Pen Pen1
        {
            get
            {
                return pen1_;
            }
            set
            {
                pen1_ = value;
            }
        }

        //────────────────────────────────────────

        private Point sliderboxLocation_;
        private int sliderboxLength_;

        /// <summary>
        /// スライダーボックスの座標。
        /// </summary>
        public Point SliderboxLocation
        {
            get
            {
                return sliderboxLocation_;
            }
        }

        /// <summary>
        /// スライダーボックスの矩形の長さ。
        /// </summary>
        public int SliderboxLength
        {
            get
            {
                return sliderboxLength_;
            }
        }

        /// <summary>
        /// スライダーボックスの座標とサイズ。
        /// </summary>
        /// <returns></returns>
        public Rectangle ToSliderboxBounds()
        {
            Rectangle result;

            if (this.IsHorizontal)
            {
                //━━━━━
                //水平スクロールバー
                //━━━━━
                result = new Rectangle(
                    this.SliderboxLocation.X,
                    this.SliderboxLocation.Y,
                    this.SliderboxLength,
                    this.Bold-1 //1px小さくします。
                    );
            }
            else
            {
                //━━━━━
                //垂直スクロールバー
                //━━━━━
                result = new Rectangle(
                    this.SliderboxLocation.X,
                    this.SliderboxLocation.Y,
                    this.Bold-1, //1px小さくします。
                    this.SliderboxLength
                    );
            }

            return result;
        }

        //────────────────────────────────────────

        private Rectangle upBtnBounds_;

        /// <summary>
        /// [▲]ボタン。
        /// </summary>
        public Rectangle UpBtnBounds
        {
            get
            {
                return upBtnBounds_;
            }
            set
            {
                upBtnBounds_ = value;
            }
        }

        //────────────────────────────────────────

        private Rectangle downBtnBounds_;

        /// <summary>
        /// [▼]ボタン。
        /// </summary>
        public Rectangle DownBtnBounds
        {
            get
            {
                return downBtnBounds_;
            }
            set
            {
                downBtnBounds_ = value;
            }
        }

        //────────────────────────────────────────

        private int arrowboxLength_;

        /// <summary>
        /// スクロールバーの[▲][▼]１個分の長さ。
        /// 垂直スクロールバーなら縦幅。
        /// </summary>
        public int ArrowboxLength
        {
            get
            {
                return arrowboxLength_;
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// スライダーボックスの座標とサイズを、表示端座標、表示領域サイズ、データの長さを指定して設定します。
        /// </summary>
        /// <param name="viewHead">表示端座標</param>
        /// <param name="viewLength">表示領域サイズ</param>
        /// <param name="dataLength">データの長さ</param>
        public void SetSliderboxBy(double viewHead, double viewLength, double dataLength)
        {
            double sizePer;
            double topPer;


            // 撮影部が、データ全体の割合の縮図になっているか
            if (0 == dataLength)
            {
                // 0除算エラーになる場合、とりあえず。
                topPer = 0.0d;
                sizePer = 0.0d;
            }
            else
            {
                topPer = viewHead / dataLength;
                //ystem.Console.WriteLine("枠　topPer（" + topPer + "）＝viewHead（" + viewHead + "）／dataLength（" + dataLength + "）");

                sizePer = viewLength / dataLength;
            }

            //━━━━━
            // スライダーボックス
            //━━━━━
            {
                if (this.IsHorizontal)
                {
                    //━━━━━
                    //水平スクロールバー
                    //━━━━━
                    Point p = this.ToLocation();
                    this.sliderboxLocation_ = new Point(
                            (int)((double)this.MovableLength * topPer) + this.ArrowboxLength,
                            p.Y
                        );
                    this.sliderboxLength_ = (int)((double)this.MovableLength * sizePer);

                    //ystem.Console.WriteLine("水平スクロールバー　sizePer（" + sizePer + "）＝viewLength（" + viewLength + "）／dataLength（" + dataLength + "）");
                    //ystem.Console.WriteLine("水平スクロールバー　this.MovableLength（" + this.MovableLength + "）");
                    //ystem.Console.WriteLine("水平スクロールバー　スライダーボックス　座標（" + p2.X + "、" + p2.Y + "）長さ（" + len + "）");
                }
                else
                {
                    //━━━━━
                    //垂直スクロールバー
                    //━━━━━
                    Point p = this.ToLocation();
                    this.sliderboxLocation_ = new Point(
                            p.X,
                            (int)((double)this.MovableLength * topPer) + this.ArrowboxLength
                        );
                    this.sliderboxLength_ = (int)((double)this.MovableLength * sizePer);
                }

            }
        }

        //────────────────────────────────────────

        private int pressedPosInSbox_;

        /// <summary>
        /// スライダーボックスの上でマウスを押した時。
        /// </summary>
        public int PressedPosInSbox
        {
            get
            {
                return pressedPosInSbox_;
            }
            set
            {
                pressedPosInSbox_ = value;
            }
        }

        //────────────────────────────────────────
        #endregion



    }


}
