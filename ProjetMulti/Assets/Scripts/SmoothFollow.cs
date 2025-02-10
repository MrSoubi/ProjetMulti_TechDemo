using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // La cible � suivre

    [Header("Position Damping")]
    public float positionDamping = 5.0f; // Lissage de la position

    [Header("Rotation Damping")]
    public bool followRotation = false; // Activer/d�sactiver le suivi de la rotation
    public float rotationDamping = 5.0f; // Lissage de la rotation

    private Vector3 velocity = Vector3.zero; // Vitesse utilis�e par SmoothDamp

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("SmoothFollow : Aucun target assign� !");
            return;
        }

        // Lissage de la position
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, 1 / positionDamping);

        // Lissage de la rotation (optionnel)
        if (followRotation)
        {
            Quaternion targetRotation = target.rotation;
            transform.rotation = targetRotation;
        }
    }
}
