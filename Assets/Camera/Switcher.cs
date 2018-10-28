
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Switcher: MonoBehaviour
{
	[SerializeField] Transform[] _targetList;
	[SerializeField] float _interval = 5;
	
	
	IEnumerator Start () {
		var follower = GetComponent<Follow>();
		yield return null;
		
		while (true)
		{
			foreach (var target in _targetList)
			{
				follower.target = target;
				yield return new WaitForSeconds(_interval);
			}
		}
	}
}
