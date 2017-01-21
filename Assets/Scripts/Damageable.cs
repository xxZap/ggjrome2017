using UnityEngine;
using System.Collections;

public class Damageable: MonoBehaviour
{
    public int life = 1;

    public void GetDamage(int damage)
    {
        life -= damage;
    }
}