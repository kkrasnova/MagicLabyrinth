using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menuPanel;        
    private PlayerController playerController; 
    
    void Start()
    {
        menuPanel.SetActive(true);
        Invoke(nameof(FindAndBlockPlayer), 0.1f); 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void FindAndBlockPlayer()
    {
        playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
            playerController.canMove = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Magic Labyrinth"); 
    }

    public void ExitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
