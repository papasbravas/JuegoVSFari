using Ilumisoft.HealthSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Animator animator;
    public GameObject target;
    public int velocidadPersecucion = 3;
    private int velocidad;
    private int velocidadOriginal;
    private int velocidadPersecucionOriginal;
    private Coroutine slowCoroutine;
    private bool isSlowed = false;
    [Header("Vida")]
    public float HP_Min;    //vida minima
    public float HP_Max;    //vida maxima
    public Image barra;   //barra de vida
    public bool muerto;     //boss muerto
    [SerializeField] private GameObject dotEffect;

    [SerializeField] private GameObject portal;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player");
        velocidadOriginal = velocidad;
        velocidadPersecucionOriginal = velocidadPersecucion;
        //barra = GameObject.Find("BorderMask").GetComponent<Image>();
        if (portal != null)
        {
            portal.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        HP_Min -= damage; // Resta el daño a la salud del enemigo
        if (HP_Min <= 0)
        {
            animator.SetTrigger("dead"); // Activa la animación de muerte del enemigo

        }
        Debug.Log(HP_Min); // Imprime la salud actual del enemigo en la consola
    }


    public void AparecePortal()
    {
        portal.SetActive(true);
    }

    public void TakeDamage(float damage)
    {
        HP_Min -= damage; // Resta el daño a la salud del enemigo
    }

    public void DestruyeEnemigo()
    {
        Destroy(gameObject); // Destruye el GameObject del enemigo
    }

    public void ApplySlow(int slowMultiplier, float duration)
    {
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
        }

        slowCoroutine = StartCoroutine(SlowEffect(slowMultiplier, duration));
    }

    private IEnumerator SlowEffect(int slowMultiplier, float duration)
    {
        isSlowed = true;
        Debug.Log("velocidad original: " + velocidadOriginal + ", velocidad persecucion original: " + velocidadPersecucionOriginal);
        velocidad = velocidadOriginal / slowMultiplier;
        velocidadPersecucion = velocidadPersecucionOriginal / slowMultiplier;
        Debug.Log("Velocidad al lentizar: " + velocidad + ", velocidad persecucion al ralentizar: " + velocidadPersecucion);
        Debug.Log(gameObject.name + " ralentizado x" + slowMultiplier + " durante " + duration + " segundos");

        yield return new WaitForSeconds(duration);

        velocidad = velocidadOriginal;
        velocidadPersecucion = velocidadPersecucionOriginal;

        isSlowed = false;
        slowCoroutine = null;

        Debug.Log(gameObject.name + " recuperó su velocidad normal");
    }

    public void ApplyDamageOverTime(float damagePerSecond, float duration)
    {
        StartCoroutine(DamageOverTime(damagePerSecond, duration));
    }

    private IEnumerator DamageOverTime(float damagePerSecond, float duration)
    {
        float elapsed = 0f;
        GameObject e = null;
        if (dotEffect != null)
        {
            //dotEffect.SetActive(true);
            Debug.Log("Hola efecto");
            e = Instantiate(dotEffect, transform.position, Quaternion.identity); // Instancia el efecto de daño por segundo en la posición del enemigo y lo hace hijo del enemigo para que siga su movimiento
        }
        while (elapsed < duration)
        {
            TakeDamage(damagePerSecond);
            yield return new WaitForSeconds(1f);
            elapsed += 1f;
        }

        if (dotEffect != null)
        {
            Debug.Log("Adios efecto");
            Destroy(e);
        }

    }

}
