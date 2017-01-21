using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Nemici : MonoBehaviour {

    public GameObject Nemico;

    private const float SpawnCheckFrequency = 1f;
    public int AverageSpawningInterval_Crystals = 2; //secs > 1

    // Use this for initialization
    void Start () {
        StartCoroutine("Spawner");
	}
	
	// Update is called once per frame


    IEnumerator Spawner()
    {
        do
        {
            //every second
            yield return new WaitForSeconds(SpawnCheckFrequency);
            float afk= Random.value;
            if (afk < 1.0 / AverageSpawningInterval_Crystals)
            {
                Vector3 SpawnLocation = GetRandomSpawnLocation();
                GameObject NemicoSpawnato = (GameObject)Instantiate(Nemico, SpawnLocation, transform.rotation);
            }
        } while (true);

    }


    Vector3 GetRandomSpawnLocation()
    {
        int x = Random.Range(-7, 7);
        int z = Random.Range(-5, 5);
        return new Vector3(x, 1, z);
    }
}
