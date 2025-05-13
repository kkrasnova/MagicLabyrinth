using UnityEngine;

public class TrueRelic : Interactable
{
    public override void Interact()
    {
        UIManager.Instance.PlayTrueRelicSound();
        Debug.Log("True Relic picked up: " + gameObject.name);
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CollectRelic();
        }
        else
        {
            Debug.LogWarning("GameManager.Instance is null!");
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Interact();
        }
    }
}
