using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoRoutine : MonoBehaviour
{

	void Start()
	{
		StartCoroutine("Message");
	}

	void Update()
	{

	}

	private IEnumerator Message()
	{
		yield return new WaitForSeconds(3.0f);
		Debug.Log(Time.realtimeSinceStartup);
		yield return new WaitForSeconds(5.0f);
		Debug.Log(Time.realtimeSinceStartup);
		

	}
}