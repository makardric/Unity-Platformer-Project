using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    private bool inGame = false;
    [SerializeField] private GameObject canvas;
    [SerializeField] private RectTransform content;
    [SerializeField] private TMP_Text bestTimeText;
    [SerializeField] private RectTransform menuButton;
    


    private void Start()
    {
        float best = PlayerPrefs.GetFloat("BestTime", 0f);
        bestTimeText.text = "PERSONAL BEST: " + PlayerPrefs.GetFloat("BestTime", 0f).ToString("F2");
        menuButton.gameObject.SetActive(false);
    }
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
                bestTimeText.text = "PERSONAL BEST: " + PlayerPrefs.GetFloat("BestTime", 0f).ToString("F2");
            }
        }
    }

    
    public void Play()
    {
        if (inGame == false)
        {
            SceneManager.LoadSceneAsync("Main Level", LoadSceneMode.Additive);
            inGame = true;
            menuButton.gameObject.SetActive(true);
            canvas.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            canvas.SetActive(false);
        }
    }
    public void QuitToMenu()
        {
            Time.timeScale = 1f;
            SceneManager.UnloadSceneAsync("Main Level");
            inGame = false;
            menuButton.gameObject.SetActive(false);
            content.anchoredPosition = Vector2.zero;
        }
    public void Quit()
    {
        Debug.Log("Application quit :(");
        Application.Quit();
    }
}
