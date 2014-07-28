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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    class Engine
    {
        private static String[] directories;
        private static String[] files;
        private static String[] fileTypes = { "*.mkv", "*.mp4", "*.mpeg", "*.avi", "*.m4v", "*.wmv" };
        private static int ranDir = 0;
        private static Random rand = new Random();
        private static string currentLine, mediaString;
        public static string movieString;

        public static void getMovie()
        {
            getMainDirs();
            loadFiles();
            pickMovie();
        }

        private static void getMainDirs()
        {
            List<string> dir = new List<string>();
            StreamReader read = new StreamReader("Settings.ini");

            currentLine = read.ReadLine();
            while (currentLine != null)
            {
                if (currentLine.Contains("exe"))
                {
                    mediaString = currentLine;
                    break;
                }
                dir.Add(currentLine);
                currentLine = read.ReadLine();
            }
            read.Close();
            directories = dir.ToArray();
        }

        private static void loadFiles()
        {
            List<string> filesTmp = new List<string>();
            List<string> selectedFiles = new List<string>();

            for (int i = 0; i < directories.Length; i++)
            {
                foreach (string extensions in fileTypes)
                {
                    if (Directory.Exists(directories[i]))
                        filesTmp.AddRange(Directory.GetFiles(directories[i], extensions, SearchOption.AllDirectories));
                }
            }

            files = filesTmp.ToArray();
         }

        public static void pickMovie()
        {
            ranDir = rand.Next(0, files.Length);
            try
            {
                movieString = (string)files[ranDir];
            }
            catch (Exception e)
            {
                MessageBox.Show("An Error Occured in the process of choosing a moive.\nMost likely you haven't choosen and directories for me to choose from. :(");
            }
        }
    }
}