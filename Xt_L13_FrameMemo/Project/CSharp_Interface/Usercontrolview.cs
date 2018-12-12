using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.FrameMemo
{
    /// <summary>
    /// フォームも、引数コントロールも、情報表示コントロールもこれ。
    /// </summary>
    public interface Usercontrolview
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 計算結果の列数。未指定またはエラーなら 0。
        /// </summary>
        void OnChanged_CountcolumnResult(float countColumn);

        /// <summary>
        /// 計算結果の行数。未指定またはエラーなら 0。
        /// </summary>
        void OnChanged_CountrowResult(float countRow);

        /// <summary>
        /// 計算結果のセルの横幅。等倍。
        /// </summary>
        void OnChanged_WidthcellResult(float widthCell);

        /// <summary>
        /// 計算結果のセルの縦幅。等倍。
        /// </summary>
        void OnChanged_HeightcellResult(float heightCell);

        /// <summary>
        /// 指定した[切り抜くフレーム／１～]
        /// </summary>
        void OnChanged_CropForce(int frame);

        /// <summary>
        /// 計算結果の[切り抜くフレーム終値／１～]
        /// </summary>
        void OnChanged_CropLastResult(int frame);

        void Refresh();

        //────────────────────────────────────────
        #endregion



    }

}
