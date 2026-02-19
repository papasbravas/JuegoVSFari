using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private float damage; // Cantidad de daño a infligir

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>(); // Obtiene el componente HealthSystem del enemigo
            Debug.Log("Golpeado: " + other.name); // Imprime el nombre del enemigo golpeado en la consola
            enemy.TakeDamage(damage); // Llama al método TakeDamage del enemigo para infligir daño
        }
    }
}
