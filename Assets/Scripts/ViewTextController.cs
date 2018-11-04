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
	int AppearTime;//説明文章が現れている時間

    public GameObject PressUI;
    public GameObject PressUIWire;
    private float commonAlpha = 0.0f;
    private int countForAlpha1 = 0;
    private int countForAlpha2 = 0;


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
    public GameObject panel;
    Image panelImage;
    float alpha = 0.2f;
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

    //sentence generatorは１フレ前を観測するからバッファが必要
    public int BufferID = 0;
	void Update ()
	{
        _oscId = _oscServer.ID;
        if(_oscId > 0){
            _isAccepted = false;
            BufferID = _oscId;
        }
        if (!_isAccepted)
        {
            if (frame >= AppearTime){
                frame = 0;
                _isAccepted = true;
            }
            ++frame;
        }

        //600frame目の段階まできたらカレントのoscのidを-1にする

		if (_isAccepted){
            _accepetTextImage.fillAmount = 1.0f;
            _accepetTextImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            _accepetText.text = "Select Button";
			GameObject.Find("ImageText").GetComponent < Text >().text = "Done" + "\n" + "100%";
			GameObject.Find("ImageText").GetComponent < Text >().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            if (alpha < 0.2) alpha += 0.01f;
            panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, alpha);

            //Wait for minutes or Select Button
            var textC = PressUI.GetComponent<Text>().color;
            var wireC = PressUIWire.GetComponent<Image>().color;
            PressUI.GetComponent<Text>().color = new Color(textC.r, textC.g, textC.b, commonAlpha);
            PressUIWire.GetComponent<Image>().color = new Color(wireC.r, wireC.g, wireC.b, commonAlpha);

            if (countForAlpha1 == 0) { 
                commonAlpha = 0.0f;
                countForAlpha2 = 0;
                ++countForAlpha1;
            }
            else{
                //commonAlpha
            }

            if (commonAlpha <= 1.0f) commonAlpha += 0.01f;
        }
        else{
			_accepetTextImage.fillAmount = (1.0f/AppearTime)*(float)frame;
            _accepetTextImage.color = new Color(1.0f, 1.0f, 1.0f, Mathf.Sin(frame * 5.0f * Mathf.Deg2Rad));
            _accepetText.text = "Please Wait";

			GameObject.Find("ImageText").GetComponent < Text >().text =
				"Calculating" + "\n" + ((1.0f / AppearTime)* frame * 100.0f).ToString() + "%";
			
			GameObject.Find("ImageText").GetComponent < Text >().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Sin(frame*5.0f * Mathf.Deg2Rad));

            if (alpha > 0.0) alpha -= 0.01f;
            panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, alpha);

            //Wait for minutes or Select Button
            var textC = PressUI.GetComponent<Text>().color;
            var wireC = PressUIWire.GetComponent<Image>().color;
            PressUI.GetComponent<Text>().color = new Color(textC.r, textC.g, textC.b, commonAlpha);
            PressUIWire.GetComponent<Image>().color = new Color(wireC.r, wireC.g, wireC.b, commonAlpha);

            if (countForAlpha2 == 0)
            {
                commonAlpha = 0.0f;
                countForAlpha1 = 0;
                ++countForAlpha2;
            }
            else
            {
                //commonAlpha
            }

            if (commonAlpha <= 1.0f) commonAlpha += 0.01f;
        }

       
        //view切り替え 
        MainViewChange(_isAccepted);
	}

	void init()
	{
		_oscServer = OCS.GetComponent < OSCServer >();
		_accepetText = GameObject.Find("AccepetText").GetComponent < Text >();
		_accepetTextImage = GameObject.Find("AccepetTextImage").GetComponent < Image >();
		
		SelectedText.SetActive(false);
		RollTextManager.SetActive(true);
		copyTexts.SetActive(false);
        AppearTime = 500;
        panelImage = panel.GetComponent<Image>();
       

    }

	bool judge(int OscId)
	{
		//tempIdは前のフレームにoscで送られてきたデータが入っている
		var isPush =  OscId != _tempId ? true : false;
		return isPush;
	}
	
	IEnumerator Instant(){
		copyTexts.SetActive(true);
        for (int i = 0; i < 501; i++)
        {
            yield return null;
        }

        copyTexts.SetActive(false);	
	}

    private void MainViewChange(bool isAccepted){
        if (!isAccepted){
            RollTextManager.SetActive(false);
            SelectedText.SetActive(true);
            //Debug.Log("selected true----------" + Time.frameCount );
            StartCoroutine("Instant");
        }
        else
        {
            RollTextManager.SetActive(true);
            SelectedText.SetActive(false);
        }
    }
}
