using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx : MonoBehaviour {

    public static sfx player { get; private set; }
    private AudioSource source;

    public AudioClip playerRespawn;
    public AudioClip wave;
    public AudioClip boh;
    public AudioClip destroyObstacle;
    public AudioClip npcDeath;

    public AudioClip powerupSpawn;
    public AudioClip powerupBlackout;
    public AudioClip powerupDoubleSpeed;
    public AudioClip powerupUnstoppableWaves;

    public AudioClip menuClick;

    public AudioClip theWinnerIs;
    public AudioClip winnerP1;
    public AudioClip winnerP2;
    public AudioClip winnerP3;
    public AudioClip winnerP4;

    public AudioClip pig;

    void Awake()
    {
        if (player == null)
        {
            player = this;
            this.gameObject.AddComponent<AudioSource>();
            source = this.gameObject.GetComponent<AudioSource>();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayRespawn()
    {
        source.PlayOneShot(playerRespawn);
    }
    public void PlayWave()
    {
        source.PlayOneShot(wave);
    }
    public void PlayBoh()
    {
        source.PlayOneShot(boh);
    }
    public void PlayDestroyObstacle()
    {
        source.PlayOneShot(destroyObstacle);
    }
    public void PlayNpcDeath()
    {
        source.PlayOneShot(npcDeath);
    }

    public void PlayPowerupSpawn()
    {
        source.PlayOneShot(powerupSpawn);
    }
    public void PlayPowerupBlackout()
    {
        source.PlayOneShot(powerupBlackout);
    }
    public void PlayPowerupDoubleSpeed()
    {
        source.PlayOneShot(powerupDoubleSpeed);
    }
    public void PlayPowerupUnstoppableWaves()
    {
        source.PlayOneShot(powerupUnstoppableWaves);
    }

    public void PlayMenuClick()
    {
        source.PlayOneShot(menuClick);
    }

    public void PlayTheWinnerIs()
    {
        source.PlayOneShot(theWinnerIs);
    }
    public void PlayWinnerP1()
    {
        source.PlayOneShot(winnerP1);
    }
    public void PlayWinnerP2()
    {
        source.PlayOneShot(winnerP2);
    }
    public void PlayWinnerP3()
    {
        source.PlayOneShot(winnerP3);
    }
    public void PlayWinnerP4()
    {
        source.PlayOneShot(winnerP4);
    }
    public void PlayPig()
    {
        source.PlayOneShot(pig);
    }

}
