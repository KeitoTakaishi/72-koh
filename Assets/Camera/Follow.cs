/*
 * targetを追うスクリプト
 * 
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using  UnityEngine;
using Random = System.Random;



public class Follow : MonoBehaviour
{
    #region Nested Classes
    public enum Interpolator{
        Lerp,
        Slerp
    }
    #endregion

    #region Editable Properties
    [SerializeField] Interpolator _interpolator;
    [SerializeField] Transform _target;

    public Interpolator interpolator
    {
        get { return _interpolator; }
        set { _interpolator = value; }
    }

    public Transform target
    {
        get { return _target; }
        set { _target = value; }
    }
    
    #endregion
    
    private void Update()
    {
        if (Time.frameCount % 30 == 1)
        {
            var _nextPos = NextPos();
            var _curPos = this.transform.position;
            this.transform.position = Vector3.Lerp(_curPos, _nextPos, Time.realtimeSinceStartup);
        }  
    }

    private Vector3 NextPos()
    {
        var rad = 1.0f;
        var _nextPos = UnityEngine.Random.insideUnitSphere * rad;
        return _nextPos;
    }

    protected virtual void LookAt()
    {
        //毎フレームTargetの方向を向くようにする
        if(target == null) return;
        this.gameObject.transform.LookAt(target.transform.position);    
    }
}
