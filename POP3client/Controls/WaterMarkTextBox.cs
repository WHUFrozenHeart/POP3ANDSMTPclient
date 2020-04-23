using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POP3client.Controls
{
    public class WaterMarkTextBox : TextBox
    {
        static WaterMarkTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WaterMarkTextBox), new FrameworkPropertyMetadata(typeof(WaterMarkTextBox)));
        }

        private string watermark = string.Empty;
        public string WaterMark
        {
            get { return watermark; }
            set { watermark = value ?? string.Empty; }
        }

        public static readonly DependencyProperty WaterMarkProperty =
            DependencyProperty.Register("WaterMark", typeof(string), typeof(WaterMarkTextBox));

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            if (this.Text == string.Empty)
            {
                VisualStateManager.GoToState(this, "Empty", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "NotEmpty", true);
            }
        }

        public enum TextStates
        {
            Empty,
            NotEmpty

        }
    }
}
