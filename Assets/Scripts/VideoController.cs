using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoControl : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject[] objectsToDisable; // Objets à désactiver pendant la vidéo
    public AudioSource gameAudio; // L'audio principal du jeu
    public GameObject videoScreen; // le Quad ou Plane devant l'utilisateur
    // Script présentation des boxeurs
    //public IntroductionController introductionController;

    void Start()
    {
        // Désactiver les éléments du jeu au début
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }

        if (gameAudio != null)
        {
            gameAudio.Pause(); // Arrête la musique de fond
        }
        if (videoScreen != null)
        {
            videoScreen.SetActive(true);
        }
        // Attacher l'événement pour détecter la fin de la vidéo
        videoPlayer.loopPointReached += EndReached;
        videoPlayer.Play();
    }

    void EndReached(VideoPlayer vp)
    {
        // Réactiver les éléments du jeu à la fin de la vidéo
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(true);
        }

        if (gameAudio != null)
        {
            gameAudio.Play(); // Redémarre la musique de fond
        }
        
        //if (introductionController != null)
        //{
        //    introductionController.StartIntroduction();
        //}
        if (videoScreen != null)
        {
            videoScreen.SetActive(false);
        }

        // Désactiver la vidéo ou son GameObject
        gameObject.SetActive(false);
    }
}
