/*
 * RollTextsかSelectdTextどっちを表示するかを決定する
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;
using OSCServer = uOSC.OSCServer;
using UnityEngine.UI;

[DefaultExecutionOrder(1)]
public class ViewTextController : MonoBehaviour {
	#region variable
	public GameObject RollTextManager;
	public GameObject SelectedText;
	public GameObject copyTexts;
	public GameObject OCS;
	public int AppearTime = 400;//説明文章が現れている時間
	
	private OSCServer _oscServer;
	private int _oscId = 0;
	private int _tempId = 0;　　
	private int frame = 0;
	private bool _isPush = false;

	private Slider _slider;
	public bool _isAccepted = true;
	private int _curFrame;
	private Text _accepetText;
	private Image _accepetTextImage;
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
		//600frame目の段階まできたらカレントのoscのidを-1にする
		if (_isAccepted){
			//_slider.value = 1.0f;
			_accepetTextImage.fillAmount = 1.0f;
			_accepetText.text = "Select Button";
			GameObject.Find("ImageText").GetComponent < Text >().text = "Done" + "\n" + "100%";
			GameObject.Find("ImageText").GetComponent < Text >().color = new Color(1.0f, 1.0f, 1.0f, 0.7f);
			_accepetTextImage.color = new Color(1.0f, 1.0f, 1.0f, Mathf.Sin(frame*5.0f * Mathf.Deg2Rad));


			
		}else{
			//_slider.value = (1.0f/AppearTime)*frame;
			
			_accepetTextImage.fillAmount = (1.0f/AppearTime)*frame;
			_accepetText.text = "Wait For Minutes";
			GameObject.Find("ImageText").GetComponent < Text >().text =
				"Calculating" + "\n" + ((1.0f / AppearTime)* frame * 100.0f).ToString() + "%";
			
			GameObject.Find("ImageText").GetComponent < Text >().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Sin(frame*5.0f * Mathf.Deg2Rad));
			_accepetTextImage.color = new Color(1.0f, 1.0f, 1.0f, Mathf.Sin(frame*5.0f * Mathf.Deg2Rad));
		}

		
		
		//_isPush = judge(_oscServer.ID);
		_oscId = _oscServer.ID; 
		Debug.Log("view"+_oscId);
		//ボタンが押されかつ受け入れ状態の時にidを更新する
		/*
		if (_isPush)
		{
			frame = 0;
			//受け付けるか受け付けないか
			if (_isAccepted)
			{
				_oscId = _oscServer.ID;
				frame += 1;
				_isAccepted = false;
			}
		}
		else
		{
			if (frame >= 1 && frame != 600)
			{
				++frame;
			}

			if (frame == AppearTime)
			{
				frame = 600;
			}
		}
		*/

		if (_oscId == 0)
		{
			++frame;
			if (frame == AppearTime)
			{
				frame = 0;
				_isAccepted = true;
			}
			
		}
		else
		{
			if (_isAccepted)
			{
				_isAccepted = false;
				frame = 0;
				//Debug.Log("Accept!! OSC :" + _oscId);
			}
		}


		//view切り替え 
		if (!_isAccepted){
			RollTextManager.SetActive(false);
			SelectedText.SetActive(true);
			StartCoroutine("Instant");
		}else{
			RollTextManager.SetActive(true);
			SelectedText.SetActive(false);
		}		
		_tempId = _oscServer.ID;
	}

	void init()
	{
		_oscServer = OCS.GetComponent < OSCServer >();
		//_slider = GameObject.Find("Slider").GetComponent<Slider>();
		//_slider.value = 0.0f;
		_accepetText = GameObject.Find("AccepetText").GetComponent < Text >();
		_accepetTextImage = GameObject.Find("AccepetTextImage").GetComponent < Image >();
		
		SelectedText.SetActive(false);
		RollTextManager.SetActive(true);
		copyTexts.SetActive(false);
	}

	bool judge(int OscId)
	{
		//tempIdは前のフレームにoscで送られてきたデータが入っている
		var isPush =  OscId != _tempId ? true : false;
		return isPush;
	}
	
	IEnumerator Instant()
	{
		copyTexts.SetActive(true);
		yield return new WaitForSeconds(3.0f);
		copyTexts.SetActive(false);
		
	}
}
