using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization

	private void OnEnable()
	{
		Debug.Log("enable");
	}

	private void Awake()
	{
		Debug.Log("awake");
	}

	void Start () {
		Debug.Log("start");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	private void OnDisable()
	{
		Debug.Log("disable");
	}
}
