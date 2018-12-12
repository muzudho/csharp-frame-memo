namespace Grayscale.FrameMemo
{
    partial class UcCanvas
    {
        /// <summary> 
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.pclblMouseDrag = new System.Windows.Forms.Label();
            this.pclblAlScale = new System.Windows.Forms.Label();
            this.pcddlAlScale = new System.Windows.Forms.ComboBox();
            this.pcdlgOpenBgFile = new System.Windows.Forms.OpenFileDialog();
            this.pclblBgclr = new System.Windows.Forms.Label();
            this.pcddlBgclr = new System.Windows.Forms.ComboBox();
            this.pclblGrid1 = new System.Windows.Forms.Label();
            this.pclblOpaque = new System.Windows.Forms.Label();
            this.pcddlOpaque = new System.Windows.Forms.ComboBox();
            this.mouseDragLst = new System.Windows.Forms.ListBox();
            this.pclblInfo1 = new System.Windows.Forms.Label();
            this.pcchkInfoVisibled = new System.Windows.Forms.CheckBox();
            this.pcchkGridVisibled = new System.Windows.Forms.CheckBox();
            this.pcddlGridColor = new System.Windows.Forms.ComboBox();
            this.pclblPartnumber1 = new System.Windows.Forms.Label();
            this.pcddlPartnumberOpaque = new System.Windows.Forms.ComboBox();
            this.pclblPartnumber2 = new System.Windows.Forms.Label();
            this.pcchkPartnumberVisible = new System.Windows.Forms.CheckBox();
            this.pcddlPartnumberColor = new System.Windows.Forms.ComboBox();
            this.pclblPartnumber3 = new System.Windows.Forms.Label();
            this.pctxtPartnumberFirst = new System.Windows.Forms.TextBox();
            this.columnRowLbl = new System.Windows.Forms.Label();
            this.columnForceTxt = new System.Windows.Forms.TextBox();
            this.rowForceTxt = new System.Windows.Forms.TextBox();
            this.columnResultLbl = new System.Windows.Forms.Label();
            this.rowResultLbl = new System.Windows.Forms.Label();
            this.cellSizeLbl = new System.Windows.Forms.Label();
            this.cellWidthForceTxt = new System.Windows.Forms.TextBox();
            this.cellHeightForceTxt = new System.Windows.Forms.TextBox();
            this.cellWidthResultLbl = new System.Windows.Forms.Label();
            this.cellHeightResultLbl = new System.Windows.Forms.Label();
            this.cropForceTxt = new System.Windows.Forms.TextBox();
            this.cropLbl = new System.Windows.Forms.Label();
            this.cropLastResultLbl = new System.Windows.Forms.Label();
            this.cropResultLbl = new System.Windows.Forms.Label();
            this.gridXTxt = new System.Windows.Forms.TextBox();
            this.gridYTxt = new System.Windows.Forms.TextBox();
            this.gridXyLbl = new System.Windows.Forms.Label();
            this.pcbtnSaveImgFrames = new Grayscale.FrameMemo.CustomcontrolButtonEx();
            this.pcbtnSaveImg = new Grayscale.FrameMemo.CustomcontrolButtonEx();
            this.pcbtnOpen = new Grayscale.FrameMemo.CustomcontrolButtonEx();
            this.SuspendLayout();
            // 
            // pclblMouseDrag
            // 
            this.pclblMouseDrag.AutoSize = true;
            this.pclblMouseDrag.Enabled = false;
            this.pclblMouseDrag.Location = new System.Drawing.Point(464, 130);
            this.pclblMouseDrag.Name = "pclblMouseDrag";
            this.pclblMouseDrag.Size = new System.Drawing.Size(65, 12);
            this.pclblMouseDrag.TabIndex = 4;
            this.pclblMouseDrag.Text = "マウスドラッグ";
            // 
            // pclblAlScale
            // 
            this.pclblAlScale.AutoSize = true;
            this.pclblAlScale.Enabled = false;
            this.pclblAlScale.Location = new System.Drawing.Point(112, 42);
            this.pclblAlScale.Name = "pclblAlScale";
            this.pclblAlScale.Size = new System.Drawing.Size(41, 12);
            this.pclblAlScale.TabIndex = 6;
            this.pclblAlScale.Text = "拡大率";
            // 
            // pcddlAlScale
            // 
            this.pcddlAlScale.Enabled = false;
            this.pcddlAlScale.FormattingEnabled = true;
            this.pcddlAlScale.Location = new System.Drawing.Point(114, 57);
            this.pcddlAlScale.Name = "pcddlAlScale";
            this.pcddlAlScale.Size = new System.Drawing.Size(48, 20);
            this.pcddlAlScale.TabIndex = 7;
            this.pcddlAlScale.SelectedIndexChanged += new System.EventHandler(this.pcddlScale_SelectedIndexChanged);
            // 
            // pcdlgOpenBgFile
            // 
            this.pcdlgOpenBgFile.FileName = "openFileDialog1";
            // 
            // pclblBgclr
            // 
            this.pclblBgclr.AutoSize = true;
            this.pclblBgclr.Enabled = false;
            this.pclblBgclr.Location = new System.Drawing.Point(202, 42);
            this.pclblBgclr.Name = "pclblBgclr";
            this.pclblBgclr.Size = new System.Drawing.Size(41, 12);
            this.pclblBgclr.TabIndex = 8;
            this.pclblBgclr.Text = "背景色";
            // 
            // pcddlBgclr
            // 
            this.pcddlBgclr.Enabled = false;
            this.pcddlBgclr.FormattingEnabled = true;
            this.pcddlBgclr.Location = new System.Drawing.Point(204, 57);
            this.pcddlBgclr.Name = "pcddlBgclr";
            this.pcddlBgclr.Size = new System.Drawing.Size(56, 20);
            this.pcddlBgclr.TabIndex = 9;
            this.pcddlBgclr.SelectedIndexChanged += new System.EventHandler(this.pcddlOpaque_SelectedIndexChanged);
            // 
            // pclblGrid1
            // 
            this.pclblGrid1.AutoSize = true;
            this.pclblGrid1.Enabled = false;
            this.pclblGrid1.Location = new System.Drawing.Point(556, 42);
            this.pclblGrid1.Name = "pclblGrid1";
            this.pclblGrid1.Size = new System.Drawing.Size(17, 12);
            this.pclblGrid1.TabIndex = 10;
            this.pclblGrid1.Text = "枠";
            // 
            // pclblOpaque
            // 
            this.pclblOpaque.AutoSize = true;
            this.pclblOpaque.Enabled = false;
            this.pclblOpaque.Location = new System.Drawing.Point(282, 42);
            this.pclblOpaque.Name = "pclblOpaque";
            this.pclblOpaque.Size = new System.Drawing.Size(77, 12);
            this.pclblOpaque.TabIndex = 13;
            this.pclblOpaque.Text = "画像不透明度";
            // 
            // pcddlOpaque
            // 
            this.pcddlOpaque.Enabled = false;
            this.pcddlOpaque.FormattingEnabled = true;
            this.pcddlOpaque.Location = new System.Drawing.Point(287, 57);
            this.pcddlOpaque.Name = "pcddlOpaque";
            this.pcddlOpaque.Size = new System.Drawing.Size(56, 20);
            this.pcddlOpaque.TabIndex = 14;
            this.pcddlOpaque.SelectedIndexChanged += new System.EventHandler(this.pcddlOpaqueBg_SelectedIndexChanged);
            // 
            // mouseDragLst
            // 
            this.mouseDragLst.Enabled = false;
            this.mouseDragLst.FormattingEnabled = true;
            this.mouseDragLst.ItemHeight = 12;
            this.mouseDragLst.Location = new System.Drawing.Point(466, 145);
            this.mouseDragLst.Name = "mouseDragLst";
            this.mouseDragLst.Size = new System.Drawing.Size(60, 28);
            this.mouseDragLst.TabIndex = 15;
            this.mouseDragLst.SelectedIndexChanged += new System.EventHandler(this.mouseDragLst_SelectedIndexChanged);
            // 
            // pclblInfo1
            // 
            this.pclblInfo1.AutoSize = true;
            this.pclblInfo1.Enabled = false;
            this.pclblInfo1.Location = new System.Drawing.Point(649, 42);
            this.pclblInfo1.Name = "pclblInfo1";
            this.pclblInfo1.Size = new System.Drawing.Size(65, 12);
            this.pclblInfo1.TabIndex = 25;
            this.pclblInfo1.Text = "情報表示中";
            // 
            // pcchkInfoVisibled
            // 
            this.pcchkInfoVisibled.AutoSize = true;
            this.pcchkInfoVisibled.Checked = true;
            this.pcchkInfoVisibled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pcchkInfoVisibled.Enabled = false;
            this.pcchkInfoVisibled.Location = new System.Drawing.Point(651, 63);
            this.pcchkInfoVisibled.Name = "pcchkInfoVisibled";
            this.pcchkInfoVisibled.Size = new System.Drawing.Size(15, 14);
            this.pcchkInfoVisibled.TabIndex = 26;
            this.pcchkInfoVisibled.UseVisualStyleBackColor = true;
            this.pcchkInfoVisibled.CheckedChanged += new System.EventHandler(this.pcchkInfo_CheckedChanged);
            // 
            // pcchkGridVisibled
            // 
            this.pcchkGridVisibled.AutoSize = true;
            this.pcchkGridVisibled.Enabled = false;
            this.pcchkGridVisibled.Location = new System.Drawing.Point(558, 83);
            this.pcchkGridVisibled.Name = "pcchkGridVisibled";
            this.pcchkGridVisibled.Size = new System.Drawing.Size(15, 14);
            this.pcchkGridVisibled.TabIndex = 31;
            this.pcchkGridVisibled.UseVisualStyleBackColor = true;
            this.pcchkGridVisibled.CheckedChanged += new System.EventHandler(this.pcchkImgBorder_CheckedChanged);
            // 
            // pcddlGridColor
            // 
            this.pcddlGridColor.Enabled = false;
            this.pcddlGridColor.FormattingEnabled = true;
            this.pcddlGridColor.Location = new System.Drawing.Point(558, 60);
            this.pcddlGridColor.Name = "pcddlGridColor";
            this.pcddlGridColor.Size = new System.Drawing.Size(56, 20);
            this.pcddlGridColor.TabIndex = 32;
            this.pcddlGridColor.SelectedIndexChanged += new System.EventHandler(this.pcddlGridcolor_SelectedIndexChanged);
            // 
            // pclblPartnumber1
            // 
            this.pclblPartnumber1.AutoSize = true;
            this.pclblPartnumber1.Enabled = false;
            this.pclblPartnumber1.Location = new System.Drawing.Point(372, 42);
            this.pclblPartnumber1.Name = "pclblPartnumber1";
            this.pclblPartnumber1.Size = new System.Drawing.Size(77, 12);
            this.pclblPartnumber1.TabIndex = 35;
            this.pclblPartnumber1.Text = "番号不透明度";
            // 
            // pcddlPartnumberOpaque
            // 
            this.pcddlPartnumberOpaque.Enabled = false;
            this.pcddlPartnumberOpaque.FormattingEnabled = true;
            this.pcddlPartnumberOpaque.Location = new System.Drawing.Point(377, 57);
            this.pcddlPartnumberOpaque.Name = "pcddlPartnumberOpaque";
            this.pcddlPartnumberOpaque.Size = new System.Drawing.Size(56, 20);
            this.pcddlPartnumberOpaque.TabIndex = 36;
            this.pcddlPartnumberOpaque.SelectedIndexChanged += new System.EventHandler(this.pcddlPartnumberOpaque_SelectedIndexChanged);
            // 
            // pclblPartnumber2
            // 
            this.pclblPartnumber2.AutoSize = true;
            this.pclblPartnumber2.Enabled = false;
            this.pclblPartnumber2.Location = new System.Drawing.Point(375, 80);
            this.pclblPartnumber2.Name = "pclblPartnumber2";
            this.pclblPartnumber2.Size = new System.Drawing.Size(41, 12);
            this.pclblPartnumber2.TabIndex = 37;
            this.pclblPartnumber2.Text = "表示中";
            // 
            // pcchkPartnumberVisible
            // 
            this.pcchkPartnumberVisible.AutoSize = true;
            this.pcchkPartnumberVisible.Checked = true;
            this.pcchkPartnumberVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pcchkPartnumberVisible.Enabled = false;
            this.pcchkPartnumberVisible.Location = new System.Drawing.Point(418, 78);
            this.pcchkPartnumberVisible.Name = "pcchkPartnumberVisible";
            this.pcchkPartnumberVisible.Size = new System.Drawing.Size(15, 14);
            this.pcchkPartnumberVisible.TabIndex = 38;
            this.pcchkPartnumberVisible.UseVisualStyleBackColor = true;
            this.pcchkPartnumberVisible.CheckedChanged += new System.EventHandler(this.pcchkPartnumberVisible_CheckedChanged);
            // 
            // pcddlPartnumberColor
            // 
            this.pcddlPartnumberColor.Enabled = false;
            this.pcddlPartnumberColor.FormattingEnabled = true;
            this.pcddlPartnumberColor.Location = new System.Drawing.Point(455, 39);
            this.pcddlPartnumberColor.Name = "pcddlPartnumberColor";
            this.pcddlPartnumberColor.Size = new System.Drawing.Size(55, 20);
            this.pcddlPartnumberColor.TabIndex = 39;
            this.pcddlPartnumberColor.SelectedIndexChanged += new System.EventHandler(this.pcddlPartnumberColor_SelectedIndexChanged);
            // 
            // pclblPartnumber3
            // 
            this.pclblPartnumber3.AutoSize = true;
            this.pclblPartnumber3.Enabled = false;
            this.pclblPartnumber3.Location = new System.Drawing.Point(453, 65);
            this.pclblPartnumber3.Name = "pclblPartnumber3";
            this.pclblPartnumber3.Size = new System.Drawing.Size(53, 12);
            this.pclblPartnumber3.TabIndex = 40;
            this.pclblPartnumber3.Text = "開始番号";
            // 
            // pctxtPartnumberFirst
            // 
            this.pctxtPartnumberFirst.Enabled = false;
            this.pctxtPartnumberFirst.Location = new System.Drawing.Point(455, 80);
            this.pctxtPartnumberFirst.Name = "pctxtPartnumberFirst";
            this.pctxtPartnumberFirst.Size = new System.Drawing.Size(37, 19);
            this.pctxtPartnumberFirst.TabIndex = 41;
            this.pctxtPartnumberFirst.TextChanged += new System.EventHandler(this.pctxtPartnumberFirst_TextChanged);
            // 
            // columnRowLbl
            // 
            this.columnRowLbl.AutoSize = true;
            this.columnRowLbl.Enabled = false;
            this.columnRowLbl.Location = new System.Drawing.Point(18, 130);
            this.columnRowLbl.Name = "columnRowLbl";
            this.columnRowLbl.Size = new System.Drawing.Size(65, 12);
            this.columnRowLbl.TabIndex = 42;
            this.columnRowLbl.Text = "列数／行数";
            // 
            // columnForceTxt
            // 
            this.columnForceTxt.Enabled = false;
            this.columnForceTxt.Location = new System.Drawing.Point(20, 145);
            this.columnForceTxt.Name = "columnForceTxt";
            this.columnForceTxt.Size = new System.Drawing.Size(31, 19);
            this.columnForceTxt.TabIndex = 43;
            this.columnForceTxt.TextChanged += new System.EventHandler(this.columnForceTxt_TextChanged);
            // 
            // rowForceTxt
            // 
            this.rowForceTxt.Enabled = false;
            this.rowForceTxt.Location = new System.Drawing.Point(53, 145);
            this.rowForceTxt.Name = "rowForceTxt";
            this.rowForceTxt.Size = new System.Drawing.Size(30, 19);
            this.rowForceTxt.TabIndex = 44;
            this.rowForceTxt.TextChanged += new System.EventHandler(this.rowForceTxt_TextChanged);
            // 
            // columnResultLbl
            // 
            this.columnResultLbl.AutoSize = true;
            this.columnResultLbl.Enabled = false;
            this.columnResultLbl.Location = new System.Drawing.Point(29, 167);
            this.columnResultLbl.Name = "columnResultLbl";
            this.columnResultLbl.Size = new System.Drawing.Size(11, 12);
            this.columnResultLbl.TabIndex = 45;
            this.columnResultLbl.Text = "-";
            // 
            // rowResultLbl
            // 
            this.rowResultLbl.AutoSize = true;
            this.rowResultLbl.Enabled = false;
            this.rowResultLbl.Location = new System.Drawing.Point(63, 167);
            this.rowResultLbl.Name = "rowResultLbl";
            this.rowResultLbl.Size = new System.Drawing.Size(11, 12);
            this.rowResultLbl.TabIndex = 46;
            this.rowResultLbl.Text = "-";
            // 
            // cellSizeLbl
            // 
            this.cellSizeLbl.AutoSize = true;
            this.cellSizeLbl.Enabled = false;
            this.cellSizeLbl.Location = new System.Drawing.Point(104, 130);
            this.cellSizeLbl.Name = "cellSizeLbl";
            this.cellSizeLbl.Size = new System.Drawing.Size(74, 12);
            this.cellSizeLbl.TabIndex = 47;
            this.cellSizeLbl.Text = "1個幅ヨコ/タテ";
            // 
            // cellWidthForceTxt
            // 
            this.cellWidthForceTxt.Enabled = false;
            this.cellWidthForceTxt.Location = new System.Drawing.Point(109, 145);
            this.cellWidthForceTxt.Name = "cellWidthForceTxt";
            this.cellWidthForceTxt.Size = new System.Drawing.Size(33, 19);
            this.cellWidthForceTxt.TabIndex = 48;
            this.cellWidthForceTxt.TextChanged += new System.EventHandler(this.cellWidthForceTxt_TextChanged);
            // 
            // cellHeightForceTxt
            // 
            this.cellHeightForceTxt.Enabled = false;
            this.cellHeightForceTxt.Location = new System.Drawing.Point(145, 145);
            this.cellHeightForceTxt.Name = "cellHeightForceTxt";
            this.cellHeightForceTxt.Size = new System.Drawing.Size(30, 19);
            this.cellHeightForceTxt.TabIndex = 49;
            this.cellHeightForceTxt.TextChanged += new System.EventHandler(this.cellHeightForceTxt_TextChanged);
            // 
            // cellWidthResultLbl
            // 
            this.cellWidthResultLbl.AutoSize = true;
            this.cellWidthResultLbl.Enabled = false;
            this.cellWidthResultLbl.Location = new System.Drawing.Point(121, 167);
            this.cellWidthResultLbl.Name = "cellWidthResultLbl";
            this.cellWidthResultLbl.Size = new System.Drawing.Size(11, 12);
            this.cellWidthResultLbl.TabIndex = 50;
            this.cellWidthResultLbl.Text = "-";
            // 
            // cellHeightResultLbl
            // 
            this.cellHeightResultLbl.AutoSize = true;
            this.cellHeightResultLbl.Enabled = false;
            this.cellHeightResultLbl.Location = new System.Drawing.Point(156, 167);
            this.cellHeightResultLbl.Name = "cellHeightResultLbl";
            this.cellHeightResultLbl.Size = new System.Drawing.Size(11, 12);
            this.cellHeightResultLbl.TabIndex = 51;
            this.cellHeightResultLbl.Text = "-";
            // 
            // cropForceTxt
            // 
            this.cropForceTxt.Enabled = false;
            this.cropForceTxt.Location = new System.Drawing.Point(211, 145);
            this.cropForceTxt.Name = "cropForceTxt";
            this.cropForceTxt.Size = new System.Drawing.Size(39, 19);
            this.cropForceTxt.TabIndex = 52;
            this.cropForceTxt.TextChanged += new System.EventHandler(this.cropForceTxt_TextChanged);
            // 
            // cropLbl
            // 
            this.cropLbl.AutoSize = true;
            this.cropLbl.Enabled = false;
            this.cropLbl.Location = new System.Drawing.Point(195, 130);
            this.cropLbl.Name = "cropLbl";
            this.cropLbl.Size = new System.Drawing.Size(105, 12);
            this.cropLbl.TabIndex = 53;
            this.cropLbl.Text = "切抜きフレーム／1～";
            // 
            // cropLastResultLbl
            // 
            this.cropLastResultLbl.AutoSize = true;
            this.cropLastResultLbl.Enabled = false;
            this.cropLastResultLbl.Location = new System.Drawing.Point(302, 130);
            this.cropLastResultLbl.Name = "cropLastResultLbl";
            this.cropLastResultLbl.Size = new System.Drawing.Size(11, 12);
            this.cropLastResultLbl.TabIndex = 54;
            this.cropLastResultLbl.Text = "1";
            // 
            // cropResultLbl
            // 
            this.cropResultLbl.AutoSize = true;
            this.cropResultLbl.Enabled = false;
            this.cropResultLbl.Location = new System.Drawing.Point(226, 167);
            this.cropResultLbl.Name = "cropResultLbl";
            this.cropResultLbl.Size = new System.Drawing.Size(11, 12);
            this.cropResultLbl.TabIndex = 55;
            this.cropResultLbl.Text = "-";
            // 
            // gridXTxt
            // 
            this.gridXTxt.Enabled = false;
            this.gridXTxt.Location = new System.Drawing.Point(374, 145);
            this.gridXTxt.Name = "gridXTxt";
            this.gridXTxt.Size = new System.Drawing.Size(33, 19);
            this.gridXTxt.TabIndex = 56;
            this.gridXTxt.TextChanged += new System.EventHandler(this.gridXTxt_TextChanged);
            // 
            // gridYTxt
            // 
            this.gridYTxt.Enabled = false;
            this.gridYTxt.Location = new System.Drawing.Point(410, 145);
            this.gridYTxt.Name = "gridYTxt";
            this.gridYTxt.Size = new System.Drawing.Size(33, 19);
            this.gridYTxt.TabIndex = 57;
            this.gridYTxt.TextChanged += new System.EventHandler(this.gridYTxt_TextChanged);
            // 
            // gridXyLbl
            // 
            this.gridXyLbl.AutoSize = true;
            this.gridXyLbl.Enabled = false;
            this.gridXyLbl.Location = new System.Drawing.Point(375, 130);
            this.gridXyLbl.Name = "gridXyLbl";
            this.gridXyLbl.Size = new System.Drawing.Size(63, 12);
            this.gridXyLbl.TabIndex = 58;
            this.gridXyLbl.Text = "グリッドX／Y";
            // 
            // pcbtnSaveImgFrames
            // 
            this.pcbtnSaveImgFrames.Enabled = false;
            this.pcbtnSaveImgFrames.Location = new System.Drawing.Point(583, 154);
            this.pcbtnSaveImgFrames.Name = "pcbtnSaveImgFrames";
            this.pcbtnSaveImgFrames.Size = new System.Drawing.Size(123, 19);
            this.pcbtnSaveImgFrames.TabIndex = 34;
            this.pcbtnSaveImgFrames.Text = "フレーム全画像を保存";
            this.pcbtnSaveImgFrames.UseVisualStyleBackColor = true;
            this.pcbtnSaveImgFrames.Click += new System.EventHandler(this.ccButtonEx1_Click);
            // 
            // pcbtnSaveImg
            // 
            this.pcbtnSaveImg.Enabled = false;
            this.pcbtnSaveImg.Location = new System.Drawing.Point(583, 130);
            this.pcbtnSaveImg.Name = "pcbtnSaveImg";
            this.pcbtnSaveImg.Size = new System.Drawing.Size(75, 23);
            this.pcbtnSaveImg.TabIndex = 12;
            this.pcbtnSaveImg.Text = "画像を保存";
            this.pcbtnSaveImg.UseVisualStyleBackColor = true;
            this.pcbtnSaveImg.Click += new System.EventHandler(this.pcbtnSaveImg_Click);
            // 
            // pcbtnOpen
            // 
            this.pcbtnOpen.Location = new System.Drawing.Point(20, 46);
            this.pcbtnOpen.Name = "pcbtnOpen";
            this.pcbtnOpen.Size = new System.Drawing.Size(63, 23);
            this.pcbtnOpen.TabIndex = 1;
            this.pcbtnOpen.Text = "画像開く";
            this.pcbtnOpen.UseVisualStyleBackColor = true;
            this.pcbtnOpen.Click += new System.EventHandler(this.pcbtnBg_Click);
            // 
            // UcCanvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridXyLbl);
            this.Controls.Add(this.gridYTxt);
            this.Controls.Add(this.gridXTxt);
            this.Controls.Add(this.cropResultLbl);
            this.Controls.Add(this.cropLastResultLbl);
            this.Controls.Add(this.cropLbl);
            this.Controls.Add(this.cropForceTxt);
            this.Controls.Add(this.cellHeightResultLbl);
            this.Controls.Add(this.cellWidthResultLbl);
            this.Controls.Add(this.cellHeightForceTxt);
            this.Controls.Add(this.cellWidthForceTxt);
            this.Controls.Add(this.cellSizeLbl);
            this.Controls.Add(this.rowResultLbl);
            this.Controls.Add(this.columnResultLbl);
            this.Controls.Add(this.rowForceTxt);
            this.Controls.Add(this.columnForceTxt);
            this.Controls.Add(this.columnRowLbl);
            this.Controls.Add(this.pctxtPartnumberFirst);
            this.Controls.Add(this.pclblPartnumber3);
            this.Controls.Add(this.pcddlPartnumberColor);
            this.Controls.Add(this.pcchkPartnumberVisible);
            this.Controls.Add(this.pclblPartnumber2);
            this.Controls.Add(this.pcddlPartnumberOpaque);
            this.Controls.Add(this.pclblPartnumber1);
            this.Controls.Add(this.pcbtnSaveImgFrames);
            this.Controls.Add(this.pcddlGridColor);
            this.Controls.Add(this.pcchkGridVisibled);
            this.Controls.Add(this.pcchkInfoVisibled);
            this.Controls.Add(this.pclblInfo1);
            this.Controls.Add(this.mouseDragLst);
            this.Controls.Add(this.pcddlOpaque);
            this.Controls.Add(this.pclblOpaque);
            this.Controls.Add(this.pcbtnSaveImg);
            this.Controls.Add(this.pclblGrid1);
            this.Controls.Add(this.pcddlBgclr);
            this.Controls.Add(this.pclblBgclr);
            this.Controls.Add(this.pcddlAlScale);
            this.Controls.Add(this.pclblAlScale);
            this.Controls.Add(this.pclblMouseDrag);
            this.Controls.Add(this.pcbtnOpen);
            this.DoubleBuffered = true;
            this.Name = "UcCanvas";
            this.Size = new System.Drawing.Size(781, 509);
            this.Load += new System.EventHandler(this.UcCanvas_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UcCanvas_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UcCanvas_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UcCanvas_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UcCanvas_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label pclblMouseDrag;
        private System.Windows.Forms.Label pclblAlScale;
        private System.Windows.Forms.ComboBox pcddlAlScale;
        private System.Windows.Forms.OpenFileDialog pcdlgOpenBgFile;
        private System.Windows.Forms.Label pclblBgclr;
        private System.Windows.Forms.ComboBox pcddlBgclr;
        private System.Windows.Forms.Label pclblGrid1;
        private System.Windows.Forms.Label pclblOpaque;
        private System.Windows.Forms.ComboBox pcddlOpaque;
        private System.Windows.Forms.ListBox mouseDragLst;
        private CustomcontrolButtonEx pcbtnOpen;
        private CustomcontrolButtonEx pcbtnSaveImg;
        private System.Windows.Forms.Label pclblInfo1;
        private System.Windows.Forms.CheckBox pcchkInfoVisibled;
        private System.Windows.Forms.CheckBox pcchkGridVisibled;
        private System.Windows.Forms.ComboBox pcddlGridColor;
        private CustomcontrolButtonEx pcbtnSaveImgFrames;
        private System.Windows.Forms.Label pclblPartnumber1;
        private System.Windows.Forms.ComboBox pcddlPartnumberOpaque;
        private System.Windows.Forms.Label pclblPartnumber2;
        private System.Windows.Forms.CheckBox pcchkPartnumberVisible;
        private System.Windows.Forms.ComboBox pcddlPartnumberColor;
        private System.Windows.Forms.Label pclblPartnumber3;
        private System.Windows.Forms.TextBox pctxtPartnumberFirst;
        private System.Windows.Forms.Label columnRowLbl;
        private System.Windows.Forms.TextBox columnForceTxt;
        private System.Windows.Forms.TextBox rowForceTxt;
        private System.Windows.Forms.Label columnResultLbl;
        private System.Windows.Forms.Label rowResultLbl;
        private System.Windows.Forms.Label cellSizeLbl;
        private System.Windows.Forms.TextBox cellWidthForceTxt;
        private System.Windows.Forms.TextBox cellHeightForceTxt;
        private System.Windows.Forms.Label cellWidthResultLbl;
        private System.Windows.Forms.Label cellHeightResultLbl;
        private System.Windows.Forms.TextBox cropForceTxt;
        private System.Windows.Forms.Label cropLbl;
        private System.Windows.Forms.Label cropLastResultLbl;
        private System.Windows.Forms.Label cropResultLbl;
        private System.Windows.Forms.TextBox gridXTxt;
        private System.Windows.Forms.TextBox gridYTxt;
        private System.Windows.Forms.Label gridXyLbl;
    }
}
