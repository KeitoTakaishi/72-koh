using System.Collections;
using System.Collections.Generic;
using MainScene;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class CreateDBFromCSV : MonoBehaviour
{
	private CreateCSV _CreateCSV;
	private const int num = 72;
	private int[] CycleData;
	private List < string > ModifyData;
	private List < float >[] tempData;

	void Start()
	{
		Initialize();
		_CreateCSV.LoadFile(ref ModifyData, "ModifyData");
		SevrntyTwoDataBase();// 1/1から順に格納されているので変更する必要がある		
	}

	void Update()
	{
	
	}

	private void Initialize()
	{
		CycleData = new int[num];
		ModifyData = new List <string>();
		tempData=new List < float >[num];
		for (int i = 0; i < num; i++){
			tempData[i] = new List<float>();
		}
		InitCycleData();
		_CreateCSV = GameObject.Find("CSV").GetComponent<CreateCSV>();
	}

	private void InitCycleData()
	{
		IEnumerable<int> DateSequence = Enumerable.Repeat(5, num);
		CycleData = DateSequence.ToArray();

		CycleData[0] = 4;
		CycleData[27] = 6;
		CycleData[30] = 6;
		CycleData[34] = 6;
		CycleData[39] = 6;
		CycleData[40] = 6;
		CycleData[42] = 4;
		CycleData[43] = 6;
		CycleData[48] = 6;
		CycleData[68] = 4;
		CycleData[69] = 6;
	}
	
	//csvから72候型のデータベースを作成
	private void SevrntyTwoDataBase(){
		int CurSeason;//71まで回ったら初期化
		int CurDate;//3
		CurSeason = 0;
		CurDate = 0;


		for (int i = 0; i < ModifyData.Count;)
		{
			if (int.Parse(ModifyData[i].Split(':')[0]) % 4 != 0)
			{
				for (int j = 0; j < CycleData[CurSeason]; j++)
				{
					var t = float.Parse(ModifyData[i].Split(':')[1]);
					tempData[CurSeason].Add(t);
					i++;
				}
				CurSeason += 1;
				if (CurSeason > 71)
				{
					CurSeason = 0;
				}
			}else{
				if (CurSeason != 11){
					for (int j = 0; j < CycleData[CurSeason]; j++)
					{
						var t = float.Parse(ModifyData[i].Split(':')[1]);
						tempData[CurSeason].Add(t);
						i++;
					}
				}else{
					for (int j = 0; j < CycleData[CurSeason]+1; j++)
					{
						var t = float.Parse(ModifyData[i].Split(':')[1]);
						tempData[CurSeason].Add(t);
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
	
	//利用出来る順にな並び替える	2/4が先頭になるようにする
	void Order()
	{
		
	}
}
