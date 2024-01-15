using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    private bool isFacingRight = true;

    public float health;

    Scene currentScene;

    private GameObject player;

    [SerializeField] private Rigidbody2D rb;

    public Animator animator;

    EnemyDetect enemyDetect;

    public float healAmount = 15f;

    public Image playerHealthBar;

    Coroutine stunCoroutine;
    Coroutine healCoroutine;
    Coroutine hitCoroutine;

    bool canHeal;

    public GameObject pauseMenu;


    // Start is called before the first frame update
    void Start()
    {
        canHeal = true;
        health = 100f;
        enemyDetect = GetComponent<EnemyDetect>();
        animator.SetBool("isDead", false);
    }

    // Update is called once per frame
    void Update()
    {
        // Pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        //
        horizontal = Input.GetAxisRaw("Horizontal");
        Flip();
        animator.SetFloat("Speed", Mathf.Abs(horizontal));


        if (health <= 0)
        {
            Debug.Log("YOU DIED!");
            animator.SetBool("isDead", true);
        }

        if (Input.GetKeyDown(KeyCode.R) && health < 100f)
        {
            if (canHeal)
            {
                health += healAmount;
                health = Mathf.Min(health, 100f);
                playerHealthBar.fillAmount = health / 100f;
                canHeal = false;
                healCoroutine = StartCoroutine(HealCoroutine());
            }
        }
    }

    public void playerDeath()
    {
        currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    public void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f || Input.GetMouseButton(0) &&
        (isFacingRight && enemyDetect.closestEnemy.transform.position.x < transform.position.x) || Input.GetMouseButton(0) &&
        (!isFacingRight && enemyDetect.closestEnemy.transform.position.x > transform.position.x))
        {
            Debug.Log("FLIP!");
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void Stunned()
    {
        stunCoroutine = StartCoroutine(StunCoroutine());
    }

    IEnumerator StunCoroutine()
    {
        speed = 0f;
        yield return new WaitForSeconds(3f);
        speed = 8f;
    }

    IEnumerator HealCoroutine()
    {
        yield return new WaitForSeconds(10f);
        canHeal = true;
    }

    public void PlayerGotHit()
    {
        hitCoroutine = StartCoroutine(GotHit());
    }

    IEnumerator GotHit() // Collider2D playerGameobject
    {
        if (health > 0)
        {
            animator.SetBool("isHurt", true);
            yield return new WaitForSeconds(0.1f);
            animator.SetBool("isHurt", false);
        }

    }
}
