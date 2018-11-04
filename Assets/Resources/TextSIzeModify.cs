using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSIzeModify : MonoBehaviour {
    TextMesh tm;
	void Start () {
        tm = this.GetComponent<TextMesh>();
        tm.lineSpacing = 2;
        tm.fontSize = 100;
        tm.tabSize = 20;
        tm.characterSize = 0.15f;
	}
	
	void Update () {
		
	}
}
