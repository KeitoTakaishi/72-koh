﻿/*
 1.72textの生成
 2.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.PlayerLoop;
[DefaultExecutionOrder(1)]
public class CopyTextsMotion : MonoBehaviour {
    #region variable
    public GameObject baseText;//コピー元テキスト(selected Text)
    private GameObject[] _copyTexts;
    private TextMesh[] _copyTextMesh;
    private int num = 4;
    private bool isInstance = false;
    [SerializeField]private GameObject pref;
    #endregion
    
    
	void Start () {
	    GenerateText();
    }

   void Update ()
    {
 
        if (Input.GetMouseButton(0))
        {
            CopyText();
        }
        MoveText();
	}

    

    //effect用のテキストメッシュオブジェクトの生成
    void GenerateText()
    {
        
        _copyTexts = new GameObject[num];//copyされた
        _copyTextMesh = new TextMesh[num];
        
        for (int i = 0; i < num; i++)
        {
            _copyTexts[i] = Instantiate(pref, baseText.transform.position, Quaternion.identity) as GameObject;
            _copyTexts[i].transform.localScale = baseText.transform.localScale;
            _copyTextMesh[i] = _copyTexts[i].GetComponent < TextMesh >();
        }
    }
    
    //textの情報のcopyを行う
    void CopyText()
    {
        foreach (var s in _copyTextMesh)
        {
            s.transform.position = baseText.transform.position;
            Quaternion r = baseText.transform.rotation;
            s.transform.rotation = r;
            s.text = baseText.GetComponent < TextMesh >().text;
        }
    }
    

    void MoveText()
    {
        for (int i = 0; i < _copyTexts.Length; i++)
        {
            var speed = (i - 1.5f) * 0.2f;
            _copyTexts[i].transform.position += new Vector3(speed, -speed, 0.0f);
        }
    }   
}