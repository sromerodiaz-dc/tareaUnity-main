using UnityEngine;

public class TransportPlayerToSecondLevel : MonoBehaviour
{
    public GameStateManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Si el objeto tiene la etiqueta "PickUp"
        {
            other.gameObject.SetActive(false);
            manager.WinGame();
        }
    }
}
