using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public float health;
    public float currHealth;
    public bool isAttacking = false;
    public bool isStunned = false;
    public bool isRetreating = false;
    public bool isIdle = true;
    public bool isChasing = false;

    public bool isDead = false;

    public Animator anim;

    public float speed = 5f;
    public Transform target;
    public float minDistance = 3f;

    public float enemyMinDistance = 8f;

    public bool enemyWillAttack;


    public LayerMask enemies;

    public Rigidbody2D rb;

    float playerPositionX;

    private EnemyStateMachine enemyStateMachine;

    public float xOffset = 0.8f;

    public int enemyIndex;


    // Start is called before the first frame update
    void Start()
    {
        //enemyAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim.SetFloat("Health", 100);
        xOffset = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= currHealth)
        {
            currHealth = health;
            Debug.Log("ENEMY Attacked!");
        }

        else if (health <= 0)
        {
            Debug.Log("ENEMY DEAD!");
            //WaitForSeconds(3f);
            // ADD the Destroy() back in
            //Destroy(gameObject);
        }

        if (isChasing)
        {
            // Moves the enemy towards the player
            SetLayerCollision("Enemy", "Enemy", false);
            SetLayerCollision("Enemy", "Foreground", false);
            anim.SetBool("isChasing", true);
            playerPositionX = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x;
            float directionToPlayer = Mathf.Abs(playerPositionX - transform.position.x);
            Flip(playerPositionX);
            Vector2 targetPosition = new Vector2(target.position.x + Mathf.Sign(transform.position.x - target.position.x) * xOffset, transform.position.y);

            //transform.position = Vector2.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }

        if (!isChasing)
        {
            SetLayerCollision("Enemy", "Enemy", true);
            SetLayerCollision("Enemy", "Foreground", true);
            anim.SetBool("isChasing", false);
        }

        if (isAttacking)
        {
            // This will play the attack animation
            anim.SetBool("isAttacking", true);
        }

        if (!isAttacking)
        {
            anim.SetBool("isAttacking", false);
        }

        if (isRetreating)
        {
            Debug.Log("Enemy Retreating");
            return;
        }

        else
        {
            // enemy is idle
            //anim.SetBool
            playerPositionX = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x;
            float directionToPlayer = Mathf.Abs(playerPositionX - transform.position.x);
            Flip(playerPositionX);
            //Vector2 directionToPlayer = player - transform.position.x;
            if (Vector2.Distance(transform.position, target.position) > minDistance)
            {
                //transform.position = Vector2.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
                transform.position = Vector2.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
                //rb.velocity = directionToPlayer.normalized * speed;
                anim.SetFloat("Speed", speed);
                //transform.position = Vector2.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
            }

            else
            {
                anim.SetFloat("Speed", 0);
            }
        }
    }

    public void enemyDeath()
    {
        //enemyStateMachine.enemies.RemoveAt(enemyStateMachine.pickedEnemyIndex);
        isDead = true;
        //Destroy(gameObject);
    }

    void Flip(float directionToPlayer)
    {

        // Determine if the player is to the left or right of the enemy
        if (directionToPlayer < transform.position.x)
        {
            Debug.Log("Player is to the LEFT of me!");
            // If the player is on the left, rotate the enemy to face left
            transform.rotation = Quaternion.Euler(0, 180, 0); // Rotate 180 degrees around the Y-axis
        }
        else if (directionToPlayer > transform.position.x)
        {
            Debug.Log("Player is to the RIGHT of enemy!");
            // If the player is on the right, rotate the enemy to face right
            transform.rotation = Quaternion.Euler(0, 0, 0); // Reset to the default rotation
        }

    }

    void SetLayerCollision(string layer1, string layer2, bool enableCollision)
    {
        int layer1Index = LayerMask.NameToLayer(layer1);
        int layer2Index = LayerMask.NameToLayer(layer2);

        Physics2D.IgnoreLayerCollision(layer1Index, layer2Index, enableCollision);
    }
}
