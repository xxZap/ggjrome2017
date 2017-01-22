using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_PowerUp : MonoBehaviour
{
    public GameObject[] power_UpPrefabs;

    Vector3 location = new Vector3(0f,1f,0f);
    int WorldPowerUp;
    float cooldownTime = 20f;
    float powerUpSelfDestructionTime = 10f;

    void Start()
    {
        StartCoroutine(StartSpawnPowerUpLoop());
    }

    
    void SettingRandomLocation()
    {
        location.x = Random.Range(-6, 6);
        location.z = Random.Range(-5, 5);
    }

    IEnumerator StartSpawnPowerUpLoop()
    {
        while(!GameManager.Instance.finished)
        {
            Object randomPowerUp = power_UpPrefabs[Random.Range(0, power_UpPrefabs.Length)];
            GameObject newPowerUp = Instantiate(randomPowerUp, location, transform.rotation) as GameObject;

            yield return new WaitForSeconds(20);
        }
    }
}
