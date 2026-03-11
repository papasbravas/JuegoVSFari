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
                if(Input.GetKeyDown(KeyCode.Alpha1))
                {
                    SiguientePaso();
                }
                break;
            case 4:
                // Invencibilidad
                if(Input.GetKeyDown(KeyCode.Alpha2))
                {
                    SiguientePaso();
                }
                break;
            case 5:
                // Defensa
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    SiguientePaso();
                }
                break;
            case 6:
                // Ralentizar jefes
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    SiguientePaso();
                }
                break;
            case 7:
                // Daño progresivo
                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    SiguientePaso();
                }
                break;
             case 8:
                // Fin del tutorial
                Fin();
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
                textoTuto.text = "Presiona el 1 para aturdir a los enemigos cercanos.";
                break;
            case 4:
                textoTuto.text = "Presiona el 2 para volverte invencible durante un tiempo.";
                break;
            case 5:
                textoTuto.text = "Presiona el 3 para activar tu defensa y reducir el daño recibido.";
                break;
            case 6:
                textoTuto.text = "Presiona el 4 para ralentizar a los jefes.";
                break;
            case 7:
                textoTuto.text = "Presiona el 5 para hacer daño progresivo.";
                break;
            case 8:
                textoTuto.text = "¡Eso es todo! ¡Buena suerte en tu aventura!";
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
