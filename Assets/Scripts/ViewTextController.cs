/*
 * RollTextsかSelectdTextどっちを表示するかを決定する
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;
using OSCServer = uOSC.OSCServer;

public class ViewTextController : MonoBehaviour {
	#region variable
	public GameObject RollTextManager;
	public GameObject SelectedText;
	public GameObject OCS;
	
	private OSCServer _oscServer;
	private int _oscId = -1;
	private int _tempId = -1;
	private int frame = 0;
	private bool _isPush = false;
	#endregion
	
	
	public int OscId 
	{
		get{ return _oscId; }
	}

	public bool IsPush
	{
		get { return _isPush; }
	}

	void Start () {
		init();
	}
	
	void Update ()
	{
		//前フレームと入力OscIdが違った場合
		_isPush = judge(_oscServer.ID);
		//押されていない
		if (_isPush){
			_oscId = _oscServer.ID;
			frame = 0;
		}else{
			frame++;
			if (frame == 360){
				_oscId = -1;
				frame = 0;
			}
		}


		if (_oscId > 0){
			RollTextManager.SetActive(false);
			SelectedText.SetActive(true);
		}else{
			RollTextManager.SetActive(true);
			SelectedText.SetActive(false);
		}

		_tempId = _oscServer.ID;
	}

	void init()
	{
		_oscServer = OCS.GetComponent < OSCServer >();
		SelectedText.SetActive(false);
		RollTextManager.SetActive(true);
		
	}

	bool judge(int OscId)
	{
		var isPush =  OscId != _tempId ? true : false;
		return isPush;
	}
}
