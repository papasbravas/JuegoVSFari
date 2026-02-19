using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    [Header("Configuracion")] // Encabezado para la sección de configuración en el Inspector
    [SerializeField] private float damage = 10f; // Daño que inflige el enemigo al jugador

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return; // Verifica si el objeto que colisiona tiene la etiqueta "Player"

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>(); // Obtener el componente de salud del jugador
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>(); // Obtener el componente de movimiento del jugador

        Debug.Log("Colision con el jugador detectada.");
        if(playerHealth != null)
        {
            playerHealth.TakeDamage(damage); // Infligir daño al jugador
            Debug.Log("Daño infligido al jugador: " + damage);
        }
    }
}
