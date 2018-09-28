using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rePlace : MonoBehaviour {
    List<GameObject> child;
    Material mat;

	void Start () {
        child = new List<GameObject>();
        var num = transform.childCount;
        Vector3 ParentPos = this.transform.position;
        float step = 6.0f;
        mat = Resources.Load("ModelMat") as Material;
       
        //init pos
        if (num == 3)
        {
            for (int i = 0; i < num; i++)
            {
                child.Add(transform.GetChild(i).gameObject);
                child[i].transform.position = new Vector3(ParentPos.x, ParentPos.y - (i-1.5f) * step, ParentPos.z);
                child[i].transform.localScale = new Vector3(0.9f,0.9f,0.9f);

                var grand_c_num = child[i].transform.childCount;
                for (int j = 0; j < grand_c_num; j++){
                    var c = child[i].transform.GetChild(j).gameObject;
                    c.GetComponent<MeshRenderer>().material = mat;
                }

            }
        }else{
            for (int i = 0; i < num; i++)
            {
                child.Add(transform.GetChild(i).gameObject);
                child[i].transform.position = new Vector3(ParentPos.x, ParentPos.y - (i-1.5f) * step, ParentPos.z);


                var grand_c_num = child[i].transform.childCount;
                for (int j = 0; j < grand_c_num; j++)
                {
                    var c = child[i].transform.GetChild(j).gameObject;
                    c.GetComponent<MeshRenderer>().material = mat;
                }
            }
        }





	}
	
	void Update () {
		
	}
}
