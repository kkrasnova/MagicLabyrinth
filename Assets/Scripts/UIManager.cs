using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI relicCounter;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI slowTimerText;
    public TextMeshProUGUI fakeRelicCounter;
    public AudioSource audioSource;
    public AudioClip trueRelicClip;
    public AudioClip fakeRelicClip;
    public AudioClip trapClip;
    public AudioClip winClip;

    void Awake()
    {
        Instance = this;
        resultText.text = "";
        resultText.fontSize = 80;
    }

    void Start()
    {
       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Тест: пробую проиграть TrapSound по клавише P");
            PlayTrapSound();
        }
    }

    public void UpdateRelicCounter(int collected, int total)
    {
        relicCounter.text = $"<color=#00FF00>Реліквії: {collected}/{total}</color>";
    }

    public void ShowResult(string message)
    {
        resultText.text = message;
        resultText.fontSize = 80;
        resultText.enabled = true;
    }

    public void ShowSlowTimer(int seconds)
    {
        slowTimerText.text = seconds.ToString();
        slowTimerText.fontSize = 150;
        slowTimerText.enabled = true;
    }

    public void HideSlowTimer()
    {
        slowTimerText.text = "";
        slowTimerText.enabled = false;
    }

    public void ShowTrapMessage(string message)
    {
        resultText.text = message;
        resultText.fontSize = 80;
        resultText.enabled = true;
    }

    public void HideTrapMessage()
    {
        resultText.text = "";
        resultText.enabled = false;
    }

    public void PlayTrueRelicSound()
    {
        if (audioSource != null && trueRelicClip != null)
            audioSource.PlayOneShot(trueRelicClip);
    }

    public void PlayFakeRelicSound()
    {
        if (audioSource != null && fakeRelicClip != null)
            audioSource.PlayOneShot(fakeRelicClip);
    }

    public void PlayTrapSound()
    {
        if (audioSource != null && trapClip != null)
            audioSource.PlayOneShot(trapClip);
    }

    public void PlayWinSound()
    {
        if (audioSource != null && winClip != null)
            audioSource.PlayOneShot(winClip);
    }

    public void UpdateFakeRelicCounter(int collected, int total)
    {
        fakeRelicCounter.text = $"<color=#FF0000>Фейкові: {collected}/{total}</color>";
    }
}
