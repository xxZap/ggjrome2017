using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayersSelection : MonoBehaviour
{
    public int id;
    public SelectionMenu menu;

    private Rewired.Player rPlayer;

	
	void Start ()
    {
        rPlayer = Rewired.ReInput.players.GetPlayer(id);
	}

    void Update()
    {
        if(rPlayer.GetButtonDown(InputActions.Fire0))
        {
            menu.SelectByPlayer(id);
        }

        if(rPlayer.GetButtonDown(InputActions.Fire1))
        {
            menu.DeselectByPlayer(id);
        }

        if(menu.players[0] && id==0 && rPlayer.GetButtonDown(InputActions.Fire2))
        {
            menu.StartTutorial();
        }
    }
	
}
