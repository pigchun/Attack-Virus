using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 20; // 恢复的血量值

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 获取玩家的 PlayerHealth 脚本
            Debug.Log("Player picked up health!");
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount); // 恢复血量
                Debug.Log("Player healed: " + healAmount);
            }

            Destroy(gameObject); // 道具消失
        }
    }
}

