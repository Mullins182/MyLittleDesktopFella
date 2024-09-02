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
        private DispatcherTimer MyLittleFellaRoutine = new();


        private bool iniComplete = false;

        public MainWindow()
        {
            InitializeComponent();

            Initialize();
        }

        public void Initialize()
        {

            // DISPATCHER-TIMER
            MyLittleFellaRoutine.Tick += MyLittleFellaRoutine_Tick;
            MyLittleFellaRoutineConfig();
            // DISPATCHER-TIMER END


            iniComplete = true;
        }

        private void MyLittleFellaRoutineConfig()
        {
            FellaSound.Stop();
            FellaSound.Position = TimeSpan.Zero;
            MyLittleFellaRoutine.Stop();
            MyLittleFellaRoutine.Interval = TimeSpan.FromSeconds(rN.Next(5, 15));
            MyLittleFellaRoutine.Start();
        }

        private async void MyLittleFellaRoutine_Tick(object? sender, EventArgs e)
        {
            FellaRectPosSet();

            FellaRect.BeginAnimation(WidthProperty, FellaAnimationWidth);
            FellaRect.BeginAnimation(HeightProperty, FellaAnimationHeight);

            await Task.Delay(animationTimerMillsec);

            FellaSound.Play();

            await Task.Delay(750);

            MyLittleFellaRoutineConfig();
        }

    }
}