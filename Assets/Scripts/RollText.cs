/*
 * textを流すスクリプト
 * -public setup
 * csv : CSV
 * prefText : subMessage
 * generator : subMessage
 */


using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.UI;


[DefaultExecutionOrder(5)]
public class RollText : MonoBehaviour
{
    #region variables

    public GameObject csv;
    public GameObject prefText;
    public GameObject generator;
    public float TextMotionSpeed = 0.1f;


    private List < float >[] _tempData;
    private CreateDBFromCSV _cdb;
    private GameObject[] _texts;
    private TextMesh[] _textMeshes;
    private string[] _sentences;
    private SentenceGenerator _senGenerator;
    private Vector3[] TextEdgePos = new Vector3[2];
    int num = 72;
    private float _stepSize = 15.0f;

    #endregion


    void Start()
    {
        init();
    }

    void Update()
    {
        TextMoveDown();
    }

    void init()
    {
        _texts = new GameObject[num];
        _textMeshes = new TextMesh[num];
        _senGenerator = generator.GetComponent < SentenceGenerator >();
        for (int i = 0; i < num; i++)
        {
            _texts[i] = Instantiate(prefText, new Vector3(0, i * _stepSize, 0), Quaternion.identity) as GameObject;
            _texts[i].name = "text"+i.ToString();
            _texts[i].transform.SetParent(this.gameObject.transform);
            _textMeshes[i] = _texts[i].GetComponent < TextMesh >();
            _textMeshes[i].text = _senGenerator.SenData[i];
        }

        _cdb = csv.GetComponent < CreateDBFromCSV >();
        _tempData = new List < float >[num];
        for (int i = 0; i < num; i++)
        {
            _tempData[i] = new List < float >();
        }

        _tempData = _cdb.OrderdTempData;
    }
    
    void TextMoveDown()
    {
        var _offSetZ = 10.0f; 
        for (int i = 0; i < num; i++)
        {
            if (_texts[i].transform.position.y < -_stepSize*4.0f)
            {
                var x = _texts[(i + 71) % 72].transform.localPosition.x;
                var y = _texts[(i + 71) % 72].transform.localPosition.y + _stepSize * 1.0f;
                var z = _texts[(i + 71) % 72].transform.localPosition.z;
                _texts[i].transform.localPosition = new Vector3(x , y, z);
            }
            _texts[i].transform.position -= Camera.main.transform.up * TextMotionSpeed;
        }
    }
}