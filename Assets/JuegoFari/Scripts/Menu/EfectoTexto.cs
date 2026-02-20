using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EfectoTexto : MonoBehaviour
{
    public TextMeshProUGUI textUI; // Referencia al componente TextMeshProUGUI para mostrar el texto en pantalla
    public GameObject nextButton; // Referencia al botón "Siguiente" para avanzar a la siguiente página de texto
    public float typingSpeed = 0.03f; // Velocidad de tipeo del texto (en segundos por carácter)
    public GameObject introCanvas; // Referencia al canvas que contiene el texto de introducción para ocultarlo después de mostrar todas las páginas de texto

    [TextArea(3, 10)] // Permite editar el array de páginas de texto en el Inspector de Unity con un área de texto más grande
    public string[] pages; // Array de páginas de texto que se mostrarán en secuencia

    private int currentPage = 0; // Índice de la página actual que se está mostrando
    private bool isTyping = false; // Bandera para indicar si el texto se está tipeando actualmente

    void Start()
    {
        nextButton.SetActive(false); // Oculta el botón "Siguiente" al inicio para evitar que el jugador avance antes de que el texto se haya mostrado completamente
        StartCoroutine(TypePage()); // Inicia la corrutina para mostrar la primera página de texto con el efecto de tipeo al comenzar el juego
    }

    IEnumerator TypePage()
    {
        isTyping = true; // Establece la bandera de tipeo a true para indicar que el texto se está mostrando actualmente
        textUI.text = ""; // Limpia el texto en pantalla antes de mostrar la nueva página

        foreach (char c in pages[currentPage]) // Recorre cada carácter de la página actual de texto y lo muestra uno por uno con un retraso entre cada carácter para crear el efecto de tipeo
        {
            textUI.text += c; // Agrega el carácter actual al texto en pantalla
            yield return new WaitForSeconds(typingSpeed); // Espera un breve período de tiempo antes de mostrar el siguiente carácter para crear el efecto de tipeo
        }

        isTyping = false; // Establece la bandera de tipeo a false para indicar que el texto se ha mostrado completamente y el jugador puede avanzar a la siguiente página
        nextButton.SetActive(true); 
    }

    // Método que se llama cuando el jugador hace clic en el botón "Siguiente" para avanzar a la siguiente página de texto o cargar la escena del juego si se han mostrado todas las páginas
    public void NextPage()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            textUI.text = pages[currentPage];
            isTyping = false;
            nextButton.SetActive(true);
            return;
        }

        currentPage++;

        if (currentPage < pages.Length)
        {
            nextButton.SetActive(false);
            StartCoroutine(TypePage());
        }
        else
        {
            // Ocultar el canvas para empezar el tutorial
            introCanvas.SetActive(false);
            GameState.inputEnabled = true;
            //SceneManager.LoadScene("Nivel1");
        }
    }
}