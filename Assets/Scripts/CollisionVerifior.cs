using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRPlayerCollisionDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Console.WriteLine("Collision d�tect�e avec : " + collision.gameObject.name);
    }

    private void OnCollisionStay(Collision collision)
    {
       Console.WriteLine("Toujours en collision avec : " + collision.gameObject.name);
    }

    private void OnCollisionExit(Collision collision)
    {
        Console.WriteLine("Fin de collision avec : " + collision.gameObject.name);
    }
}

