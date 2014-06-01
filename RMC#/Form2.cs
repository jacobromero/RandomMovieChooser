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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace WindowsFormsApplication2
{
    public partial class Form2 : Form
    {
        public static string currentDir = "";

        public Form2()
        {
            InitializeComponent();
            if (!File.Exists("Settings.ini"))
            {
                File.Create("Settings.ini").Close();
            }
            System.IO.StreamReader settingsLoad = new System.IO.StreamReader("Settings.ini");
            string line;
            int counter = 0;
            while ((line = settingsLoad.ReadLine()) != null)
            {
                if (!line.Equals("") && !line.Contains("exe"))
                {
                    this.dataGridView1.Rows.Add(line);
                    counter++;
                }
            }
            settingsLoad.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.ActiveControl = dataGridView1;
            
            try
            {
                dataGridView1.Rows[0].Selected = false;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String path = "";
            String mediaString = "";
            FolderBrowserDialog mediaDialog = new FolderBrowserDialog();
            DialogResult result = mediaDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
               path = mediaDialog.SelectedPath;
               this.dataGridView1.Rows.Add(path);
            }

            if (!path.Equals(""))
            {
                bool control = true;
                String currentLine = "";
                var oldLines = System.IO.File.ReadAllLines("Settings.ini");
                var newLines = oldLines.Where(line => (!line.Contains("exe") && !line.Equals("")));
                
                if (newLines != null)
                {
                    System.IO.File.WriteAllLines("Settings1.ini", newLines);
                }
                
                StreamReader settingFile = new StreamReader("Settings.ini");
                StreamWriter settings = new StreamWriter("Settings1.ini", true);
                currentLine = settingFile.ReadLine();
                while (currentLine != null && control)
                {
                    if (currentLine.Contains("exe"))
                    {
                        mediaString = currentLine;
                        control = false;
                        break;
                    }
                    currentLine = settingFile.ReadLine();
                }
                
                settings.Write(path);
               
                if (!mediaString.Equals(""))
                {
                    settings.Write("\n" + mediaString);
                }

                settings.Close();
                settingFile.Close();

                File.Delete("Settings.ini");
                File.Move("Settings1.ini", "Settings.ini");
                File.Delete("Settings1.ini");
                
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = dataGridView1.CurrentCell.RowIndex;
            int columnindex = dataGridView1.CurrentCell.ColumnIndex;

            currentDir = dataGridView1.Rows[rowindex].Cells[columnindex].Value.ToString();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            var oldLines = System.IO.File.ReadAllLines("Settings.ini");
            var newLines = oldLines.Where(line => !line.Equals(currentDir));
            System.IO.File.WriteAllLines("Settings.ini", newLines);

            dataGridView1.Rows.Clear();
           
            System.IO.StreamReader settingsLoad = new System.IO.StreamReader("Settings.ini");
            string lineToWrite;
            while ((lineToWrite = settingsLoad.ReadLine()) != null)
            {
                if (!lineToWrite.Contains("exe"))
                    this.dataGridView1.Rows.Add(lineToWrite);
                else if (lineToWrite.Equals(""))
                {
                    continue;
                }
            }
            settingsLoad.Close();
        }
    }
}
