using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rePlace : MonoBehaviour {
    List<GameObject> child;


	void Start () {
        child = new List<GameObject>();
        var num = transform.childCount;
        Vector3 ParentPos = this.transform.position;
        float step = 6.0f;

        if (num == 3)
        {
            for (int i = 0; i < num; i++)
            {
                child.Add(transform.GetChild(i).gameObject);
                Debug.Log(child[i]);
                child[i].transform.position = new Vector3(ParentPos.x, ParentPos.y - (i-1.0f) * step, ParentPos.z);
            }
        }else{
            for (int i = 0; i < num; i++)
            {
                child.Add(transform.GetChild(i).gameObject);
                Debug.Log(child[i]);
                child[i].transform.position = new Vector3(ParentPos.x, ParentPos.y - (i-1.5f) * step, ParentPos.z);
            }
        }


	}
	
	void Update () {
		
	}
}
