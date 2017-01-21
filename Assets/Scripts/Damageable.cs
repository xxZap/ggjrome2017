using UnityEngine;
using System.Collections;

public class Damageable: MonoBehaviour
{
    public int life = 1;

    public MeshRenderer[] meshRenderer;

    void Update()
    {
        for(int i=0; i<meshRenderer.Length; i++)
        {
            meshRenderer[i].material.color = Color.Lerp (meshRenderer[i].material.color, Color.white, Time.deltaTime);
        }
    }

    public void GetDamage(int damage)
    {
        if(life == 1)
        {
            Die();
        }
        else
        {
            life -= damage;
            for(int i=0; i<meshRenderer.Length; i++)
            {
                meshRenderer[i].material.color = new Color(1,0,0);
            }
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}