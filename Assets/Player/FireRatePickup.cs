using UnityEngine;

public class FireRatePickup : MonoBehaviour
{
    public float fireRateMultiplier = 0.5f; // 减少射速间隔（例如 0.5 表示射速加快一倍）
    public float duration = 5f; // 提升射速的持续时间（秒）

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 获取玩家的射击脚本
            PlayerAimWeapon playerAimWeapon = collision.GetComponent<PlayerAimWeapon>();
            if (playerAimWeapon != null)
            {
                // 修改玩家的射速
                playerAimWeapon.ModifyFireRate(fireRateMultiplier, duration);
                Destroy(gameObject); // 销毁道具
            }
        }
    }
}

