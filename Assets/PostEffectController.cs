using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostEffectController : MonoBehaviour {
    PostProcessingBehaviour behaviour;
    BloomModel.Settings bloomSettings;
    //ColorGradingModel temp;
    ColorGradingModel.Settings temp;
    void Start()
    {
        behaviour = GetComponent<PostProcessingBehaviour>();
    }
 	void Update () {
        temp = behaviour.profile.colorGrading.settings;
        temp.basic.temperature = 30.0f * Mathf.Sin(Time.frameCount * Mathf.Deg2Rad/2.0f);
        behaviour.profile.colorGrading.settings = temp;
    }

}
