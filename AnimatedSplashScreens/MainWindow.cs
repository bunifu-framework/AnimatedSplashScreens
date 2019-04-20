using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimatedSplashScreens
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Splash1 splash1 = new Splash1();
            splash1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Splash2 splash2 = new Splash2();
            splash2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Splash3 splash3 = new Splash3();
            splash3.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Splash4 splash4 = new Splash4();
            splash4.Show();
        }
    }
}
