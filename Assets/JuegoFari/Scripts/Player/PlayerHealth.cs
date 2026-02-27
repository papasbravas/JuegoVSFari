using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Ajustes de Salud del Jugador")]
    [SerializeField] private float maxHealth = 100f; // Salud máxima del jugador
    private float currentHealth; // Salud actual del jugador
    public bool isInvincible = false; // Indica si el jugador es invencible 
    public bool hasDefenseBuff = false;
    [SerializeField] private float defenseMultiplier = 0.5f; // 50% de daño reducido cuando el buff de defensa está activo


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth; // Inicializa la salud actual al máximo
        HUDHealth.Instance.UpdateHealthBar(currentHealth, maxHealth); // Actualiza la barra de salud en el HUD
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible)
        {
            Debug.Log("Jugador es invencible y no recibe daño.");
            return;
        }

        float finalDamage = damage;

        if (hasDefenseBuff)
        {
            finalDamage *= defenseMultiplier;
            Debug.Log("Buff defensivo activo. Daño reducido a: " + finalDamage);
        }

        currentHealth -= finalDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Jugador ha recibido daño: " + finalDamage + ". Salud actual: " + currentHealth);
        HUDHealth.Instance.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("MenuInicio");
        }
    }
}
