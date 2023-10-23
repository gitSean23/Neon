using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{

    public float speed = 5f;
    public Transform target;
    public float minDistance = 3f;

    public float enemyMinDistance = 8f;

    public bool enemyWillAttack;

    public float width;
    public float height;

    //public GameObject enemySpace;

    public LayerMask enemies;

    public Rigidbody2D rb;

    public Animator anim;

    float playerPositionX;

    //public Vector enemyRotation;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim.SetFloat("Health", 100);
        //enemyWillAttack = false;
        //enemyRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
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

    void FixedUpdate()
    {
        // if (Vector2.Distance(transform.position, target.position) > minDistance)
        // {
        //     rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        //     //transform.position = Vector2.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
        // }
    }

    // void FixedUpdate()
    // {
    //     StartCoroutine(ChasePlayer());
    // }

    IEnumerator willAttack()
    {
        int attackChance = Random.Range(1, 5); // replace 5 with the length of the enemies array

        if (attackChance == 1)
        {
            enemyWillAttack = true;
        }

        yield return new WaitForSeconds(3f);
    }

    void goAttack()
    {
        if (Vector2.Distance(transform.position, target.position) > minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
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

    // IEnumerator ChasePlayer()
    // {
    //     if (Vector2.Distance(transform.position, target.position) > minDistance)
    //     {
    //         //rb.velocity = Vector2.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
    //         transform.position = Vector2.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
    //     }

    //     yield return new WaitForSeconds(2f);
    // }

    // void EnemySocialDistance()
    // {
    //     Collider2D enemy = Physics2D.OverlapBox(enemySpace.transform.position, new Vector2(width, height), enemies);
    //     Debug.Log(gameObject.name + ": " + enemy);

    //     if (enemy && enemy.GetComponent<Transform>().position.x != transform.position.x) // Ensuring that the enemy we collide with is not ourself
    //     {
    //         if (enemy.GetComponent<Transform>().position.x > transform.position.x) // Checking if the enemy is to the right of us
    //         {
    //             if (enemy.GetComponent<Transform>().position.x - transform.position.x < enemyMinDistance) // 6 - 2 = 4
    //             {
    //                 transform.position = Vector2.MoveTowards(transform.position, new Vector3(Mathf.Abs(enemyMinDistance - transform.position.x), transform.position.y, transform.position.z), speed * Time.deltaTime);
    //             }
    //         }

    //         // Checking if the enemy is to the left of us
    //         else
    //         {
    //             if (transform.position.x - enemy.GetComponent<Transform>().position.x < enemyMinDistance) // 2 - 4 = 4
    //             {
    //                 transform.position = Vector2.MoveTowards(transform.position, new Vector3(Mathf.Abs(enemyMinDistance - transform.position.x), transform.position.y, transform.position.z), speed * Time.deltaTime);
    //             }
    //         }
    //     }
    // }

    private void OnDrawGizmos()
    {
        // Gizmos.DrawWireCube(enemySpace.GetComponent<Transform>().position, new Vector3(width, height, 1));
    }
}

