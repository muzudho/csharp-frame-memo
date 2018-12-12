using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Xenon.Lib;

namespace Xenon.FrameMemo
{
    public partial class UcCanvas : UserControl, Usercontrolview
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public UcCanvas()
        {
            InitializeComponent();

            //部品番号
            {
                this.Partnumberconfig = new PartnumberconfigImpl();
                this.Partnumberconfig.FirstIndex = 0;

                this.Partnumberconfig.SetBrushByColor( Color.FromArgb(192, 0, 255, 0));//75%透明の緑。
            }

            //添付情報
            {
                this.infodisplay = new Usercontrolview_Infodisplay();
            }

            MemorySpriteImpl moSprite = new MemorySpriteImpl();
            moSprite.List_Usercontrolview.Add(this.infodisplay);
            moSprite.List_Usercontrolview.Add(this);
            this.MemorySprite = moSprite;
            this.infodisplay.MemorySprite = moSprite;


            this.enumMousedragmode = EnumMousedragmode.None;

            this.IsAutoinputMouseDragLst = true;
            this.mouseDragLst.Items.Add("なし");
            this.mouseDragLst.Items.Add("画像移動");
            this.mouseDragLst.SelectedIndex = 0;
            this.IsAutoinputMouseDragLst = false;

            this.pcddlAlScale.Items.Add("x0.25");
            this.pcddlAlScale.Items.Add("x0.5");
            this.pcddlAlScale.Items.Add("x  1");//初期選択
            this.pcddlAlScale.Items.Add("x  2");
            this.pcddlAlScale.Items.Add("x  4");
            this.pcddlAlScale.Items.Add("x  8");
            this.pcddlAlScale.Items.Add("x 16");
            this.pcddlAlScale.SelectedIndex = 2;

            this.pcddlBgclr.Items.Add("自動");//初期選択
            this.pcddlBgclr.Items.Add("白");
            this.pcddlBgclr.Items.Add("灰色");
            this.pcddlBgclr.Items.Add("黒");
            this.pcddlBgclr.Items.Add("赤");
            this.pcddlBgclr.Items.Add("黄");
            this.pcddlBgclr.Items.Add("緑");
            this.pcddlBgclr.Items.Add("青");
            this.pcddlBgclr.SelectedIndex = 0;

            this.pcddlOpaque.Items.Add("100");//初期選択
            this.pcddlOpaque.Items.Add(" 75");
            this.pcddlOpaque.Items.Add(" 50");
            this.pcddlOpaque.Items.Add(" 25");
            this.pcddlOpaque.SelectedIndex = 0;
            this.imgOpaque = 1.0F;

            this.pcchkGridVisibled.Checked = true;

            // 格子枠の色
            this.pcddlGridColor.Items.Add("自動");
            this.pcddlGridColor.Items.Add("白");
            this.pcddlGridColor.Items.Add("灰色");
            this.pcddlGridColor.Items.Add("黒");
            this.pcddlGridColor.Items.Add("赤");
            this.pcddlGridColor.Items.Add("黄");
            this.pcddlGridColor.Items.Add("緑");//初期選択
            this.pcddlGridColor.Items.Add("青");
            this.pcddlGridColor.SelectedIndex = 6;

            this.scale = 1;
            this.preScale = 1;

            //部品番号の色
            this.pcddlPartnumberColor.Items.Add("自動");
            this.pcddlPartnumberColor.Items.Add("白");
            this.pcddlPartnumberColor.Items.Add("灰色");
            this.pcddlPartnumberColor.Items.Add("黒");
            this.pcddlPartnumberColor.Items.Add("赤");
            this.pcddlPartnumberColor.Items.Add("黄");
            this.pcddlPartnumberColor.Items.Add("緑");//初期選択
            this.pcddlPartnumberColor.Items.Add("青");
            this.pcddlPartnumberColor.SelectedIndex = 6;

            //部品番号の半透明度
            this.pcddlPartnumberOpaque.Items.Add("100");
            this.pcddlPartnumberOpaque.Items.Add(" 75");//初期選択
            this.pcddlPartnumberOpaque.Items.Add(" 50");
            this.pcddlPartnumberOpaque.Items.Add(" 25");
            this.pcddlPartnumberOpaque.SelectedIndex = 1;

            //部品番号の開始インデックス
            this.pctxtPartnumberFirst.Text = "0";
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        public void OnChanged_CountcolumnResult(float nValue)
        {
            this.ColumnResultLbl.Text = nValue.ToString();
        }

        //────────────────────────────────────────

        public void OnChanged_CountrowResult(float nValue)
        {
            this.RowResultLbl.Text = nValue.ToString();
        }

        //────────────────────────────────────────

        public void OnChanged_WidthcellResult(float nValue)
        {
            this.CellWidthResultLbl.Text = nValue.ToString();
        }

        //────────────────────────────────────────

        public void OnChanged_HeightcellResult(float nValue)
        {
            this.CellHeightResultLbl.Text = nValue.ToString();
        }

        //────────────────────────────────────────

        public void OnChanged_CropForce(int nValue)
        {
            this.CropResultLbl.Text = nValue.ToString();
        }

        //────────────────────────────────────────

        public void OnChanged_CropLastResult(int nValue)
        {
            this.CropLastResultLbl.Text = nValue.ToString();
        }

        //────────────────────────────────────────

        /// <summary>
        /// スプライト画像ファイルが開かれたとき。
        /// </summary>
        public void OnImageOpened()
        {
            // 列数／行数
            this.ColumnRowLbl.Enabled = true;
            this.ColumnForceTxt.Enabled = true;
            this.RowForceTxt.Enabled = true;

            // 1個幅ヨコ／タテ
            this.CellSizeLbl.Enabled = true;
            this.CellWidthForceTxt.Enabled = true;
            this.CellHeightForceTxt.Enabled = true;

            // 切抜きフレーム
            this.CropLbl.Enabled = true;
            this.CropLastResultLbl.Enabled = true;
            this.CropForceTxt.Enabled = true;

            // ベースX／Y
            this.GridXyLbl.Enabled = true;
            this.GridXTxt.Enabled = true;
            this.GridYTxt.Enabled = true;
        }

        //────────────────────────────────────────

        /// <summary>
        /// スプライト画像ファイルが無くなったとき。
        /// </summary>
        public void OnImageClosed()
        {
            // 列数／行数
            this.ColumnRowLbl.Enabled = false;
            this.ColumnForceTxt.Enabled = false;
            this.RowForceTxt.Enabled = false;

            // 1個幅ヨコ／タテ
            this.CellSizeLbl.Enabled = false;
            this.CellWidthForceTxt.Enabled = false;
            this.CellHeightForceTxt.Enabled = false;

            // 切抜きフレーム
            this.CropLbl.Enabled = false;
            this.CropLastResultLbl.Enabled = false;
            this.CropForceTxt.Enabled = false;

            // グリッドX／Y
            this.GridXyLbl.Enabled = false;
            this.GridXTxt.Enabled = false;
            this.GridYTxt.Enabled = false;
        }

        //────────────────────────────────────────

        /// <summary>
        /// カーソルキーで1px動かしたいときなどに。
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public void MoveActiveSprite(float dx, float dy)
        {
            if (this.enumMousedragmode == EnumMousedragmode.Image_Move)
            {
                //
                // 画像移動
                //
                this.MoveImg(dx, dy);
            }

        }

        //────────────────────────────────────────

        private void MoveImg(float dx, float dy)
        {
            // 背景画像移動
            this.infodisplay.MemorySprite.Lefttop = new PointF(
                this.infodisplay.MemorySprite.Lefttop.X + dx,
                this.infodisplay.MemorySprite.Lefttop.Y + dy
                );

            // 再描画
            this.Refresh();
        }

        //────────────────────────────────────────

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bOnWindow"></param>
        /// <param name="baseX"></param>
        /// <param name="baseY"></param>
        /// <param name="scale2"></param>
        public void PaintSprite(
            Graphics g,
            bool bOnWindow,
            float baseX,
            float baseY,
            float scale2
            )
        {
            if (null != this.infodisplay.MemorySprite.Bitmap)
            {
                if (this.infodisplay.MemorySprite.IsCrop)
                {
                    // 切抜き

                    Actions.DrawCrop(
                        g,
                        bOnWindow,
                        this.infodisplay.MemorySprite,
                        baseX,
                        baseY,
                        scale2,
                        this.imgOpaque,
                        this.isImageGrid,
                        this.pcchkInfoVisibled.Checked,
                        this.infodisplay
                        );
                }
                else
                {
                    // 全体図

                    Actions.DrawWhole(
                        g,
                        bOnWindow,
                        this.infodisplay.MemorySprite,
                        baseX,
                        baseY,
                        scale2,
                        this.imgOpaque,
                        this.isImageGrid,
                        this.pcchkInfoVisibled.Checked,
                        this.Partnumberconfig,
                        this.Infodisplay
                        );
                }
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// １段階、拡大します。
        /// </summary>
        public void ZoomUp()
        {
            ComboBox listBox = this.pcddlAlScale;

            if (listBox.SelectedIndex + 1 < listBox.Items.Count)
            {
                listBox.SelectedIndex++;
                this.Refresh();
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// １段階、縮小します。
        /// </summary>
        public void ZoomDown()
        {
            ComboBox listBox = this.pcddlAlScale;

            if (0 <= listBox.SelectedIndex - 1)
            {
                listBox.SelectedIndex--;
                this.Refresh();
            }
        }

        //────────────────────────────────────────
        #endregion



        #region イベントハンドラー
        //────────────────────────────────────────

        private void pcbtnBg_Click(object sender, EventArgs e)
        {
            this.pcdlgOpenBgFile.InitialDirectory = Application.StartupPath;
            DialogResult result = this.pcdlgOpenBgFile.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                // 絶対ファイルパス
                string sFpatha = this.pcdlgOpenBgFile.FileName;

                // 画像ファイルが開かれたものとして、ビットマップにする。
                try
                {
                    this.pcbtnSaveImg.Enabled = true;
                    this.pcbtnSaveImgFrames.Enabled = true;
                    this.pclblOpaque.Enabled = true;
                    this.pcddlOpaque.Enabled = true;
                    this.pclblBgclr.Enabled = true;
                    this.pcddlBgclr.Enabled = true;
                    this.pclblAlScale.Enabled = true;
                    this.pcddlAlScale.Enabled = true;
                    this.pclblMouseDrag.Enabled = true;

                    this.IsAutoinputMouseDragLst = true;
                    this.mouseDragLst.Enabled = true;
                    this.mouseDragLst.SelectedIndex = 1;//「画像移動」を選択
                    this.IsAutoinputMouseDragLst = false;

                    //枠線
                    this.pclblGrid1.Enabled = true;
                    this.pcchkGridVisibled.Enabled = true;
                    this.pcddlGridColor.Enabled = true;

                    this.pclblInfo1.Enabled = true;
                    this.pcchkInfoVisibled.Enabled = true;

                    //部品番号
                    this.pclblPartnumber1.Enabled = true;
                    this.pcddlPartnumberOpaque.Enabled = true;
                    this.pclblPartnumber2.Enabled = true;
                    this.pcchkPartnumberVisible.Enabled = true;
                    this.pcddlPartnumberColor.Enabled = true;
                    this.pclblPartnumber3.Enabled = true;
                    this.pctxtPartnumberFirst.Enabled = true;


                    this.infodisplay.Filepath = sFpatha;

                    this.OnImageOpened();

                    this.infodisplay.MemorySprite.IsAutoinputting = true;//自動入力開始
                    // 画像設定
                    this.infodisplay.MemorySprite.Bitmap = new Bitmap(sFpatha);

                    this.infodisplay.MemorySprite.Lefttop = new Point(//.Lt
                        this.Width / 2 - this.infodisplay.MemorySprite.Bitmap.Width / 2,
                        this.Height / 2 - this.infodisplay.MemorySprite.Bitmap.Height / 2
                        );

                    // フォームを再描画。
                    this.Refresh();
                    this.infodisplay.MemorySprite.IsAutoinputting = false;//自動入力終了
                }
                catch (ArgumentException)
                {
                    // 指定したファイルが画像じゃなかった。

                    this.pcbtnSaveImg.Enabled = false;
                    this.pcbtnSaveImgFrames.Enabled = false;
                    this.pclblOpaque.Enabled = false;
                    this.pcddlOpaque.Enabled = false;
                    this.pclblBgclr.Enabled = false;
                    this.pcddlBgclr.Enabled = false;
                    this.pclblAlScale.Enabled = false;
                    this.pcddlAlScale.Enabled = false;
                    this.pclblMouseDrag.Enabled = false;
                    this.mouseDragLst.Enabled = false;
                    this.mouseDragLst.SelectedIndex = 0;//「なし」を選択

                    //枠線
                    this.pclblGrid1.Enabled = false;
                    this.pcchkGridVisibled.Enabled = false;
                    this.pcddlGridColor.Enabled = false;

                    //部品番号
                    this.pclblPartnumber1.Enabled = false;
                    this.pcddlPartnumberOpaque.Enabled = false;
                    this.pclblPartnumber2.Enabled = false;
                    this.pcchkPartnumberVisible.Enabled = false;
                    this.pcddlPartnumberColor.Enabled = false;
                    this.pclblPartnumber3.Enabled = false;
                    this.pctxtPartnumberFirst.Enabled = false;

                    this.OnImageClosed();

                    this.infodisplay.MemorySprite.IsAutoinputting = true;//自動入力開始
                    this.infodisplay.MemorySprite.Bitmap = null;

                    // フォームを再描画。
                    this.Refresh();
                    this.infodisplay.MemorySprite.IsAutoinputting = false;//自動入力終了
                }

                //━━━━━
                // フェーズ進行
                //━━━━━
                if (0<this.Phase && this.Phase <= 1)
                {
                    this.Phase = 2;
                    this.Refresh();
                }
            }
            else if (result == DialogResult.Cancel)
            {
                //変更なし
            }
            else
            {
                // ？
                this.pcbtnSaveImg.Enabled = false;
                this.pcbtnSaveImgFrames.Enabled = false;
            }
        }

        private void pcddlScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ドロップダウンリスト
            ComboBox pcddl = (ComboBox)sender;

            if (0 <= pcddl.SelectedIndex)
            {
                string sSelectedValue = (string)pcddl.Items[pcddl.SelectedIndex];

                if ("x0.25" == sSelectedValue)
                {
                    this.preScale = this.scale;
                    this.scale = 0.25f;
                }
                else if ("x0.5" == sSelectedValue)
                {
                    this.preScale = this.scale;
                    this.scale = 0.5f;
                }
                else if ("x  1" == sSelectedValue)
                {
                    this.preScale = this.scale;
                    this.scale = 1;
                }
                else if ("x  2" == sSelectedValue)
                {
                    this.preScale = this.scale;
                    this.scale = 2;
                }
                else if ("x  4" == sSelectedValue)
                {
                    this.preScale = this.scale;
                    this.scale = 4;
                }
                else if ("x  8" == sSelectedValue)
                {
                    this.preScale = this.scale;
                    this.scale = 8;
                }
                else if ("x 16" == sSelectedValue)
                {
                    this.preScale = this.scale;
                    this.scale = 16;
                }
                else
                {
                    this.preScale = this.scale;
                    this.scale = 1;
                }
            }
            else
            {
                // 未選択

                this.preScale = this.scale;
                this.scale = 1;
            }


            // 現在見えている画面上の中心を固定するようにズーム。
            if (null != this.infodisplay.MemorySprite.Bitmap)
            {

                //
                // 位置調整 

                float multiple = this.scale / this.preScale; //何倍になったか。

                // 画面の中心に位置する、ズームされた画像上の位置（固定点）
                float imgFixX = (this.Width / 2.0f) - this.infodisplay.MemorySprite.Lefttop.X;
                float imgFixY = (this.Height / 2.0f) - this.infodisplay.MemorySprite.Lefttop.Y;

                // 背景位置
                this.infodisplay.MemorySprite.Lefttop = new PointF(//.Lt
                    this.infodisplay.MemorySprite.Lefttop.X - (imgFixX * multiple - imgFixX),
                    this.infodisplay.MemorySprite.Lefttop.Y - (imgFixY * multiple - imgFixY)
                    );
            }


            //━━━━━
            // フェーズ進行
            //━━━━━
            if (0 < this.Phase && this.Phase <= 2)
            {
                this.Phase = 3;
            }

            // 再描画
            this.Refresh();
        }

        private void UcCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            this.mouseDraggingNone = true;
            this.mouseDragging = true;
            this.mouseDownLocation = e.Location;

            this.preDragLocation = e.Location;

            // フォーカスをコントロールから外すことで、フォーカスをフォームに戻します。
            this.ActiveControl = null;
        }

        //────────────────────────────────────────

        private void UcCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.enumMousedragmode == EnumMousedragmode.Image_Move)
            {
                //
                // 画像移動
                //

                if (this.mouseDragging)
                {
                    // 前回ドラッグした位置との差分
                    float dx;
                    float dy;
                    if (this.mouseDraggingNone)
                    {
                        dx = 0;
                        dy = 0;
                        this.mouseDraggingNone = false;
                    }
                    else
                    {
                        dx = e.Location.X - this.preDragLocation.X;
                        dy = e.Location.Y - this.preDragLocation.Y;
                    }

                    this.MoveImg(dx, dy);

                    // ドラッグした位置
                    this.preDragLocation = e.Location;
                }
            }
        }

        private void UcCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            this.mouseDragging = false;
        }

        private void UcCanvas_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            Pen pen1 = new Pen(Brushes.Gold, 3.0f);
            Font font1 = new Font("MS UI Gothic", 16.0f, FontStyle.Bold);

            //━━━━━
            //（１）
            //━━━━━
            {
                Rectangle r = new Rectangle(10,10,80,80);

                if(this.Phase==1)
                {
                    g.FillRectangle(Brushes.SkyBlue, r);
                }

                g.DrawRectangle(pen1,r);
                g.DrawString("1", font1, Brushes.Gold, new Point(r.X + 5, r.Y + 5));
            }

            //━━━━━
            //（２）
            //━━━━━
            {
                Rectangle r = new Rectangle(100, 10, 80, 80);

                if (this.Phase == 2)
                {
                    g.FillRectangle(Brushes.SkyBlue, r);
                }

                g.DrawRectangle(pen1, r);
                g.DrawString("2", font1, Brushes.Gold, new Point(r.X + 5, r.Y + 5));
            }

            //━━━━━
            //（３）
            //━━━━━
            {
                Rectangle r = new Rectangle(190, 10, 80, 80);

                if (this.Phase == 3)
                {
                    g.FillRectangle(Brushes.SkyBlue, r);
                }

                g.DrawRectangle(pen1, r);
                g.DrawString("3", font1, Brushes.Gold, new Point(r.X + 5, r.Y + 5));
            }

            //━━━━━
            //（４）
            //━━━━━
            {
                Rectangle r = new Rectangle(280, 10, 80, 80);

                if (this.Phase == 4)
                {
                    g.FillRectangle(Brushes.SkyBlue, r);
                }

                g.DrawRectangle(pen1, r);
                g.DrawString("4", font1, Brushes.Gold, new Point(r.X + 5, r.Y + 5));
            }

            //━━━━━
            //（５）番号
            //━━━━━
            {
                Rectangle r = new Rectangle(370, 10, 170, 80);

                if (this.Phase == 5)
                {
                    g.FillRectangle(Brushes.SkyBlue, r);
                }

                g.DrawRectangle(pen1, r);
                g.DrawString("5", font1, Brushes.Gold, new Point(r.X + 5, r.Y + 5));
            }

            //━━━━━
            //（６）
            //━━━━━
            {
                Rectangle r = new Rectangle(550, 10, 80, 80);

                if (this.Phase == 6)
                {
                    g.FillRectangle(Brushes.SkyBlue, r);
                }

                g.DrawRectangle(pen1, r);
                g.DrawString("6", font1, Brushes.Gold, new Point(r.X + 5, r.Y + 5));
            }

            //━━━━━
            //（７）
            //━━━━━
            {
                Rectangle r = new Rectangle(640, 10, 80, 80);

                if (this.Phase == 7)
                {
                    g.FillRectangle(Brushes.SkyBlue, r);
                }

                g.DrawRectangle(pen1, r);
                g.DrawString("7", font1, Brushes.Gold, new Point(r.X + 5, r.Y + 5));
            }



            //━━━━━
            //（８）
            //━━━━━
            {
                Rectangle r = new Rectangle(10, 100, 170, 80);

                if (this.Phase == 8)
                {
                    g.FillRectangle(Brushes.SkyBlue, r);
                }

                g.DrawRectangle(pen1, r);
                g.DrawString("8", font1, Brushes.Gold, new Point(r.X + 5, r.Y + 5));
            }

            //━━━━━
            //（９）
            //━━━━━
            {
                Rectangle r = new Rectangle(190, 100, 170, 80);

                if (this.Phase == 9)
                {
                    g.FillRectangle(Brushes.SkyBlue, r);
                }

                g.DrawRectangle(pen1, r);
                g.DrawString("9", font1, Brushes.Gold, new Point(r.X + 5, r.Y + 5));
            }

            //━━━━━
            //（１０）
            //━━━━━
            {
                Rectangle r = new Rectangle(370, 100, 80, 80);

                if (this.Phase == 11)
                {
                    g.FillRectangle(Brushes.SkyBlue, r);
                }

                g.DrawRectangle(pen1, r);
                g.DrawString("10", font1, Brushes.Gold, new Point(r.X + 5, r.Y + 5));
            }

            //━━━━━
            //（１１）
            //━━━━━
            {
                Rectangle r = new Rectangle(460, 100, 80, 80);

                if (this.Phase == 11)
                {
                    g.FillRectangle(Brushes.SkyBlue, r);
                }

                g.DrawRectangle(pen1, r);
                g.DrawString("11", font1, Brushes.Gold, new Point(r.X + 5, r.Y + 5));
            }

            //━━━━━
            //（１２）
            //━━━━━
            {
                Rectangle r = new Rectangle(550, 100, 170, 80);

                if (this.Phase == 12)
                {
                    g.FillRectangle(Brushes.SkyBlue, r);
                }

                g.DrawRectangle(pen1, r);
                g.DrawString("12", font1, Brushes.Gold, new Point(r.X + 5, r.Y + 5));
            }



            // 画像
            this.PaintSprite(
                e.Graphics,
                true,
                0,
                0,
                this.scale
                );
        }

        private void pcddlOpaque_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ドロップダウンリスト
            ComboBox pcddl = (ComboBox)sender;

            if (0 <= pcddl.SelectedIndex)
            {
                string sSelectedValue = (string)pcddl.Items[pcddl.SelectedIndex];

                if ("自動" == sSelectedValue)
                {
                    this.BackColor = SystemColors.Control;
                }
                else
                {
                    this.BackColor = new ColorFromName().FromName(sSelectedValue);
                }
            }
            else
            {
                // 未選択

                this.BackColor = SystemColors.Control;
            }


            //━━━━━
            // フェーズ進行
            //━━━━━
            if (0 < this.Phase && this.Phase <= 3)
            {
                this.Phase = 4;
            }

            // 再描画
            this.Refresh();
        }


        
        private void mouseDragLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            // リストボックス
            ListBox pclst = (ListBox)sender;

            if (0 <= pclst.SelectedIndex)
            {
                string sSelectedValue = (string)pclst.Items[pclst.SelectedIndex];

                if ("画像移動" == sSelectedValue)
                {
                    this.enumMousedragmode = EnumMousedragmode.Image_Move;
                }
                else
                {
                    this.enumMousedragmode = EnumMousedragmode.None;
                }
            }
            else
            {
                // 未選択

                this.enumMousedragmode = EnumMousedragmode.None;
            }


            if (!this.IsAutoinputMouseDragLst)
            {
                //━━━━━
                // フェーズ進行
                //━━━━━
                if (0 < this.Phase && this.Phase <= 11)
                {
                    this.Phase = 12;
                    this.Refresh();
                }
            }
        }

        //────────────────────────────────────────

        private void pcddlBorder_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ドロップダウンリスト
            ComboBox pcddl = (ComboBox)sender;

            if (0 <= pcddl.SelectedIndex)
            {
                string sSelectedValue = (string)pcddl.Items[pcddl.SelectedIndex];

                if ("なし" == sSelectedValue)
                {
                    this.isImageGrid = false;
                }
                else if ("あり" == sSelectedValue)
                {
                    this.isImageGrid = true;
                }
                else
                {
                    this.isImageGrid = false;
                }
            }
            else
            {
                // 未選択

                this.isImageGrid = false;
            }

            // 再描画
            this.Refresh();
        }

        private void pcchkImgBorder_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox pcchk = (CheckBox)sender;

            this.isImageGrid = pcchk.Checked;


            //━━━━━
            // フェーズ進行
            //━━━━━
            if (0 < this.Phase && this.Phase <= 6)
            {
                this.Phase = 7;
            }

            // 再描画
            this.Refresh();
        }

        //────────────────────────────────────────

        /// <summary>
        /// 画像を保存。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pcbtnSaveImg_Click(object sender, EventArgs e)
        {
            Actions.Save1(
                this.Infodisplay,
                this.PcchkInfo,
                this
                );
        }

        //────────────────────────────────────────

        /// <summary>
        /// 全フレーム画像保存。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ccButtonEx1_Click(object sender, EventArgs e)
        {
            Actions.Save3(
                this.Infodisplay,
                this.PcchkInfo,
                this
                );
        }

        //────────────────────────────────────────
        #endregion




        #region イベントハンドラー
        //────────────────────────────────────────

        /// <summary>
        /// 枠線の色。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pcddlGridcolor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ドロップダウンリスト
            ComboBox pcddl = (ComboBox)sender;

            Color c;
            if (0 <= pcddl.SelectedIndex)
            {
                string valueSelected = (string)pcddl.Items[pcddl.SelectedIndex];

                if ("自動" == valueSelected)
                {
                    c = SystemColors.Control;
                }
                else
                {
                    c = new ColorFromName().FromName(valueSelected);
                }
            }
            else
            {
                // 未選択

                c = SystemColors.Control;
            }

            this.infodisplay.GridPen = new Pen(c);


            //━━━━━
            // フェーズ進行
            //━━━━━
            if (0 < this.Phase && this.Phase <= 6)
            {
                this.Phase = 7;
            }

            // 再描画
            this.Refresh();
        }

        //────────────────────────────────────────

        /// <summary>
        /// 部品番号の色。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pcddlPartnumberColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ドロップダウンリスト
            ComboBox pcddl = (ComboBox)sender;

            Color clr;
            if (0 <= pcddl.SelectedIndex)
            {
                string valueSelected = (string)pcddl.Items[pcddl.SelectedIndex];

                if ("自動" == valueSelected)
                {
                    clr = SystemColors.Control;
                }
                else
                {
                    clr = new ColorFromName().FromName(valueSelected);
                }
            }
            else
            {
                // 未選択

                clr = SystemColors.Control;
            }

            this.Partnumberconfig.SetBrushByColor( clr);


            //━━━━━
            // フェーズ進行
            //━━━━━
            if (0 < this.Phase && this.Phase <= 5)
            {
                this.Phase = 6;
            }

            // 再描画
            this.Refresh();
        }

        //────────────────────────────────────────

        /// <summary>
        /// 画像の不透明度。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pcddlOpaqueBg_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ドロップダウンリスト
            ComboBox pcddl = (ComboBox)sender;

            if (0 <= pcddl.SelectedIndex)
            {
                string valueSelected = (string)pcddl.Items[pcddl.SelectedIndex];

                if ("100" == valueSelected)
                {
                    this.imgOpaque = 1.0F;
                }
                else if (" 75" == valueSelected)
                {
                    this.imgOpaque = 0.75F;
                }
                else if (" 50" == valueSelected)
                {
                    this.imgOpaque = 0.50F;
                }
                else if (" 25" == valueSelected)
                {
                    this.imgOpaque = 0.25F;
                }
                else
                {
                    this.imgOpaque = 1.0F;
                }
            }
            else
            {
                // 未選択

                this.imgOpaque = 1.0F;
            }


            //━━━━━
            // フェーズ進行
            //━━━━━
            if (0 < this.Phase && this.Phase <= 4)
            {
                this.Phase = 5;
            }

            // 再描画
            this.Refresh();
        }

        //────────────────────────────────────────

        /// <summary>
        /// 部品番号の不透明度。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pcddlPartnumberOpaque_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ドロップダウンリスト
            ComboBox pcddl = (ComboBox)sender;

            if (0 <= pcddl.SelectedIndex)
            {
                string valueSelected = (string)pcddl.Items[pcddl.SelectedIndex];

                if ("100" == valueSelected)
                {
                    this.Partnumberconfig.SetBrushByAlpha( 255);
                }
                else if (" 75" == valueSelected)
                {
                    this.Partnumberconfig.SetBrushByAlpha( 192);
                }
                else if (" 50" == valueSelected)
                {
                    this.Partnumberconfig.SetBrushByAlpha( 128);
                }
                else if (" 25" == valueSelected)
                {
                    this.Partnumberconfig.SetBrushByAlpha( 64);
                }
                else
                {
                    this.Partnumberconfig.SetBrushByAlpha( 255);
                }
            }
            else
            {
                // 未選択

                this.Partnumberconfig.SetBrushByAlpha( 255);
            }


            //━━━━━
            // フェーズ進行
            //━━━━━
            if (0 < this.Phase && this.Phase <= 5)
            {
                this.Phase = 6;
            }

            // 再描画
            this.Refresh();
        }

        //────────────────────────────────────────

        /// <summary>
        /// 添付情報表示チェックボックス。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pcchkInfo_CheckedChanged(object sender, EventArgs e)
        {

            //━━━━━
            // フェーズ進行
            //━━━━━
            if (0 < this.Phase && this.Phase <= 7)
            {
                this.Phase = 8;
            }

            // 再描画。
            this.Refresh();
        }

        //────────────────────────────────────────

        /// <summary>
        /// 部品番号表示チェックボックス。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pcchkPartnumberVisible_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox pcchk = (CheckBox)sender;

            this.Partnumberconfig.Visibled = pcchk.Checked;


            //━━━━━
            // フェーズ進行
            //━━━━━
            if (0 < this.Phase && this.Phase <= 5)
            {
                this.Phase = 6;
            }

            // 再描画。
            this.Refresh();
        }

        //────────────────────────────────────────

        /// <summary>
        /// 部品番号開始インデックス。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pctxtPartnumberFirst_TextChanged(object sender, EventArgs e)
        {
            TextBox pctxt = (TextBox)sender;

            int number;
            if (int.TryParse(pctxt.Text, out number))
            {
                this.Partnumberconfig.FirstIndex = number;
            }
            else
            {
                //エラー
                this.Partnumberconfig.FirstIndex = 0;
            }


            //━━━━━
            // フェーズ進行
            //━━━━━
            if (0 < this.Phase && this.Phase <= 5)
            {
                this.Phase = 6;
            }

            // 再描画。
            this.Refresh();
        }

        //────────────────────────────────────────
        #endregion




        #region プロパティー
        //────────────────────────────────────────

        protected EnumMousedragmode enumMousedragmode;

        //────────────────────────────────────────

        /// <summary>
        /// マウスのドラッグをこれから始める最初なら真。
        /// </summary>
        protected bool mouseDraggingNone;

        //────────────────────────────────────────

        /// <summary>
        /// マウスをドラッグ中なら真。
        /// </summary>
        protected bool mouseDragging;

        //────────────────────────────────────────

        /// <summary>
        /// マウス押下点XY。
        /// </summary>
        protected PointF mouseDownLocation;

        //────────────────────────────────────────

        /// <summary>
        /// 1つ前のドラッグ点XY。
        /// </summary>
        protected PointF preDragLocation;

        //────────────────────────────────────────

        /// <summary>
        /// 拡大率。
        /// </summary>
        protected float scale;

        //────────────────────────────────────────

        /// <summary>
        /// 変更前の拡大率。
        /// </summary>
        protected float preScale;

        //────────────────────────────────────────

        /// <summary>
        /// 画像の不透明度。0.0F～1.0F。
        /// </summary>
        protected float imgOpaque;

        //────────────────────────────────────────

        /// <summary>
        /// 画像のグリッド線の有無。
        /// </summary>
        protected bool isImageGrid;

        //────────────────────────────────────────

        private PartnumberconfigImpl partnumberconfig;

        public PartnumberconfigImpl Partnumberconfig
        {
            get
            {
                return this.partnumberconfig;
            }
            set
            {
                this.partnumberconfig = value;
            }
        }

        //────────────────────────────────────────

        protected Usercontrolview_Infodisplay infodisplay;

        /// <summary>
        /// width,height,ファイル名等表示。
        /// </summary>
        public Usercontrolview_Infodisplay Infodisplay
        {
            get
            {
                return infodisplay;
            }
        }

        //────────────────────────────────────────

        public CheckBox PcchkInfo
        {
            get
            {
                return pcchkInfoVisibled;
            }
        }

        //────────────────────────────────────────

        public TextBox ColumnForceTxt
        {
            get
            {
                return this.columnForceTxt;
            }
        }

        public TextBox RowForceTxt
        {
            get
            {
                return this.rowForceTxt;
            }
        }

        public Label ColumnRowLbl
        {
            get
            {
                return this.columnRowLbl;
            }
        }

        public Label ColumnResultLbl
        {
            get
            {
                return this.columnResultLbl;
            }
        }

        public Label RowResultLbl
        {
            get
            {
                return this.rowResultLbl;
            }
        }

        //────────────────────────────────────────

        public TextBox CellWidthForceTxt
        {
            get
            {
                return this.cellWidthForceTxt;
            }
        }

        public TextBox CellHeightForceTxt
        {
            get
            {
                return this.cellHeightForceTxt;
            }
        }

        public Label CellSizeLbl
        {
            get
            {
                return this.cellSizeLbl;
            }
        }

        public Label CellWidthResultLbl
        {
            get
            {
                return this.cellWidthResultLbl;
            }
        }

        public Label CellHeightResultLbl
        {
            get
            {
                return this.cellHeightResultLbl;
            }
        }

        //────────────────────────────────────────

        public TextBox CropForceTxt
        {
            get
            {
                return this.cropForceTxt;
            }
        }

        public Label CropLbl
        {
            get
            {
                return this.cropLbl;
            }
        }

        public Label CropLastResultLbl
        {
            get
            {
                return this.cropLastResultLbl;
            }
        }

        public Label CropResultLbl
        {
            get
            {
                return this.cropResultLbl;
            }
        }

        //────────────────────────────────────────

        public TextBox GridXTxt
        {
            get
            {
                return this.gridXTxt;
            }
        }

        public TextBox GridYTxt
        {
            get
            {
                return this.gridYTxt;
            }
        }

        public Label GridXyLbl
        {
            get
            {
                return this.gridXyLbl;
            }
        }

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

        private int phase_;

        public int Phase
        {
            get
            {
                return phase_;
            }
            set
            {
                phase_ = value;
            }
        }

        //────────────────────────────────────────

        private bool isAutoinputMouseDragLst_;

        public bool IsAutoinputMouseDragLst
        {
            get
            {
                return this.isAutoinputMouseDragLst_;
            }
            set
            {
                this.isAutoinputMouseDragLst_ = value;
            }
        }

        //────────────────────────────────────────
        #endregion

        /// <summary>
        /// 列数。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void columnForceTxt_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            int nValue;
            int.TryParse(txt.Text, out nValue);


            this.MemorySprite.IsAutoinputting = true;//自動入力開始
            this.MemorySprite.CountcolumnForce = nValue;
            this.MemorySprite.RefreshViews();// 対応ビューの再描画
            this.MemorySprite.IsAutoinputting = false;//自動入力終了


            //━━━━━
            // フェーズ進行
            //━━━━━
            if (0 < this.Phase && this.Phase <= 8)
            {
                if(
                    (""!=this.ColumnForceTxt.Text || ""!=this.cellWidthForceTxt.Text)
                    &&
                    (""!=this.RowForceTxt.Text || ""!=this.cellHeightForceTxt.Text)
                    )
                {
                    this.Phase = 9;
                }

                this.Refresh();
            }
        }

        private void rowForceTxt_TextChanged(object sender, EventArgs e)
        {
            TextBox pctxt = (TextBox)sender;

            int value = 0;
            int.TryParse(pctxt.Text, out value);

            this.MemorySprite.IsAutoinputting = true;//自動入力開始
            this.MemorySprite.CountrowForce = value;
            this.MemorySprite.RefreshViews();// 対応ビューの再描画
            this.MemorySprite.IsAutoinputting = false;//自動入力終了


            //━━━━━
            // フェーズ進行
            //━━━━━
            if (0 < this.Phase && this.Phase <= 8)
            {
                if (
                    ("" != this.ColumnForceTxt.Text || "" != this.cellWidthForceTxt.Text)
                    &&
                    ("" != this.RowForceTxt.Text || "" != this.cellHeightForceTxt.Text)
                    )
                {
                    this.Phase = 9;
                }

                this.Refresh();
            }
        }

        private void cellWidthForceTxt_TextChanged(object sender, EventArgs e)
        {
            TextBox pctxt = (TextBox)sender;

            int value = 0;
            int.TryParse(pctxt.Text, out value);

            this.MemorySprite.IsAutoinputting = true;//自動入力開始

            this.MemorySprite.SizecellForce = new SizeF(value, this.MemorySprite.SizecellForce.Height);

            this.MemorySprite.RefreshViews();// 対応ビューの再描画
            this.MemorySprite.IsAutoinputting = false;//自動入力終了


            //━━━━━
            // フェーズ進行
            //━━━━━
            if (0 < this.Phase && this.Phase <= 8)
            {
                if (
                    ("" != this.ColumnForceTxt.Text || "" != this.cellWidthForceTxt.Text)
                    &&
                    ("" != this.RowForceTxt.Text || "" != this.cellHeightForceTxt.Text)
                    )
                {
                    this.Phase = 9;
                }

                this.Refresh();
            }
        }

        private void cellHeightForceTxt_TextChanged(object sender, EventArgs e)
        {
            TextBox pctxt = (TextBox)sender;

            int value = 0;
            int.TryParse(pctxt.Text, out value);

            this.MemorySprite.IsAutoinputting = true;//自動入力開始

            this.MemorySprite.SizecellForce = new SizeF(this.MemorySprite.SizecellForce.Width, value);

            this.MemorySprite.RefreshViews();// 対応ビューの再描画
            this.MemorySprite.IsAutoinputting = false;//自動入力終了


            //━━━━━
            // フェーズ進行
            //━━━━━
            if (0 < this.Phase && this.Phase <= 8)
            {
                if (
                    ("" != this.ColumnForceTxt.Text || "" != this.cellWidthForceTxt.Text)
                    &&
                    ("" != this.RowForceTxt.Text || "" != this.cellHeightForceTxt.Text)
                    )
                {
                    this.Phase = 9;
                }

                this.Refresh();
            }
        }

        /// <summary>
        /// [切抜きフレーム]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cropForceTxt_TextChanged(object sender, EventArgs e)
        {
            TextBox pctxt = (TextBox)sender;

            string sCropForce = pctxt.Text.Trim();
            int nCropForce;
            if (!int.TryParse(sCropForce, out nCropForce))
            {
                nCropForce = 0;
            }

            this.MemorySprite.IsAutoinputting = true;//自動入力開始
            this.MemorySprite.FrameCropForce = nCropForce;
            this.MemorySprite.RefreshViews();// 対応ビューの再描画
            this.MemorySprite.IsAutoinputting = false;//自動入力終了

            //━━━━━
            // フェーズ進行
            //━━━━━
            if (0 < this.Phase && this.Phase <= 9)
            {
                this.Phase = 10;
                this.Refresh();
            }
        }

        private void gridXTxt_TextChanged(object sender, EventArgs e)
        {
            TextBox pctxt = (TextBox)sender;

            int value = 0;
            int.TryParse(pctxt.Text, out value);

            this.MemorySprite.IsAutoinputting = true;//自動入力開始
            this.MemorySprite.GridLefttop = new PointF(
                value,
                this.MemorySprite.GridLefttop.Y
                );
            this.MemorySprite.RefreshViews();// 対応ビューの再描画
            this.MemorySprite.IsAutoinputting = false;//自動入力終了


            //━━━━━
            // フェーズ進行
            //━━━━━
            if (0 < this.Phase && this.Phase <= 10)
            {
                this.Phase = 11;
                this.Refresh();
            }
        }

        private void gridYTxt_TextChanged(object sender, EventArgs e)
        {
            TextBox pctxt = (TextBox)sender;

            int value = 0;
            int.TryParse(pctxt.Text, out value);

            this.MemorySprite.IsAutoinputting = true;//自動入力開始
            this.MemorySprite.GridLefttop = new PointF(
                this.MemorySprite.GridLefttop.X,
                value
                );
            this.MemorySprite.RefreshViews();// 対応ビューの再描画
            this.MemorySprite.IsAutoinputting = false;//自動入力終了


            //━━━━━
            // フェーズ進行
            //━━━━━
            if (0 < this.Phase && this.Phase <= 10)
            {
                this.Phase = 11;
                this.Refresh();
            }
        }

        private void UcCanvas_Load(object sender, EventArgs e)
        {
            this.Phase = 1;

            this.Focus();

            this.Size = this.ClientSize;
            //this.Refresh();
        }



    }
}
