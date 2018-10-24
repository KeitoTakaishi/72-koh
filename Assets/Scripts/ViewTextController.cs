/*
 * RollTextsかSelectdTextどっちを表示するかを決定する
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityOSC;
using OSCServer = uOSC.OSCServer;
using UnityEngine.UI;

public class ViewTextController : MonoBehaviour {
	#region variable
	public GameObject RollTextManager;
	public GameObject SelectedText;
	public GameObject copyTexts;
	public GameObject OCS;
	public int AppearTime = 600;//説明文章が現れている時間
	
	private OSCServer _oscServer;
	private int _oscId = -1;
	private int _tempId = -1;　　
	private int frame = 0;
	private bool _isPush = false;

	private Slider _slider;
	private bool _isAccepted = true;
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
		if (frame == 600){
			_slider.value = 1.0f;
			_accepetTextImage.fillAmount = 1.0f;
			_accepetText.text = "Select Button";
			GameObject.Find("ImageText").GetComponent < Text >().text = "Done!";
			
		}else{
			_slider.value = (1.0f/AppearTime)*frame;
			_accepetTextImage.fillAmount = (1.0f/AppearTime)*frame;
			_accepetText.text = "Wait For Minutes";
			GameObject.Find("ImageText").GetComponent < Text >().text = "";
		}

				
		//前フレームと入力OscIdが違った場合
		_isPush = judge(_oscServer.ID);
		//押されていない
		if (_isPush)
		{
			frame = 0;
			//受け付けるか受け付けないか
			if (_isAccepted)
			{
				_oscId = _oscServer.ID;
				frame += 1;
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
				_oscId = -1;
				frame = 600;
			}
		}



		if (_oscId > 0){
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
		_slider = GameObject.Find("Slider").GetComponent<Slider>();
		_slider.value = 0.0f;
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
