using UnityEngine;

public class MusicZoneByName : MonoBehaviour
{
    [Header("Nombre de zona")]
    [SerializeField] private string zoneNameToTrigger = "ZonaJefe";

    [Header("Qué música pone al entrar")]
    [SerializeField] private bool bossMusicOnEnter = true;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[ZONE ENTER] Zona:{gameObject.name} | Other:{other.name} | Tag:{other.tag}");

        if (!other.CompareTag("Player")) return;
        if (MusicaManager.Instance == null) { Debug.LogError("[ZONE] MusicaManager.Instance es NULL"); return; }

        Debug.Log("[ZONE] Player detectado. Intentando cambiar a música de jefe...");
        MusicaManager.Instance.CambiarAMusicaJefe();
        Debug.Log("CAMBIA");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (MusicaManager.Instance == null) return;

        if (gameObject.name == zoneNameToTrigger)
        {
            // Al salir, vuelve a ambiental
            MusicaManager.Instance.VolverAMusicaAmbiental();
        }
    }
}