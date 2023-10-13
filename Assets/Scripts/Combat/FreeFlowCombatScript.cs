using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FreeFlowCombatScript : MonoBehaviour
{
    public float comboResetTime = 2f;
    // public KeyCode attackButton = KeyCode.F;
    // public KeyCode resetButton = KeyCode.E;

    private float lastAttackTime = 0;
    private int comboCount = 0;
    public static int realComboCount = 0;

    private float nextAttackTime = 0f;

    float maxComboDelay = 1;

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

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerPunch")) //anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && 
        {
            Debug.Log("Ending attack1");
            anim.SetBool("attack1", false);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerPunch2")) // anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f &&
        {
            Debug.Log("Ending attack2");
            anim.SetBool("attack2", false);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Light_Kick")) // anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && 
        {
            Debug.Log("Ending attack3");
            anim.SetBool("attack3", false);
            realComboCount = 0;
        }

        if (Time.time - lastAttackTime > maxComboDelay)
        {
            realComboCount = 0;
        }

        if (Time.time > nextAttackTime)
        {
            if (Input.GetMouseButton(0))
            {
                attack();
            }
        }





    }



    public void attack()
    {
        lastAttackTime = Time.time;
        realComboCount++;

        if (realComboCount == 1)
        {
            Debug.Log("Transitioning to Attack 1!");
            anim.SetBool("attack1", true);
        }

        realComboCount = Mathf.Clamp(realComboCount, 0, 3);
        Debug.Log("Combo: " + realComboCount);

        if (realComboCount >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerPunch")) //anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("attack1")
        {
            Debug.Log("Transitioning to Attack 2!");
            anim.SetBool("attack1", false);
            anim.SetBool("attack2", true);
        }

        if (realComboCount >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerPunch2")) //anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("attack2")
        {
            Debug.Log("Transitioning to Attack 3!");
            anim.SetBool("attack2", false);
            anim.SetBool("attack3", true);
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




    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }

}