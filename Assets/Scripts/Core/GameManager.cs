using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool[] PLAYERS_TO_SPAWN = new bool[] {true, true, true, true};
    public static GameManager Instance { get; private set; }

    public Light redLight;

    public Transform[] spawners;
    public Player[] players;
    public Object[] playerPrefabs;

    public Text timeText;
    public float timer;

    public bool finished;

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
        timeText.text = GetFormattedTime((int)timer);
    }

    void Update()
    {
        if(finished)
            return;
        
        if(timer <= 0)
        {
            timer = 0;
            finished = true;
            ShowFinishGame();
            return;
        }

        timer -= Time.deltaTime;
        timeText.text = GetFormattedTime((int)timer);
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
        //RemovePointFromPlayer(killed);
        players[killed].gameObject.SetActive(false);
        StartCoroutine(DelayBeforeSpawn(killed));
    }

    IEnumerator DelayBeforeSpawn(int playerId)
    {
        yield return new WaitForSeconds(3f);
        SpawnPlayer(playerId);
    }

    private string GetFormattedTime(int totalSeconds)
    {
        int minutes = (int)(totalSeconds / 60);
        int seconds = totalSeconds - minutes * 60;

        string redColorTextPrefix = "<color=red>";
        string redColorTextSuffix = "</color>";

        string timestring = "0" + ((minutes > 0) ? minutes : 0) + ":" + ((seconds > 9) ? seconds.ToString() : ("0" + seconds.ToString()));

        if(minutes == 0 && seconds < 11)
        {
            timestring = redColorTextPrefix + timestring + redColorTextSuffix;
        }

        return timestring;
    }

    private void ShowFinishGame()
    {
        Time.timeScale = 0;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

}
