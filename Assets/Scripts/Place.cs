﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour {
    public GameObject[] fonts;
    Vector3[] pos;
    const int num = 72;
    float stepDegree;
    float radius = 1.0f;
	void Start () {
        fonts = new GameObject[num];
        pos = new Vector3[num];
        stepDegree = 2.0f*Mathf.PI / num;



        for (int i = 0; i < fonts.Length; i++){
            string index = (i + 1).ToString();
            fonts[i] = (GameObject)Resources.Load(index+"-72");
            pos[i] = new Vector3(radius * Mathf.Sin(stepDegree * i), 0.0f, radius * Mathf.Cos(stepDegree * i));

            fonts[i] = Instantiate(fonts[i], pos[i], Quaternion.identity);                                        
        }
	}
	
	void Update () {
        for (int i = 0; i < fonts.Length; i++)
        {
            float t = Time.realtimeSinceStartup/5.0f;
            fonts[i].transform.position = new Vector3(radius * Mathf.Sin(stepDegree * i+ t), 0.0f, radius * Mathf.Cos(stepDegree * i + t));

        }
	}
}
