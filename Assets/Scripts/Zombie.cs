
using System.Collections;
using System.Collections.Generic;
using System;
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
    private Vector3 scale;

    public ZombieScriptableObject config;
    public ZombieScriptableObject config2;
    private GameObject player;

    public static event Action ZombieDied;                  //acá declaro el evento

    private bool sd;

    private void Awake()
    {
        int rand = UnityEngine.Random.Range(0, 2);
        if (rand == 0)
        {
            speed = config.speed;
            timeToEscape = config.timeToEscape;
            stoppingDistance = config.stoppingDistance;
            scale = new Vector3(config.scale, config.scale, config.scale);
        }
        if (rand == 1)
        {
            speed = config2.speed;
            timeToEscape = config2.timeToEscape;
            stoppingDistance = config2.stoppingDistance;
            scale = new Vector3(config2.scale, config2.scale, config2.scale);
        }

    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        animator.SetBool("isClose", false);
        alive = true;
        sd = false;
        transform.localScale = scale;
    }

   
    void Update()
    {
        Gravity();
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);


        if (alive && distanceToPlayer > stoppingDistance)
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
                }                                                       //para hacer que esto funcione debidamente tengo que rehacer toda la IA basada en raycast
            }*/
            Movement();
            timeToEscape = 1f;
            animator.SetBool("isClose", false);

        }
        else if (alive && distanceToPlayer < stoppingDistance)
        {
            animator.SetBool("isClose", true);
            Attack();
        }
        else if (!alive)
        {
            animator.SetBool("alive", false);
            Destroy(gameObject, 2);
            
            if (!sd)
            {
                LevelManager.amountOfZombies--;
                ZombieDied.Invoke();                        //acá llamo el evento
                score++;
                sd = true;
                Debug.Log("un zombie llamó el evento de su muerte para spawnear uno nuevo");
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
