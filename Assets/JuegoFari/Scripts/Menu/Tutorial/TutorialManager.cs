using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI textoTuto; // Referencia al componente TextMeshProUGUI para mostrar el texto del tutorial
    public GameObject canvasTuto; // Referencia al GameObject del canvas del tutorial

    private int pasos = 0; // Variable para controlar los pasos del tutorial

    void Start()
    {
        
        MostrarPasos(); // Llama a la función para mostrar los pasos del tutorial al inicio del juego
    }

    private void Update()
    {
        switch (pasos)
        {
            case 0:
                // Movimiento
                if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || 
                    Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                {
                    SiguientePaso();
                }
                break;
            case 1:
                // Ataque normal
                if(Input.GetMouseButtonDown(0))
                {
                    SiguientePaso();
                }
                break;
            case 2:
                // Ataque múltiple
                if(Input.GetMouseButtonDown(1))
                {
                    SiguientePaso();
                }
                break;
            case 3:
                // Aturdimiento
                if(Input.GetKeyDown(KeyCode.T))
                {
                    SiguientePaso();
                }
                break;
            case 4:
                // Invencibilidad
                if(Input.GetKeyDown(KeyCode.I))
                {
                    Fin();
                }
                break;
        }
    }

    void MostrarPasos()
    {
        switch (pasos)
        {
            case 0:
                textoTuto.text = "¡Bienvenido! Usa WASD para moverte.";
                break;
            case 1:
                textoTuto.text = "Presiona el botón izquierdo del ratón para atacar.";
                break;
            case 2:
                textoTuto.text = "Presiona el botón derecho del ratón para un ataque en área.";
                break;
            case 3:
                textoTuto.text = "Presiona T para aturdir a los enemigos cercanos.";
                break;
            case 4:
                textoTuto.text ="Presiona I para volverte invencible durante un tiempo.";
                break;
        }
    }

    void SiguientePaso()
    {
        pasos++; // Incrementa el contador de pasos para avanzar al siguiente paso del tutorial
        MostrarPasos(); // Llama a la función para mostrar el nuevo paso del tutorial
    }

    void Fin()
    {
        canvasTuto.SetActive(false); // Desactiva el canvas del tutorial para ocultarlo al finalizar
        Debug.Log("Tutorial terminado"); // Imprime un mensaje en la consola indicando que el tutorial ha terminado 
    }

}
