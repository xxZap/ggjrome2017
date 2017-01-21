using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool[] PLAYERS_TO_SPAWN = new bool[] {true, true, false, false};
    public static GameManager Instance { get; private set; }

    public Transform[] spawners;
    public Player[] players;
    public Object[] playerPrefabs;

    public Text timeText;
    private float timer;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        for(int i=0; i<PLAYERS_TO_SPAWN.Length; i++)
        {
            if(PLAYERS_TO_SPAWN[i]) SpawnPlayer(i);
        }

        timer = 120f;
        timeText.text = ((int)timer).ToString();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        timeText.text = ((int)timer).ToString();
    }


    public void AddPointToPlayer(int id)
    {
        players[id].points++;
    }

    public void RemovePointFromPlayer(int id)
    {
        if(players[id].points > 0)
            players[id].points--;
    }

    public void SpawnPlayer(int id)
    {
        if(id > players.Length || players[id] == null)
        {
            GameObject newPlayer = Instantiate(playerPrefabs[id], Vector3.zero, Quaternion.identity) as GameObject;
            Player player = newPlayer.GetComponent<Player>();
            players[id] = player;
        }
        players[id].transform.position = spawners[id].position;
        players[id].gameObject.SetActive(true);
    }

    public void PlayerKilledPlayer(int killer, int killed)
    {
        AddPointToPlayer(killer);
        players[killed].gameObject.SetActive(false);
        StartCoroutine(DelayBeforeSpawn(killed));
    }

    IEnumerator DelayBeforeSpawn(int playerId)
    {
        yield return new WaitForSeconds(3f);
        SpawnPlayer(playerId);
    }

}
