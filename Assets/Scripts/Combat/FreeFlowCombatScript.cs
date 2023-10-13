using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FreeFlowCombatScript : MonoBehaviour
{
    public float comboResetTime = 2f;
    // public KeyCode attackButton = KeyCode.F;
    // public KeyCode resetButton = KeyCode.E;

    private float lastAttackTime;
    private int comboCount = 0;
    private int realComboCount = 0;

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
        if (Input.GetMouseButton(0))
        {
            attack();

        }

        if (Time.time - lastAttackTime <= comboResetTime)
        {
            realComboCount = 0;
            Debug.Log("COMBO RESET!");
        }

        else
        {
            comboCount = 0;
            realComboCount = 0;
            //endAttack();
            Debug.Log("Combo 1");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            comboCount = 0;
            realComboCount = 0;
            endAttack();
            Debug.Log("Combo Reset");
        }

    }

    // IEnumerator something()
    // {
    //     if (Time.time - lastAttackTime <= comboResetTime)
    //     {
    //         comboCount++;
    //         realComboCount++;
    //         //Debug.Log("Combo: " + comboCount);
    //     }

    //     attack();
    //     lastAttackTime = Time.time;
    //     yield return new WaitForSeconds(.5f);
    // }

    public void attack()
    {
        lastAttackTime = Time.time;

        realComboCount++;

        switch (realComboCount)
        {

            case 0:
                Debug.Log("Attack ZERO");
                break;

            case 1:
                Debug.Log("Attack 1");
                anim.SetBool("isAttacking", true);
                break;

            case 2:
                anim.SetBool("isAttacking", true);
                Debug.Log("Attack 2");
                anim.SetTrigger("Combo1");
                break;

            case 3:
                anim.SetBool("isAttacking", true);
                Debug.Log("Attack 3");
                anim.SetTrigger("Combo2");
                break;

            // case 4:
            //     realComboCount = 0;
            //     Debug.Log("Attack 4");
            //     // anim.SetTrigger("Combo3");
            //     break;

            default:
                realComboCount = 1;
                Debug.Log("Cycling back to Attack 1");
                anim.SetBool("isAttacking", true);
                //Debug.Log("Max combo reached!");
                break;
        }
    }

    public void attackCollide()
    {
        //Debug.Log("Collide detection activated..");

        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);

        if (enemy.Length < 1)
        {
            soundManager.playSfx(soundManager.lightWhoosh);
            Debug.Log("No enemies to hit in range..");
        }

        else
        {
            foreach (Collider2D enemyGameobject in enemy)
            {
                if (enemyGameobject != null)
                {
                    soundManager.playSfx(soundManager.lightPunch);
                    Debug.Log("ENEMY HIT!");
                    enemyGameobject.GetComponent<EnemyScript>().health -= dmg;
                }
            }
        }
    }


    public void endAttack()
    {
        anim.SetBool("isAttacking", false);
    }

    // For future use, make one endCombo method 
    // and just put in an arguement for which combo to end
    public void endCombo()
    {
        anim.ResetTrigger("Combo1");
    }

    public void endCombo2()
    {
        anim.ResetTrigger("Combo2");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }

}