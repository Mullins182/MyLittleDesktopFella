using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MyLittleDesktopFella
{
    public partial class MainWindow : Window
    {
        private bool iniComplete = false;

        private int imageWidth = 600;
        private int imageHeight = 500;
        private int animationTimerMillsec = 800;

        private Random rN = new();

        private Canvas MainCanvas = new();
        private Rectangle FellaRect = new();
        private BitmapImage FellaImage = new();
        private ImageBrush FellaImageBrush = new();

        private DispatcherTimer MyLittleFellaRoutine = new();

        private DoubleAnimation FellaAnimationWidth = new();
        private DoubleAnimation FellaAnimationHeight = new();

        public MediaPlayer FellaSound = new();

        public MainWindow()
        {
            InitializeComponent();

            Initialize();
        }

        public async void Initialize()
        {
            FellaSound.IsMuted = true;

            await Task.Delay(750);

            FellaSound.Open(new Uri("sound/facePunch.mp3", UriKind.Relative));
            FellaSound.Stop();
            FellaSound.IsMuted = false;

            MainGrid.Children.Add(MainCanvas);
            MainCanvas.Children.Add(FellaRect);
            MainCanvas.Background = Brushes.Transparent;
            FellaRect.Width = 0;
            FellaRect.Height = 0;
            FellaImage.BeginInit();
            FellaImage.UriSource = new Uri("pack://application:,,,/png/fist.png");
            FellaImage.EndInit();
            FellaImageBrush.ImageSource = FellaImage;
            FellaRect.Fill = FellaImageBrush;

            // DISPATCHER-TIMER
            MyLittleFellaRoutine.Tick += MyLittleFellaRoutine_Tick;
            MyLittleFellaRoutineConfig();
            // DISPATCHER-TIMER END

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
            iniComplete = true;
        }

        private void MyLittleFellaRoutineConfig()
        {
            this.WindowState = WindowState.Minimized;
            FellaSound.Stop();
            FellaSound.Position = TimeSpan.Zero;
            MyLittleFellaRoutine.Stop();
            MyLittleFellaRoutine.Interval = TimeSpan.FromMinutes(rN.Next(1, 121));
            MyLittleFellaRoutine.Start();
        }

        private async void MyLittleFellaRoutine_Tick(object? sender, EventArgs e)
        {
            //this.Topmost = true;
            this.WindowState = WindowState.Maximized;

            FellaRectPosSet();

            FellaRect.BeginAnimation(WidthProperty, FellaAnimationWidth);
            FellaRect.BeginAnimation(HeightProperty, FellaAnimationHeight);

            await Task.Delay(animationTimerMillsec);

            FellaSound.Play();

            await Task.Delay(750);

            MyLittleFellaRoutineConfig();
        }

        private void FellaRectPosSet()
        {
            Canvas.SetTop(FellaRect, rN.Next(0, (int)MainCanvas.ActualHeight - imageHeight));
            Canvas.SetLeft(FellaRect, rN.Next(0, (int)MainCanvas.ActualWidth - imageWidth));
        }
    }
}