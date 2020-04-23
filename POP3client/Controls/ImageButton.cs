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
    public class ImageButton : Button
    {
        static ImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
        }

        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(ImageButton), new PropertyMetadata((double)48));

        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(double), typeof(ImageButton), new PropertyMetadata((double)48));

        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));

        public ButtonType ButtonType
        {
            get { return (ButtonType)GetValue(ButtonTypeProperty); }
            set { SetValue(ButtonTypeProperty, value); }
        }

        public static readonly DependencyProperty ButtonTypeProperty =
            DependencyProperty.Register("ButtonType", typeof(ButtonType), typeof(ImageButton), new PropertyMetadata(ButtonType.Icon));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ImageButton), new PropertyMetadata(new CornerRadius(0)));


        public Brush MouseOverForeground
        {
            get { return (Brush)GetValue(MouseOverForegroundProperty); }
            set { SetValue(MouseOverForegroundProperty, value); }
        }

        public static readonly DependencyProperty MouseOverForegroundProperty =
            DependencyProperty.Register("MouseOverForeground", typeof(Brush), typeof(ImageButton), new PropertyMetadata());


        public Brush MousePressedForeground
        {
            get { return (Brush)GetValue(MousePressedForegroundProperty); }
            set { SetValue(MousePressedForegroundProperty, value); }
        }

        public static readonly DependencyProperty MousePressedForegroundProperty =
            DependencyProperty.Register("MousePressedForeground", typeof(Brush), typeof(ImageButton), new PropertyMetadata());


        public Brush MouseOverBorderbrush
        {
            get { return (Brush)GetValue(MouseOverBorderbrushProperty); }
            set { SetValue(MouseOverBorderbrushProperty, value); }
        }

        public static readonly DependencyProperty MouseOverBorderbrushProperty =
            DependencyProperty.Register("MouseOverBorderbrush", typeof(Brush), typeof(ImageButton), new PropertyMetadata());


        public Brush MouseOverBackground
        {
            get { return (Brush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }

        public static readonly DependencyProperty MouseOverBackgroundProperty =
            DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(ImageButton), new PropertyMetadata());


        public Brush MousePressedBackground
        {
            get { return (Brush)GetValue(MousePressedBackgroundProperty); }
            set { SetValue(MousePressedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty MousePressedBackgroundProperty =
            DependencyProperty.Register("MousePressedBackground", typeof(Brush), typeof(ImageButton), new PropertyMetadata());
    }

    public enum ButtonType
    {
        Icon,
        IconWithTextVertical,
        IconWithTextHorizontal
    }
}