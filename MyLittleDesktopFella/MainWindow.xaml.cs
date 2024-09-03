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

        private readonly DispatcherTimer MyLittleFellaRoutine = new();

        public static event EventHandler? FellaCall;

        private readonly Random rN = new();

        private bool iniComplete = false;

        public MainWindow()
        {
            InitializeComponent();

            Initialize();
        }

        public void Initialize()
        {
            MyLittleFellaRoutine.Tick += MyLittleFellaRoutine_Tick;

            ChooseAnimTimerStartLabel.Content = $"Random Fella Show-Up From: {ChooseAnimTimerStartSlider.Value} Minutes";
            ChooseAnimTimerEndLabel.Content = $"Random Fella Show-Up Till: {ChooseAnimTimerEndSlider.Value} Minutes";

            MyLittleFellaRoutineConfig(1, 2);

            iniComplete = true;
        }

        private void MyLittleFellaRoutineConfig(int x, int y)
        {
            MyLittleFellaRoutine.Stop();
            MyLittleFellaRoutine.Interval = TimeSpan.FromMinutes(rN.Next(x, y));
            MyLittleFellaRoutine.Start();
        }

        private async void MyLittleFellaRoutine_Tick(object? sender, EventArgs e)
        {
            FellaWindow MyLittleFella = new();

            MyLittleFella.Show();

            FellaCall?.Invoke(this, EventArgs.Empty);

            await Task.Delay(500);

            MyLittleFellaRoutineConfig((int)ChooseAnimTimerStartSlider.Value, (int)ChooseAnimTimerEndSlider.Value);
        }

        // Slider Events
        private void ChooseAnimTimerStartSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!iniComplete) { return; }
            ChooseAnimTimerStartLabel.Content = $"Random Fella Show-Up From: {ChooseAnimTimerStartSlider.Value} Minutes";
            ChooseAnimTimerEndSlider.Value = ChooseAnimTimerStartSlider.Value + 1;
            ChooseAnimTimerEndSlider.Minimum = ChooseAnimTimerStartSlider.Value + 1;
            MyLittleFellaRoutineConfig((int)ChooseAnimTimerStartSlider.Value, (int)ChooseAnimTimerEndSlider.Value);
        }

        private void ChooseAnimTimerEndSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!iniComplete) { return; }
            ChooseAnimTimerEndLabel.Content = $"Random Fella Show-Up Till: {ChooseAnimTimerEndSlider.Value} Minutes";
            MyLittleFellaRoutineConfig((int)ChooseAnimTimerStartSlider.Value, (int)ChooseAnimTimerEndSlider.Value);
        }

        // Slider Events END !!!
    }
}