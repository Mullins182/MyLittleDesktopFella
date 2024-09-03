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
        //private readonly FellaWindow MyLittleFella = new();

        private DispatcherTimer MyLittleFellaRoutine = new();

        public static event EventHandler? FellaCall;

        private Random rN = new();

        private bool iniComplete = false;

        public MainWindow()
        {
            InitializeComponent();

            Initialize();
        }

        public void Initialize()
        {
            MyLittleFellaRoutine.Tick += MyLittleFellaRoutine_Tick;

            MyLittleFellaRoutineConfig();

            iniComplete = true;
        }

        private void MyLittleFellaRoutineConfig()
        {
            MyLittleFellaRoutine.Stop();
            MyLittleFellaRoutine.Interval = TimeSpan.FromSeconds(rN.Next(5, 10));
            MyLittleFellaRoutine.Start();
        }

        private async void MyLittleFellaRoutine_Tick(object? sender, EventArgs e)
        {
            FellaWindow MyLittleFella = new();

            MyLittleFella.Show();

            FellaCall?.Invoke(this, EventArgs.Empty);

            await Task.Delay(500);

            MyLittleFellaRoutineConfig();
        }
    }
}