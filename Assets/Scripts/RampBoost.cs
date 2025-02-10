using UnityEngine;
using System.Collections;

public class RampBoost : MonoBehaviour
{
    public float boostForce = 40f; // Fuerza del impulso
    public float boostDuration = 1f; // Duración del impulso
    public float minSpeed = 5f; // Velocidad mínima después del impulso

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
        Vector3 movementDirection = rb.velocity.normalized; // Dirección actual del jugador

        // Solo aplicar el boost si el jugador se está moviendo
        if (rb.velocity.magnitude > 0.1f)
        {
            Vector3 boost = movementDirection * boostForce;
            rb.AddForce(boost, ForceMode.Impulse); // Aplica el impulso en la dirección actual
        }

        yield return new WaitForSeconds(boostDuration);

        // Asegurar que el jugador mantenga una velocidad mínima
        if (rb.velocity.magnitude < minSpeed)
        {
            rb.velocity = rb.velocity.normalized * minSpeed;
        }
    }
}

