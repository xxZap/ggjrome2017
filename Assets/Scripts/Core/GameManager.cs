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

    public Transform pigSpawner;
    public Object pigPrefab;
    private bool pigHasBeenSpawned;

    public GameObject winnerView;
    public Text winnerLabel;

    public Text timeText;
    public float timer;

    public bool finished;

    // powerups
    public bool playersCanAttack = true;
    public bool[] unstopablePlayersWave = new bool[] {false, false, false, false};
    public GameObject uiPowerUpVelocity;
    public GameObject uiPowerUpUnstopable;
    public GameObject uiPowerUpStop;



    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            winnerView.SetActive(false);
            
            for(int i=0; i<PLAYERS_TO_SPAWN.Length; i++)
            {
                if(PLAYERS_TO_SPAWN[i]) SpawnPlayer(i);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        timer = 120f;
        timeText.text = GetFormattedTime((int)timer);
    }

    void Update()
    {
        if(finished)
            return;

        if(!pigHasBeenSpawned && timer <= 30)
        {
            pigHasBeenSpawned = true;
            SpawnPig();
        }
        
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

    public void PigKillPlayer(int killed)
    {
        sfx.player.PlayDestroyObstacle();
        players[killed].gameObject.SetActive(false);
        players[killed].points = 0;
        StartCoroutine(DelayBeforeSpawn(killed));
    }

    public void PlayerKilledPlayer(int killer, int killed)
    {
        AddPointToPlayer(killer);
        AddPointToPlayer(killer);
        //RemovePointFromPlayer(killed);
        players[killed].gameObject.SetActive(false);
        StartCoroutine(DelayBeforeSpawn(killed));
    }

    IEnumerator DelayBeforeSpawn(int playerId)
    {
        yield return new WaitForSeconds(3f);
        SpawnPlayer(playerId);
        sfx.player.PlayRespawn();
    }

    private string GetFormattedTime(int totalSeconds)
    {
        int minutes = (int)(totalSeconds / 60);
        int seconds = totalSeconds - minutes * 60;

        string redColorTextPrefix = "<color=red>";
        string redColorTextSuffix = "</color>";

        string timestring = "0" + ((minutes > 0) ? minutes : 0) + ":" + ((seconds > 9) ? seconds.ToString() : ("0" + seconds.ToString()));

        if(minutes == 0 && seconds <= 30)
        {
            timestring = redColorTextPrefix + timestring + redColorTextSuffix;
        }

        return timestring;
    }

    private void SpawnPig()
    {
        GameObject newPig = Instantiate(pigPrefab, pigSpawner.position, Quaternion.identity) as GameObject;
        StartCoroutine("PlayPig");
    }

    IEnumerator PlayPig()
    {
        do
        {
            yield return new WaitForSeconds(Random.Range(0f, 3f));
            sfx.player.PlayPig();
        } while (true);
    }


    private void ShowFinishGame()
    {
        Time.timeScale = 0;
        int indexPlayer = 0;
        int maxPoint = players[0].points;
        bool draw = false;
        for(int i=1; i<players.Length; i++)
        {
            if (players[i] == null) continue;
            if (players[i].points > maxPoint)
            {
                draw = false;
                indexPlayer = i;
                maxPoint = players[i].points;
            }
            else
            { if (players[i].points == maxPoint) draw = true; }
        }
        winnerLabel.text = "";
        winnerView.SetActive(true);
        sfx.player.PlayTheWinnerIs();
        System.Threading.Thread.Sleep(4000);//ms

        if (!draw)
        {
            switch (indexPlayer)
            {
                case 0:
                    sfx.player.PlayWinnerP1();
                    break;
                case 1:
                    sfx.player.PlayWinnerP2();
                    break;
                case 2:
                    sfx.player.PlayWinnerP3();
                    break;
                case 3:
                    sfx.player.PlayWinnerP4();
                    break;
            }
            winnerLabel.text = "PLAYER " + (indexPlayer + 1).ToString() + ": " + players[indexPlayer].points + "pt";
        }
        else
        {
            winnerLabel.text = "DRAW";
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowPowerUp(PowerUpType type)
    {
        switch(type)
        {
            case PowerUpType.IncreaseVelocity:
                uiPowerUpVelocity.GetComponent<Animator>().SetTrigger("show");
                break;
            case PowerUpType.StopWaveAttacks:
                uiPowerUpStop.GetComponent<Animator>().SetTrigger("show");
                break;
            case PowerUpType.UnstopableWaves:
                uiPowerUpUnstopable.GetComponent<Animator>().SetTrigger("show");
                break;
        }
    }

}
