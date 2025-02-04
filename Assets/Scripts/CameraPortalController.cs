using UnityEngine;

public class CameraPortalController : MonoBehaviour
{
    public Transform playerCamera;  // C�mara del jugador
    public Transform portal;        // Portal actual
    public Transform otherPortal;   // Portal de destino

    void LateUpdate()
    {
        // 1. Obtener la posici�n del jugador en coordenadas locales del otro portal
        Vector3 localPosition = otherPortal.InverseTransformPoint(playerCamera.position);
        
        // 2. Transformar la posici�n local a las coordenadas del portal actual
        transform.position = portal.TransformPoint(localPosition);

        // 3. Calcular la diferencia de rotaci�n entre los portales
        Quaternion portalRotationDifference = portal.rotation * Quaternion.Inverse(otherPortal.rotation);

        // 4. Aplicar la diferencia de rotaci�n a la c�mara del jugador
        transform.rotation = portalRotationDifference * playerCamera.rotation;
    }
}
