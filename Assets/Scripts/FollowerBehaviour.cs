using UnityEngine;
using System.Collections;

public class FollowerBehaviour : MonoBehaviour
{

    public Transform[] players;
    private float MoveSpeed = 4;


    void Start()
    {
        players = System.Array.ConvertAll<GameObject, Transform>(GameObject.FindGameObjectsWithTag("Player"), x => x.transform);
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
            transform.LookAt(closestPlayer);
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
        
    }
}