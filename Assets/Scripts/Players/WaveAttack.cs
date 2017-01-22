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

    void OnTriggerEnter(Collider collider)
    {
        Damageable damageable = collider.gameObject.GetComponent<Damageable>();
        if(damageable != null)
        {
            if (collider.gameObject.tag == "Obstacle")
            {
                if(GameManager.Instance.unstopablePlayersWave[waveOwner] == false)
                    Destroy(waveParent, 0.2f);
                
                damageable.GetDamage(damage);
                Camera.main.GetComponent<CameraShake>().Shake();
            }
            else if (collider.gameObject.tag == "Player")
            {
                Player player = damageable.gameObject.GetComponent<Player>();
                if (player == null || player.id == waveOwner)
                    return;

                damageable.GetDamage(damage);
                Camera.main.GetComponent<CameraShake>().Shake();
                GameManager.Instance.PlayerKilledPlayer(waveOwner, player.id);
                Rigidbody rb = player.gameObject.GetComponent<Rigidbody>();
                Vector3 force = (player.gameObject.transform.position - transform.position).normalized;
                rb.AddForce(force * 30000f);
            }
            else if (collider.gameObject.tag == "Enemy")
            {
                FollowerBehaviour enemy = damageable.gameObject.GetComponent<FollowerBehaviour>();
                //check enemy type = wave type
                if (enemy == null || enemy.type != this.type)
                    return;

                damageable.GetDamage(damage);
                Camera.main.GetComponent<CameraShake>().Shake();
            }

        }

        WaveAttack waveAttack = collider.gameObject.GetComponent<WaveAttack>();
        if(waveAttack != null)
        {
            if(waveAttack.type == this.type && waveAttack.waveOwner != waveOwner)
            {
                if(GameManager.Instance.unstopablePlayersWave[waveOwner] == false)
                    Destroy(waveParent);
                
                Destroy(collider.gameObject);
                Camera.main.GetComponent<CameraShake>().Shake();
            }
        }
    }
}
