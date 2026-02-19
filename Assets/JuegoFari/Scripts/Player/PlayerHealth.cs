using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Ajustes de Salud del Jugador")]
    [SerializeField] private float maxHealth = 100f; // Salud máxima del jugador
    private float currentHealth; // Salud actual del jugador
    public bool isInvincible = false; // Indica si el jugador es invencible 



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
            return; // No aplicar daño si el jugador es invencible
        }

        currentHealth -= damage; // Restar el daño a la salud actual
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegurarse de que la salud no sea menor que 0 ni mayor que la máxima

        Debug.Log("Jugador ha recibido daño: " + damage + ". Salud actual: " + currentHealth);
        HUDHealth.Instance.UpdateHealthBar(currentHealth, maxHealth); // Actualiza la barra de salud en el HUD

        if(currentHealth <= 0)
        {
            // Llamar al metodo de morir o mandar a menu de inicio
        }
    }
}
