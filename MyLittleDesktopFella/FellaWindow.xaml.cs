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
using System.Windows.Threading;

namespace MyLittleDesktopFella
{
    public partial class FellaWindow : Window
    {

        private DispatcherTimer closeTimer;
        private Random rN = new();
        private Rectangle FellaRect = new();
        private BitmapImage FellaImage = new();
        private ImageBrush FellaImageBrush = new();
        private DoubleAnimation FellaAnimationWidth = new();
        private DoubleAnimation FellaAnimationHeight = new();
        private int imageWidth = 600;
        private int imageHeight = 500;
        public static bool initFinished = false;
        private int animationTimerMillsec = 800;


    public FellaWindow()
        {
            InitializeComponent();

            Init();
        }

        //public async void FellaWindow_FellaCall(object? sender, EventArgs e)
        //{
        //    while (!initFinished) { await Task.Delay(50); }

        //    FellaRectPosSet();

        //    FellaRect.BeginAnimation(WidthProperty, FellaAnimationWidth);
        //    FellaRect.BeginAnimation(HeightProperty, FellaAnimationHeight);

        //    this.Close();
        //}

        public async void FellaWindow_FellaCall(object? sender, EventArgs e)
        {
            while (!initFinished) { await Task.Delay(50); }

            FellaRectPosSet();

            FellaRect.BeginAnimation(WidthProperty, FellaAnimationWidth);
            FellaRect.BeginAnimation(HeightProperty, FellaAnimationHeight);

            // Starten Sie einen Timer, um das Fenster nach 2 Sekunden zu schließen
            closeTimer = new DispatcherTimer();
            closeTimer.Interval = TimeSpan.FromSeconds(2);
            closeTimer.Tick += (s, args) => {
                this.Close();
                closeTimer.Stop();
            };
            closeTimer.Start();
        }

        private void Init()
        {
            this.Topmost = true;

            MainWindow.FellaCall += FellaWindow_FellaCall;

            // Canvas / Canvas Elements ini
            MainCanvas.Width = 800;
            MainCanvas.Height = 600;
            MainCanvas.Children.Add(FellaRect);
            MainCanvas.Background = Brushes.Transparent;
            FellaRect.Width = 0;
            FellaRect.Height = 0;


            //FellaImage.BeginInit();
            //FellaImage.UriSource = new Uri("pack://application:,,,/png/fist.png");
            //FellaImage.DownloadCompleted += (s, e) => {
            //    FellaImageBrush.ImageSource = FellaImage;
            //    FellaRect.Fill = FellaImageBrush;
            //};            
            //FellaImage.EndInit();

            FellaImage.BeginInit();
            FellaImage.UriSource = new Uri("pack://application:,,,/MyLittleDesktopFella;component/png/fist.png", UriKind.Absolute);
            FellaImage.DownloadCompleted += (s, e) => {
                FellaImageBrush.ImageSource = FellaImage;
                FellaRect.Fill = FellaImageBrush;
                initFinished = true;
            };
            FellaImage.EndInit();

            //FellaRect.Fill = FellaImageBrush;
            // Canvas / Canvas Elements ini

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

            initFinished = true;
        }

        private void FellaRectPosSet()
        {
            int fWidth = (int)(MainCanvas.Width - FellaImage.Width);
            int fHeight = (int)(MainCanvas.Height - FellaImage.Height);

            //Canvas.SetLeft(FellaRect, rN.Next(0, fWidth));
            //Canvas.SetTop(FellaRect, rN.Next(0, fHeight));

            Canvas.SetLeft(FellaRect, 10);
            Canvas.SetTop(FellaRect, 10);

        }
    }
}
