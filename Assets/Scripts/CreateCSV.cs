using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



namespace MainScene
{
    public class CreateCSV : MonoBehaviour
    {

        //csv data
        private List<string> data = new List<string>();

        void Start()
        {
            //存在してない場合のみ生成
            if (!(System.IO.File.Exists(Application.dataPath + "/Resources/CSV/CSVData.csv")))
            {
                ReadCSV(ref data);
                WriteCSV(ref data);
            }
        }


        #region ReadCSV
        public void ReadCSV(ref List<string> data)
        {
            var fileName = "temp";
            var csvFile = Resources.Load("csv/" + fileName) as TextAsset;
            var reader = new StringReader(csvFile.text);

            while (reader.Peek() > -1)
            {
                string lineData = reader.ReadLine();
                string[] SplitArray = lineData.Split(',');
                var _SplitList = new List<string>(SplitArray);
                _SplitList.RemoveRange(2, 2);
                data.AddRange(_SplitList);
            }
        }
        #endregion

        #region WriteCSV
        private void WriteCSV(ref List<string> data)
        {
            StreamWriter sw;
            FileInfo fi;
            string str = null;
            for (int i = 0; i < data.Count / 3; i += 4)
            {
                str += "DATA:" + data[i] + " : " + data[i + 1] + "\n";
            }
            fi = new FileInfo(Application.dataPath + "/Resources/CSV/CSVData.csv");
            sw = fi.AppendText();
            sw.WriteLine(str);
            sw.Flush();
            sw.Close();
        }
        #endregion
    }
}