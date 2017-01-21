using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    [Range(90000, 200000)]
    public float speed = 180000;
    public float maxSpeed = 10;

    private Rigidbody rb;
    private Player player;

    private Rewired.Player rPlayer;

    private float verticalInput;
    private float horizontalInput;

    private Vector3 movementDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
        rPlayer = Rewired.ReInput.players.GetPlayer(player.id);
    }

    void Update()
    {
        Move();
        
        FixMaxSpeed();

        FixInertia();

        Turn();

        FixUpRotation();
    }

    void FixedUpdate()
    {
        GetInput();
    }

    #region Private functions

    private void GetInput()
    {
        verticalInput = rPlayer.GetAxis(InputActions.VerticalMovement);
        horizontalInput = rPlayer.GetAxis(InputActions.HorizontalMovement);
    }

    private void Move()
    {
        // Transform input in to movement
        movementDirection = new Vector3(horizontalInput, 0f, verticalInput);

        // Fix supervelocity glitch if diagonal input
        float diagonalGlitchCorrector = (Mathf.Abs(horizontalInput) > 0.3f && Mathf.Abs(verticalInput) > 0.3f) ? 0.7f : 1f;

        movementDirection = movementDirection.normalized * speed * Time.deltaTime * diagonalGlitchCorrector;
        rb.AddForce(movementDirection);
    }

    private void Turn()
    {
        Vector3 velocity = rb.velocity;
        if (velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(velocity, Vector3.up);
        }
    }

    private void FixUpRotation()
    {
        Quaternion rot = transform.rotation;
        rot.x = 0f;
        rot.z = 0f;

        transform.rotation = rot;
    }

    private void FixMaxSpeed()
    {
        Vector3 vel = rb.velocity;
        
        if(vel.x > maxSpeed)  vel.x = maxSpeed;
        if(vel.x < -maxSpeed) vel.x = -maxSpeed;

        if(vel.z > maxSpeed)  vel.z = maxSpeed;
        if(vel.z < -maxSpeed) vel.z = -maxSpeed;

        rb.velocity = vel;
    }

    void FixInertia()
    {
        if(Mathf.Abs(horizontalInput) < 0.1f)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        }
        if(Mathf.Abs(verticalInput) < 0.1f)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);      
        }
    }

    #endregion
}
