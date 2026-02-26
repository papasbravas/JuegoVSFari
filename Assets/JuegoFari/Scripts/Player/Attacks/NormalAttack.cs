using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NormalAttack : MonoBehaviour
{
    [Header("Daño y Hitbox")]
    [SerializeField] public float singleDamage = 10f; // Daño hecho por un ataque normal
    [SerializeField] public float multiDamage = 5f; // Daño hecho por un ataque múltiple
    [SerializeField] private GameObject attackHitboxSingle;
    [SerializeField] private GameObject attackHitboxMulti;

    [Header("Habilidad 1: Aturdimiento")]
    [SerializeField] public Image imageStun; // Imagen del botón de aturdimiento en el HUD
    [SerializeField] public float stunRadius = 5f; // Radio de aturdimiento
    [SerializeField] public float stunDuration = 2f; // Duración del aturdimiento
    [SerializeField] public float stunCoolddown = 10f; // Tiempo de recarga del aturdimiento
    [SerializeField] public bool canStun = true; // Indica si el jugador puede aturdir
    [SerializeField] public ParticleSystem stunEffect; // Efecto visual para el aturdimiento


    [Header("Habilidad 2: Invencible")]
    [SerializeField] public Image imageInvincible; // Imagen del botón de invencibilidad en el HUD
    [SerializeField] public float invincibleDuration = 3f; // Duración de la invencibilidad
    [SerializeField] public bool isInvincible = false; // Indica si el jugador es invencible
    [SerializeField] public float invincibleCooldown = 25f; // Tiempo de recarga de la invencibilidad
    [SerializeField] public bool canUseInvincible = true;
    [SerializeField] public ParticleSystem invincibleEffect; // Efecto visual para la invencibilidad

    [Header("Habilidad 3: Defensa")]
    [SerializeField] public float defenseBuffDuration = 5f; // Duración del buff de defensa
    [SerializeField] public float defenseBuffAmount = 0.5f; // Cantidad de reducción de daño (ejemplo: 0.5 para reducir el daño a la mitad)
    [SerializeField] public bool canUseDefenseBuff = true; // Indica si el jugador puede usar el buff de defensa
    [SerializeField] public float defenseBuffCooldown = 15f; // Tiempo de recarga del buff de defensa
    [SerializeField] public Image imageDefenseBuff; // Imagen del botón de buff de defensa en el HUD
    [SerializeField] public ParticleSystem defenseBuffEffect; // Efecto visual para el buff de defensa

    
    private void Start()
    {
        imageStun.fillAmount = 0f; // Asegura que la imagen del botón de aturdimiento esté llena al inicio
        imageInvincible.fillAmount = 0f; // Asegura que la imagen del botón de invencibilidad esté llena al inicio
        invincibleEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting); // Asegura que el efecto de invencibilidad esté detenido al inicio
        stunEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting); // Asegura que el efecto de aturdimiento esté detenido al inicio
    }

    void Update()
    {
        // Cuando haces click izquierdo
        //if (!GameState.inputEnabled)
        //    return;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Ataque normal activado");
            // anima.SetTrigger("attack"); // Activa la animación de ataque
            singleAttack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Animación de ataque múltiple activada");
            //anima.SetTrigger("attackMulti"); // Activa la animación de ataque múltiple
            multipleAttack();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (canStun)
            {
                Debug.Log("Aturdimiento activado");
                areaStun();
                canStun = false; // Desactiva el aturdimiento hasta que se recargue
                StartCoroutine(StunCooldown());
                StartCoroutine(StunCooldownUI());

            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (canUseInvincible)
            {
                Debug.Log("Invencibilidad activada");
                StartCoroutine(invencibleBuff());
                StartCoroutine(invencibleCooldown());
                StartCoroutine(InvencibleCooldownUI());
            }
        }

        // Si pulsa la tecla con el numero 3, activa el buff de defensa
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (canUseDefenseBuff)
            {
                Debug.Log("Buff de defensa activado");
                StartCoroutine(ActivateDefenseBuff());
                StartCoroutine(DefenseBuffCooldown());
                //StartCoroutine(DefenseBuffCooldownUI());
            }
        }
    }

    IEnumerator ActivateDefenseBuff()
    {
        yield return new WaitForSeconds(defenseBuffDuration);
    }

    IEnumerator DefenseBuffDuration()
    {
        // Debe de reducir a la mitad todo el daño recibido por el jugador durante la duración del buff de defensa
        yield return null;
    }

    IEnumerator DefenseBuffCooldown()
    {
        canUseDefenseBuff = false;
        Debug.Log("Cooldown de buff de defensa iniciado");
        yield return new WaitForSeconds(defenseBuffCooldown);
        canUseDefenseBuff = true;
        Debug.Log("Buff de defensa listo otra vez");
    }

    void singleAttack()
    {
        StartCoroutine(ActivateHitbox());
    }

    IEnumerator ActivateHitbox()
    {
        attackHitboxSingle.SetActive(true);
        yield return new WaitForSeconds(0.2f); // tiempo del golpe
        attackHitboxSingle.SetActive(false);
    }


    void multipleAttack()
    {
        StartCoroutine(ActivateHitboxMult());
    }

    IEnumerator ActivateHitboxMult()
    {
        attackHitboxMulti.SetActive(true);
        yield return new WaitForSeconds(0.2f); // tiempo del golpe
        attackHitboxMulti.SetActive(false);
    }

    void areaStun()
    {
        StartCoroutine(stunDebuff());
    }

    IEnumerator stunDebuff()
    {
        // Obtiene todos los colliders dentro del radio de aturdimiento
        Collider[] hits = Physics.OverlapSphere(transform.position, stunRadius);
        stunEffect.Play(); // Empieza el efecto de aturdimiento
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Enemigo enemy = hit.GetComponent<Enemigo>(); // Obtiene el componente Enemy del enemigo
                if (enemy != null)
                {
                    enemy.ApplyStun(stunDuration);
                    Debug.Log("Enemigo aturdido: " + hit.name); // Imprime el nombre del enemigo aturdido en la consola
                }   
            }
        }

        yield return new WaitForSeconds(stunDuration); // Espera la duración del aturdimiento antes de permitir otro aturdimiento
        stunEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting); // Detiene el efecto de aturdimiento pero deja las partículas que ya están en el aire
        //StartCoroutine(StunCooldown());
    }

    IEnumerator StunCooldown()
    { 
        Debug.Log("Cooldown de stun iniciado"); // Imprime un mensaje indicando que el cooldown ha comenzado
        yield return new WaitForSeconds(stunCoolddown); // Espera el tiempo de recarga del aturdimiento
        canStun = true; // Permite que el jugador pueda aturdir nuevamente
        Debug.Log("Stun listo otra vez"); // Imprime un mensaje indicando que el aturdimiento está listo nuevamente
    }

    IEnumerator invencibleBuff()
    {
        isInvincible = true;
        GetComponent<PlayerHealth>().isInvincible = true;
        invincibleEffect.Play(); // empieza el efecto

        yield return new WaitForSeconds(invincibleDuration);

        isInvincible = false;
        GetComponent<PlayerHealth>().isInvincible = false;
        invincibleEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting); // para el efecto pero deja las partículas que ya están en el aire
        Debug.Log("Invencibilidad terminada");
    }


    IEnumerator invencibleCooldown()
    {
        canUseInvincible = false;

        Debug.Log("Cooldown de invencibilidad iniciado");
        yield return new WaitForSeconds(invincibleCooldown);

        canUseInvincible = true;
        Debug.Log("Invencibilidad lista otra vez");
    }


    IEnumerator StunCooldownUI()
    {
        float tiempo = stunCoolddown;
        imageStun.fillAmount = 1f;

        while (tiempo > 0)
        {
            tiempo -= Time.deltaTime;
            imageStun.fillAmount = tiempo / stunCoolddown;
            yield return null;
        }

        imageStun.fillAmount = 0f;
    }

    IEnumerator InvencibleCooldownUI()
    {
        float tiempo = invincibleCooldown;
        imageInvincible.fillAmount = 1f;
        while (tiempo > 0)
        {
            tiempo -= Time.deltaTime;
            imageInvincible.fillAmount = tiempo / invincibleCooldown;
            yield return null;
        }
        imageInvincible.fillAmount = 0f;
    }

    IEnumerator DefenseCooldownUI()
    {
        float tiempo = defenseBuffCooldown;
        imageDefenseBuff.fillAmount = 1f;
        while (tiempo > 0)
        {
            tiempo -= Time.deltaTime;
            imageDefenseBuff.fillAmount = tiempo / defenseBuffCooldown;
            yield return null;
        }
        imageDefenseBuff.fillAmount = 0f;
    }

}

