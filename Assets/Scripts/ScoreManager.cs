using UnityEngine;
using System; // Necesario para usar eventos

public class ScoreManager : MonoBehaviour
{
    public static event Action<int> OnPuntuacionActualizada; // Evento que envía el puntaje actual
    public static event Action OnSeisPuntos; // Evento cuando llega a 6 puntos

    private int puntuacion = 0; // Puntuación del jugador

    public void AumentarPuntos(int puntos)
    {
        puntuacion += puntos;
        Debug.Log("Puntuación: " + puntuacion);

        // Disparar el evento y enviar la puntuación actual
        OnPuntuacionActualizada?.Invoke(puntuacion);

        // Si el jugador llega a 6 puntos, activamos otro evento
        if (puntuacion > 4)
        {
            OnSeisPuntos?.Invoke();
        }
    }
}
