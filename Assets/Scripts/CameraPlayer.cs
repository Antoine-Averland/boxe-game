using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 decalage;

    // Start is called before the first frame update
    void Start()
    {
        decalage = new Vector3 (-4, 4, 0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + decalage;
    }
}
