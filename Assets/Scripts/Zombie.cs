using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : LevelManager
{
    public float distanceToPlayer;
    private float speed;
    private float timeToEscape;
    private float stoppingDistance;
    public bool alive;
    private bool onFloor;
    private float gravity = -9.81f;
    public float distanceToFloor = 0.2f;
    private Vector3 fallSpeed;
    public LayerMask floorMask;
    private Animator animator;
    public GameManager manager;
    public PlayerController playerScript;
    public GameObject rayPoint;
    public Transform floorCol;
    public CharacterController zom;

    public ZombieScriptableObject config;
    private GameObject player;

    private void Awake()
    {
        speed = config.speed;
        timeToEscape = config.timeToEscape;
        stoppingDistance = config.stoppingDistance;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        animator.SetBool("isClose", false);
        alive = true;
    }

   
    void Update()
    {
        Gravity();
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (alive == true && distanceToPlayer > stoppingDistance)
        {
            /*RaycastHit lookingAtPlayer;
            if (Physics.Raycast(rayPoint.transform.position, rayPoint.transform.forward, out lookingAtPlayer))
            {
                if (lookingAtPlayer.transform.tag == "Obstacle")
                {
                    transform.rotation = new Quaternion (0, 90, 0, 0); //no sé cómo usar quaternion para que rote como yo quiero para esquivar obstáculos xd
                                                                       //tengo que investigar eso
                    OnlyFrontMovement();
                    Debug.Log("detecta obstáculos");
                }
                else 
                {
                    Movement();
                }                                                       //para hacer que esto funcione debidamente tengo que rehacer toda la IA basada en raycast,
                                                                        //no me va a dar tiempo para la entrega, lo dejo así y lo termino para la próxima xd
            }*/
            Movement();
            timeToEscape = 1f;
            animator.SetBool("isClose", false);

        }
        else if (alive == true && distanceToPlayer < stoppingDistance)
        {
            animator.SetBool("isClose", true);
            Attack();
            
        }
        else if (alive == false)
        {
            for (int i = 0; i < 1; i++)
            {
                animator.SetBool("alive", false);
                LevelManager.amountOfZombies--;
                Destroy(gameObject, 1);
            }
        }
    }
    void Movement()
    {
        Vector3 movementV = transform.forward * speed * Time.deltaTime;
        transform.position = transform.position + movementV;
        Quaternion movementQ = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, movementQ, speed * Time.deltaTime);
        Quaternion aim = Quaternion.LookRotation(player.transform.position - transform.position);
    }
    void OnlyFrontMovement()
    {
        Vector3 movementV = transform.forward * speed * Time.deltaTime;
        transform.position = transform.position + movementV;
    }
    void Attack()
    {
        timeToEscape -= Time.deltaTime;
        if (timeToEscape < 0 && distanceToPlayer < 2f)
        {
            playerIsDead = true;
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
        zom.Move(fallSpeed * Time.deltaTime);
    }
}
