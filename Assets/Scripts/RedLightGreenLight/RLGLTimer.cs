using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RLGLTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float time = 150f;
    public bool countDown = true;
    public bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        time -= (countDown ? -1 : 1) * Time.deltaTime;

        if (countDown && time <= 0f)
        {
            time = 0f;
            isRunning = false;
            OnTimerEnd();
        }

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = $"{minutes:0}:{seconds:00}";
    }

    void OnTimerEnd()
    {
        SceneManager.LoadScene("YouDiedScreen");
    }
}



