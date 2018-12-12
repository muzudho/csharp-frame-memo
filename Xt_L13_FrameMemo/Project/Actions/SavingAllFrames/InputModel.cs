using System.Windows.Forms;

namespace Grayscale.FrameMemo.Actions.SavingAllFrames
{
    /// <summary>
    /// 
    /// </summary>
    public class InputModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Usercontrolview_Infodisplay InfoDisplay { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public CheckBox InfoCheckBox { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="infoCheckBox"></param>
        public InputModel(Usercontrolview_Infodisplay infoDisplay, CheckBox infoCheckBox)
        {
            InfoDisplay = infoDisplay;
            InfoCheckBox = infoCheckBox;
        }
    }
}
