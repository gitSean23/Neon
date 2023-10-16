using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private bool isFacingRight = true;

    private GameObject player;

    [SerializeField] private Rigidbody2D rb;

    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(-0.125499994f, -0.0500000007f, 0.0f);
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

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        // rb.AddForce(Physics.gravity, ForceMode2D.Acceleration);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
