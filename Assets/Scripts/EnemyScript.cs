using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public float health;
    public float currHealth;
    // private Animator anim;
    // Start is called before the first frame update
    void Start()
    {

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
            Destroy(gameObject);
        }
    }
}
