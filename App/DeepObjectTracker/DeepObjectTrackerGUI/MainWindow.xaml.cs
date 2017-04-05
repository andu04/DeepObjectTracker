using RecurrentNeuralNetworkCUDA_Wrapper;
using System.Windows;


namespace DeepObjectTrackerGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //RecurrentNeuralNetworkWrapper rnnWrapper;
        MouseRecorder recorder;

        public MainWindow()
        {
            InitializeComponent();

            recorder = new MouseRecorder();

            drawingCanvas.Focus();
            //rnnWrapper = new RecurrentNeuralNetworkWrapper();
        }

        #region Dependency properties



        public float TrueXPosition
        {
            get { return (float)GetValue(TrueXPositionProperty); }
            set { SetValue(TrueXPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TrueXPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrueXPositionProperty =
            DependencyProperty.Register("TrueXPosition", typeof(float), typeof(MainWindow), new PropertyMetadata(.0f));




        public float TrueYPosition
        {
            get { return (float)GetValue(TrueYPositionProperty); }
            set { SetValue(TrueYPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TrueYPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrueYPositionProperty =
            DependencyProperty.Register("TrueYPosition", typeof(float), typeof(MainWindow), new PropertyMetadata(.0f));
        private bool ctrlIsDown;




        #endregion

        private void exitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        private void drawingCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                var mousePosition = e.GetPosition(drawingCanvas);
                TrueXPosition = (float)mousePosition.X;
                TrueYPosition = (float)mousePosition.Y;

                if (ctrlIsDown)
                {
                    string strTimestamp = System.DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
                    double timestamp = long.Parse(strTimestamp) / 1000.0;
                    recorder.AddNewPosition(timestamp, mousePosition);
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(string.Format("Method: {0}\nReason: {1}", "drawingCanvas_MouseMove", ex.Message));
                throw;
            }

        }

        private void drawingCanvas_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                var mousePosition = e.GetPosition(drawingCanvas);
                TrueXPosition = (float)mousePosition.X;
                TrueYPosition = (float)mousePosition.Y;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(string.Format("Method: {0}\nReason: {1}", "drawingCanvas_MouseEnter", ex.Message));
            }
        }

        private void drawingCanvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                var mousePosition = e.GetPosition(drawingCanvas);
                TrueXPosition = (float)mousePosition.X;
                TrueYPosition = (float)mousePosition.Y;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(string.Format("Method: {0}\nReason: {1}", "drawingCanvas_MouseLeave", ex.Message));
            }
        }

        private void drawingCanvas_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (e.Key == System.Windows.Input.Key.LeftCtrl || e.Key == System.Windows.Input.Key.RightCtrl)
                {
                    recorder.TopLeft = new Point(0, 0);
                    recorder.BottomRight = new Point(drawingCanvas.ActualWidth, drawingCanvas.ActualHeight);
                    recorder.Properties = new MouseRecorder.RecordingProperties() { speedMultiplier = 1 };

                    ctrlIsDown = true;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(string.Format("Method: {0}\nReason: {1}", "drawingCanvas_KeyDown", ex.Message));
            }
        }

        private void drawingCanvas_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (e.Key == System.Windows.Input.Key.LeftCtrl || e.Key == System.Windows.Input.Key.RightCtrl)
                {
                    ctrlIsDown = false;
                    recorder.Playback(drawingCanvas);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(string.Format("Method: {0}\nReason: {1}", "drawingCanvas_KeyUp", ex.Message));
            }
        }
    }
}
