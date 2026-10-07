using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    private bool inGame = false;
    [SerializeField] GameObject canvas;

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && inGame)
        {
            bool isCurrentlyPaused = Time.timeScale == 0f;

            if (isCurrentlyPaused)
            {
                Time.timeScale = 1f;
                canvas.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f;
                canvas.SetActive(true);
            }
        }
    }

    public void Play()
    {
        if (inGame == false)
        {
            SceneManager.LoadSceneAsync("Main Level", LoadSceneMode.Additive);
            inGame = true;
            canvas.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            canvas.SetActive(false);
        }
    }

    public void Quit()
    {
        Debug.Log("Application quit :(");
        Application.Quit();
    }
}
