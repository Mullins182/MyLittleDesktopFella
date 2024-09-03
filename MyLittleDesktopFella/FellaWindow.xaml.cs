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
        private Rectangle FellaRect = new();
        private BitmapImage FellaImage = new();
        private ImageBrush FellaImageBrush = new();
        private DoubleAnimation FellaAnimationWidth = new();
        private DoubleAnimation FellaAnimationHeight = new();
        private MediaPlayer FellaSound = new();
        private int imageWidth = 600;
        private int imageHeight = 500;
        private int animationTimerMillsec = 800;
        private bool initFinished = false;


        public FellaWindow()
        {
            InitializeComponent();

            MainWindow.FellaCall += FellaWindow_FellaCall;

            Init();
        }

        public async void FellaWindow_FellaCall(object? sender, EventArgs e)
        {
            while (!initFinished)
            {
                await Task.Delay(50);
            }

            FellaRectPosSet();

            FellaRect.BeginAnimation(WidthProperty, FellaAnimationWidth);
            FellaRect.BeginAnimation(HeightProperty, FellaAnimationHeight);

            await Task.Delay(animationTimerMillsec);

            FellaSound.Play();

            await Task.Delay(850);

            FellaSound.Stop();
            FellaSound.Position = TimeSpan.Zero;

            this.Close();
        }

        private void Init()
        {
            this.Topmost = true;

            // Sound ini
            FellaSound.IsMuted = true;

            FellaSound.Open(new Uri("sound/facePunch.mp3", UriKind.Relative));
            FellaSound.Stop();
            FellaSound.IsMuted = false;
            // Sound ini END

            // Canvas / Canvas Elements ini
            MainCanvas.Children.Add(FellaRect);
            MainCanvas.Background = Brushes.Transparent;
            FellaRect.Width = 0;
            FellaRect.Height = 0;
            FellaImage.BeginInit();
            FellaImage.UriSource = new Uri("pack://application:,,,/png/fist.png");
            FellaImage.EndInit();
            FellaImageBrush.ImageSource = FellaImage;
            FellaRect.Fill = FellaImageBrush;
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
            Canvas.SetLeft(FellaRect, rN.Next(0, (int)(MainCanvas.ActualWidth - FellaImage.Width)));
            Canvas.SetTop(FellaRect, rN.Next(0, (int)(MainCanvas.ActualHeight - FellaImage.Height)));
        }
    }
}
