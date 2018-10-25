/*
 * 1.コピー元となるテキストの動きについてのスクリプト
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;
using UnityEngine;

public class BaseTextMotion : MonoBehaviour {

	#region variable
	private bool isAnimetionComp = false;
	private Quaternion _startRotation;
	#endregion

	public bool IsAnimetionComp{
		get{ return isAnimetionComp;}
	}

	private void OnEnable()
	{
		StartCoroutine("Motion");
		_startRotation = this.transform.localRotation;
	}


	void Update ()
	{
//		if(isAnimetionComp)Debug.Log("flag");
//		if (Time.frameCount == 180.0)StartCoroutine("Motion");
//		isAnimetionComp = false;
	}

	private IEnumerator Motion()
	{
		
		//待ち
//		int frameNum = 60;
//		for (int i = 0; i < frameNum; i++){
//			yield return null;
//		}
		int frameNum = 60;

		//回転1
		frameNum = 60;
		for (int i = 0; i < frameNum; i++){
			var dy = 30.0f / frameNum;
			this.transform.Rotate(new Vector3(0.0f, -dy, 0.0f));
			yield return null;
		}

		//回転2
		frameNum = 20;
		for (int i = 0; i < frameNum; i++){
			var dx = 25.0f / frameNum;
			this.transform.Rotate(new Vector3(dx, 0.0f, 0.0f));
			yield return null;
		}
		
		isAnimetionComp = true;
		yield return null;
		
		//`回転2	
		frameNum = 20;
		for (int i = 0; i < frameNum; i++){
			var dx = -25.0f / frameNum;
			this.transform.Rotate(new Vector3(dx, 0.0f, 0.0f));
			yield return null;
		}
		
		//`回転1	
		frameNum = 60;
		for (int i = 0; i < frameNum; i++){
			var dy = -30.0f / frameNum;
			this.transform.Rotate(new Vector3(0.0f, -dy, 0.0f));
			yield return null;
		}
		

		yield return null;
	}

	private void OnDisable()
	{
		//this.transform.rotation = _startRotation;
	}
}
