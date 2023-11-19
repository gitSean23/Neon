using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FreeFlowCombatScript : MonoBehaviour
{
    public float comboResetTime = 0.8f;
    // public KeyCode attackButton = KeyCode.F;
    // public KeyCode resetButton = KeyCode.E;

    private float lastAttackTime = 0;
    //private int comboCount = 0;
    public static int realComboCount = 0;

    private float nextAttackTime = 0f;

    float maxComboDelay = 0.5f;

    private Animator anim;
    //private Animator enemyAnim;

    public GameObject attackPoint;

    public float radius;

    public LayerMask enemies;

    public float dmg = 15;
    int currentCombatMode = 0;

    SoundScript soundManager;

    Coroutine enemyHitCoroutine;

    void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundScript>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        //enemyAnim = GetC
    }

    void Update()
    {
        if (currentCombatMode == 0)
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
            }

            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerKick2")) // anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && 
            {
                Debug.Log("Ending attack4");
                anim.SetBool("attack4", false);
                realComboCount = 0; // ONLY put this line on the last move of the combo
            }
        }

        // if (currentCombatMode == 1)
        // {
        //     if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("playerPunch3")) // anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && 
        //     {
        //         Debug.Log("Ending AI attack1");
        //         anim.SetBool("aiAttack1", false);
        //     }

        //     if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerPunch4")) // anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && 
        //     {
        //         Debug.Log("Ending AI attack2");
        //         anim.SetBool("aiAttack2", false);
        //         realComboCount = 0;
        //     }
        // }

        if (Time.time - lastAttackTime > maxComboDelay || Input.GetKeyDown(KeyCode.E))
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
        Debug.Log("REAL COMBO COUNT: " + realComboCount);

        if (realComboCount == 1)
        {
            Debug.Log("Transitioning to Attack 1!");
            anim.SetBool("attack1", true);
        }

        realComboCount = Mathf.Clamp(realComboCount, 0, 4); // Update the last number whenever you add or remove an attack
        Debug.Log("Human Combo: " + realComboCount);

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

        if (realComboCount >= 4 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Light_Kick")) //anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("attack2")
        {
            Debug.Log("Transitioning to Attack 4!");
            anim.SetBool("attack3", false);
            anim.SetBool("attack4", true);
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
                    enemyHitCoroutine = StartCoroutine(EnemyGotHit(enemyGameobject));
                    //enemyGameobject.GetComponent<Animator>().SetBool("EnemyHit", true);
                    enemyGameobject.GetComponent<EnemyScript>().health -= dmg;
                    enemyGameobject.GetComponent<Animator>().SetFloat("Health", enemyGameobject.GetComponent<EnemyScript>().health);
                    //StopCoroutine(enemyHitCoroutine);
                    //enemyGameobject.GetComponent<Animator>().SetBool("EnemyHit", false);
                }
            }
        }
    }

    IEnumerator EnemyGotHit(Collider2D enemyGameobject)
    {
        if (enemyGameobject != null)
        {
            if (enemyGameobject.GetComponent<Animator>() != null)
            {
                enemyGameobject.GetComponent<Animator>().SetBool("isStunned", true);
                yield return new WaitForSeconds(0.1f);
                enemyGameobject.GetComponent<Animator>().SetBool("isStunned", false);
            }
        }

        else
        {
            Debug.Log("Enemy prob died..");
            yield return null;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }

}