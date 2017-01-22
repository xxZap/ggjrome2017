using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PigMob : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    private float updateTime = 2f;
    private int prevTargetID;
    private bool needToJumpUpdate = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        StartCoroutine(UpdateTarget());
    }

    void Update()
    {
        if(target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player killedPlayer = collision.gameObject.GetComponent<Player>();
            if(killedPlayer == null) return;

            GameManager.Instance.PigKillPlayer(killedPlayer.id);
            if(killedPlayer.id == prevTargetID)
            {
                prevTargetID++;
                if(prevTargetID >= GameManager.Instance.players.Length || GameManager.Instance.players[prevTargetID] == null)
                    prevTargetID = 0;

                target = GameManager.Instance.players[prevTargetID].gameObject.transform;
                agent.SetDestination(target.position);
                needToJumpUpdate = true;
            }

        }

        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }

    IEnumerator UpdateTarget()
    {
        while(!GameManager.Instance.finished)
        {
            if(!needToJumpUpdate)
            {
                target = SearchTarget();
            }
            else
            {
                needToJumpUpdate = false;
            }

            yield return new WaitForSeconds(updateTime);
        }
    }

    private Transform SearchTarget()
    {
        int indexPlayer = prevTargetID;
        int maxPoint = GameManager.Instance.players[prevTargetID].points;
        for(int i=0; i<GameManager.Instance.players.Length; i++)
        {
            if(GameManager.Instance.players[i].points > maxPoint)
            {
                indexPlayer = i;
                maxPoint = GameManager.Instance.players[i].points;
            }
        }

        prevTargetID = indexPlayer;
        return GameManager.Instance.players[indexPlayer].gameObject.transform;
    }
}
