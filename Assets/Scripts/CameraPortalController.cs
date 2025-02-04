using UnityEngine;

public class CameraPortalController : MonoBehaviour
{
    public Transform playerCamera;  // Cámara del jugador
    public Transform portal;        // Portal actual
    public Transform otherPortal;   // Portal de destino

    void LateUpdate()
    {
        // 1. Obtener la posición del jugador en coordenadas locales del otro portal
        Vector3 localPosition = otherPortal.InverseTransformPoint(playerCamera.position);
        
        // 2. Transformar la posición local a las coordenadas del portal actual
        transform.position = portal.TransformPoint(localPosition);

        // 3. Calcular la diferencia de rotación entre los portales
        Quaternion portalRotationDifference = portal.rotation * Quaternion.Inverse(otherPortal.rotation);

        // 4. Aplicar la diferencia de rotación a la cámara del jugador
        transform.rotation = portalRotationDifference * playerCamera.rotation;
    }
}
