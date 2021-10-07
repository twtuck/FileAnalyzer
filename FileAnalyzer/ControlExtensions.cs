using System.Reflection;
using System.Windows.Forms;

namespace FileAnalyzer
{
    /// <summary>
    /// Provides the helper method to set the DoubleBuffered property of a control to avoid flickering when the control is rendered
    /// </summary>
    public static class ControlExtensions
    {
        public static void DoubleBuffered(this Control control, bool enable)
        {
            var doubleBufferPropertyInfo = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBufferPropertyInfo?.SetValue(control, enable, null);
        }
    }
}