using UnityEngine;
using System.Collections;

public class AlignXROriginToRingFloor : MonoBehaviour
{
    public Transform xrOrigin;
    public Transform cameraTransform;
    public Transform ringFloor; // Objet de référence dans le ring

    void Start()
    {
        StartCoroutine(AlignOrigin());
    }

    IEnumerator AlignOrigin()
    {
        // Attendre que le casque ait reçu sa position réelle (position.y > 0.1)
        while (cameraTransform.position.y < 0.1f)
        {
            yield return null;
        }

        // Calculer la différence entre la position actuelle de la tête et le ring_floor
        Vector3 offset = ringFloor.position - cameraTransform.position;

        // Déplacer le XR Origin pour que la tête arrive sur le ring_floor
        xrOrigin.position += new Vector3(offset.x, offset.y, offset.z);
    }
}
