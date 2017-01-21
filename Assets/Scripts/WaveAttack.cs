using UnityEngine;
using System.Collections;

public enum WaveType { Blue, Green, Red }

public class WaveAttack : MonoBehaviour
{
    public WaveType type;
    public int damage = 1;
    public int waveOwner;
    public float lifeTime = 1f;
    public GameObject waveParent;

    public MeshRenderer[] meshRenderers;

    public Material greenMaterial;
    public Material blueMaterial;
    public Material redMaterial;

    private float currTime;

    void OnTriggerEnter(Collider collider)
    {
        Damageable damageable = collider.gameObject.GetComponent<Damageable>();
        if(damageable != null)
        {
            if(collider.gameObject.tag == "Obstacle")
            {
                Destroy(waveParent);
                damageable.GetDamage(damage);
                // TODO: per gli ostacoli, non devono scoppiare all'istante ma avere comportamenti particolari: alcuni esplodere, altri cambiare texture, altri "abbassarsi"
            }
            else if(collider.gameObject.tag == "Player")
            {
                Player player = damageable.gameObject.GetComponent<Player>();
                if(player != null && player.id == waveOwner)
                    return;
                
                damageable.GetDamage(damage);
                Rigidbody rb = player.gameObject.GetComponent<Rigidbody>();
                Vector3 force = (player.gameObject.transform.position - transform.position).normalized;
                rb.AddForce(force * 30000f);
            }
        }

        WaveAttack waveAttack = collider.gameObject.GetComponent<WaveAttack>();
        if(waveAttack != null)
        {
            if(waveAttack.type == this.type && waveAttack.waveOwner != waveOwner)
            {
                Destroy(waveParent);
                Destroy(collider.gameObject);
            }
        }
    }
}
