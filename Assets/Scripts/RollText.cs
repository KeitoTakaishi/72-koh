using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using Vuforia;

public class RollText : MonoBehaviour
{

	#region variables
	public GameObject _csv;
	public GameObject _prefText;
	public GameObject _generator;
	
	
	private List< float >[]_tempData;
	private CreateDBFromCSV _cdb;
	private GameObject[] _texts;
	private TextMesh[] _textMeshes;
	private string[] _sentences;
	private SentenceGenerator _senGenerator;
	int num = 72;
	
	
	#endregion
	
	
	void Start ()
	{
		init();
	}
	
	void Update () {
		for (int i = 0; i < num; i++)
		{
			_texts[i].transform.position -= new Vector3(0.0f, 0.1f, 0.0f);
		}
	}

	void init()
	{
		_texts = new GameObject[num];
		_textMeshes = new TextMesh[num];
		_senGenerator = _generator.GetComponent < SentenceGenerator >();
		for (int i = 0; i < num; i++)
		{
			_texts[i] = Instantiate(_prefText, new Vector3(0, i*10.0f, 0), Quaternion.identity) as GameObject;
			_textMeshes[i] = _texts[i].GetComponent <TextMesh>();
			_textMeshes[i].text = _senGenerator.SenData[i];
		}
		
		_cdb = _csv.GetComponent < CreateDBFromCSV >();
		_tempData = new List <float>[num];
		for (int i = 0; i < num; i++){
			_tempData[i] = new List<float>();
		}
		_tempData = _cdb.OrderdTempData;
	}
}
