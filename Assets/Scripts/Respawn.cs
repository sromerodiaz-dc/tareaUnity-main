using UnityEngine;

public class TransportPlayer : MonoBehaviour
{
    public Vector3 targetPosition = new Vector3(0, 0, 0); // Coordenadas a las que se transportará el jugador.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el GameObject del jugador tiene la etiqueta "Player".
        {
            Debug.Log("Respawn");
            other.transform.position = targetPosition; // Transporta al jugador.
            other.GetComponent<Rigidbody>().velocity = Vector3.zero; // Reinicia la velocidad para evitar deslizamientos.
        }
    }
}
