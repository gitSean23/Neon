using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FB2Script : MonoBehaviour
{

    float enemyRetreatPoint;
    float playerPositionX;
    public Transform target;

    public Animator anim;
    [SerializeField] public float attackRange;
    public GameObject alertIcon;
    Coroutine bossCycle;

    float speed = 4f;
    float xOffset = 0.9f;

    float dmg = 20f;

    SoundScript soundManager;

    public float radius;

    public Image playerHealthBar;
    public LayerMask player;
    Coroutine playerHitCoroutine;

    public GameObject attackPoint;
    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundScript>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bossCycle = StartCoroutine(BossCycle());
    }

    IEnumerator BossCycle()
    {
        enemyRetreatPoint = transform.position.x;


        anim.SetBool("isIdle", false);
        anim.SetBool("isChasing", true);
        alertIcon.SetActive(true);
        Chase();


        yield return new WaitUntil(() => Mathf.Abs(transform.position.x - GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x) <= attackRange);



        anim.SetBool("isChasing", false);
        alertIcon.SetActive(false);
        anim.SetBool("isAttacking", true);

        yield return new WaitForSeconds(1f);

        anim.SetBool("isAttacking", false);
        anim.SetBool("isRetreating", true);
        transform.position = Vector2.MoveTowards(transform.position, new Vector3(enemyRetreatPoint, transform.position.y, transform.position.z), speed * Time.deltaTime);



        yield return new WaitUntil(() => Mathf.Abs(transform.position.x) >= Mathf.Abs(enemyRetreatPoint));


        anim.SetBool("isRetreating", false);
        anim.SetBool("isIdle", true);


        yield return new WaitForSeconds(2f);
    }

    public void Chase()
    {
        playerPositionX = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x;
        float directionToPlayer = Mathf.Abs(playerPositionX - transform.position.x);
        Flip(playerPositionX);
        Vector2 targetPosition = new Vector2(target.position.x + Mathf.Sign(transform.position.x - target.position.x) * xOffset, transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
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


    public void FlurryOfPunches()
    {

    }

    public void Stun()
    {

    }

    public void Heal()
    {

    }


}
