using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DJimmy.GUI
{
    class ResponsiveTextBox
    {
        private readonly TextBox textBox;
        private readonly Timer timer = new Timer();
        private DateTime latestEvent;

        public ResponsiveTextBox(TextBox textBox)
        {
            this.textBox = textBox;
            this.textBox.TextChanged += TextBoxOnTextChanged;
            timer.Tick += timer_Tick;
            timer.Interval = 200;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Subtract(latestEvent).TotalSeconds > 1)
            {
                timer.Stop();

                if (TypingDone != null)
                {
                    TypingDone(this, EventArgs.Empty);
                }
            }
        }

        private void TextBoxOnTextChanged(object sender, EventArgs eventArgs)
        {
            latestEvent = DateTime.Now;

            if (!timer.Enabled)
            {
                timer.Start();
            }            
        }

        public event EventHandler TypingDone;
    }
}
