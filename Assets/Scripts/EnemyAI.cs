using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public LayerMask whatIsGround, whatIsPlayer;
    public Transform player;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public int damageAmount = 10;
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool canMove = true;
    private float rotationSpeed = 5f;
    Animator anim;
    bool Run;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        playerInSightRange = GameObject.FindGameObjectWithTag("Player").transform;  
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();   
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange) Patroling();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInSightRange && playerInAttackRange) attackAnim();
        if(playerInSightRange && playerInAttackRange) anim.SetBool("Run", false);

        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
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
        if (canMove == true)
        {
            anim.SetBool("Run", true);
            agent.SetDestination(player.position);
        }
    }


    private void attackAnim()
    {
        if (alreadyAttacked == false)
        {
            anim.SetTrigger("Attack");
            canMove = false;
        }

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void moveReset()
    {
        canMove = true;
    } 
    private void AttackPlayer()
    {

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        if(playerInAttackRange)
        {
            playerHealth.TakeDamage(damageAmount);
            ResetAttack();
        }
        

        agent.SetDestination(transform.position);


        Vector3 direction = (player.position - transform.position).normalized;
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
