using UnityEngine;
using System.Collections;

public enum WaveType { Blue, Green, Red }

public class WaveAttack : MonoBehaviour
{
    public WaveType type;
    public int damage = 1;
    public int waveOwner;
    public float lifeTime = 1f;
    public float scaleGrowthFactor = 1.1f;
    public float scaleModifier = 1f;

    public AnimationCurve scaleCurve;
    public AnimationCurve alphaCurve;
    public MeshRenderer meshRenderer;
    private Material material;

    private float currTime;

    void Start()
    {
        material = meshRenderer.material;

        Destroy(this.gameObject, lifeTime);
        currTime = 0;
        transform.localScale = new Vector3(0,0,0);
    }

    void Update()
    {
        float percentage = currTime / lifeTime;

        Vector3 newScale = new Vector3(1f,1f,1f);
        newScale *= scaleCurve.Evaluate(percentage) * scaleModifier;
        transform.localScale = newScale;

        Color newColor = material.color;
        newColor.a = alphaCurve.Evaluate(percentage);
        material.color = newColor;

        currTime  += Time.deltaTime;
    }

    void OnTriggerEnter(Collider collider)
    {
        Damageable damageable = collider.gameObject.GetComponent<Damageable>();
        if(damageable != null)
        {
            if(collider.gameObject.tag == "Obstacle")
            {
                Destroy(this.gameObject, 0.1f);
                damageable.GetDamage(damage);
                // TODO: per gli ostacoli, non devono scoppiare all'istante ma avere comportamenti particolari: alcuni esplodere, altri cambiare texture, altri "abbassarsi"
            }
            else if(collider.gameObject.tag == "Player")
            {
                Player player = damageable.gameObject.GetComponent<Player>();
                if(player != null && player.id == waveOwner)
                    return;
                
                damageable.GetDamage(damage);
            }
        }

        WaveAttack waveAttack = collider.gameObject.GetComponent<WaveAttack>();
        if(waveAttack != null)
        {
            if(waveAttack.type == this.type && waveAttack.waveOwner != waveOwner)
            {
                Destroy(this.gameObject);
                Destroy(collider.gameObject);
            }
        }
    }
}
