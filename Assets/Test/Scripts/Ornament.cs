using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ornament : MonoBehaviour
{
	public GameObject pref;
	private GameObject[] obj;
	private int num = 50;
	void Start () {
		obj = new GameObject[num];
		for (int i = 0; i < num; i++)
		{
			var rad = 15;
			obj[i] = Instantiate(pref, new Vector3(rad*Random.insideUnitCircle.x, rad*Random.insideUnitCircle.y, transform.position.z), Quaternion.identity) as GameObject;
		}
	}
	
	
	void Update () {
		foreach (var o in obj)
		{
			o.transform.position -= new Vector3(0f, 0f, Random.RandomRange(0.05f, 0.1f));
		}
	}
}
