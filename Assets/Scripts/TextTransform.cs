/*
 textの複製と動きのエフェクトを作成している
 
 todo 
 - oscの値を確認して変わったタイミングで文字を生成,変更
 - その後，複製
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.PlayerLoop;
[DefaultExecutionOrder(1)]
public class TextTransform : MonoBehaviour {
    #region variable

    private GameObject originalSen;//コピー元テキスト
    private GameObject[] effectSen;
    private TextMesh[] effectTextMesh;
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
        TextRotate();
        MoveText();
	}

    void TextRotate()
    {
        this.transform.Rotate(Vector3.up, Time.deltaTime*10.0f);
    }

    //effect用のテキストメッシュオブジェクトの生成
    void GenerateText()
    {
        originalSen = GameObject.Find("SubMessage") as GameObject;
        effectSen = new GameObject[num];
        effectTextMesh = new TextMesh[num];
        for (int i = 0; i < num; i++)
        {
            effectSen[i] = Instantiate(pref, originalSen.transform.position, Quaternion.identity) as GameObject;
            effectSen[i].transform.localScale = originalSen.transform.localScale;
            effectTextMesh[i] = effectSen[i].GetComponent < TextMesh >();
        }
    }
    
    //mainのtextのコピーを行う
    void CopyText()
    {
        foreach (var s in effectTextMesh)
        {
            s.transform.position = originalSen.transform.position;
            Quaternion r = originalSen.transform.rotation;
            s.transform.rotation = r;
          
            
            s.text = originalSen.GetComponent < TextMesh >().text;
        }
    }

    void MoveText()
    {
        for (int i = 0; i < effectSen.Length; i++)
        {
            var speed = (i - 1.5f) * 0.2f;
            effectSen[i].transform.position += new Vector3(speed, -speed, 0.0f);
        }
    }   
}
