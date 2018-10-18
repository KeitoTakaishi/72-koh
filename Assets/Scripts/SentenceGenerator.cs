/*
説明の文章を表示するためのスクリプト
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using System.IO;
using System.Linq;
using SimpleJSON;
using System.Text;
using System.Xml;
using uOSC;

[DefaultExecutionOrder(-1)]
public class SentenceGenerator : MonoBehaviour
{
    #region Variable

    private string m_jsonFilePath;
    private JSONNode m_jsonData;
    private int num = 72;
    private string[] SenData;
    public GameObject OCS;
    private OSCServer oscServer;
    private int tempId=0;
    TextMesh tm;
    Vector2 screenSize;
    public Camera cam;
    private bool isChanging = true;

    #endregion

    void Start()
    {
        ReadFile();
        SenData = new String[num];
        InitStr(ref SenData);    
        LoadData(ref SenData);
        ModifyData(10);
        oscServer = OCS.GetComponent < OSCServer >();
        tm = this.GetComponent<TextMesh>();
    }

    void Update()
    {
        var w = Screen.width;
        var h = Screen.height;
        Vector3 anchor = new Vector3(w*0.5f, h*0.5f, 0.0f);
      
        Vector3 screen_point = anchor;
        screen_point.z = 10.0f; 
        this.transform.position = Camera.main.ScreenToWorldPoint(screen_point);
        Judge(ref tempId, oscServer.ID);
    }



    //jsonDataを生成
    void ReadFile()
    {
        m_jsonFilePath = Application.streamingAssetsPath + "/Json/ExpressSentence.json";
        string fileText = "";
       
        FileInfo file = new FileInfo(m_jsonFilePath);
        
        using (StreamReader sr = new StreamReader(file.OpenRead(), Encoding.UTF8))
        {
            fileText = sr.ReadToEnd();
            // JSONをパースして値を取り出す
            m_jsonData = JSONNode.Parse(fileText);
        }
        
    }
    
    void LoadData(ref String[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = m_jsonData["SenData"][i]["Sen"];
        }
    }

    void InitStr(ref string[] str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            str[i] = "";
        }
    }

    void Judge(ref int temp, int oscID)
    {
        if (temp != oscID)
        {
            SelectData(oscID);
            temp = oscID;
        }
    }
    //offsetによって2/4の東風解凍からのスタートになっている
    void SelectData(int id)
    {
        tm.text = SenData[id - 1];
    }
    
    //modify data->改行の挿入
    void ModifyData(int senLen)
    {
        for (int id = 1; id <= 72; id++)
        {
            var len = SenData[id - 1].Length;
            var paraNum = len / senLen;
            string temp = null;

            for (int i = 0; i < paraNum; i++)
            {
                SenData[id - 1] = SenData[id - 1].Insert(senLen * (i + 1) + i, "\n");
            }
        }
    }

}