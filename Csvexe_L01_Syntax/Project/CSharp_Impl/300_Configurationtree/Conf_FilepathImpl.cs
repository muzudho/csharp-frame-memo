using System;
using System.Collections.Generic;
using System.IO;  //Path,Directory
using System.Linq;
using System.Text;

using System.Windows.Forms;//Application

namespace Xenon.Syntax
{

    /// <summary>
    /// ファイルパス。
    /// 
    /// 旧名：Ｃonfigurationtree_NodeFilepathImpl
    /// </summary>
    public class Conf_FilepathImpl : Conf_StringImpl, Conf_Filepath
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="name"></param>
        public Conf_FilepathImpl(string name, Conf_String cParent_OrNull)
            : base(name, cParent_OrNull)
        {
            this.humaninput = "";
            this.directory_Base = "";
        }

        //────────────────────────────────────────

        /// <summary>
        /// パスを設定します。相対パス、絶対パスの違いを吸収するためのものです。
        /// </summary>
        /// <param name="folderRel_OrEmpty">フォルダーパス。指定するものがない場合は、System.Windows.Forms.StartupPath を入れてください。</param>
        /// <param name="fileBoth_Newhumaninput">ファイルパス。</param>
        public void Init1(
            string folderRel_OrEmpty,
            string fileBoth_Newhumaninput,
            Log_Reports log_Reports
            )
        {
            // ダミー・フラグ。使いません。
            bool bDammyFlagCheckPathTooLong = false;

            if (log_Reports.Successful)
            {
                // チェック。絶対パスにすることができればOK。
                Util_Filepath.ToAbsolute(
                    folderRel_OrEmpty,
                    fileBoth_Newhumaninput,
                    ref bDammyFlagCheckPathTooLong,
                    false,//ファイル名の長さが上限超過ならエラー
                    log_Reports,//out out_sErrorMsg,
                    this
                    );
            }

            if (log_Reports.Successful)
            {
                this.SetDirectory_Base(folderRel_OrEmpty);
            }

            if (log_Reports.Successful)
            {
                this.SetHumaninput(
                    fileBoth_Newhumaninput,
                    log_Reports
                    );
            }

            goto gt_EndMethod;
        //
        //
        gt_EndMethod:
            return;
        }

        //────────────────────────────────────────

        ///// <summary>
        ///// パスを設定します。相対パス、絶対パスの違いを吸収するためのものです。
        ///// </summary>
        ///// <param name="newDirectoryPath">指定するものがない場合は、System.Windows.Forms.StartupPath を入れてください。</param>
        ///// <param name="newHumanInputFilePath"></param>
        //public void I nitPath(
        //    string fileRel_Newhumaninput,
        //    Log_Reports log_Reports
        //    )
        //{
        //    this.I nitPath(
        //        "",
        //        fileRel_Newhumaninput,
        //        log_Reports
        //        );
        //}

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        public override void ToText_Content(Log_TextIndented s)
        {
            s.Increment();


            s.AppendI(0, "<");
            s.Append(this.Name);
            s.Append(">");
            s.Newline();

            //
            // 子要素なし。
            //

            //
            // メンバ変数
            //

            s.Append("directory_Base=[");
            s.Append(this.directory_Base);
            s.Append("]");
            s.Newline();

            s.Append("humaninput=[");
            s.Append(this.humaninput);
            s.Append("]");
            s.Newline();


            s.AppendI(0, "</");
            s.Append(this.Name);
            s.Append(">");
            s.Newline();


            s.Decrement();
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 人間が入力したままの、トリム等が行われていないテキスト。
        /// </summary>
        private string humaninput;

        /// <summary>
        /// 設定ファイルに記述されているままのファイル・パス表記。
        /// 
        /// 相対パス、絶対パスのどちらでも構わない。
        /// 
        /// 例："Data\\Monster.csv"
        /// </summary>
        /// <returns></returns>
        public string GetHumaninput()
        {
            return humaninput;
        }

        public void SetHumaninput(
            string fileRel_Humaninput_New,
            Log_Reports log_Reports
            )
        {

            if (log_Reports.Successful)
            {
                // ダミー・フラグ。使いません。
                bool bDammyFlagCheckPathTooLong = false;

                // チェックするだけ。 絶対パスにすることができれば問題なし、そのままスルー。
                Util_Filepath.ToAbsolute(
                    this.directory_Base,
                    fileRel_Humaninput_New,
                    ref bDammyFlagCheckPathTooLong,
                    false,//ファイル名の長さが上限超過ならエラー
                    log_Reports,
                    this.Parent
                );
            }

            if (log_Reports.Successful)
            {
                // 引数を受け入れます。
                this.humaninput = fileRel_Humaninput_New;
            }

            goto gt_EndMethod;
        //
        //
        gt_EndMethod:
            return;
        }

        //────────────────────────────────────────

        private string directory_Base;

        /// <summary>
        /// 相対パスが設定されていた場合、その相対元となるディレクトリーへのパスです。
        /// そうでない場合は、System.Windows.Forms.StartupPath を入れてください。
        /// </summary>
        public string Directory_Base
        {
            get
            {
                return directory_Base;
            }
        }

        /// <summary>
        /// 初期化用。
        /// </summary>
        /// <param name="baseDirectory"></param>
        public void SetDirectory_Base(string directory_Base)
        {
            this.directory_Base = directory_Base;
        }

        //────────────────────────────────────────
        #endregion



    }
}
