using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Xenon.FrameMemo
{
    public partial class CustomcontrolButtonEx : Button
    {



        #region 生成と破棄
        //────────────────────────────────────────

        public CustomcontrolButtonEx()
        {
            InitializeComponent();
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// ボタンにフォーカスが合わさっていても、カーソルキーをキャッチできるようにします。
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool IsInputKey(Keys keyData)
        {
            //Altキーが押されているか確認する
            if ((keyData & Keys.Alt) != Keys.Alt)
            {
                //矢印キーが押されたときは、trueを返す
                Keys kcode = keyData & Keys.KeyCode;
                if (kcode == Keys.Up || kcode == Keys.Down ||
                    kcode == Keys.Left || kcode == Keys.Right)
                {
                    return true;
                }
            }
            return base.IsInputKey(keyData);
        }

        //────────────────────────────────────────
        #endregion



    }
}
