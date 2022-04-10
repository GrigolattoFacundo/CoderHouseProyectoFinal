using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f;
    public CharacterController player;
    private Vector3 fallSpeed;
    private float gravity = -9.81f;
    public Transform floorCol;
    public float distanceToFloor = 0.3f;
    public LayerMask floorMask;
    private bool onFloor;
    private float jump = 1.5f;
    public GameManager manager;
    
    void Update()
    {
        Gravity();
        if (manager.playerIsDead == false)
        {
            Movement();
        }
    }

    void Movement()
    {
        float forward = Input.GetAxisRaw("Vertical");
        float sideways = Input.GetAxisRaw("Horizontal");

        Vector3 walking = transform.right * sideways + transform.forward * forward;
        player.Move(walking * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 5f;

        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 2f;
        }

        if (Input.GetButtonDown("Jump") && onFloor)
        {
            fallSpeed.y = Mathf.Sqrt(jump * -2 * gravity);
        }
    }
    void Gravity()
    {
        
        onFloor = Physics.CheckSphere(floorCol.position, distanceToFloor, floorMask);
        if (onFloor && fallSpeed.y < 0)
        {
            fallSpeed.y = -1;
        }
        fallSpeed.y += gravity * Time.deltaTime;
        player.Move(fallSpeed * Time.deltaTime);
    }
}
