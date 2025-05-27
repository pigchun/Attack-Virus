using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaDepletionRate = 10f;
    [SerializeField] private float staminaRecoveryRate = 5f;
    [SerializeField] private float staminaCooldownDuration = 2f;  // Cooldown duration in seconds

    private float currentSpeed;
    private float currentStamina;
    private bool isSprinting = false;
    private bool isCooldownActive = false;
    private float staminaCooldownTimer = 0f;

    public Rigidbody2D rb;
    public Animator animator;
    public Slider staminaBar;
    Vector2 movement;

    void Start()
    {
        currentSpeed = moveSpeed;
        currentStamina = maxStamina;

        if (staminaBar != null)
        {
            staminaBar.maxValue = maxStamina;
            staminaBar.value = currentStamina;
        }
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Check if player can sprint (stamina > 0 and cooldown is not active)
        isSprinting = Input.GetKey(KeyCode.LeftShift) && movement.sqrMagnitude > 0 && currentStamina > 0 && !isCooldownActive;

        if (isSprinting)
        {
            currentSpeed = sprintSpeed;
            currentStamina -= staminaDepletionRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

            if (currentStamina <= 0)
            {
                isCooldownActive = true;
                staminaCooldownTimer = staminaCooldownDuration;
            }
        }
        else
        {
            currentSpeed = moveSpeed;

            if (isCooldownActive)
            {
                // Count down the cooldown timer
                staminaCooldownTimer -= Time.deltaTime;
                if (staminaCooldownTimer <= 0)
                {
                    isCooldownActive = false;  // End cooldown after 2 seconds
                }
            }
            else
            {
                // Regenerate stamina when cooldown is not active
                currentStamina += staminaRecoveryRate * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            }
        }

        // Update stamina bar
        if (staminaBar != null)
        {
            staminaBar.value = currentStamina;
        }

        // Update animation parameters
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
    }
}
