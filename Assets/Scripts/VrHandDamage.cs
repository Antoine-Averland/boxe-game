using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRHandDamage : MonoBehaviour
{
    public DamageController damageController;
    public DamageController.AttackType attackType = DamageController.AttackType.Punch;
    public DamageController.AttackSide attackSide = DamageController.AttackSide.Right;
    public XRNode handNode = XRNode.RightHand;

    public AudioClip punchSound;
    public AudioClip firstPunchSound;
    public AudioClip thirdPunchSound;

    private AudioSource audioSource;
    private int punchCount = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoxerBlueSide"))
        {
            punchCount++;

            // Définir les infos d’attaque
            damageController.SetLastAttack(attackType);
            damageController.SetLastAttackSide(attackSide);
            damageController.SetCanDamage(true);

            // Déclencher vibration
            TriggerHapticFeedback(0.8f, 0.5f); // amplitude (0-1), durée en secondes

            PlayImpactSound();

            if (punchCount == 1 && firstPunchSound != null)
                audioSource.PlayOneShot(firstPunchSound);

            if (punchCount == 3 && thirdPunchSound != null)
                audioSource.PlayOneShot(thirdPunchSound);
        }
    }

    private void TriggerHapticFeedback(float amplitude, float duration)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(handNode);

        if (device.TryGetHapticCapabilities(out HapticCapabilities capabilities) && capabilities.supportsImpulse)
        {
            device.SendHapticImpulse(0, amplitude, duration);
        }
    }

    private void PlayImpactSound()
    {
        if (attackType == DamageController.AttackType.Punch && punchSound != null)
        {
            audioSource.PlayOneShot(punchSound);
        }
    }
}

