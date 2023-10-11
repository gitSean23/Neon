using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FreeFlowCombatScript : MonoBehaviour
{
    public float comboResetTime = 0.1f;
    // public KeyCode attackButton = KeyCode.F;
    // public KeyCode resetButton = KeyCode.E;

    private float lastAttackTime;
    private int comboCount = 0;

    private Animator anim;

    public GameObject attackPoint;

    public float radius;

    public LayerMask enemies;

    public float dmg = 20;

    SoundScript soundManager;

    void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundScript>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastAttackTime <= comboResetTime)
            {
                comboCount++;
                Debug.Log("Combo: " + comboCount);

            }

            else
            {
                comboCount = 1;
                Debug.Log("Combo 1");
            }

            attack();
            lastAttackTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            comboCount = 0;
            Debug.Log("Combo Reset");
        }
    }

    public void attack()
    {
        switch (comboCount)
        {
            case 1:
                Debug.Log("Attack 1");
                anim.SetBool("isAttacking", true);
                break;

            case 2:
                Debug.Log("Attack 2");
                anim.SetTrigger("Combo1");
                break;

            case 3:
                Debug.Log("Attack 3");
                break;

            case 4:
                Debug.Log("Attack 4");
                break;

            default:
                Debug.Log("Max combo reached!");
                break;
        }
    }

    public void attackCollide()
    {
        //Debug.Log("Collide detection activated..");

        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            soundManager.playSfx(soundManager.lightPunch);
            Debug.Log("ENEMY HIT!");
            enemyGameobject.GetComponent<EnemyScript>().health -= dmg;
        }
    }


    public void endAttack()
    {
        anim.SetBool("isAttacking", false);
    }

    public void endCombo()
    {
        anim.ResetTrigger("Combo1");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
    // private EnemyManager enemyManager;
    //     private EnemyDetection2D enemyDetection;
    //     // private MovementInput2D movementInput;
    //     // private Animator animator;

    //     private int comboCount = 0;
    //     private float lastAttackTime = 0f;
    //     private float comboResetTime = 1.5f
    //     // Start is called before the first frame update
    //     void Start()
    //     {
    //         // enemyManager = FindObjectOfType<EnemyManager>();
    //         // animator = GetComponent<Animator>();
    //         enemyDetection = GetComponentInChildren<EnemyDetection2D>();
    //         // movementInput = GetComponent<MovementInput2D>(); 
    //         // impulseSource = GetComponentInChildren<CinemachineImpulseSource2D>(); 
    //     }

    //     // After locking onto an enemy..
    //     // Reset the combo
    //     // When the player attacks, start checking the player's attacks

    //     // Update is called once per frame
    //     void Update()
    //     {
    //         if (Time.time - lastAttackTime > comboResetTime)
    //         {
    //             comboCount = 0; // Reset the combo count if no attack within comboResetTime
    //         }


    //     }

    //     void AttackCheck()
    //     {
    //         if (isAttackingEnemy)
    //             return;

    //         if (enemyDetection.currTarget() == null)
    //         {
    //             if (enemyManager.AliveEnemyCount() == 0)
    //             {
    //                 AttackC(null, 0);
    //                 return;
    //             }

    //             else
    //                 lockedTarget = enemyManager.RandomEnemy();
    //         }
    //     }

    //     if (enemyDetection.InputMagnitude() > .2f)
    //         lockedTarget = enemyDetection.currTarget();

    //     if (lockedTarget == null)
    //         lockedTarget = enemyManager.RandomEnemy();

    //     if (lockedTarget != null && lockedTarget != lastComboTarget)
    //         comboCount = 0;

    //     Attack(lockedTarget, TargetDistance(lockedTarget));
    // }

    // public void Attack(EnemyScript target, float distance)
    // {
    //     string[] attacks = new string[] { "Kick", "Punch" };

    //     if (target == null)
    //     {
    //         AttackType("Stand", .2f, null, 0);
    //         return;
    //     }

    //     if (distance < 10)
    //     {
    //         if (Time.time - lastAttackTime > comboResetTime) ;
    //         comboCount = 0;

    //         animationCount = (int)Mathf.Repeat((float)animationCount + 1, (float)attacks.Length);
    //         string attackString = isLastHit() ? attacks[Random.Range(0, attacks.Length)] : attacks[animationCount];

    //         comboCount++;

    //         if (target != lastComboTarget)
    //             comboCount = 1;

    //         if (comboCount > 1)
    //             attackString = "ComboAttack" + comboCount;

    //         AttackType(attackString, attackCooldown, target, .05f);

    //         lastAttackTime = Time.time;

    //         lastComboTarget = target;

    //     }

    //     else
    //     {
    //         lockedTarget = null;
    //         AttackType("Stand", .2f, null, 0);
    //     }

    //     impulseSource.m_ImpulseDefinition.m_AmplitudeGain = Mathf.Max(3, 1 * distance);
}