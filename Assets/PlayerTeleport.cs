using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject currentTeleporter;
    public FadeController fadeController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentTeleporter != null)
            {
                StartCoroutine(TeleportPlayer());
            }
        }
    }

    private IEnumerator TeleportPlayer()
    {
        // Get the destination Transform from the Teleporter component
        Transform destination = currentTeleporter.GetComponent<Teleporter>().GetDestination();

        // Check if the destination is not null
        if (destination != null)
        {
            yield return StartCoroutine(fadeController.FadeOut());
            transform.position = destination.position;
            yield return StartCoroutine(fadeController.FadeIn());
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
    }
}
