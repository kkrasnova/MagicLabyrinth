using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.AllRelicsCollected())
            {
                GameManager.Instance.Win();
            }
            else
            {
                UIManager.Instance.ShowResult("Соберите все реликвии!");
            }
        }
    }
}
