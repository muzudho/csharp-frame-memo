using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;//Ｂｉｔｍａｐ

namespace Xenon.FrameMemo
{
    public class MemorySpriteImpl
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public MemorySpriteImpl()
        {
            this.frameCropForce = 0;//指定なし＝全体図
            this.frameCropLast = 1;

            this.gridLefttop = new PointF();
            this.lefttop = new PointF();

            this.list_Usercontrolview = new List<Usercontrolview>();
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 切り抜く位置。
        /// </summary>
        /// <returns></returns>
        public PointF GetCropXy()
        {
            float viWidth = (float)this.Bitmap.Width / this.CountcolumnResult;
            float viHeight = (float)this.Bitmap.Height / this.CountrowResult;

            float srcX;
            if (0.0f == this.CountcolumnResult)
            {
                srcX = 0.0f;
            }
            else
            {
                srcX = (this.FrameCropForce - 1) % (int)this.CountcolumnResult * viWidth;
            }

            float srcY;
            if (0.0f == this.CountrowResult)
            {
                srcY = 0.0f;
            }
            else
            {
                srcY = (this.FrameCropForce - 1) / (int)this.CountcolumnResult * viHeight;
            }

            return new PointF(srcX, srcY);
        }

        //────────────────────────────────────────

        public void RefreshViews()
        {
            foreach (Usercontrolview voFrame in this.List_Usercontrolview)
            {
                voFrame.Refresh();
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// 再計算。
        /// </summary>
        private void CalculateSize()
        {
            //
            // 横幅と列数
            //
            if (0 < this.SizecellForce.Width && 0 < this.CountcolumnForce)
            {
                //
                // 強制横幅、強制列数　共に指定されているとき
                //
                this.WidthcellResult = this.SizecellForce.Width;
                this.CountcolumnResult = this.CountcolumnForce;
                if (null == this.Bitmap)
                {
                    this.SizeImage = new SizeF(0.0f, this.SizeImage.Height);
                }
                else
                {
                    this.SizeImage = new SizeF(this.Bitmap.Width, this.SizeImage.Height);
                }
            }
            else if (0 < this.SizecellForce.Width)
            {
                //
                // 強制横幅が指定されているとき
                //
                this.WidthcellResult = this.SizecellForce.Width;
                if (null == this.Bitmap)
                {
                    this.CountcolumnResult = 0.0f;
                    this.SizeImage = new SizeF(0.0f, this.SizeImage.Height);
                }
                else
                {
                    this.CountcolumnResult = (float)this.Bitmap.Width / this.SizecellForce.Width;
                    this.SizeImage = new SizeF(this.Bitmap.Width, this.SizeImage.Height);
                }
            }
            else if (0 < this.CountcolumnForce)
            {
                //
                // 強制列数が指定されているとき
                //
                this.CountcolumnResult = this.CountcolumnForce;
                if (null == this.Bitmap)
                {
                    this.WidthcellResult = 0.0f;
                    this.SizeImage = new SizeF(0.0f, this.SizeImage.Height);
                }
                else
                {
                    this.WidthcellResult = (float)this.Bitmap.Width / this.CountcolumnForce;
                    this.SizeImage = new SizeF(this.Bitmap.Width, this.SizeImage.Height);
                }
            }
            else
            {
                //
                // 強制されていないとき
                //
                this.CountcolumnResult = 1;
                if (null == this.Bitmap)
                {
                    this.WidthcellResult = 0.0f;
                    this.SizeImage = new SizeF(0.0f, this.SizeImage.Height);
                }
                else
                {
                    this.WidthcellResult = (float)this.Bitmap.Width;
                    this.SizeImage = new SizeF(this.Bitmap.Width, this.SizeImage.Height);
                }
            }


            //
            // 縦幅と行数
            //
            if (0 < this.SizecellForce.Height && 0 < this.CountrowForce)
            {
                //
                // 強制縦幅、強制行数　共に指定されているとき
                //
                this.HeightcellResult = this.SizecellForce.Height;
                this.CountrowResult = this.CountrowForce;
                if (null == this.Bitmap)
                {
                    this.SizeImage = new SizeF(this.SizeImage.Width, 0.0f);
                }
                else
                {
                    this.SizeImage = new SizeF(this.SizeImage.Width, this.Bitmap.Height);
                }
            }
            else if (0 < this.SizecellForce.Height)
            {
                //
                // 強制縦幅が指定されているとき
                //
                this.HeightcellResult = this.SizecellForce.Height;
                if (null == this.Bitmap)
                {
                    this.CountrowResult = 0.0f;
                    this.SizeImage = new SizeF(this.SizeImage.Width, 0.0f);
                }
                else
                {
                    this.CountrowResult = (float)this.Bitmap.Height / this.SizecellForce.Height;
                    this.SizeImage = new SizeF(this.SizeImage.Width, this.Bitmap.Height);
                }
            }
            else if (0 < this.CountrowForce)
            {
                //
                // 強制行数が指定されているとき
                //
                this.CountrowResult = this.CountrowForce;
                if (null == this.Bitmap)
                {
                    this.HeightcellResult = 0.0f;
                    this.SizeImage = new SizeF(this.SizeImage.Width, 0.0f);
                }
                else
                {
                    this.HeightcellResult = (float)this.Bitmap.Height / this.CountrowForce;
                    this.SizeImage = new SizeF(this.SizeImage.Width, this.Bitmap.Height);
                }
            }
            else
            {
                //
                // 強制されていないとき
                //
                this.CountrowResult = 1;
                if (null == this.Bitmap)
                {
                    this.HeightcellResult = 0.0f;
                    this.SizeImage = new SizeF(this.SizeImage.Width, 0.0f);
                }
                else
                {
                    this.HeightcellResult = (float)this.Bitmap.Height;
                    this.SizeImage = new SizeF(this.SizeImage.Width, this.Bitmap.Height);
                }
            }
        }

        //────────────────────────────────────────

        private void CalculateCrop()
        {
            //切抜き位置の最終番号を計算。
            this.FrameCropLast = (int)(this.CountrowResult * this.CountcolumnResult);

            this.isCrop = 0 < this.FrameCropForce && this.FrameCropForce <= this.FrameCropLast;
        }

        //────────────────────────────────────────

        /// <summary>
        /// w,h（枠）
        /// </summary>
        /// <returns></returns>
        public SizeF GetFrameSize()
        {
            SizeF size;

            float col = this.CountcolumnResult;
            float row = this.CountrowResult;
            if (col < 1)
            {
                col = 1;
            }

            if (row < 1)
            {
                row = 1;
            }

            float cw = this.WidthcellResult;
            float ch = this.HeightcellResult;

            size = new SizeF(
                col * cw,
                row * ch
            );

            return size;
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        protected Bitmap bitmap;

        /// <summary>
        /// （１）画像
        /// </summary>
        public Bitmap Bitmap
        {
            get
            {
                return bitmap;
            }
            set
            {
                bitmap = value;

                // 再計算
                this.CalculateSize();
            }
        }

        //────────────────────────────────────────

        protected PointF gridLefttop;

        /// <summary>
        /// 格子線の原点XY。
        /// 
        /// (left top)
        /// </summary>
        public PointF GridLefttop
        {
            get
            {
                return gridLefttop;
            }
            set
            {
                gridLefttop = value;
            }
        }

        //────────────────────────────────────────

        protected PointF lefttop;

        /// <summary>
        /// スプライトの原点XY。
        /// 
        /// (left top)
        /// </summary>
        public PointF Lefttop
        {
            get
            {
                return lefttop;
            }
            set
            {
                lefttop = value;
            }
        }

        //────────────────────────────────────────

        protected bool isAutoinputting;

        /// <summary>
        /// 自動入力中なら真。
        /// </summary>
        public bool IsAutoinputting
        {
            get
            {
                return isAutoinputting;
            }
            set
            {
                isAutoinputting = value;
            }
        }

        //────────────────────────────────────────

        protected List<Usercontrolview> list_Usercontrolview;

        public List<Usercontrolview> List_Usercontrolview
        {
            get
            {
                return list_Usercontrolview;
            }
        }

        //────────────────────────────────────────

        protected float countcolumnForce;

        /// <summary>
        /// 指定された列数。未指定またはエラーなら 0。
        /// (Column Count)
        /// </summary>
        public float CountcolumnForce
        {
            get
            {
                return countcolumnForce;
            }
            set
            {
                countcolumnForce = value;

                // 再計算
                this.CalculateSize();
                this.CalculateCrop();
            }
        }

        //────────────────────────────────────────

        protected float countrowForce;

        /// <summary>
        /// 指定された行数。未指定またはエラーなら 0。
        /// </summary>
        public float CountrowForce
        {
            get
            {
                return countrowForce;
            }
            set
            {
                countrowForce = value;

                // 再計算
                this.CalculateSize();
                this.CalculateCrop();

                // 再描画はここでは行わない。
            }
        }

        //────────────────────────────────────────

        protected float countcolumnResult;

        /// <summary>
        /// 計算結果の列数。未指定またはエラーなら 0。
        /// (Column Count)
        /// </summary>
        public float CountcolumnResult
        {
            get
            {
                return countcolumnResult;
            }
            set
            {
                countcolumnResult = value;

                // 再計算
                this.CalculateCrop();

                foreach (Usercontrolview voFrame in this.List_Usercontrolview)
                {
                    voFrame.OnChanged_CountcolumnResult(countcolumnResult);
                }
            }
        }

        //────────────────────────────────────────

        protected float countrowResult;

        /// <summary>
        /// 計算結果の行数。未指定またはエラーなら 0。
        /// </summary>
        public float CountrowResult
        {
            get
            {
                return countrowResult;
            }
            set
            {
                countrowResult = value;

                // 再計算
                this.CalculateCrop();

                foreach (Usercontrolview voFrame in this.List_Usercontrolview)
                {
                    voFrame.OnChanged_CountrowResult(countrowResult);
                }
            }
        }

        //────────────────────────────────────────

        protected SizeF sizecellForce;

        /// <summary>
        /// 利用者が指定したセルの横幅、縦幅。未指定またはエラーなら 0。
        /// </summary>
        public SizeF SizecellForce
        {
            get
            {
                return sizecellForce;
            }
            set
            {
                sizecellForce = value;

                // 再計算
                this.CalculateSize();
                this.CalculateCrop();

                // 再描画はここでは行わない。
            }
        }

        //────────────────────────────────────────

        protected float widthcellResult;

        /// <summary>
        /// 計算結果のセルの横幅。未指定またはエラーなら 0。
        /// </summary>
        public float WidthcellResult
        {
            get
            {
                return widthcellResult;
            }
            set
            {
                widthcellResult = value;

                foreach (Usercontrolview voFrame in this.List_Usercontrolview)
                {
                    voFrame.OnChanged_WidthcellResult(widthcellResult);
                }
            }
        }

        //────────────────────────────────────────

        protected float heightcellResult;

        /// <summary>
        /// 計算結果のセルの縦幅。未指定またはエラーなら 0。
        /// </summary>
        public float HeightcellResult
        {
            get
            {
                return heightcellResult;
            }
            set
            {
                heightcellResult = value;

                foreach (Usercontrolview voFrame in this.List_Usercontrolview)
                {
                    voFrame.OnChanged_HeightcellResult(heightcellResult);
                }
            }
        }

        //────────────────────────────────────────

        protected SizeF sizeImage;

        /// <summary>
        /// 画像の横幅、縦幅。等倍。
        /// </summary>
        public SizeF SizeImage
        {
            get
            {
                return sizeImage;
            }
            set
            {
                sizeImage = value;
            }
        }

        //────────────────────────────────────────

        protected int frameCropForce;

        /// <summary>
        /// 指定した[切り抜くフレーム／１～]
        /// </summary>
        public int FrameCropForce
        {
            get
            {
                return frameCropForce;
            }
            set
            {
                frameCropForce = value;

                // 再計算
                this.CalculateCrop();

                foreach (Usercontrolview voFrame in this.List_Usercontrolview)
                {
                    voFrame.OnChanged_CropForce(frameCropForce);
                }
            }
        }

        //────────────────────────────────────────

        protected int frameCropLast;

        /// <summary>
        /// 切り抜くフレーム終値／１～
        /// </summary>
        public int FrameCropLast
        {
            get
            {
                return frameCropLast;
            }
            set
            {
                frameCropLast = value;

                foreach (Usercontrolview voFrame in this.List_Usercontrolview)
                {
                    voFrame.OnChanged_CropLastResult(frameCropLast);
                }
            }
        }

        //────────────────────────────────────────

        protected bool isCrop;

        /// <summary>
        /// 切抜きなら真、全体図なら偽。
        /// </summary>
        public bool IsCrop
        {
            get
            {
                return this.isCrop;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
