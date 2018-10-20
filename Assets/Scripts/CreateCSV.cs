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
            //毎回処理をすると重いので，形成後のファイル存在してない場合のみ生成
            if (!(System.IO.File.Exists(Application.dataPath + "/Resources/CSV/ModifyData.csv")))
            {
                ReadCSV(ref data, "temp");//date,temp
                WriteCSV(ref data);//year;temp
            }else{
                Debug.Log("exist");
            }
        }
        
        #region Func
        
        //オリジナルのCSVファイルの読み込み
        public void ReadCSV(ref List<string> data, string FileName)
        {
            var csvFile = Resources.Load("csv/" + FileName) as TextAsset;
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
        
        //年度と気温データだけ抽出してファイル書き出しを行っている
        private void WriteCSV(ref List<string> data)
        {
            StreamWriter sw;
            FileInfo fi;
            string str = null;
            
            
            for (int i = 0; i < data.Count; i+=2)
            {
                //date + temperature
                data[i] = data[i].Split('/')[0]; 
                str += data[i] + ":" + data[i + 1] + "\n";
            }
            fi = new FileInfo(Application.dataPath + "/Resources/CSV/ModifyData.csv");
            sw = fi.AppendText();
            sw.WriteLine(str);
            sw.Flush();
            sw.Close();
        }
        
        //第2引数で指定したファイルをロードしてstringListへ変換
        public void LoadFile(ref List<string> data, string FileName)
        {
            var csvFile = Resources.Load("csv/" + FileName) as TextAsset;
            var reader = new StringReader(csvFile.text);
            while (reader.Peek() > -1)
            {
                string lineData = reader.ReadLine();
                data.Add(lineData);
            }
        }
        #endregion
    }
}