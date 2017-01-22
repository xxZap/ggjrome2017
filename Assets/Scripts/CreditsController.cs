using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    private Rewired.Player rPlayer;

    void Start ()
    {
        rPlayer = Rewired.ReInput.players.GetPlayer(0);
    }

    void Update()
    {
        if(rPlayer.GetButtonDown(InputActions.Fire0) || rPlayer.GetButtonDown(InputActions.Fire1))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
