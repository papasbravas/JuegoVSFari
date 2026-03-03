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
    private AudioSource fuenteActual; // Referencia a la fuente de audio actualmente en reproducción

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Estado inicial
        musicaAmbiental.volume = 1f;
        musicaJefe.volume = 0f;
        musicaMenu.volume = 0f;

        if (!musicaAmbiental.isPlaying) musicaAmbiental.Play();
        musicaJefe.Stop();
        musicaMenu.Stop();

        fuenteActual = musicaAmbiental;
    }

    // Métodos públicos para cambiar la música según el estado del juego
    public void CambiarAMusicaJefe() => CambiarMusica(musicaJefe);
    public void VolverAMusicaAmbiental() => CambiarMusica(musicaAmbiental);
    public void CambiarAMusicaMenu() => CambiarMusica(musicaMenu);

    private void CambiarMusica(AudioSource nueva)
    {
        if (nueva == null) return;

        // Si ya está sonando esa, no hagas nada
        if (fuenteActual == nueva) return;

        // Si no había actual, simplemente arranca la nueva
        if (fuenteActual == null)
        {
            nueva.volume = 1f;
            if (!nueva.isPlaying) nueva.Play();
            fuenteActual = nueva;
            return;
        }

        // Evitar solapamientos de fades
        if (fadeActual != null) StopCoroutine(fadeActual);

        // Preparar la nueva
        nueva.volume = 0f;
        if (!nueva.isPlaying) nueva.Play();

        fadeActual = StartCoroutine(FadeCoroutine(fuenteActual, nueva));
        fuenteActual = nueva;
    }

    private IEnumerator FadeCoroutine(AudioSource from, AudioSource to)
    {
        float time = 0f;

        float fromStart = from != null ? from.volume : 0f; // Guardar el volumen inicial de la fuente que se va a apagar
        float toStart = to != null ? to.volume : 0f; // Guardar el volumen inicial de la fuente que se va a encender

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / fadeDuration); // Normalizar el tiempo para que vaya de 0 a 1

            if (from != null) from.volume = Mathf.Lerp(fromStart, 0f, t); // Interpolar el volumen de la fuente que se va a apagar
            if (to != null) to.volume = Mathf.Lerp(toStart, 1f, t); // Interpolar el volumen de la fuente que se va a encender

            yield return null;
        }

        if (from != null)
        {
            from.volume = 0f;
            from.Stop();
        }

        if (to != null)
            to.volume = 1f;

        fadeActual = null;
    }

}
