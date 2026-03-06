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


    // Update is called once per frame
    void Update()
    {
        
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
}
