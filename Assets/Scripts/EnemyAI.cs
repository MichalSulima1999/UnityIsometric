using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Transform playerCenter;
    public LayerMask whatIsGround, whatIsPlayer;

    // Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    float timeToMove = 5f;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private Animator animator;
    private bool running = false;
    private bool attacking = false;

    private bool attackingrevolver = false;
    private bool reloadRevolver = false;
    int revolverCount = 0;
    public GameObject bullet;
    private GameObject bulletInstantiated;
    public GameObject bulletSlot;

    //private bool attackingBow = false;

    public int maxHP;
    public int currentHP;
    public int expToGive;
    private EQManager eqManager;

    private Loots loot;
    public int maxDrops;

    public int enemyType;
    /*
    0 - Swordsman
    1 - revolver
        
    */
    

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerCenter = GameObject.Find("Player center").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        eqManager = GameObject.FindGameObjectWithTag("Player").GetComponent<EQManager>();

        loot = GameObject.Find("Loots").GetComponent<Loots>();
    }

    private void Update() {
        // Check for attack and sight range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        
        if (!playerInSightRange && !playerInAttackRange && !reloadRevolver) {
            Patrolling();
            attacking = false;
            attackingrevolver = false;
        } else if (playerInSightRange && !playerInAttackRange && !reloadRevolver) {
            ChasePlayer();
            attacking = false;
            attackingrevolver = false;
        } else
            AttackPlayer();

        if (agent.remainingDistance <= agent.stoppingDistance) {
            running = false;
        } else {
            running = true;
        }

        animator.SetBool("running", running);
        animator.SetBool("attacking", attacking);
        animator.SetBool("revolverShooting", attackingrevolver);
        animator.SetBool("revolverReload", reloadRevolver);
    }

    private void Patrolling() {
        if (!walkPointSet)
            SearchWalkPoint();
        else
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // walkpoint reached

        if (distanceToWalkPoint.magnitude < 1f)
            timeToMove -= Time.deltaTime;

        if(timeToMove <= 0f) {
            walkPointSet = false;
            timeToMove = 5f;
        }
    }

    private void SearchWalkPoint() {
        // calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer() {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer() {
        // enemy stop
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        switch (enemyType) {
            case 0:
                attacking = true;
                break;
            case 1:
                if (!alreadyAttacked) {
                    // attack code here
                    Shoot();

                    alreadyAttacked = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                }
                break;
        }
             
        //animator.SetBool("attacking", false);
    }

    private void Shoot() {
        if (revolverCount >= 6) {
            reloadRevolver = true;
            Invoke("Reload", 1.1f);
        } else {
            attackingrevolver = true;
            revolverCount++;
            bulletInstantiated = Instantiate(bullet, bulletSlot.transform.position, bulletSlot.transform.rotation);
            bulletInstantiated.transform.LookAt(playerCenter);
            bulletInstantiated.GetComponent<Rigidbody>().AddForce(bulletInstantiated.transform.forward * 500);
                
        }
    }

    private void Reload() {
        revolverCount = 0;
        reloadRevolver = false;
    }

    private void ResetAttack() {
        alreadyAttacked = false;
    }

    private void DestroyBullet(GameObject bullet) {
        GameObject.Destroy(bullet);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public void HpLoss(int amount) {
        currentHP -= amount;

        if (currentHP <= 0) {
            currentHP = 0;
            eqManager.currentExp += expToGive;
            Drops();

            Destroy(gameObject);
        }
    }

    private void Drops() {
        int numberOfDrops = Random.Range(0, maxDrops + 1);

        for(int i = 0; i < maxDrops; i++) {
            int dropIndex = Random.Range(0, loot.easyLoot.Length);
            float randx = Random.Range(-2f, 2f);
            float randz = Random.Range(-2f, 2f);
            Vector3 pos = new Vector3(gameObject.transform.position.x + randx, gameObject.transform.position.y, gameObject.transform.position.z + randz);

            Instantiate(loot.easyLoot[dropIndex], pos, loot.easyLoot[dropIndex].transform.rotation);
        }
    }
}
