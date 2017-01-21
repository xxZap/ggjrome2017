using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class FollowerBehaviour : MonoBehaviour
{

    public Transform[] players;
    public float MoveSpeed = 4f;
    private NavMeshAgent pathfinder;
    public WaveType type;

    void Start()
    {
        players = System.Array.ConvertAll<GameObject, Transform>(GameObject.FindGameObjectsWithTag("Player"), x => x.transform);
        pathfinder = GetComponent<NavMeshAgent>();
        pathfinder.speed = MoveSpeed;
    }

    void FixedUpdate()
    {

        //from the list look for the closest player
        float minDistance = float.MaxValue;
        Transform closestPlayer = null;
        foreach (Transform player in players)
        {
            if (player == null) continue;
            float distanceFromThisPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceFromThisPlayer < minDistance)
            {
                closestPlayer = player;
                minDistance = distanceFromThisPlayer;
            }
        }

        //if a closest player was found, go for it!
        if (minDistance < float.MaxValue)
        {
            //transform.LookAt(closestPlayer);
            //transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            pathfinder.SetDestination(closestPlayer.position);
        }
        
    }
}