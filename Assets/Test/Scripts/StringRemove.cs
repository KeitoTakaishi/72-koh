using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class StringRemove : MonoBehaviour
{
	private string Def;
	private int index = 0;
	private int startIndex = 0;
	private string sen;
	void Start ()
	{
		Def = "暖かい春の風が、冬の間張りつめていた氷を解かし始める頃。いよいよ春の暖かい足音が聞こえ始めてきました。";
		index = Def.IndexOf("、");
		while (index != -1)
		{
			var buf = Def.Substring(startIndex, index+1);
			//Debug.Log(buf);
			sen += buf+"\n";
			//Debug.Log(sen);
			Def = Def.Remove(0, index + 1);
			index = Def.IndexOf("、");
			if (index == -1)
			{
				index = Def.IndexOf("。");
			}
		}
		Debug.Log(sen);
		

	}
	
	
	void Update () {
		
	}
}
