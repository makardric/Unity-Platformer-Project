using TMPro;
using UnityEngine;

public class PersonalBest : MonoBehaviour
{
    private bool timerStarted = false;
    private double timer = 0.0;
    [SerializeField] private TMP_Text timerText;

    void Start()
    {
        timerStarted = true;
    }

    private void FixedUpdate()
    {
        if (timerStarted)
        {
            timer += Time.fixedDeltaTime;
            timerText.text = "Time: " + ((float)timer).ToString("F2");
        }
    }

    public void StopTimer()
    {
        timerStarted = false;
    }
    public void AddPersonalBestTime()
    {
        float best = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        float currentRunTime = Mathf.Round((float)timer * 100f) / 100f;

        if (currentRunTime < best)
        {
            Debug.Log("Personal Best!: " + currentRunTime);
            PlayerPrefs.SetFloat("BestTime", currentRunTime);
            PlayerPrefs.Save();
        }
    }
}