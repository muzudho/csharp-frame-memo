using System;
using System.Text;
using System.Windows.Forms;
using SavingImage = Grayscale.FrameMemo.Actions.SavingImage;

namespace Grayscale.FrameMemo
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Form1 : Form
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        //────────────────────────────────────────
        #endregion



        #region イベントハンドラー
        //────────────────────────────────────────

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.ucCanvas1.Size = this.ClientSize;
            this.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // タイトル
            {
                StringBuilder s = new StringBuilder();

                s.Append(Application.ProductName);
                s.Append(" v");
                s.Append(Application.ProductVersion);
                //s.Append(" - ゼノンツールズ");

                this.Text = s.ToString();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            // bug:リストボックスで　カーソルを動かしているときも、利いてしまう。

            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.S:
                        // [Ctrl]+[S]
                        {
                            // [画像を保存]ボタンを押したときの内容。
                            {
                                SavingImage.ContextModel context = new SavingImage.ContextModel(this.ucCanvas1);
                                SavingImage.InputModel input = new SavingImage.InputModel(this.ucCanvas1.Infodisplay, this.ucCanvas1.InfoCheckBox);
                                SavingImage.OutputModel output = new SavingImage.OutputModel();
                                SavingImage.Action.Perfrom(context, input, output);
                            }
                        }
                        break;
                }
            }

            switch (e.KeyCode)
            {
                case Keys.Up:
                    this.ucCanvas1.MoveActiveSprite(0, -1);
                    break;
                case Keys.Right:
                    this.ucCanvas1.MoveActiveSprite(1, 0);
                    break;
                case Keys.Down:
                    this.ucCanvas1.MoveActiveSprite(0, 1);
                    break;
                case Keys.Left:
                    this.ucCanvas1.MoveActiveSprite(-1, 0);
                    break;

                case Keys.X:
                    {
                        // [X]ズームダウン
                        this.ucCanvas1.ZoomDown();
                    }
                    break;

                case Keys.Z:
                    {
                        // [Z]ズームアップ
                        this.ucCanvas1.ZoomUp();
                    }
                    break;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
