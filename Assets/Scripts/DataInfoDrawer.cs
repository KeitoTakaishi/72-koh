using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using uOSC;


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
    public GameObject sentenceGene;
    public GameObject osc;


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
    public int bufferID = 0;
    private bool flag = true;
    private int frame = 0;
    void Update()
    {
        if(flag){
            if (osc.GetComponent<OSCServer>().ID > 0)
            {
                //if(viewTextController.GetComponent<ViewTextController>().BufferID > 0){
                _id = viewTextController.GetComponent<ViewTextController>().BufferID - 1;
                flag = false;
            }
        }else{
            ++frame;
            if (frame == 500){
                flag = true;
                frame = 0;
            }
        }


        if (Time.frameCount % 10 == 0){
            _randIndex = UnityEngine.Random.Range(0, _CDBfromCSV.OrderdTempData[_id].Count);
        }


        _dateInfo.seasonName = SeasonName[_id];
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
        _id = 1;

    }

    void SetSeasonName()
    {
        SeasonName = new string[72];
        SeasonName[0] = "東風解凍" + " " + "2/4〜2/8";
        SeasonName[1] = "黄鶯睍睆" + " " + "2/9〜2/13";
        SeasonName[2] = "魚上氷" + " " + "2/14〜2/17";
        SeasonName[3] = "土脉潤起" + " " + "2/18〜2/22";
        SeasonName[4] = "霞始靆" + " " + "2/23〜2/27";
        SeasonName[5] = "草木萌動" + " " + "2/28〜3/4";
        SeasonName[6] = "蟄虫啓戸" + " " + "3/5〜3/9";
        SeasonName[7] = "桃始笑" + " " + "3/10〜3/14";
        SeasonName[8] = "菜虫化蝶" + " " + "3/15〜3/19";
        SeasonName[9] = "雀始巣" + " " + "3/20〜3/24";
        SeasonName[10] = "桜始開" + " " + "3/25〜3/29";
        SeasonName[11] = "雷乃発声" + " " + "3/30〜4/4";
        SeasonName[12] = "玄鳥至" + " " + "4/5〜4/9";
        SeasonName[13] = "鴻雁北" + " " + "4/10〜4/14";
        SeasonName[14] = "虹始見" + " " + "4/15〜4/19";
        SeasonName[15] = "葭始生" + " " + "4/20〜4/24";
        SeasonName[16] = "霜止出苗" + " " + "4/25〜4/29";
        SeasonName[17] = "牡丹華" + " " + "4/30〜5/4";
        SeasonName[18] = "蛙始鳴" + " " + "5/5〜5/9";
        SeasonName[19] = "蚯蚓出" + " " + "5/10〜5/14";
        SeasonName[20] = "竹笋生" + " " + "5/15〜5/20";
        SeasonName[21] = "蚕起食桑" + " " + "5/21〜5/25";
        SeasonName[22] = "紅花栄" + " " + "5/26〜5/30";
        SeasonName[23] = "麦秋至" + " " + "5/31〜6/4";
        SeasonName[24] = "螳螂生" + " " + "6/5〜6/9";
        SeasonName[25] = "腐草為蛍" + " " + "6/10〜6/14";
        SeasonName[26] = "梅子黄" + " " + "6/15〜6/20";
        SeasonName[27] = "乃東枯" + " " + "6/21〜6/25";
        SeasonName[28] = "菖蒲華" + " " + "6/26〜6/30";
        SeasonName[29] = "半夏生" + " " + "7/1〜7/6";
        SeasonName[30] = "温風至" + " " + "7/7〜7/11";
        SeasonName[31] = "蓮始開" + " " + "7/12〜7/16";
        SeasonName[32] = "鷹乃学習" + " " + "7/17〜7/22";
        SeasonName[33] = "桐始結花" + " " + "7/23〜7/27";
        SeasonName[34] = "土潤溽暑" + " " + "7/28〜8/1";
        SeasonName[35] = "大雨時行" + " " + "8/2〜8/6";
        SeasonName[36] = "涼風至" + " " + "8/7〜8/11";
        SeasonName[37] = "寒蝉鳴" + " " + "8/12〜8/16";
        SeasonName[38] = "蒙霧升降" + " " + "8/17〜8/22";
        SeasonName[39] = "綿柎開" + " " + "8/23〜8/27";
        SeasonName[40] = "天地始粛" + " " + "8/28〜9/1";
        SeasonName[41] = "禾乃登" + " " + "9/2〜9/6";
        SeasonName[42] = "草露白" + " " + "9/7〜9/11";
        SeasonName[43] = "鶺鴒鳴" + " "+ "9/12〜9/16";
        SeasonName[44] = "玄鳥去"+" "+ "9/17〜9/22";
        SeasonName[45] = "雷乃収声"+" "+ "9/23〜9/27";
        SeasonName[46] = "蟄虫坏戸"+" "+ "9/28〜10/2";
        SeasonName[47] = "水始涸"+" "+ "10/3〜10/7";
        SeasonName[48] = "鴻雁来"+" "+ "10/8〜10/12";
        SeasonName[49] = "菊花開"+" "+ "10/13〜10/17";
        SeasonName[50] = "蟋蟀在戸"+" "+ "10/18〜10/22";
        SeasonName[51] = "霜始降"+" "+ "10/23〜10/27";
        SeasonName[52] = "霎時施"+" "+ "10/28〜11/1";
        SeasonName[53] = "楓蔦黄"+" "+ "11/2〜11/6";
        SeasonName[54] = "山茶始開"+" "+ "11/7〜11/11";
        SeasonName[55] = "地始凍"+" "+ "11/12〜11/16";
        SeasonName[56] = "金盞香"+" "+ "11/17〜11/21";
        SeasonName[57] = "虹蔵不見" + " " + "11/22〜11/26";
        SeasonName[58] = "朔風払葉"+" "+ "11/27〜12/1";
        SeasonName[59] = "橘始黄"+" "+ "12/2〜12/6";
        SeasonName[60] = "閉塞成冬"+" "+ "12/7〜12/11";
        SeasonName[61] = "熊蟄穴"+" "+ "12/12〜12/16";
        SeasonName[62] = "鱖魚群"+" "+ "12/17〜12/21";
        SeasonName[63] = "乃東生"+" "+ "12/22〜12/26";
        SeasonName[64] = "麋角解"+" "+ "12/27〜12/31";
        SeasonName[65] = "雪下出麦"+" "+ "1/1〜1/4";
        SeasonName[66] = "芹乃栄"+" "+ "1/5〜1/9";
        SeasonName[67] = "水泉動"+ "1/10〜1/14";
        SeasonName[68] = "雉始雊"+" "+ "1/15〜1/19";
        SeasonName[69] = "款冬華"+" "+ "1/20〜1/24";
        SeasonName[70] = "水沢腹堅"+" "+ "1/25〜1/29";
        SeasonName[71] = "鶏始乳"+" "+ "1/30〜2/3";


        /*
10/3〜10/7
10/8〜10/12
10/13〜10/17
10/18〜10/22
10/23〜10/27
10/28〜11/1
11/2〜11/6
11/7〜11/11
11/12〜11/16
11/17〜11/21
11/22〜11/26
11/22〜11/26
12/2〜12/6
12/7〜12/11
12/12〜12/16
12/17〜12/21
12/22〜12/26
12/27〜12/31
1/1〜1/4
1/5〜1/9
1/10〜1/14
1/15〜1/19
1/20〜1/24
1/25〜1/29
1/30〜2/2
         */

    }


    void GenerateSentence(string seasonName,string date,float temp,float lowest, float highest, float avg)
    {
        string info = "72候: "+ seasonName +"\n"+
                      "Year:1998〜2018\n" + 
                      "Date:"+ date + "\n" +
                      "Temperature Data: " + temp +"℃"+ "\n"+
                      "Lowest Temp:" + lowest.ToString()+"℃"+"\n" +
                      "Highest Temp:" + highest.ToString()+"℃"+ "\n" +
                      "Average:" + avg.ToString()+"℃"+"\n";
        var _text = this.GetComponent < Text >();
        _text.text = info;
    }
}