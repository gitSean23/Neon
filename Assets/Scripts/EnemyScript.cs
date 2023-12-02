using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyScript : MonoBehaviour
{
    public Image playerHealthBar;
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


    //public LayerMask enemies;

    public Rigidbody2D rb;

    float playerPositionX;

    public EnemyStateMachine enemyStateMachine;

    public float xOffset = 0.8f;

    public int enemyIndex;

    public LayerMask player;

    public float radius = 0.5f;

    public GameObject attackPoint;

    public float dmg = 10f;

    SoundScript soundManager;

    Coroutine playerHitCoroutine;
    Coroutine stunCoroutine;

    // Enemy Alert variables
    public GameObject alertIcon; // Reference to the alert icon GameObject

    // Gravity guy variables
    public Transform playerTransform;

    void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundScript>();
    }

    // Start is called before the first frame update
    void Start()
    {

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

        if (health <= 0)
        {
            Debug.Log("ENEMY DEAD!");
            isDead = true;

            Debug.Log("Enemy List: " + enemyStateMachine.enemies);

            if (enemyStateMachine != null)
            {
                enemyStateMachine.enemies.Remove(transform);
            }

            Destroy(gameObject);
        }

        if (isChasing)
        {
            // Moves the enemy towards the player
            alertIcon.SetActive(true);

            SetLayerCollision("Enemy", "Enemy", true);

            anim.SetBool("isChasing", true);
            playerPositionX = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x;
            float directionToPlayer = Mathf.Abs(playerPositionX - transform.position.x);
            Flip(playerPositionX);
            Vector2 targetPosition = new Vector2(target.position.x + Mathf.Sign(transform.position.x - target.position.x) * xOffset, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }

        if (!isChasing)
        {
            SetLayerCollision("Enemy", "Enemy", false);

            anim.SetBool("isChasing", false);
            alertIcon.SetActive(false);
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
            SetLayerCollision("Enemy", "Enemy", true);

            anim.SetBool("isRetreating", true);
            Debug.Log("Enemy Retreating");
            Debug.Log("Enemy Retreat Point: " + enemyStateMachine.enemyRetreatPoint);
            transform.position = Vector2.MoveTowards(transform.position, new Vector3(enemyStateMachine.enemyRetreatPoint, transform.position.y, transform.position.z), speed * Time.deltaTime);
            anim.SetFloat("Speed", speed);
            return;
        }

        if (!isRetreating)
        {
            SetLayerCollision("Enemy", "Enemy", false);

            anim.SetBool("isRetreating", false);
        }

        else
        {
            // enemy is idle

            playerPositionX = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x;
            float directionToPlayer = Mathf.Abs(playerPositionX - transform.position.x);
            Flip(playerPositionX);

            if (Vector2.Distance(transform.position, target.position) > minDistance)
            {

                transform.position = Vector2.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);

                anim.SetFloat("Speed", speed);

            }

            else
            {
                anim.SetFloat("Speed", 0);
            }
        }
    }

    public void enemyDeath()
    {
        // enemyStateMachine.enemies.RemoveAt(enemyStateMachine.pickedEnemyIndex);
        // Debug.Log("Enemy List: " + enemyStateMachine.enemies);
        // isDead = true;
        //Destroy(gameObject);
    }

    public void enemyDamage()
    {
        Collider2D[] thePlayer = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, player);

        if (thePlayer.Length < 1)
        {
            soundManager.playSfx(soundManager.lightWhoosh);
            Debug.Log("No player to hit in range..");
        }

        else
        {
            foreach (Collider2D playerGameobject in thePlayer)
            {
                if (playerGameobject != null)
                {
                    soundManager.playSfx(soundManager.lightPunch);
                    Debug.Log("PLAYER HIT!");
                    // MODIFY THE LINE BELOW!
                    playerHitCoroutine = StartCoroutine(PlayerGotHit(playerGameobject));
                    playerGameobject.GetComponent<PlayerScript>().health -= dmg;
                    playerHealthBar.fillAmount = playerGameobject.GetComponent<PlayerScript>().health / 100f;

                }
            }
        }
    }

    IEnumerator PlayerGotHit(Collider2D playerGameobject)
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().health > 0)
        {
            playerGameobject.GetComponent<Animator>().SetBool("isHurt", true);
            yield return new WaitForSeconds(0.1f);
            playerGameobject.GetComponent<Animator>().SetBool("isHurt", false);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
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


        Physics2D.IgnoreLayerCollision(layer1Index, layer2Index, !enableCollision);
    }

    // SPECIAL ENEMY ATTACKS

    public void Pull()
    {
        playerTransform.DOMove(TargetOffset(transform), .65f);
    }

    // For gravity pull
    public Vector2 TargetOffset(Transform target)
    {
        Vector2 position;
        position = target.position;
        return Vector2.MoveTowards(position, transform.position, .95f);
    }

    public void PlayerStun()
    {
        stunCoroutine = StartCoroutine(StunCoroutine());
    }

    IEnumerator StunCoroutine()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().speed = 0f;
        yield return new WaitForSeconds(5f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().speed = 8f;
    }

}
