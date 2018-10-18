using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class AsyncTest : MonoBehaviour {

	void Start () {
		asynctes();
		
	}
	
	void Update () {
		
	}

	async void asynctes()
	{
		//Debug.Log(DateTime.Now);
		Debug.Log(Thread.CurrentThread.ManagedThreadId);
		await Task.Delay(5000);
		//Debug.Log(DateTime.Now);
		Debug.Log(Thread.CurrentThread.ManagedThreadId);
	}
}
