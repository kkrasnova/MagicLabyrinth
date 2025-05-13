using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int relicsToCollect = 3;
    private int collectedRelics = 0;
    private int trapsTriggered = 0;
    public int maxTraps = 3;
    private int fakeRelicsCollected = 0;
    public int maxFakeRelics = 3;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UIManager.Instance.UpdateRelicCounter(collectedRelics, relicsToCollect);
        UIManager.Instance.UpdateFakeRelicCounter(fakeRelicsCollected, maxFakeRelics);
    }

    public void CollectRelic()
    {
        collectedRelics++;
        UIManager.Instance.UpdateRelicCounter(collectedRelics, relicsToCollect);
        if (collectedRelics >= relicsToCollect)
            Win();
    }

    public void TriggerTrap()
    {
        trapsTriggered++;
        if (trapsTriggered >= maxTraps)
            Lose();
    }

    public void Win()
    {
        Debug.Log($"ПЕРЕМОГА! Зібрано {collectedRelics} з {relicsToCollect} реліквій!");
        string message = $"<color=#00FF00>ПЕРЕМОГА!</color> Зібрано <color=#FFD700>{collectedRelics}</color> з <color=#FFD700>{relicsToCollect}</color> реліквій!";
        UIManager.Instance.ShowResult(message);
        UIManager.Instance.PlayWinSound();
        StartCoroutine(QuitAfterDelay(5f));
    }

    private IEnumerator QuitAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void Lose()
    {
        Debug.Log("You lose!");
        UIManager.Instance.ShowResult("You Lose!");
        StartCoroutine(ResetGameAfterDelay(3f));
    }

    public void TeleportPlayerRandomly()
    {
       
    }

    public bool AllRelicsCollected()
    {
        return collectedRelics >= relicsToCollect;
    }

    public void CollectFakeRelic()
    {
        fakeRelicsCollected++;
        UIManager.Instance.UpdateFakeRelicCounter(fakeRelicsCollected, maxFakeRelics);
        if (fakeRelicsCollected >= maxFakeRelics)
            LoseFakeRelics();
    }

    public void LoseFakeRelics()
    {
        Debug.Log($"ПОРАЗКА! Зібрано {fakeRelicsCollected} з {maxFakeRelics} фальшивих реліквій!");
        string message = $"<color=#FF0000>ПОРАЗКА!</color> Зібрано <color=#FFD700>{fakeRelicsCollected}</color> з <color=#FFD700>{maxFakeRelics}</color> фальшивих реліквій!";
        UIManager.Instance.ShowResult(message);
        StartCoroutine(ResetGameAfterDelay(3f));
    }

    private IEnumerator ResetGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetGame();
    }

    private void ResetGame()
    {
        collectedRelics = 0;
        trapsTriggered = 0;
        fakeRelicsCollected = 0;
        UIManager.Instance.UpdateRelicCounter(collectedRelics, relicsToCollect);
        UIManager.Instance.UpdateFakeRelicCounter(fakeRelicsCollected, maxFakeRelics);
        UIManager.Instance.HideTrapMessage();
        
        var trueRelics = FindObjectsOfType<TrueRelic>();
        foreach (var relic in trueRelics)
        {
            Destroy(relic.gameObject);
        }

        var fakeRelics = FindObjectsOfType<FakeRelic>();
        foreach (var relic in fakeRelics)
        {
            Destroy(relic.gameObject);
        }

        var traps = FindObjectsOfType<Trap>();
        foreach (var trap in traps)
        {
            Destroy(trap.gameObject);
        }

        var maze = FindObjectOfType<SimpleMaze>();
        if (maze != null)
        {
            maze.CreateMaze();
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
