using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class csvTest : MonoBehaviour
{

    //csv data
    private List<string> data = new List<string>();
    //mesh
    private MeshFilter meshFilter;
    private Mesh mesh;
    private List<Vector3> vertexList = new List<Vector3>();
    private List<int> indexList = new List<int>();


    void Start()
    {
        InitCSV(ref data);
        Debug.Log(data.Count);
        //for (int i = 0; i < data.Count / 3; i += 4)
        //{
        //    Debug.Log("DATA:" + data[i] + " : " + data[i + 1]);
        //}

        StreamWriter sw;
        FileInfo fi;
        string str = null;
        for (int i = 0; i < data.Count / 3; i += 4)
        {
            str += "DATA:" + data[i] + " : " + data[i + 1] + "\n";
        }
        fi = new FileInfo(Application.dataPath + "/CSVData.csv");
        sw = fi.AppendText();
        sw.WriteLine(str);
        sw.Flush();
        sw.Close();
    }
    void Update()
    {

    }


    #region InitCSV
    private void InitCSV(ref List<string> data)
    {
        var fileName = "temp";
        var csvFile = Resources.Load("csv/" + fileName) as TextAsset;
        var reader = new StringReader(csvFile.text);

        while (reader.Peek() > -1)
        {
            string lineData = reader.ReadLine();
            var SplitData = lineData.Split(',');
            var _SplitList = new List<string>(SplitData);
            _SplitList.RemoveRange(2,2);
            data.AddRange(_SplitList);
        }
    }
    #endregion


    #region CreateMesh
    private Mesh CreateMesh(ref List<string>data){
        var mesh = new Mesh();
        int LoopNum = 100;

        for(int i = 0; i < LoopNum; i++)
        {
            Debug.Log("DATA:" + data[i] + " : " + data[i + 1]);
            var x = -1 * LoopNum / 2.0f + i;
            //vertexList.Add(new Vector3(x, data[i+1], 0.0f));
        }

        return mesh;
    }
    #endregion
}