using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoControl : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject[] objectsToDisable; // Objets � d�sactiver pendant la vid�o
    public AudioSource gameAudio; // L'audio principal du jeu
    // Script pr�sentation des boxeurs
    public IntroductionController introductionController;

    void Start()
    {
        // D�sactiver les �l�ments du jeu au d�but
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }

        if (gameAudio != null)
        {
            gameAudio.Pause(); // Arr�te la musique de fond
        }

        // Attacher l'�v�nement pour d�tecter la fin de la vid�o
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        // R�activer les �l�ments du jeu � la fin de la vid�o
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(true);
        }

        if (gameAudio != null)
        {
            gameAudio.Play(); // Red�marre la musique de fond
        }
        
        if (introductionController != null)
        {
            introductionController.StartIntroduction();
        }
        
        // D�sactiver la vid�o ou son GameObject
        gameObject.SetActive(false);
    }
}