using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerNameSpace;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    private Animator animator;
    private bool isDead = false;
    public HealthBar healthBar;
    public int currHealth;

    void Start()
    {
        animator = GetComponent<Animator>();
        currHealth = health;
        healthBar.SetMaxHealth(health);
    }

    void Update()
    {
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("IsDead");
        // ��ֹ��ҿ��ƽ�ɫ�ƶ��Ȳ���
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerAimWeapon>().enabled = false;
        Debug.Log("Player Died");
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currHealth -= damage;
        Debug.Log("Player Health: " + currHealth);

        // ���� HealthBar
        healthBar.SetHealth(currHealth);

        if (currHealth <= 0)
        {
            Die();
            Player.playerAlive = 0;
        }
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currHealth = Mathf.Min(currHealth + amount, health); // 确保血量不会超过最大值
        healthBar.SetHealth(currHealth); // 更新血量 UI
        Debug.Log("Health Restored: " + currHealth);
    }

}

