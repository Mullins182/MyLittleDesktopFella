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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyLittleDesktopFella
{
    public partial class FellaWindow : Window
    {
        private Random rN = new();
        public Canvas MainCanvas = new();
        public Rectangle FellaRect = new();
        public BitmapImage FellaImage = new();
        public ImageBrush FellaImageBrush = new();
        public DoubleAnimation FellaAnimationWidth = new();
        public DoubleAnimation FellaAnimationHeight = new();
        public MediaPlayer FellaSound = new();
        public int imageWidth = 600;
        public int imageHeight = 500;
        public int animationTimerMillsec = 800;


        public FellaWindow()
        {
            InitializeComponent();
            Init();
        }

        private async void Init()
        {
            this.Topmost = true;

            FellaSound.IsMuted = true;

            await Task.Delay(750);

            FellaSound.Open(new Uri("sound/facePunch.mp3", UriKind.Relative));
            FellaSound.Stop();
            FellaSound.IsMuted = false;

            MainCanvas.Children.Add(FellaRect);
            MainCanvas.Background = Brushes.Transparent;
            FellaRect.Width = 0;
            FellaRect.Height = 0;
            FellaImage.BeginInit();
            FellaImage.UriSource = new Uri("pack://application:,,,/png/fist.png");
            FellaImage.EndInit();
            FellaImageBrush.ImageSource = FellaImage;
            FellaRect.Fill = FellaImageBrush;

            // DOUBLE ANIMATIONS
            FellaAnimationWidth.Duration = TimeSpan.FromMilliseconds(animationTimerMillsec);
            //FellaAnimationWidth.AutoReverse = true;
            FellaAnimationWidth.From = 0;
            FellaAnimationWidth.To = imageWidth;

            FellaAnimationHeight.Duration = TimeSpan.FromMilliseconds(animationTimerMillsec);
            //FellaAnimationHeight.AutoReverse = true;
            FellaAnimationHeight.From = 0;
            FellaAnimationHeight.To = imageHeight;
            // DOUBLE ANIMATION END

        }

        private void FellaRectPosSet()
        {
            this.Visibility = Visibility.Visible;

            Canvas.SetTop(FellaRect, rN.Next(0, 800));
            Canvas.SetLeft(FellaRect, rN.Next(0, 1000));
        }

        private async void ShowUpFella()
        {
        }
    }
}
