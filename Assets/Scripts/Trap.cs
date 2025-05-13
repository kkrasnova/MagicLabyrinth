using UnityEngine;
using System.Collections;

public class Trap : Interactable
{
    public enum TrapType { Slow, Teleport, Blind, Confuse, ReverseTeleport }
    public TrapType trapType;
    public bool isPermanent = true;

    public override void Interact()
    {
        Debug.Log("Trap triggered: " + trapType);
        if (trapType == TrapType.Slow)
        {
            var player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                var controller = player.GetComponent<PlayerController>();
                if (controller != null)
                {
                    controller.ApplySlow(5f);
                }
            }
        }
        else
        {
            GameManager.Instance.TriggerTrap();
        }

        if (!isPermanent)
            StartCoroutine(DestroyWithDelay());

        Debug.Log("PlayTrapSound called!");
        Debug.Log("UIManager.Instance: " + (UIManager.Instance != null));
        UIManager.Instance.PlayTrapSound();
    }

    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(0.2f);
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
