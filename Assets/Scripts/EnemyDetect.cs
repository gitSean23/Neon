using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyDetect : MonoBehaviour
{
    // [SerializeField] private EnemyManager enemyManager;
    // private MovementInput movementInput;
    // private CombatScript combatScript;
    // public LayerMask layerMask;
    // [SerializeField] Vector2 inputDirection;
    [SerializeField] private GameObject currTarget;
    [SerializeField] public float lockOnRange = 10f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject closestEnemy = null;
            float closestDistance = lockOnRange;

            foreach (GameObject enemy in enemies)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }

            if (closestEnemy != null)
            {
                currTarget = closestEnemy;
                Debug.Log("Locked onto " + currTarget.name);
                MoveToTarget(currTarget, .65f);

            }

            else
            {
                currTarget = null;
                Debug.Log("No enemy in range..");
            }

        }
    }

    void MoveToTarget(GameObject target, float duration)
    {
        //transform.DOLookAt(target.transform.position, .2f);
        transform.DOMove(TargetOffset(target.transform), duration);
    }

    public Vector2 TargetOffset(Transform target)
    {
        Vector2 position;
        position = target.position;
        return Vector2.MoveTowards(position, transform.position, .95f);
    }
}
