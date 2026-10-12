using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    private bool inGame = false;
    [SerializeField] private GameObject canvas;
    [SerializeField] private RectTransform content;
    [SerializeField] private TMP_Text bestTimeText;
    [SerializeField] private RectTransform menuButton;
    [SerializeField] private RectTransform titleImage;
    [SerializeField] private Sprite paused_image;

    private Image titleImageComponent;
    private Sprite titleSprite;


    private void Start()
    {
        titleImageComponent = titleImage.GetComponent<Image>();
        titleSprite = titleImageComponent.sprite;
        float best = PlayerPrefs.GetFloat("BestTime", 0f);
        bestTimeText.text = "PERSONAL BEST: " + PlayerPrefs.GetFloat("BestTime", 0f).ToString("F2");
        menuButton.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && inGame)
        {
            content.anchoredPosition = Vector2.zero;
            bool isCurrentlyPaused = Time.timeScale == 0f;

            if (isCurrentlyPaused)
            {
                Time.timeScale = 1f;
                canvas.SetActive(false);
                titleImageComponent.sprite = paused_image;
            }
            else
            {
                Time.timeScale = 0f;
                canvas.SetActive(true);
                titleImageComponent.sprite = paused_image;
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
    public void ResetPB()
    {
        PlayerPrefs.DeleteKey("BestTime");
        PlayerPrefs.Save();
        bestTimeText.text = "PERSONAL BEST: " + PlayerPrefs.GetFloat("BestTime", 0f).ToString("F2");
    }
    public void QuitToMenu()
        {
            Time.timeScale = 1f;
            SceneManager.UnloadSceneAsync("Main Level");
            inGame = false;
            menuButton.gameObject.SetActive(false);
            titleImageComponent.sprite = titleSprite;
            content.anchoredPosition = Vector2.zero;
        }
    public void Quit()
    {
        Debug.Log("Application quit :(");
        Application.Quit();
    }
}
