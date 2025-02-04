using UnityEngine;
using System; // Necesario para usar eventos

public class ScoreManager : MonoBehaviour
{
    public static event Action<int> OnPuntuacionActualizada; // Evento que env�a el puntaje actual
    public static event Action OnSeisPuntos; // Evento cuando llega a 6 puntos

    private int puntuacion = 0; // Puntuaci�n del jugador

    public void AumentarPuntos(int puntos)
    {
        puntuacion += puntos;
        Debug.Log("Puntuaci�n: " + puntuacion);

        // Disparar el evento y enviar la puntuaci�n actual
        OnPuntuacionActualizada?.Invoke(puntuacion);

        // Si el jugador llega a 6 puntos, activamos otro evento
        if (puntuacion > 4)
        {
            OnSeisPuntos?.Invoke();
        }
    }
}
