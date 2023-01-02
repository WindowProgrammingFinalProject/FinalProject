using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Boss3AIScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    // add Animator here

    public LayerMask whatIsGround, whatIsPlayer; // layer


    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    [SerializeField] float timeBetweenAttacks = 1;
    bool alreadyAttacked = false;

    //States
    [SerializeField] float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public int mushroomDamage = 7;
    public bool alive = true;

    public AudioClip die;
    AudioSource audiosource;
    Animator animator;

    //variable for animator
    [SerializeField] private bool attack = false;

    private void Awake()
    {
        player = GameObject.Find("maincharacter").transform;
        agent = GetComponent<NavMeshAgent>();
        audiosource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Check for sight and attack range
        if (alive) playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        if (alive) playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!alive) playerInSightRange = false;
        if (!alive) playerInAttackRange = false;
        if (!playerInSightRange && !playerInAttackRange && alive) Patroling();
        if (playerInSightRange && !playerInAttackRange && alive) ChasePlayer();
        if (playerInAttackRange && playerInSightRange && alive) AttackPlayer();
        animator.SetBool("attack", attack);
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (!agent.isOnNavMesh)
            agent.Warp(new Vector3(0, 0, 0));

        if (walkPointSet && agent.isOnNavMesh)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position); // make sure it wouldn't move
        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            attack = true;
            animator.SetBool("attack", attack);
            Invoke(nameof(attackVariableChange), 0.7f);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            Debug.Log("damage");
            player.GetComponent<PlayerController>().TakeDamage(mushroomDamage);
        }
    }
    private void attackVariableChange()
    {
        attack = !attack;
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
