using UnityEngine;
using System.Collections;

public class AlignXROriginToRingFloor : MonoBehaviour
{
    public Transform xrOrigin;
    public Transform cameraTransform;
    public Transform ringFloor; // Objet de r�f�rence dans le ring

    void Start()
    {
        StartCoroutine(AlignOrigin());
    }

    IEnumerator AlignOrigin()
    {
        // Attendre que le casque ait re�u sa position r�elle (position.y > 0.1)
        while (cameraTransform.position.y < 0.1f)
        {
            yield return null;
        }

        // Calculer la diff�rence entre la position actuelle de la t�te et le ring_floor
        Vector3 offset = ringFloor.position - cameraTransform.position;

        // D�placer le XR Origin pour que la t�te arrive sur le ring_floor
        xrOrigin.position += new Vector3(offset.x, offset.y, offset.z);
    }
}
