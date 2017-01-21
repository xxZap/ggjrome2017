using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuSelezione : MonoBehaviour {
    //Default Images
    public Image P1_Normal;
    public Image P2_Normal;
    public Image P3_Normal;
    public Image P4_Normal;
    // Selected Characters Images
    public Image P1_Selected;
    public Image P2_Selected;
    public Image P3_Selected;
    public Image P4_Selected;
    // False Button
    Image Player_1;
    Image Player_2;
    Image Player_3;
    Image Player_4;
    //Player Index
    int Index;
    // Use this for initialization
    void Start () {
        Index = 1;
	}
	
	// Update is called once per frame
	void Update () {
        GiocatoreSelezionaPersonaggio();
        
	}

    void GiocatoreSelezionaPersonaggio()
    {
        //player 1
        if(Index == 1 && Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            Player_1 = P1_Selected;
            Player_2 = P2_Normal;
            Player_3 = P3_Normal;
            Player_4 = P4_Normal;
            Index = Index + 1;

        }

        //player 2
        if (Index == 2 && Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            Player_1 = P1_Normal;
            Player_2 = P2_Selected;
            Player_3 = P3_Normal;
            Player_4 = P4_Normal;
            Index = Index + 1;

        }

        //Player 3
        if (Index == 3 && Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            Player_1 = P1_Normal;
            Player_2 = P2_Normal;
            Player_3 = P3_Selected;
            Player_4 = P4_Normal;
            Index = Index + 1;

        }

        //Player 4
        if (Index == 4 && Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            Player_1 = P1_Normal;
            Player_2 = P2_Normal;
            Player_3 = P3_Normal;
            Player_4 = P4_Selected;
            Index = Index + 1;

        }

    }
}
