using UnityEngine;
using System.Collections;

public class Damageable: MonoBehaviour
{
    public int life = 1;

    public MeshRenderer meshRenderer;

    void Update()
    {
        meshRenderer.material.color = Color.Lerp (meshRenderer.material.color, Color.white, Time.deltaTime);
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
            meshRenderer.material.color = new Color(1,0,0);
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}