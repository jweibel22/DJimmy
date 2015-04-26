using System;
using System.Collections.Generic;

namespace DJimmy.Domain
{
    public class ProgressStatus : IDisposable
    {
        public int Progress { get; set; }

        public string Text { get; set; }

        public event EventHandler Progressed;

        private readonly int max;

        private readonly IDictionary<ProgressStatus, int> childStartsAt = new Dictionary<ProgressStatus, int>();

        public ProgressStatus(int max = 100)
        {
            this.max = max;
        }

        public ProgressStatus SpawnChild(int duration)
        {
            var child = new ProgressStatus(duration);
            child.Progressed += child_Progressed;
            childStartsAt.Add(child, Progress);

            return child;
        }

        void child_Progressed(object sender, EventArgs e)
        {
            var child = (ProgressStatus) sender;
            Progress = childStartsAt[child] + child.Progress;
            if (child.Text != null)
            {
                Text = child.Text;
            }

            if (Progressed != null)
            {
                Progressed(this, EventArgs.Empty);
            }
        }

        public void OnProgress(int progress)
        {
            Progress = (int)((progress / 100.0) * max);

            if (Progressed != null)
            {
                Progressed(this, EventArgs.Empty);
            }
        }

        public void OnProgress(int progress, string status)
        {
            if (progress < 0 || progress > 100)
            {
                throw new ArgumentException("A percentage was expected");
            }

            Progress = (int)((progress / 100.0) * max);
            Text = status;

            if (Progressed != null)
            {
                Progressed(this, EventArgs.Empty);
            }
        }

        public void Dispose()
        {
            foreach (var child in childStartsAt.Keys)
            {
                child.Progressed -= child_Progressed;                    
            }
        }
    }
}
