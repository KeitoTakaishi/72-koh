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
        Debug.Log("start Coroutine");
		StartCoroutine("Motion");
		_startRotation = this.transform.localRotation;
	}


	void Update ()
	{

	}
    
    private bool buffer = false;
	private IEnumerator Motion()
	{

        isAnimetionComp = false;
        int frameNum = 300;
        for (int i = 0; i < frameNum; i++){
            yield return null;
        }


        float randX = UnityEngine.Random.Range(-50, 50);
        float randY= UnityEngine.Random.Range(-90, 90);

        //回転1
        frameNum = 50;
		for (int i = 0; i < frameNum; i++){
            //var dy = 90.0f / frameNum;
            var dy = randY / frameNum;
            this.transform.Rotate(new Vector3(0.0f, -dy, 0.0f));
			yield return null;
		}

		//回転2
		frameNum = 50;
		for (int i = 0; i < frameNum; i++){
            //var dx = 25.0f / frameNum;
            var dx = randX / frameNum;
            this.transform.Rotate(new Vector3(dx, 0.0f, 0.0f));
			yield return null;
		}
		
		
        isAnimetionComp = true;
        if (isAnimetionComp)
        {
            Debug.Log("isAnimComp!!!!!!!");
        }

        //`回転2  
        frameNum = 50;
        for (int i = 0; i < frameNum; i++)
        {
            //var dx = -25.0f / frameNum;
            var dx = -randX / frameNum;
            this.transform.Rotate(new Vector3(dx, 0.0f, 0.0f));
            yield return null;
        }

        //`回転1  
        frameNum = 50;
        for (int i = 0; i < frameNum; i++)
        {
            //var dy = -90.0f / frameNum;
            var dy = -randY / frameNum;
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
