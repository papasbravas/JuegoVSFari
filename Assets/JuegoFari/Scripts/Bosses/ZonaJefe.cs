using Unity.VisualScripting;
using UnityEngine;

public class ZonaJefe : MonoBehaviour
{
    public GameObject muros;    // Referencia a los muros que se activarán al entrar en la zona del jefe
    public GameObject jefe;     // Referencia al jefe que se activará al entrar en la zona del jefe
    private bool jefeActivado = false; // Variable para asegurarse de que el jefe solo se active una vez
    public GameObject BarraVidaJefe;

    //private void Start()
    //{
    //    jefe.SetActive(false); // Asegura que el jefe esté desactivado al inicio
    //}
    private void OnTriggerEnter(Collider other)
    {
        muros.SetActive(true); // Activa los muros para bloquear la salida
        if (!jefeActivado)
        {
            BarraVidaJefe.SetActive(true); // Activa la barra de vida del jefe 
            jefe.SetActive(true); // Activa el jefe para que aparezca en la escena
            //Instantiate(Jefe, new Vector3(posJefe.position.x, posJefe.position.y, posJefe.position.z), Quaternion.identity); // Instancia el jefe en la posición de la zona del jefe
            jefeActivado = true; // Marca que el jefe ha sido activado
        }
            
    }
}