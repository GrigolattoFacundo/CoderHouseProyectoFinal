using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float distanceToPlayer;
    public Transform player;
    public float speed = 1f;
    private Animator animator;
    public bool alive;
    public GameManager manager;
    public float timeToEscape = 1f;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isClose", false);
        alive = true;
    }

   
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (alive == true && distanceToPlayer > 1.5f)
        {
            timeToEscape = 1f;
            animator.SetBool("isClose", false);
            Movement();
        }
        else if (alive == true && distanceToPlayer < 1.5f)
        {
            animator.SetBool("isClose", true);
            Attack();
            
        }
        else if (alive == false)
        {
            animator.SetBool("alive", false);
            Destroy(gameObject, 10);

        }
    }
    void Movement()
    {
        Quaternion movementQ = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, movementQ, speed * Time.deltaTime);
        Quaternion aim = Quaternion.LookRotation(player.transform.position - transform.position);
        Vector3 movementV = transform.forward * speed * Time.deltaTime;
        transform.position = transform.position + movementV;
    }
    void Attack()
    {
        timeToEscape -= Time.deltaTime;
        if (timeToEscape < 0 && distanceToPlayer < 2f)
        {
            manager.playerIsDead = true;
        }
    }
}
