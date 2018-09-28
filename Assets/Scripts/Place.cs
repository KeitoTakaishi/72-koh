using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour {
    public GameObject[] fonts;
    Vector3[] pos;
    const int num = 71;
    float stepDegree;
    float radius = 40.0f;

	void Start () {
        fonts = new GameObject[num];
        pos = new Vector3[num];
        stepDegree = 2.0f*Mathf.PI / num;



        for (int i = 0; i < fonts.Length; i++){
            string index = "RowModel/row"+(i + 1).ToString();
            fonts[i] = (GameObject)Resources.Load(index);
            pos[i] = new Vector3(radius * Mathf.Sin(stepDegree * i), 0.0f, radius * Mathf.Cos(stepDegree * i));

            fonts[i] = Instantiate(fonts[i], pos[i], Quaternion.identity);
            fonts[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
           
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
