using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused = false;
    //[SerializeField] private Animator pauseAnimator;

    public static bool InputsBlocked = false;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    void PauseGame()
    {
        pauseMenu.SetActive(true);

        // Reproduce la animación de entrada
        //pauseAnimator.Play("PauseFadeIn", 0, 0f);

        Time.timeScale = 0f;
        AudioListener.pause = true;
        isPaused = true;
        InputsBlocked = true;
    }


    void ResumeGame()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        pauseMenu.SetActive(false);
        isPaused = false;
        InputsBlocked = false;
    }
}
