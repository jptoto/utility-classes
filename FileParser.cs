using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ConnectCsharpToMysql
{
    public class FileParser
    {
        public static List<string> parseCSV(string path)
        {
            List<string> parsedData = new List<string>();

            try
            {
                using (StreamReader readFile = new StreamReader(path))
                {
                    string line;
                    //string[] row;

                    while ((line = readFile.ReadLine()) != null)
                    {
                        //row = line.Split(',');
                        parsedData.Add(line);
                    }
                    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return parsedData;
        }
    }
}
