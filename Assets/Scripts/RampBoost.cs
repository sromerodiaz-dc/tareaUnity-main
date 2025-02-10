using UnityEngine;
using System.Collections;

public class RampBoost : MonoBehaviour
{
    public float boostForce = 40f; // Fuerza del impulso
    public float boostDuration = 1f; // Duraci�n del impulso
    public float minSpeed = 5f; // Velocidad m�nima despu�s del impulso

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica que el jugador tenga la etiqueta "Player"
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                StartCoroutine(ApplyTemporaryBoost(rb));
            }
        }
    }

    private IEnumerator ApplyTemporaryBoost(Rigidbody rb)
    {
        Vector3 movementDirection = rb.velocity.normalized; // Direcci�n actual del jugador

        // Solo aplicar el boost si el jugador se est� moviendo
        if (rb.velocity.magnitude > 0.1f)
        {
            Vector3 boost = movementDirection * boostForce;
            rb.AddForce(boost, ForceMode.Impulse); // Aplica el impulso en la direcci�n actual
        }

        yield return new WaitForSeconds(boostDuration);

        // Asegurar que el jugador mantenga una velocidad m�nima
        if (rb.velocity.magnitude < minSpeed)
        {
            rb.velocity = rb.velocity.normalized * minSpeed;
        }
    }
}

