using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{


    /// <summary>
    /// ファイルパス。相対パス、絶対パスの違いを吸収するためのものです。
    /// 
    /// 旧名：Ｃonfigurationtree_NodeFilepath
    /// </summary>
    public interface Conf_Filepath : Conf_String
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// パスを設定します。
        /// </summary>
        /// <param name="folderRel_New">フォルダーのパス。</param>
        /// <param name="fileBoth_Newhumaninput">ファイルの相対パス、または絶対パス。</param>
        void Init1(
            string folderRel_New,
            string fileBoth_Newhumaninput,
            Log_Reports log_Reports
            );

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 初期化用。
        /// </summary>
        /// <param name="directory_Base"></param>
        void SetDirectory_Base(string directory_Base);

        /// <summary>
        /// 設定ファイルに記述されているままのファイル・パス表記。
        /// 
        /// 相対パス、絶対パスのどちらでも構わない。
        /// 
        /// 例："Data\\Monster.csv"
        /// </summary>
        /// <returns></returns>
        string GetHumaninput();

        /// <summary>
        /// 設定ファイルに記述されているままのファイル・パス表記。
        /// 
        /// 相対パス、絶対パスのどちらでも構わない。
        /// 
        /// 例："Data\\Monster.csv"
        /// </summary>
        /// <param name="newHumanInputFilePath"></param>
        void SetHumaninput(
            string fileRel_Newhumaninput,
            Log_Reports log_Reports
            );

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 相対パスが設定されていた場合、その相対元となるディレクトリーへのパスです。
        /// そうでない場合は、System.Windows.Forms.StartupPath を入れてください。
        /// </summary>
        string Directory_Base
        {
            get;
        }

        //────────────────────────────────────────
        #endregion



    }
}
