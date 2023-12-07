using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FB1Script : MonoBehaviour
{

    float enemyRetreatPoint;
    float playerPositionX;
    public Transform target;

    [SerializeField] public float health = 150f;

    public Animator anim;
    [SerializeField] public float attackRange;
    //public GameObject alertIcon;
    Coroutine bossCycle;

    float speed = 4f;
    float xOffset = 0.9f;

    float dmg = 20f;

    SoundScript soundManager;

    public float radius;

    public Image playerHealthBar;
    public LayerMask player;
    Coroutine playerHitCoroutine;

    bool isChasing = false;

    public GameObject attackPoint;

    // Start is called before the first frame update
    void Start()
    {

        soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundScript>();
        anim = GetComponent<Animator>();
        bossCycle = StartCoroutine(BossCycle());
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            Debug.Log("FB1 DEAD!");
            //isDead = true;

            Destroy(gameObject);
        }
    }

    IEnumerator BossCycle()
    {
        while (health > 0)
        {
            enemyRetreatPoint = transform.position.x;


            anim.SetBool("isIdle", false);
            anim.SetBool("isChasing", true);
            isChasing = true;
            //alertIcon.SetActive(true);



            //yield return new WaitUntil(() => Mathf.Abs(transform.position.x - GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x) <= attackRange);
            while (isChasing && Mathf.Abs(transform.position.x - GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x) > attackRange)
            {
                Chase();
                yield return null;  // Wait for the next frame
            }

            //alertIcon.SetActive(false);
            isChasing = false;
            anim.SetBool("isChasing", false);
            anim.SetBool("isAttacking", true);

            yield return new WaitForSeconds(1.5f);

            anim.SetBool("isAttacking", false);
            anim.SetBool("isRetreating", true);
            transform.position = Vector2.MoveTowards(transform.position, new Vector3(enemyRetreatPoint, transform.position.y, transform.position.z), speed * Time.deltaTime);


            while (Mathf.Abs(transform.position.x) < Mathf.Abs(enemyRetreatPoint))
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector3(enemyRetreatPoint, transform.position.y, transform.position.z), speed * Time.deltaTime);
                yield return null;  // Wait for the next frame
            }
            //yield return new WaitUntil(() => Mathf.Abs(transform.position.x) >= Mathf.Abs(enemyRetreatPoint));


            anim.SetBool("isRetreating", false);
            anim.SetBool("isIdle", true);


            yield return new WaitForSeconds(7f);
        }
    }

    public void Chase()
    {
        if (isChasing)
        {
            Debug.Log("Chasing Player");
            playerPositionX = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x;
            float directionToPlayer = Mathf.Abs(playerPositionX - transform.position.x);
            Flip(playerPositionX);
            Vector2 targetPosition = new Vector2(target.position.x + Mathf.Sign(transform.position.x - target.position.x) * xOffset, transform.position.y);
            Debug.Log(targetPosition);

            Debug.Log("moving my transform");
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            Debug.Log("transform move uccessful");
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

    public void enemyDamage()
    {
        Collider2D[] thePlayer = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, player);

        if (thePlayer.Length < 1)
        {

            Debug.Log("No player to hit in range..");
        }

        else
        {
            foreach (Collider2D playerGameobject in thePlayer)
            {
                if (playerGameobject != null)
                {

                    Debug.Log("PLAYER HIT!");
                    // MODIFY THE LINE BELOW!
                    // playerHitCoroutine = StartCoroutine(PlayerGotHit(playerGameobject));
                    playerGameobject.GetComponent<PlayerScript>().PlayerGotHit();
                    playerGameobject.GetComponent<PlayerScript>().health -= dmg;
                    playerHealthBar.fillAmount = playerGameobject.GetComponent<PlayerScript>().health / 100f;

                }
            }
        }
    }

    // IEnumerator PlayerGotHit(Collider2D playerGameobject)
    // {
    //     if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().health > 0)
    //     {
    //         playerGameobject.GetComponent<Animator>().SetBool("isHurt", true);
    //         yield return new WaitForSeconds(0.1f);
    //         playerGameobject.GetComponent<Animator>().SetBool("isHurt", false);
    //     }

    // }


}
