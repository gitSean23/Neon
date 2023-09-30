using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private int health = 100;
    private int MAX_HEALTH = 100;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Damage(10);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10);
        }
    }

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Can't damage someone below ZERO health!");

        }

        this.health -= amount;

        if (health <= 0)
        {
            Dead();
        }
    }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Can't have negative healing!");
        }

        bool willBeOverMaxHealth = health + amount > MAX_HEALTH;


        if (willBeOverMaxHealth)
        {
            this.health = MAX_HEALTH;
        }

        else
        {
            this.health += amount;
        }
    }

    private void Dead()
    {
        Debug.Log("RIP");
        Destroy(gameObject);
    }
}
