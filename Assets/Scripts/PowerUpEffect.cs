using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType {
    IncreaseVelocity,
    UnstopableWaves,
    StopWaveAttacks
}

public class PowerUpEffect : MonoBehaviour
{
    public PowerUpType type;
    public Player playerOwner;
    int effects;

    public GameObject chest;

    void Start()
    {
        Destroy(this.gameObject, 10f);
    }

    void OnDestroy()
    {
        if(playerOwner != null)
            CleanEffects();
    }

    void StartIncreaseVelocity()
    {
        MovementController movementController = playerOwner.gameObject.GetComponent<MovementController>();
        movementController.maxSpeed *= 2;
    }

    void StartUnstopableWaves()
    {
        GameManager.Instance.unstopablePlayersWave[playerOwner.id] = true;
    }

    void StartStopWaveAttack()
    {
        foreach(Player player in GameManager.Instance.players)
        {
            player.powerupLights.SetActive(true);
        }
        GameManager.Instance.playersCanAttack = false;
    }

    void CleanEffects()
    {
        playerOwner.powerupLights.SetActive(false);
        switch(type)
        {
            case PowerUpType.IncreaseVelocity:
                MovementController movementController = playerOwner.gameObject.GetComponent<MovementController>();
                movementController.maxSpeed /= 2;
                break;
            case PowerUpType.UnstopableWaves:
                GameManager.Instance.unstopablePlayersWave[playerOwner.id] = false;
                break;
            case PowerUpType.StopWaveAttacks:
                foreach(Player player in GameManager.Instance.players)
                {
                    player.powerupLights.SetActive(false);
                }

                GameManager.Instance.playersCanAttack = true;
                break;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag != "Player")
            return;

        GetComponent<BoxCollider>().enabled = false;

        Player player = collider.gameObject.GetComponent<Player>();
        playerOwner = player;

        switch(type)
        {
            case PowerUpType.IncreaseVelocity:
                StartIncreaseVelocity();
                break;
            case PowerUpType.StopWaveAttacks:
                StartStopWaveAttack();
                break;
            case PowerUpType.UnstopableWaves:
                StartUnstopableWaves();
                break;
        }

        GameManager.Instance.ShowPowerUp(type);
        playerOwner.powerupLights.SetActive(true);
        chest.SetActive(false);
        Destroy(this.gameObject, 10f);
    }

}
