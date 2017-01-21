using UnityEngine;
using System.Collections;

public class Damageable: MonoBehaviour
{
    public int life = 1;

    public MeshRenderer[] meshRenderer;

    private bool canReceiveDamage = true;
    private float invincibilityCooldown = 0.7f;
    private float currCooldown = 0f;

    void Update()
    {
        for(int i=0; i<meshRenderer.Length; i++)
        {
            meshRenderer[i].material.color = Color.Lerp (meshRenderer[i].material.color, Color.white, Time.deltaTime);
        }

        currCooldown -= Time.deltaTime;
        canReceiveDamage = currCooldown < 0;
    }

    public void GetDamage(int damage)
    {
        if(!canReceiveDamage)
            return;
        
        if(life == 1)
        {
            Die();
        }
        else
        {
            life -= damage;
            canReceiveDamage = false;
            currCooldown = invincibilityCooldown;
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