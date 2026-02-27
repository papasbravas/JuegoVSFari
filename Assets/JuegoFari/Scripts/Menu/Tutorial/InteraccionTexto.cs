using UnityEngine;
using UnityEngine.SceneManagement;

public class InteraccionTexto : MonoBehaviour
{
    public GameObject texto; // Referencia al GameObject del texto que se mostrará al interactuar
    public string nameLevel; // Nombre del nivel al que se desea cargar

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        texto.SetActive(false); // Asegura que el texto esté oculto al inicio del juego
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el objeto que entra en el trigger tiene la etiqueta "Player"
        {
            texto.SetActive(true); // Muestra el texto al interactuar con el trigger

            if(Input.GetKeyDown(KeyCode.E)) // Verifica si se presiona la tecla "E" para cargar el nivel
            {
                SceneManager.LoadScene(nameLevel); // Carga el nivel especificado en nameLevel
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el objeto que sale del trigger tiene la etiqueta "Player"
        {
            texto.SetActive(false); // Oculta el texto al salir del trigger
        }
    }
}
