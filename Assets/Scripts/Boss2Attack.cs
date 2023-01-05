using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class Boss2Attack : MonoBehaviour
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
    [SerializeField] float sightRange, attackRange = 12f;
    public bool playerInSightRange, playerInAttackRange;
    public int archerDamage = 5;
    public bool alive = true;

    private Animator myAnimator;

    // bullet
    [SerializeField] GameObject projectile;

    public AudioClip die;
    AudioSource audiosource;

    private int attackNumber;
    [SerializeField] private int maxAttackNumber = 30;
    [SerializeField] private int attackStopTime = 17;

    private void Awake()
    {
        player = GameObject.Find("maincharacter").transform;
        agent = GetComponent<NavMeshAgent>();
        audiosource = GetComponent<AudioSource>();
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!alive) playerInSightRange = false;
        if (!alive) playerInAttackRange = false;
        if (!playerInSightRange && !playerInAttackRange && alive) Patroling();
        if (playerInSightRange && !playerInAttackRange && alive) ChasePlayer();
        if (playerInAttackRange && playerInSightRange && alive) AttackPlayer();

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
        if (!alreadyAttacked )
        {
            alreadyAttacked = true;
            //myAnimator.SetBool("FarAttact", true);      /////why it fail
            //Debug.Log(myAnimator.GetBool("FarAttact"));      /////but it can still get bool¡H
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
            bullet.GetComponent<BulletScript>().damage = archerDamage;
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 5f, ForceMode.Impulse);
            attackNumber++;
            Debug.Log("(1)attackNumber" + attackNumber);
            if(attackNumber <= maxAttackNumber) Invoke(nameof(ResetAttack), timeBetweenAttacks);
            else
            {
                attackNumber = 0;
                Debug.Log("(2)attackNumber" + attackNumber);
                playerInSightRange = false; playerInAttackRange = false;  ///not work ¡H
                Invoke(nameof(ResetAttack), timeBetweenAttacks * attackStopTime);
            }
        }

    }
    private void ResetAttack()
    {
        //myAnimator.SetBool("FarAttact", false);
        alreadyAttacked = false;
        playerInSightRange = true; playerInAttackRange = true;
        Debug.Log("alreadyAttacked" + alreadyAttacked);
    }
}
