using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grayscale.FrameMemo.Actions.SavingAllFrames
{
    /// <summary>
    /// 
    /// </summary>
    public class ContextModel
    {
        /// <summary>
        /// 
        /// </summary>
        public UcCanvas UcCanvas { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ucCanvas"></param>
        public ContextModel(UcCanvas ucCanvas)
        {
            UcCanvas = ucCanvas;
        }
    }
}
