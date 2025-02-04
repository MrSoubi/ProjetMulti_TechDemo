using UnityEngine;

public class CameraSizeAdjuster : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    [SerializeField] private Transform objectA;
    [SerializeField] private Transform objectB;
    [SerializeField] private float minSize = 5f;
    [SerializeField] private float maxSize = 20f;
    [SerializeField] private float padding = 1.5f;

    void Update()
    {
        if (targetCamera == null || objectA == null || objectB == null)
        {
            Debug.LogWarning("CameraSizeAdjuster: Assurez-vous d'assigner la caméra et les objets de référence.");
            return;
        }

        // Calcul de la distance entre les deux objets
        float distance = Vector2.Distance(objectA.position, objectB.position);

        // Ajustement de la taille de la caméra en fonction de la distance avec une marge de padding
        float newSize = Mathf.Clamp((distance * 0.5f) + padding, minSize, maxSize);
        targetCamera.orthographicSize = newSize;
    }
}
