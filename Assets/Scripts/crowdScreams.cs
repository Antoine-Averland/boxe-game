using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public AudioClip audioScream = null;
    private AudioSource crowdAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        crowdAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            crowdAudioSource.PlayOneShot(audioScream);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            crowdAudioSource.PlayOneShot(audioScream);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            crowdAudioSource.PlayOneShot(audioScream);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            crowdAudioSource.PlayOneShot(audioScream);
        }
    }
}
