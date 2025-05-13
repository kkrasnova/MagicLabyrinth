using UnityEngine;

public class FakeRelic : Interactable
{
    public override void Interact()
    {
        Debug.Log("Fake Relic picked up!");
        var player = GameObject.FindWithTag("Player");
        var startObj = GameObject.FindWithTag("Start");
        if (player != null && startObj != null)
        {
            var controller = player.GetComponent<CharacterController>();
            var pc = player.GetComponent<PlayerController>();
            Vector3 cellCenter = new Vector3(1 * 3, 1.0f, 1 * 3); 

            if (controller != null)
            {
                controller.enabled = false;
                player.transform.position = cellCenter;
                controller.enabled = true;
            }
            else
            {
                player.transform.position = cellCenter;
            }
            if (pc != null)
            {
                pc.ResetVerticalVelocity();
            }
        }
        UIManager.Instance.PlayFakeRelicSound();
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CollectFakeRelic();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Interact();
        }
    }
}
