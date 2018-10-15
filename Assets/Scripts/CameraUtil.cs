using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraUtil : MonoBehaviour
{
    Vector3 bufferpos;
    [SerializeField]
    GameObject tar;

    private List < GameObject > target;
    Vector3 nextPos;

    float t; //time for interpolation
    public float rad = 10.0f;

    #region mono
    enum InterpolationMode{
        Lerp,
        Slerp
    }

    [SerializeField]
    InterpolationMode im;
    void Start()
    {
        bufferpos = this.transform.position;
        target = new List<GameObject>();
        target.Add(GameObject.Find("row1(Clone)"));
        target.Add(GameObject.Find("row11(Clone)"));
        target.Add(GameObject.Find("row21(Clone)"));
        target.Add(GameObject.Find("row31(Clone)"));
        target.Add(GameObject.Find("row41(Clone)"));
        target.Add(GameObject.Find("row51(Clone)"));
        
    }

    void Update()
    {
        if (Time.frameCount % 30 == 0)
        {
            bufferpos = this.transform.position;
            nextPos = Random.insideUnitSphere * rad;
            t = 0.0f;
            transform.LookAt(tar.transform);
            LookAtSelect();
        }


        if (im == InterpolationMode.Lerp){
            //transform.position = Vector3.Lerp(bufferpos, nextPos, t);
            transform.position = InterpolationLerp(bufferpos, nextPos, t);
        }else if(im == InterpolationMode.Slerp){
            transform.position = Vector3.Slerp(bufferpos, nextPos, t);
        }
        transform.LookAt(tar.transform);
        t += Time.deltaTime;
        if (t > 1.0f){
            t = 1.0f;
        }

    }
    #endregion


    Vector3 InterpolationLerp(Vector3 from, Vector3 to, float time){
        Vector3 dir = to - from;
        Vector3 result = to + dir * time;
        return result;
    }

    void LookAtSelect()
    {
        tar = target[(int)Random.RandomRange(0, target.Count)];
    }

}
