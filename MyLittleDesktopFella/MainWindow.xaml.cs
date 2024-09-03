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

            LabelContentsForTimeChoiceSliders(true, true);

            MyLittleFellaRoutineConfig(1, 2);

            iniComplete = true;
        }

        private void MyLittleFellaRoutineConfig(int x, int y)
        {
            MyLittleFellaRoutine.Stop();
            MyLittleFellaRoutine.Interval = TimeSpan.FromMinutes(rN.Next(x, y + 1));
            MyLittleFellaRoutine.Start();
        }

        private async void MyLittleFellaRoutine_Tick(object? sender, EventArgs e)
        {
            FellaWindow MyLittleFella = new();

            MyLittleFella.Show();

            FellaCall?.Invoke(this, EventArgs.Empty);

            while (MyLittleFella.IsLoaded) { await Task.Delay(50); }

            MyLittleFellaRoutineConfig((int)ChooseAnimTimerStartSlider.Value, (int)ChooseAnimTimerEndSlider.Value);
        }

        private void LabelContentsForTimeChoiceSliders(bool startSlider, bool endSlider)
        {
            if (startSlider) { ChooseAnimTimerStartLabel.Content = $"Random Fella Show-Up From: {ChooseAnimTimerStartSlider.Value} Minutes"; }

            if (endSlider) { ChooseAnimTimerEndLabel.Content = $"Random Fella Show-Up Till: {ChooseAnimTimerEndSlider.Value} Minutes"; }
        }

        // Slider Events
        private void ChooseAnimTimerStartSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!iniComplete) { return; }
            LabelContentsForTimeChoiceSliders(true, false);
            ChooseAnimTimerEndSlider.Value = ChooseAnimTimerStartSlider.Value + 1;
            ChooseAnimTimerEndSlider.Minimum = ChooseAnimTimerStartSlider.Value + 1;
            MyLittleFellaRoutineConfig((int)ChooseAnimTimerStartSlider.Value, (int)ChooseAnimTimerEndSlider.Value);
        }

        private void ChooseAnimTimerEndSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!iniComplete) { return; }
            LabelContentsForTimeChoiceSliders(false, true);
            MyLittleFellaRoutineConfig((int)ChooseAnimTimerStartSlider.Value, (int)ChooseAnimTimerEndSlider.Value);
        }

        // Slider Events END !!!
    }
}