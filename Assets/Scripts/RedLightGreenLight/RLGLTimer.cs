using UnityEngine;
using TMPro;

public class RLGLTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float time = 0f;
    public bool countDown = false;
    public bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        time += (countDown ? -1 : 1) * Time.deltaTime;

        if (countDown && time < 0)
        {
            time = 0;
            isRunning = false;
        }

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}

