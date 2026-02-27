using System.Collections;
using UnityEngine;

public class MusicaManager : MonoBehaviour
{
    public static MusicaManager Instance; // Instancia estática para el singleton

    [Header("Fuentes de audio")]
    [SerializeField] private AudioSource musicaAmbiental; // Fuente de audio para la música ambiental
    [SerializeField] private AudioSource musicaJefe; // Fuente de audio para la música de combate
    [SerializeField] private AudioSource musicaMenu; // Fuente de audio para la música del menú

    [Header("Fades")]
    [SerializeField] private float fadeDuration = 2f; // Duración del fade in/out
    private Coroutine fadeActual; // Referencia al fade actual para evitar solapamientos

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        musicaAmbiental.Play(); // Iniciar la musica de ambiente al inicio
        musicaJefe.Stop(); // Asegurarse de que la musica de jefe esté detenida al inicio
    }

    public void CambiarAMusicaJefe()
    {
        // Al entrar en la zona con el tag designado, cambiar a la música de cada jefe correspondiente con un fade suave
    }

    public void OnCanvasGroupChanged()
    {
        // Al salir de la zona del tag designado, volver a la música ambiental con un fade suave
    }

    public void IniciarFace()
    {
        // Método para iniciar el fade in de la música ambiental al inicio del juego o al volver al menú
    }

    private IEnumerator FadeCoroutine(AudioSource from, AudioSource to)
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            from.volume = Mathf.Lerp(1f, 0f, time / fadeDuration); // Fade out de la música actual
            to.volume = Mathf.Lerp(0f, 1f, time / fadeDuration); // Fade in de la nueva música
            time += Time.deltaTime;
            yield return null;
        }
        from.Stop(); // Detener la música anterior al finalizar el fade out
        to.volume = 1f; // Asegurar que la nueva música esté al volumen máximo al finalizar el fade in
    }

}
