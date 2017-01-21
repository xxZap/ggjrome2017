using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Nemici : MonoBehaviour {

    public GameObject Nemico;

    private const float SpawnCheckFrequency = 0.1f;
    public int AverageSpawningInterval_Crystals; //secs > 1

    // Use this for initialization
    void Start () {
        StartCoroutine("Spawner");
        AverageSpawningInterval_Crystals = 10; //secs > 1
    }
	
	// Update is called once per frame


    IEnumerator Spawner()
    {
        do
        {
            //every SpawnCheckFrequency seconds
            yield return new WaitForSeconds(SpawnCheckFrequency);
            float afk= Random.value;
            //if (afk < 1.0 / AverageSpawningInterval_Crystals)
            float micVolume = AudioIn.MicLoudness;
            if (afk < Mathf.Max(micVolume, 1f / AverageSpawningInterval_Crystals))
            {
                Vector3 SpawnLocation = GetRandomSpawnLocation();
                GameObject NemicoSpawnato = (GameObject)Instantiate(Nemico, SpawnLocation, transform.rotation);
                FollowerBehaviour untypedEnemy = NemicoSpawnato.GetComponent<FollowerBehaviour>();
                untypedEnemy.type = (WaveType)Mathf.RoundToInt(Random.Range(0, 3));

                var mats = NemicoSpawnato.GetComponentInChildren<Renderer>().materials;

                switch(untypedEnemy.type)
                {
                    case WaveType.Blue:
                        for(int i=0; i<mats.Length; i++)
                        {
                            mats[i] = untypedEnemy.blueMaterial;
                        }
                        break;
                    case WaveType.Green:
                        for(int i=0; i<mats.Length; i++)
                        {
                            mats[i] = untypedEnemy.greenMaterial;
                        }
                        break;
                    case WaveType.Red:
                        for(int i=0; i<mats.Length; i++)
                        {
                            mats[i] = untypedEnemy.redMaterial;
                        }
                        break;
                }

                NemicoSpawnato.GetComponentInChildren<Renderer>().materials = mats;
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
