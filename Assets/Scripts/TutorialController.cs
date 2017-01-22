using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    private Rewired.Player rPlayer;

    void Start ()
    {
        rPlayer = Rewired.ReInput.players.GetPlayer(0);
    }

    void Update()
    {
        if(rPlayer.GetButtonDown(InputActions.Fire0))
        {
            SceneManager.LoadScene("AlessioSceneTest");
        }
    }
}
