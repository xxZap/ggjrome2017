using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Object wavePrefab;
    public Animator animator;

    public float cooldown = 1.1f;
    private float currentCooldown = 0f;
    
    private bool attackBlueInput;
    private bool attackGreenInput;
    private bool attackRedInput;
    private Player player;
    private MovementController movementController;

    private Rewired.Player rPlayer;

    void Awake()
    {
        player = GetComponent<Player>();
        movementController = GetComponent<MovementController>();
        rPlayer = Rewired.ReInput.players.GetPlayer(player.id);
    }
	
	void LateUpdate ()
    {
        GetInput();
        PerformAttack();
	}

    #region Private functions

    private void GetInput()
    {
        if(currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
            attackBlueInput  = false;
            attackRedInput   = false;
            attackGreenInput = false;
            return;
        }
        
        attackBlueInput  = rPlayer.GetButtonDown(InputActions.Fire2);
        attackRedInput   = rPlayer.GetButtonDown(InputActions.Fire1);
        attackGreenInput = rPlayer.GetButtonDown(InputActions.Fire0);
    }

    private void PerformAttack()
    {
        if(attackBlueInput)
            SpawnWave(WaveType.Blue);
        if(attackGreenInput)
            SpawnWave(WaveType.Green);
        if(attackRedInput)
            SpawnWave(WaveType.Red);
    }

    private void SpawnWave(WaveType type)
    {
        currentCooldown = cooldown;
        //movementController.StopMovement();

        if(animator != null)
            animator.SetTrigger("attack");
        
        WaveAttack waveAtt;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y-1f, transform.position.z);
        GameObject newWave = Instantiate(wavePrefab, pos, transform.rotation) as GameObject;
        waveAtt = newWave.transform.GetChild(0).GetComponentInChildren<WaveAttack>();
        waveAtt.waveOwner = player.id;
        waveAtt.waveParent = newWave;
        waveAtt.type = type;
        Destroy(newWave, waveAtt.lifeTime);

        switch(type)
        {
            case WaveType.Blue:
                foreach(MeshRenderer mesh in waveAtt.meshRenderers)
                    mesh.material = waveAtt.blueMaterial;
                break;
            case WaveType.Green:
                foreach(MeshRenderer mesh in waveAtt.meshRenderers)
                    mesh.material = waveAtt.greenMaterial;
                break;
            case WaveType.Red:
                foreach(MeshRenderer mesh in waveAtt.meshRenderers)
                    mesh.material = waveAtt.redMaterial;
                break;
        }
    }

    #endregion
}
