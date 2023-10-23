using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public float health;
    public float currHealth;

    //private Animator enemyAnim;
    //private Animator enemyAnim;
    // Start is called before the first frame update
    void Start()
    {
        //enemyAnim = GetComponent<Animator>();
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
            //WaitForSeconds(3f);
            // ADD the Destroy() back in
            //Destroy(gameObject);
        }
    }

    public void enemyDeath()
    {
        Destroy(gameObject);
    }
}
