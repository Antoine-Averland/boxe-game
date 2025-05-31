using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRHandDamage : MonoBehaviour
{
    public DamageController damageController;
    public DamageController.AttackType attackType = DamageController.AttackType.Punch;
    public DamageController.AttackSide attackSide = DamageController.AttackSide.Right; // à définir par main
    public XRNode handNode = XRNode.RightHand; // Choisir LeftHand ou RightHand dans l’inspecteur

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoxerBlueSide"))
        {
            // Définir les infos d’attaque
            damageController.SetLastAttack(attackType);
            damageController.SetLastAttackSide(attackSide);
            damageController.SetCanDamage(true);

            // Déclencher vibration
            TriggerHapticFeedback(0.8f, 0.5f); // amplitude (0-1), durée en secondes
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
}

