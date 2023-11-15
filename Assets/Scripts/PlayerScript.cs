using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private bool isFacingRight = true;

    public float health;

    Scene currentScene;

    private GameObject player;

    [SerializeField] private Rigidbody2D rb;

    public Animator animator;

    EnemyDetect enemyDetect;


    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(-0.125499994f, -0.0500000007f, 0.0f);
        health = 100f;
        enemyDetect = GetComponent<EnemyDetect>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        Flip();
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        // if (Input.GetMouseButtonDown(0))
        // {
        //     rb.AddForce(P)
        // }
        if (health <= 0)
        {
            Debug.Log("YOU DIED!");
            currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        // rb.AddForce(Physics.gravity, ForceMode2D.Acceleration);
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
}
