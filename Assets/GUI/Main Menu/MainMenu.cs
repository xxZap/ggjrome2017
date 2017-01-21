using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    public Button PlayButton;
    public Button CreditsButton;
    public Button ExitButton;

    

    // Use this for initialization

    void Start () {
        PlayButton.onClick.AddListener(() => { UnityEngine.SceneManagement.SceneManager.LoadScene("LivelloSelezionePersonaggio"); });
        CreditsButton.onClick.AddListener(() => { UnityEngine.SceneManagement.SceneManager.LoadScene("CreditsMenu"); });
        ExitButton.onClick.AddListener(() => { Application.Quit(); });

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
