using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityOSC;
using System.Linq;


struct DateInfo
{
    public string seasonName;
    public string date;
    public float temp;
    public float highestTemp;
    public float lowestTemp;
    public float avgTemp;

}

public class DataInfoDrawer : MonoBehaviour
{
    #region variables
    public GameObject createDataBase;
    public GameObject viewTextController;

    private ViewTextController _viewTextController;
    private CreateDBFromCSV _CDBfromCSV;
    private Text _text;
    private string _template;
    private int[] _orderedSysleDates;
    private int _id;
    private DateInfo _dateInfo;
    private string[] SeasonName;
    private int _randIndex;
    #endregion

    void Start()
    {
        Init();
        SetSeasonName();
        var l = _CDBfromCSV.OrderdTempData[0];

        _orderedSysleDates = new int[72];
        for(int i = 0; i < _CDBfromCSV.CycleData.Length; i++){
             _orderedSysleDates[i] = _CDBfromCSV.CycleData[(i + 7) % 72];
        }
        
        
    }

    void Update()
    {
        if (!_viewTextController._isAccepted)
        {
            _id = _viewTextController.OscId;
        }

        if (_id < 0 || _id == null) _id = 0;

        if (Time.frameCount % 10 == 0){
            _randIndex = UnityEngine.Random.Range(0, _CDBfromCSV.OrderdTempData[_id].Count);
        }

        _dateInfo.seasonName = SeasonName[_id-1];
        _dateInfo.date = _CDBfromCSV.OrderdDateData[_id][_randIndex];
        _dateInfo.temp = _CDBfromCSV.OrderdTempData[_id][_randIndex];
        _dateInfo.highestTemp = _CDBfromCSV.OrderdTempData[_id].Max();
        _dateInfo.lowestTemp = _CDBfromCSV.OrderdTempData[_id].Min();
        _dateInfo.avgTemp = _CDBfromCSV.OrderdTempData[_id].Average();
        
        
        GenerateSentence(_dateInfo.seasonName, _dateInfo.date, _dateInfo.temp, _dateInfo.lowestTemp
            , _dateInfo.highestTemp, _dateInfo.avgTemp);
    }

    void Init()
    {
        _viewTextController = viewTextController.GetComponent < ViewTextController >();
        _CDBfromCSV = createDataBase.GetComponent < CreateDBFromCSV >();
        _dateInfo = new DateInfo();
        _randIndex = UnityEngine.Random.Range(0, _CDBfromCSV.OrderdTempData[_id].Count);

    }

    void SetSeasonName()
    {
        SeasonName = new string[72];
        SeasonName[0] = "東風解凍";
        SeasonName[1] = "黄鶯睍睆";
        SeasonName[2] = "魚上氷";
        SeasonName[3] = "土脉潤起";
        SeasonName[4] = "霞始靆";
        SeasonName[5] = "草木萌動";
        SeasonName[6] = "蟄虫啓戸";
        SeasonName[7] = "桃始笑";
        SeasonName[8] = "菜虫化蝶";
        SeasonName[9] = "雀始巣";
        SeasonName[10] = "桜始開";
        SeasonName[11] = "雷乃発声";
        SeasonName[12] = "玄鳥至";
        SeasonName[13] = "鴻雁北";
        SeasonName[14] = "虹始見";
        SeasonName[15] = "葭始生";
        SeasonName[16] = "霜止出苗";
        SeasonName[17] = "牡丹華";
        SeasonName[18] = "蛙始鳴";
        SeasonName[19] = "蚯蚓出";
        SeasonName[20] = "竹笋生";
        SeasonName[21] = "蚕起食桑";
        SeasonName[22] = "紅花栄";
        SeasonName[23] = "麦秋至";
        SeasonName[24] = "螳螂生";
        SeasonName[25] = "腐草為蛍";
        SeasonName[26] = "梅子黄";
        SeasonName[27] = "乃東枯";
        SeasonName[28] = "菖蒲華";
        SeasonName[29] = "半夏生";
        SeasonName[30] = "温風至";
        SeasonName[31] = "蓮始開";
        SeasonName[32] = "鷹乃学習";
        SeasonName[33] = "桐始結花";
        SeasonName[34] = "土潤溽暑";
        SeasonName[35] = "大雨時行";
        SeasonName[36] = "涼風至";
        SeasonName[37] = "寒蝉鳴";
        SeasonName[38] = "蒙霧升降";
        SeasonName[39] = "綿柎開";
        SeasonName[40] = "天地始粛";
        SeasonName[41] = "禾乃登";
        SeasonName[42] = "草露白";
        SeasonName[43] = "鶺鴒鳴";
        SeasonName[44] = "玄鳥去";
        SeasonName[45] = "雷乃収声";
        SeasonName[46] = "蟄虫坏戸";
        SeasonName[47] = "水始涸";
        SeasonName[48] = "鴻雁来";
        SeasonName[49] = "菊花開";
        SeasonName[50] = "蟋蟀在戸";
        SeasonName[51] = "霜始降";
        SeasonName[52] = "霎時施";
        SeasonName[53] = "楓蔦黄";
        SeasonName[54] = "山茶始開";
        SeasonName[55] = "地始凍";
        SeasonName[56] = "金盞香";
        SeasonName[57] = "虹蔵不見";
        SeasonName[58] = "朔風払葉";
        SeasonName[59] = "橘始黄";
        SeasonName[60] = "閉塞成冬";
        SeasonName[61] = "熊蟄穴";
        SeasonName[62] = "鱖魚群";
        SeasonName[63] = "乃東生";
        SeasonName[64] = "麋角解";
        SeasonName[65] = "雪下出麦";
        SeasonName[66] = "芹乃栄";
        SeasonName[67] = "水泉動";
        SeasonName[68] = "雉始雊";
        SeasonName[69] = "款冬華";
        SeasonName[70] = "水沢腹堅";
        SeasonName[71] = "鶏始乳";
        
    }


    void GenerateSentence(string seasonName,string date,float temp,float lowest, float highest, float avg)
    {
        string info = "72候: "+ seasonName +"\n"+
                      "Year:1998 ~ 2018\n" + 
                      "Date:"+ date + "\n" +
                      "Temperature Data: " + temp +"℃"+ "\n"+
                      "Lowest Temp:" + lowest.ToString()+"℃"+"\n" +
                      "Highest Temp:" + highest.ToString()+"℃"+ "\n" +
                      "Agerage:" + avg.ToString()+"℃"+"\n";
        var _text = this.GetComponent < Text >();
        _text.text = info;
    }
}