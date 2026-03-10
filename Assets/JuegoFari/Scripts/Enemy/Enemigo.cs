using System.Collections;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] protected float velocidad = 1f;
    [SerializeField] protected float velocidadPersecucion = 3f;
    private float velocidadOriginal;
    private float velocidadPersecucionOriginal;
    private Coroutine slowCoroutine;
    private bool isSlowed = false;

    public int rutina;
    public float cronometro;
    public Animator animator;
    public Quaternion angulo;
    public float grado;
    

    public  bool atacando;
    [SerializeField] private BoxCollider colliderAtaque;

    public GameObject target;
    public bool isStunned = false;
    [SerializeField] private float health; // Salud del enemigo
    [SerializeField] private GameObject dotEffect; // Prefab del efecto de daño por segundo (DOT) para mostrar visualmente el daño continuo

    [SerializeField] private AudioSource audioSource; // Componente de audio para reproducir sonidos del enemigo
    [SerializeField] private AudioClip[] ataques; // Sonido que se reproducirá al atacar
    [SerializeField] private AudioClip muerte;
    [SerializeField] private AudioClip hit;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player");

        velocidadOriginal = velocidad;
        velocidadPersecucionOriginal = velocidadPersecucion;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStunned) 
        { 
            ComportamientoEnemigo(); 
        }
        else
        { // Si está aturdido, se queda quieto
          animator.SetBool("walk", false);
          animator.SetBool("run", false); 
          animator.SetBool("attack", false); 
        }
    }

    public void ComportamientoEnemigo()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 5)
        {
            animator.SetBool("run", false);
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }

            switch (rutina)
            {
                case 0:
                    animator.SetBool("walk", false);
                    break;
                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
                    animator.SetBool("walk", true);
                    break;
            }
        }
        else
        {
            if(Vector3.Distance(transform.position, target.transform.position) > 1.9 && !atacando){     //1.9 es la diustancia justa para que le de al jugador
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
                animator.SetBool("walk", false);

                animator.SetBool("run", true);
                transform.Translate(Vector3.forward * velocidadPersecucion * Time.deltaTime);

                animator.SetBool("attack", false);
            } else
            {
                animator.SetBool("walk", false);
                animator.SetBool("run", false);

                animator.SetBool("attack", true);
                atacando = true;
            }
        }
        
    }

    public void Final_Ani()
    {
        animator.SetBool("attack", false);
        atacando = false;
    }

    public void ApplyStun(float duration)
    {
        if (!isStunned)
        {
            StartCoroutine(StunEffect(duration)); // Inicia la corrutina para el efecto de aturdimiento
        }
    }

    public void ApplySlow(float slowMultiplier, float duration)
    {
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
        }

        slowCoroutine = StartCoroutine(SlowEffect(slowMultiplier, duration));
    }

    private IEnumerator SlowEffect(float slowMultiplier, float duration)
    {
        isSlowed = true;

        velocidad = velocidadOriginal * slowMultiplier;
        velocidadPersecucion = velocidadPersecucionOriginal * slowMultiplier;

        Debug.Log(gameObject.name + " ralentizado x" + slowMultiplier + " durante " + duration + " segundos");

        yield return new WaitForSeconds(duration);

        velocidad = velocidadOriginal;
        velocidadPersecucion = velocidadPersecucionOriginal;

        isSlowed = false;
        slowCoroutine = null;

        Debug.Log(gameObject.name + " recuperó su velocidad normal");
    }

    IEnumerator StunEffect(float duration)
    {
        isStunned = true; // Marca al enemigo como aturdido
        Debug.Log("Enemigo aturdido por " + duration + " segundos"); // Imprime un mensaje en la consola indicando que el enemigo está aturdido
        yield return new WaitForSeconds(duration); // Espera la duración del aturdimiento
        isStunned = false; // Marca al enemigo como no aturdido
        Debug.Log("Enemigo recuperado del aturdimiento"); // Imprime un mensaje en la consola indicando que el enemigo se ha recuperado del aturdimiento
    }

    public void TakeDamage(float damage)
    {
        health -= damage; // Resta el daño a la salud del enemigo
        SonidoHit(); // Reproduce el sonido de impacto al recibir daño
        Debug.Log(health); // Imprime la salud actual del enemigo en la consola
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

    public void SonidoAtaque()
    {
        int n = Random.Range(0,3);
        audioSource.PlayOneShot(ataques[n]);
    }

    public void SonidoMuerte()
    {
        audioSource.PlayOneShot(muerte);
    }

    public void SonidoHit()
        {
            audioSource.PlayOneShot(hit);
    }

    public void DestruyeEnemigo()
    {
        Destroy(gameObject); // Destruye el objeto del enemigo
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag("Player"))
    //    {
    //        Debug.Log("Has recibido daño");
    //        colliderAtaque.enabled = false;
    //    }
    //}
}
