using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Syntax
{

    /// <summary>
    /// 名前。 GetElementByName(s_name)のような引数として使う。
    /// </summary>
    public class XenonNameImpl : XenonName
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// 
        /// </summary>
        public XenonNameImpl(Conf_String cOwner)
        {
            this.sValue = "";
            this.conf_ = cOwner;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param select="nValue"></param>
        /// <param select="s_OwnerNode"></param>
        public XenonNameImpl(string sValue, Conf_String cOwner)
        {
            this.sValue = sValue;
            this.conf_ = cOwner;
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private string sValue;

        /// <summary>
        /// 名前の文字列。
        /// </summary>
        public string SValue
        {
            get
            {
                return sValue;
            }
        }

        //────────────────────────────────────────

        private Conf_String conf_;

        /// <summary>
        /// 
        /// </summary>
        public Conf_String Conf
        {
            get
            {
                return conf_;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
