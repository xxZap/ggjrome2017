using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int id = 1;

    public int points = 0;

    public TextMesh meshPoints;

    public GameObject powerupLights;

    void OnDisable()
    {
        if(points > 0)
            points--;
    }

    void Update()
    {
        meshPoints.text = points.ToString();
    }
}

