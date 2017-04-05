using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace DeepObjectTrackerGUI
{
    class MouseRecorder
    {
        public struct RecordingProperties
        {
            public float speedMultiplier;
        }

        private List<Point> positions;
        private List<double> timestamps;

        public Point TopLeft { get; set; }
        public Point BottomRight { get; set; }
        public RecordingProperties Properties { get; set; }

        public MouseRecorder()
        {
            positions = new List<Point>();
            timestamps = new List<double>();
        }

        public void AddNewPosition(double timestamp, Point p)
        {
            if (p.X >= TopLeft.X && p.X <= BottomRight.X &&
                p.Y >= TopLeft.Y && p.Y <= BottomRight.Y)
            {
                positions.Add(p);
                timestamps.Add(timestamp);
            }
        }

        public void Save(string name)
        {

        }

        BackgroundWorker worker = new BackgroundWorker();
        Canvas canv;
        public void Playback(System.Windows.Controls.Canvas drawingCanvas)
        {
            canv = drawingCanvas;
            worker.WorkerReportsProgress = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;

            worker.RunWorkerAsync();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int i = e.ProgressPercentage;
            Line l = new Line();
            l.Stroke = System.Windows.Media.Brushes.Black;
            l.X1 = positions[i - 1].X;
            l.Y1 = positions[i - 1].Y;
            l.X2 = positions[i].X;
            l.Y2 = positions[i].Y;

            Canvas.SetTop(l, 0);
            Canvas.SetBottom(l, 0);

            canv.Children.Add(l);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i < positions.Count; i++)
            {
                worker.ReportProgress(i);
                System.Threading.Thread.Sleep(33);
            }
        }
    }
}
