using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Button PlayButton;
    public Button CreditsButton;
    public Button ExitButton;

    void Start ()
    {
        PlayButton.onClick.AddListener(() => {
            sfx.player.PlayMenuClick();
            UnityEngine.SceneManagement.SceneManager.LoadScene("ChoosePlayers");
        });
        CreditsButton.onClick.AddListener(() => {
            sfx.player.PlayMenuClick();
            UnityEngine.SceneManagement.SceneManager.LoadScene("CreditsMenu");
        });
        ExitButton.onClick.AddListener(() => {
            sfx.player.PlayMenuClick();
            Application.Quit();
        });
	}
}
