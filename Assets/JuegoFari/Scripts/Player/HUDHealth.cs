using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDHealth : MonoBehaviour
{
    public static HUDHealth Instance;
    public Image barra;   //barra de vida

    //[SerializeField] private TextMeshProUGUI healthText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateHealthBar(float current, float max)
    {
        //healthText.text = current + " / " + max;
        barra.fillAmount = current / max; // Actualiza la barra de salud en función de la salud actual y máxima
    }
}
