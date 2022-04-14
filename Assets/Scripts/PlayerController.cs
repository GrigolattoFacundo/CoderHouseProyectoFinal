using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f;
    public float distanceToFloor = 0.3f;
    private float gravity = -9.81f;
    private float jump = 1.5f;
    private bool onFloor;
    public bool isDead;

    private Vector3 fallSpeed;
    public LayerMask floorMask;

    public CharacterController player;
    public Transform floorCol;
    public GameManager manager;
    public GameObject crosshair;
    public GameObject deadText;


    private void Start()
    {
        isDead = false;
        transform.position = new Vector3(0, 1, 0);
    }

    void Update()
    {
        Gravity();
        if (isDead == false)
        {
            Movement();
            AliveUI();
        }
        if (isDead == true)
        {
            DeadUI();
        }
    }
    void DeadUI()
    { 
        crosshair.SetActive(false);
        deadText.SetActive(true);
    }
    void AliveUI()
    {
        crosshair.SetActive(true);
        deadText.SetActive(false);
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
