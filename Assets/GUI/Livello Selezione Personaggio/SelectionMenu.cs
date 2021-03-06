﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionMenu : MonoBehaviour
{
    public bool[] players;

    public Sprite[] normalSpriteResources;
    public Sprite[] selectedSpriteResources;

    public Image[] uiImages;

    public Text startIndications;

    void Awake()
    {
        players = new bool[4] {false, false, false, false};
    }


    public void SelectByPlayer(int id)
    {
        players[id] = true;
        uiImages[id].sprite = selectedSpriteResources[id];

        if(id == 0)
        {
            sfx.player.PlayMenuClick();
            startIndications.gameObject.SetActive(true);
        }
    }

    public void DeselectByPlayer(int id)
    {
        players[id] = false;
        uiImages[id].sprite = normalSpriteResources[id];

        if(id == 0)
        {
            sfx.player.PlayMenuClick();
            startIndications.gameObject.SetActive(false);
        }
    }

    public void StartTutorial()
    {
        GameManager.PLAYERS_TO_SPAWN = players;
        
        SceneManager.LoadScene("TutorialScreen");
    }


}
