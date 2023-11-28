using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{

    // take in all of the enemies from the children of the enemy manager
    // put all those enemy game objects into an array of game objects
    // generate a random number ranging between 0 and the length of the enemy array
    // once our random number is picked, we use that as the index to select the specific enemy from the array
    // the picked enemy, we'll call them BRO. BRO will switch to an ATTACK state and after that, BRO will switch to a RETREAT state, and then a PATROL state.
    // after that is done, the cycle will repeat itself. 
    // any time an enemy is defeated, we must decrement the enemy array. 

    public List<Transform> enemies = new List<Transform>();
    public int pickedEnemyIndex;
    Coroutine enemyCycle;

    private int count = -1;

    float playerPositionX;

    public float attackRange = 1.2f;

    public float enemyRetreatPoint;


    // Start is called before the first frame update
    void Start()
    {

        attackRange = 1.2f;
        foreach (Transform enemy in transform)
        {
            count++;
            enemies.Add(enemy);
            enemy.GetComponent<EnemyScript>().enemyIndex = count;
        }

        enemyCycle = StartCoroutine(EnemyCycle());
    }

    // Update is called once per frame
    void Update()
    {

        if (enemyCycle == null && enemies.Count > 0)
        {
            enemyCycle = StartCoroutine(EnemyCycle());
        }
    }

    IEnumerator EnemyCycle()
    {
        Debug.Log("First enemy count: " + enemies.Count);
        while (enemies.Count > 0)
        {

            int randNum = Random.Range(0, enemies.Count);
            Debug.Log("Enemy Count: " + enemies.Count);
            pickedEnemyIndex = randNum;
            if (enemies[pickedEnemyIndex] == null)
            {
                //enemies.RemoveAt(pickedEnemyIndex);
                continue;
            }

            if (enemies[pickedEnemyIndex] != null)
            {
                EnemyScript pickedEnemy = enemies[pickedEnemyIndex].GetComponent<EnemyScript>();

                if (pickedEnemy == null)
                {
                    continue;
                }

                enemyRetreatPoint = pickedEnemy.transform.position.x;
                pickedEnemy.isIdle = false;
                pickedEnemy.isChasing = true;

                if (pickedEnemy == null)
                {
                    continue;
                }

                yield return new WaitUntil(() => pickedEnemy == null || Mathf.Abs(pickedEnemy.transform.position.x - GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x) <= attackRange);

                if (pickedEnemy == null)
                {
                    continue;
                }

                pickedEnemy.isChasing = false;
                pickedEnemy.isAttacking = true;

                yield return new WaitForSeconds(1f);

                pickedEnemy.isAttacking = false;
                pickedEnemy.isRetreating = true;

                if (pickedEnemy == null)
                {
                    continue;
                }

                yield return new WaitUntil(() => pickedEnemy == null || Mathf.Abs(pickedEnemy.transform.position.x) >= Mathf.Abs(enemyRetreatPoint));

                if (pickedEnemy == null)
                {
                    continue;
                }

                pickedEnemy.isRetreating = false;
                pickedEnemy.isIdle = true;

                Debug.Log("Enemy Count: " + enemies.Count);

                yield return new WaitForSeconds(0.5f);
            }
        }

        StopCoroutine(enemyCycle);
        enemyCycle = null;
    }

    public int GetEnemyCount()
    {
        return enemies.Count;
    }
}
