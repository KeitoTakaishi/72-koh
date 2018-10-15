using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField]
	private GameObject _main;
	[SerializeField]
	private GameObject _sub1;

	
	public GameObject Main
	{
		get { return _main;}
		set { _main = value; }
	}
	
	public GameObject Sub1
	{
		get { return _sub1;}
		set { _sub1 = value; }
	}

	void Start () {
		
	}
	
	void Update () {
		if(Input.GetKeyDown("1")) {
			_main.SetActive(!Main.activeSelf);
			_sub1.SetActive(!Sub1.activeSelf);
		}	
	}
}
