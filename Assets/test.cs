using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		var x = this.transform.localEulerAngles.x;
		var y = this.transform.localEulerAngles.y;
		var z = this.transform.localEulerAngles.z;
		x = Mathf.Sin(-1*z * Mathf.Deg2Rad);
		y = Mathf.Cos(x * Mathf.Deg2Rad);
		z = Mathf.Sin(x * Mathf.Deg2Rad);
		
		Debug.Log(this.transform.up);
		
	}
}
