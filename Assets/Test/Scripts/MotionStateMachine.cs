/**
 * コルーチンを用いたアニメーションスクリプト
 * 回転->複製->移動+色のフェード->destroy
 */


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionStateMachine : MonoBehaviour {

	void Start ()
	{
		Destroy(gameObject, 3.0f);
		StartCoroutine("Sample");
		
	}
	
	void Update () {
		
	}

	private IEnumerator Sample()
	{
//		var fromAtngle = 0.0f;
//		var toAngle = 90.0f;
//		float angle = 0.0f;
//		while (angle != 90.0f)
//		{
//			angle = Mathf.LerpAngle(fromAtngle, toAngle, Time.time);
//			transform.eulerAngles = new Vector3(0, angle, 0);
//		}

		for (int i = 0; i < 50; i++)
		{

			transform.Rotate(new Vector3(0.0f, 3.6f, 0));
			yield return null;
		}
		yield return new WaitForSeconds(0.1f);

		for (int i = 0; i < 20.0f; i++)
		{
			this.transform.position += new Vector3(0f, 0.05f, 0f);
			yield return null;
		}	
		yield return new WaitForSeconds(0.1f);
		var obj = new GameObject[4];
		var mat = new MeshRenderer[4];
		for (int i = 0; i < 4; i++)
		{
			obj[i] = Instantiate(this.gameObject, this.transform.position, this.transform.rotation);
			mat[i] = obj[i].GetComponent <MeshRenderer >();
		}

		var a = 1.0f;
		for (int i = 0; i < 20.0f; i++)
		{
			for(int j = 0; j < 4; j++)
			{
				var speed = j - 3.0f / 2.0f;
				speed *= .07f;
				obj[j].transform.position += new Vector3(speed, 0.0f, 0f);
				mat[j].material.color = new Color(1.0f, 1.0f, 1.0f, a);
			}
			a -= 0.08f;
			yield return null;
		}	
		yield break;
	}
}
