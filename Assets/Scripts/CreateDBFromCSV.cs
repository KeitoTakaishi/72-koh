using System.Collections;
using System.Collections.Generic;
using MainScene;
using UnityEngine;
using System.Linq;
using UnityEditor;
using UnityEngine.Assertions.Comparers;

[DefaultExecutionOrder(-1)]
public class CreateDBFromCSV : MonoBehaviour
{
	private CreateCSV _CreateCSV;
	private const int _num = 72;
	private int[] _CycleData;
	private List < string > _ModifyData;
	private List < float >[] _tempData;
	private List < float >[] _orderdTempData;//order後のデータ

	public List<float>[] OrderdTempData
	{
		get { return _orderdTempData;}
	}


	void Start()
	{
		Initialize();
		_CreateCSV.LoadFile(ref _ModifyData, "ModifyData");
		SevrntyTwoDataBase();// 1/1から順に格納されているので変更する必要がある	
		Order();
	}

	void Update()
	{
		
	}

	private void Initialize()
	{
		_CycleData = new int[_num];
		_ModifyData = new List <string>();
		_tempData=new List < float >[_num];
		for (int i = 0; i < _num; i++){
			_tempData[i] = new List<float>();
		}
		InitCycleData();
		_CreateCSV = GameObject.Find("CSV").GetComponent<CreateCSV>();
	}

	private void InitCycleData()
	{
		IEnumerable<int> DateSequence = Enumerable.Repeat(5, _num);
		_CycleData = DateSequence.ToArray();

		_CycleData[0] = 4;
		_CycleData[27] = 6;
		_CycleData[30] = 6;
		_CycleData[34] = 6;
		_CycleData[39] = 6;
		_CycleData[40] = 6;
		_CycleData[42] = 4;
		_CycleData[43] = 6;
		_CycleData[48] = 6;
		_CycleData[68] = 4;
		_CycleData[69] = 6;
	}
	
	//csvから72候型のデータベースを作成
	private void SevrntyTwoDataBase(){
		int CurSeason;//71まで回ったら初期化
		int CurDate;//3
		CurSeason = 0;
		CurDate = 0;


		for (int i = 0; i < _ModifyData.Count;)
		{
			if (int.Parse(_ModifyData[i].Split(':')[0]) % 4 != 0)
			{
				for (int j = 0; j < _CycleData[CurSeason]; j++)
				{
					var t = float.Parse(_ModifyData[i].Split(':')[1]);
					_tempData[CurSeason].Add(t);
					i++;
				}
				CurSeason += 1;
				if (CurSeason > 71)
				{
					CurSeason = 0;
				}
			}else{
				if (CurSeason != 11){
					for (int j = 0; j < _CycleData[CurSeason]; j++)
					{
						var t = float.Parse(_ModifyData[i].Split(':')[1]);
						_tempData[CurSeason].Add(t);
						i++;
					}
				}else{
					for (int j = 0; j < _CycleData[CurSeason]+1; j++)
					{
						var t = float.Parse(_ModifyData[i].Split(':')[1]);
						_tempData[CurSeason].Add(t);
						i++;
					}
				}
				CurSeason += 1;
				if (CurSeason > 71)
				{
					CurSeason = 0;
				}
			}
		}
	} 
	
	//利用出来る順にな並び替える2/4が先頭になるようにする
	void Order()
	{
		
		_orderdTempData = new List < float >[72];
		
		for (int i = 0; i < _CycleData.Length; i++){
			_orderdTempData[i] = _tempData[(i+7)%72];
		}
	}
}
