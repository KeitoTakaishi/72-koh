using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rePlace : MonoBehaviour {
    List<GameObject> child;
    List<Vector3> initPos;
    List<Vector3> modifyPos;
    List<Vector3> dir;

    Material mat;
    int num;

	void Start () {
        child = new List<GameObject>();
        initPos = new List<Vector3>();
        modifyPos = new List<Vector3>();
        dir = new List<Vector3>();

        num = transform.childCount;
        Vector3 ParentPos = this.transform.position;
        float step = 6.0f;
        mat = Resources.Load("ModelMat") as Material;
       
        //init pos
        if (num == 3)
        {
            for (int i = 0; i < num; i++)
            {
                child.Add(transform.GetChild(i).gameObject);
                initPos.Add(child[i].transform.position);
                child[i].transform.position = new Vector3(ParentPos.x, ParentPos.y - (i-1.5f) * step, ParentPos.z);
                modifyPos.Add(child[i].transform.position);
                dir.Add((initPos[i] - modifyPos[i]).normalized);
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
                initPos.Add(child[i].transform.position);
                child[i].transform.position = new Vector3(ParentPos.x, ParentPos.y - (i-1.5f) * step, ParentPos.z);
                modifyPos.Add(child[i].transform.position);
                dir.Add((initPos[i] - modifyPos[i]).normalized);


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
        if (Input.GetKey(KeyCode.A))
        {
            for (int i = 0; i < num; i++)
            {
                //child[i].transform.position = 1.0f*Mathf.Sin(Time.realtimeSinceStartup/10.0f*Mathf.Rad2Deg);
                var d = initPos[i] - child[i].transform.position; 
                child[i].transform.position += d.normalized * 0.2f;
            }
        }
	}
}
