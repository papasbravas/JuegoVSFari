using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health; // Salud del enemigo
    private bool isStunned = false; // Indica si el enemigo está aturdido

    public void TakeDamage(float damage)
    {
        health -= damage; // Resta el daño a la salud del enemigo
        Debug.Log(health); // Imprime la salud actual del enemigo en la consola
    }


    //public void ApplyStun(float duration)
    //{
    //    if (!isStunned)
    //    {
    //        StartCoroutine(StunEffect(duration)); // Inicia la corrutina para el efecto de aturdimiento
    //    }
    //}

    //IEnumerator StunEffect(float duration)
    //{
    //    isStunned = true; // Marca al enemigo como aturdido
    //    Debug.Log("Enemigo aturdido por " + duration + " segundos"); // Imprime un mensaje en la consola indicando que el enemigo está aturdido
    //    yield return new WaitForSeconds(duration); // Espera la duración del aturdimiento
    //    isStunned = false; // Marca al enemigo como no aturdido
    //    Debug.Log("Enemigo recuperado del aturdimiento"); // Imprime un mensaje en la consola indicando que el enemigo se ha recuperado del aturdimiento
    //}
}
