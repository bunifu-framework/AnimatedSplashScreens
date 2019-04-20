using System;
using System.Drawing;
using System.Windows.Forms;

using Bunifu.UI.WinForms.BunifuAnimatorNS;

namespace AnimatedSplashScreens
{
    public partial class Splash4 : Form
    {
        #region Constructor

        public Splash4()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region Window Animations

        #region Fields

        bool isMinimizing = false;
        bool isExpanding = false;

        #endregion

        #region Properties

        private Control Minimizer { get; set; }
        private Control Maximizer { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates parameters for use when restoring
        /// a window into view from the Taskbar.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get {
                const int WS_MINIMIZEBOX = 0x20000;
                const int CS_DBLCLKS = 0x8;

                CreateParams cp = base.CreateParams;

                cp.Style |= WS_MINIMIZEBOX;
                cp.ClassStyle |= CS_DBLCLKS;

                return cp;
            }
        }

        /// <summary>
        /// Handles the processing of a Window's messages.
        /// </summary>
        /// <param name="m">
        /// A reference to the Window's 
        /// message being passed.
        /// </param>
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020;
            const int SC_RESTORE = 0xF120;

            switch (m.Msg)
            {
                case WM_SYSCOMMAND:

                    int command = m.WParam.ToInt32();

                    if (command == SC_RESTORE)
                    {
                        FormBorderStyle = FormBorderStyle.Sizable;
                    }

                    if (command == SC_MINIMIZE)
                    {
                        Minimize();
                    }

                    break;
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// Sets-up a borderless Windows Form to 
        /// include the standard Form animations.
        /// </summary>
        /// <param name="form">The borderless <see cref="Form"/> to setup.</param>
        /// <param name="minimizer">The Form-minimizing <see cref="Control"/>.</param>
        /// <param name="maximizer">The Form-maximizing <see cref="Control"/>.</param>
        public void SetupFormAnimations(Form form, Control minimizer = null, Control maximizer = null)
        {
            Minimizer = minimizer;
            Maximizer = maximizer;

            form.SizeChanged += delegate
            {
                if (isMinimizing)
                    return;

                if (isExpanding)
                    return;

                if (WindowState != FormWindowState.Maximized)
                    FormBorderStyle = FormBorderStyle.None;

                if (WindowState != FormWindowState.Minimized)
                    FormBorderStyle = FormBorderStyle.None;
            };

            try
            {
                minimizer.Click += delegate
                {
                    Minimize();
                };

                maximizer.Click += delegate
                {
                    Maximize();
                };
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Handles the Minimize event.
        /// </summary>
        public void Minimize()
        {
            isMinimizing = true;
            FormBorderStyle = FormBorderStyle.Sizable;

            WindowState = FormWindowState.Minimized;
            FormBorderStyle = FormBorderStyle.None;

            isMinimizing = false;
        }

        /// <summary>
        /// Handles the Maximize event.
        /// </summary>
        public void Maximize()
        {
            isExpanding = true;
            FormBorderStyle = FormBorderStyle.Sizable;

            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
                FormBorderStyle = FormBorderStyle.None;

                isExpanding = false;
            }
            else if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
                FormBorderStyle = FormBorderStyle.None;

                isExpanding = false;
            }
        }

        #endregion

        #endregion

        public void PlayAnimation()
        {
            PlayFromBeginning();
        }

        public void PlayFromBeginning()
        {
            // bunifuTransition1.ShowSync(pictureBox2, true, Animation.Transparent);

            var count = 0;
            var timer = new Timer();

            timer.Interval = 1000;
            timer.Enabled = true;

            timer.Tick += delegate
            {
                count++;

                if (count == 1)
                {
                    bunifuTransition1.ShowSync(pictureBox3, true, Animation.Transparent);

                    PlayEndingAnimation();

                    timer.Stop();
                }
            };
        }

        public void PlayEndingAnimation()
        {
            pictureBox3.Enabled = true;

            var count = 0;
            var timer = new Timer();

            timer.Interval = 1000;
            timer.Enabled = true;

            timer.Tick += delegate
            {
                count++;

                if (count == 14)
                {
                    pictureBox3.Enabled = false;
                    bunifuTransition1.ShowSync(bunifuButton2, true, Animation.Transparent);

                    timer.Stop();
                }
            };
        }

        #endregion

        #region Events

        private void OnShowForm(object sender, EventArgs e)
        {
            PlayAnimation();

            SetupFormAnimations(this);
        }

        private void OnClickClose(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
