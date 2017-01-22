using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayInGameFaded : MonoBehaviour {

    private AudioSource _as;
    const float fadeTime = 4f; //sec

	// Use this for initialization
	void Start () {
        //StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        _as = GetComponent<AudioSource>();
        _as.volume = 0;
        _as.Play();
        do
        {
            yield return new WaitForSeconds(0.1f);
            _as.volume += 0.1f * 1f / fadeTime;
        } while (_as.volume < 0.3);
    }

}
