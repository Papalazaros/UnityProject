using UnityEngine;
using UnityEngine.UI;

public class Sprint : MonoBehaviour
{
    public float rechargeRate;
    public float speed;
    public float maxStamina;
    public KeyCode sprintKey = KeyCode.LeftShift;
    private bool canSprint;
    private float currentStamina;
    private float rechargeDelay;

    public Slider StaminaBar;

    private void Start()
    {
        currentStamina = maxStamina;
    }

    private void Update()
    {
        canSprint = currentStamina > 0;

        if (Input.GetKey(KeyCode.LeftShift) && canSprint && rechargeDelay <= 0)
        {
            Player.instance.currentMovementSpeed = Player.instance.baseMovementSpeed * speed;
            currentStamina -= Time.deltaTime * 25;
            rechargeDelay = 1.0f;
        }

        if (currentStamina < maxStamina && !Input.GetKey(KeyCode.LeftShift))
        {
            currentStamina = Mathf.Clamp(currentStamina + (Time.deltaTime * 10), 0, maxStamina);
        }

        if (!Input.GetKey(KeyCode.LeftShift) || !canSprint)
        {
            Player.instance.currentMovementSpeed = Player.instance.baseMovementSpeed;
        }

        if (rechargeDelay > 0)
        {
            rechargeDelay -= Time.deltaTime;
        }

        StaminaBar.value = currentStamina;
    }
}
