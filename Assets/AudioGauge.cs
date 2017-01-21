using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioGauge : MonoBehaviour {

    Text text;

    void Awake()
    {
        //set up the reference
        text = GetComponent<Text>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float micVolume = AudioIn.MicLoudness;
        text.text = string.Format("{0:0.000}", micVolume);
    }

}
