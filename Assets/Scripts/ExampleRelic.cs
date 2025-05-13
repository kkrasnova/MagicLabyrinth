using UnityEngine;

public class ExampleRelic : Interactable
{
    public override void Interact()
    {
        Debug.Log("Relic picked up: " + gameObject.name);
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
