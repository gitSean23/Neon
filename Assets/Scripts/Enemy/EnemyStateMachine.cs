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

    public List<Transform> enemies;
    public int pickedEnemyIndex;
    Coroutine enemyCycle;

    private int count = -1;

    float playerPositionX;

    float attackRange = 1.2f;



    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<Transform>();
        //enemyCycle = null;

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

    }

    IEnumerator EnemyCycle()
    {
        while (true)
        {
            playerPositionX = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x;
            // picks an enemy to be the attacker
            if (enemies.Count > 0)
            {
                int randNum = Random.Range(0, enemies.Count);
                pickedEnemyIndex = randNum;
                if (enemies[pickedEnemyIndex] == null)
                {
                    enemies.RemoveAt(pickedEnemyIndex);
                    yield return null;
                }
                EnemyScript pickedEnemy = enemies[pickedEnemyIndex].GetComponent<EnemyScript>();
                pickedEnemy.isIdle = false;
                pickedEnemy.isChasing = true;
                Debug.Log(Mathf.Abs(pickedEnemy.transform.position.x - playerPositionX));
                yield return new WaitUntil(() => Mathf.Abs(pickedEnemy.transform.position.x - playerPositionX) <= attackRange);
                //yield return new WaitForSeconds(2.5f);
                pickedEnemy.isChasing = false;
                pickedEnemy.isAttacking = true;
                yield return new WaitForSeconds(2f);
                pickedEnemy.isAttacking = false;
                pickedEnemy.isRetreating = true;
                pickedEnemy.isIdle = true;
                yield return new WaitForSeconds(1f);
            }

            else
            {
                yield return null;
            }

            yield return new WaitForSeconds(2f);
        }
    }
}
