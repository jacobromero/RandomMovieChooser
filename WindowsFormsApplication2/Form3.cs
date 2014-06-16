/*This file is part of Random Movie Chooser. Designed and Written by Jacob Romero

    Random Movie Chooser is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Random Movie Chooser is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Random Movie Chooser.  If not, see <http://www.gnu.org/licenses/>.*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form3 : Form
    {
        private static Process startMovie = new Process();
        private bool eventHandled = false;

        public Form3()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainApp.ActiveForm.Show();
            this.Close(); 
            
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label1.Text = Engine.movieString;
        }

        private void yesButton_Click(object sender, EventArgs e)
        {
            startMovie.StartInfo.FileName = Engine.movieString;
            startMovie.EnableRaisingEvents = true;
            startMovie.Exited += new EventHandler(startMovie_Exited);
            try
            {
                startMovie.Start();
                while (!eventHandled)
                {
                    this.WindowState = FormWindowState.Minimized;
                }
                this.WindowState = FormWindowState.Normal;
                eventHandled = false;
            }
            catch (Exception error)
            {
                MessageBox.Show("An Error Occured in the process of choosing a moive.\nMost likely you haven't choosen and directories for me to choose from. :(");
            }
            
        }

        private void startMovie_Exited(object sender, EventArgs e)
        {
            eventHandled = true;       
        }

        private void noButton_Click(object sender, EventArgs e)
        {
            Engine.getMovie();
            label1.Text = Engine.movieString;
        }
    }
}
