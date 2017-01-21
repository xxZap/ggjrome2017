using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Object waveBlueprefab;
    public Object waveGreenprefab;
    public Object waveRedprefab;
    
    private bool attackBlueInput;
    private bool attackGreenInput;
    private bool attackRedInput;
    private Player player;

    private Rewired.Player rPlayer;

    void Awake()
    {
        player = GetComponent<Player>();
        rPlayer = Rewired.ReInput.players.GetPlayer(player.id);
    }
	
	void LateUpdate ()
    {
        GetInput();
        PerformAttack();
	}


    #region Private functions

    private void GetInput()
    {
        attackBlueInput = rPlayer.GetButtonDown(InputActions.Fire0);
        attackRedInput = rPlayer.GetButtonDown(InputActions.Fire1);
        attackGreenInput = rPlayer.GetButtonDown(InputActions.Fire2);
    }

    private void PerformAttack()
    {
        if(attackBlueInput)
            SpawnWave(WaveType.Blue);
        if(attackGreenInput)
            SpawnWave(WaveType.Green);
        if(attackRedInput)
            SpawnWave(WaveType.Red);
    }

    private void SpawnWave(WaveType type)
    {
        WaveAttack waveAtt;

        switch(type)
        {
            case WaveType.Blue:
                GameObject blue = Instantiate(waveBlueprefab, transform.position, Quaternion.identity) as GameObject;
                waveAtt = blue.GetComponent<WaveAttack>();
                waveAtt.waveOwner = player.id;
                break;
            case WaveType.Green:
                GameObject green = Instantiate(waveGreenprefab, transform.position, Quaternion.identity) as GameObject;
                waveAtt = green.GetComponent<WaveAttack>();
                waveAtt.waveOwner = player.id;
                break;
            case WaveType.Red:
                GameObject red = Instantiate(waveRedprefab, transform.position, Quaternion.identity) as GameObject;
                waveAtt = red.GetComponent<WaveAttack>();
                waveAtt.waveOwner = player.id;
                break;
        }
    }

    #endregion
}
