using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : LevelManager
{
    public float speed = 2f;
    public float distanceToFloor = 0.2f;
    private float gravity = -9.81f;
    private float jump = 1.5f;
    private bool onFloor;
    
    private Vector3 fallSpeed;
    public LayerMask floorMask;

    public CharacterController player;
    public Transform floorCol;
    public GameManager gameManager;
    public LevelManager levelManager;

   
    private void Start()
    {
        playerIsDead = false;
        transform.position = new Vector3(0, 1, 0);
    }

    void Update()
    {
        Gravity();
        if (!playerIsDead)
        {
            Movement();
        }
        if (playerIsDead)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        levelManager.OutOfPlayZone();
    }
    private void OnTriggerExit(Collider other)
    {
        levelManager.outTime = 5f;
        levelManager.AliveUI();
        levelManager.playerIsOut = false;
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
        if (onFloor == true && fallSpeed.y < 0)
        {
            fallSpeed.y = -1;
        }
        fallSpeed.y += gravity * Time.deltaTime;
        player.Move(fallSpeed * Time.deltaTime);
    }
}
