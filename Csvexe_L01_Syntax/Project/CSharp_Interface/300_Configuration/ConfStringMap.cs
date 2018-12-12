using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{


    /// <summary>
    /// 属性の連想配列。
    /// 
    /// 旧名：Dictionary_Ｃonfigurationtree_String
    /// </summary>
    public interface ConfStringMap
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// new された直後の内容に戻します。
        /// </summary>
        void Clear(Conf_String cOwner, Log_Reports log_Reports);

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// 属性を追加します。
        /// </summary>
        /// <param name="key">「Pm:☆;」のPm付き。</param>
        /// <param name="value"></param>
        /// <param name="cValue"></param>
        /// <param name="isRequired"></param>
        /// <param name="log_Reports"></param>
        void Add(
            string key,
            string value,
            Conf_String cValue,
            bool isRequired,
            Log_Reports log_Reports
            );

        /// <summary>
        /// 属性を上書きします。
        /// </summary>
        void Set(
            string key,
            string value,
            Log_Reports log_Reports
            );

        /// <summary>
        /// 属性を取得します。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="result"></param>
        /// <param name="isRequired"></param>
        /// <param name="log_Reports"></param>
        /// <returns></returns>
        bool TryGetValue(
            PmName name,
            out string result,
            bool isRequired,
            Log_Reports log_Reports
            );

        bool TryGetValue2(
            PmName name,
            out Expr_String result,
            Conf_String cElm,
            Expr_String eParent,
            bool isRequired,
            Log_Reports log_Reports
        );

        //────────────────────────────────────────
        #endregion



        #region 判定
        //────────────────────────────────────────

        /// <summary>
        /// 属性の有無を確認します。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool ContainsKey(string key);

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 属性数。
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// それぞれの属性。
        /// </summary>
        /// <param name="dlgt1"></param>
        void ForEach(DELEGATE_StringAttributes dlgt1);

        //────────────────────────────────────────
        #endregion



    }
}
