using Ilumisoft.HealthSystem;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Animator animator;
    public GameObject target;
    public int velocidadPersecucion = 3;
    [Header("Vida")]
    public float HP_Min;    //vida minima
    public float HP_Max;    //vida maxima
    public Image barra;   //barra de vida
    public bool muerto;     //boss muerto

    [SerializeField] private GameObject portal;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player");
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
}
