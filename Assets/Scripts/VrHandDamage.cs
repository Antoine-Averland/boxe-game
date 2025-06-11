using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRHandDamage : MonoBehaviour
{
    public int kickPower = 20;
    public int punchPower = 10;
    public DamageController.AttackType attackType = DamageController.AttackType.Punch;
    public DamageController.AttackSide attackSide = DamageController.AttackSide.Right;
    public XRNode handNode = XRNode.RightHand;

    public AudioClip punchSound;
    public AudioClip firstPunchSound;
    public AudioClip thirdPunchSound;

    private AudioSource audioSource;
    private int punchCount = 0;
    private bool canDamage = true;
    private float damageCooldown = 1.5f;

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
        if (other.CompareTag("BoxerBlueSide") && canDamage)
        {
            punchCount++;

            PlayerControllerV2 player = other.GetComponent<PlayerControllerV2>();
            if (player == null) return;

            int damage = attackType == DamageController.AttackType.Kick ? kickPower : punchPower;
            player.healthPower -= damage;

            player.IsHit(attackType, attackSide);
            player.SetHealthPowerText();
            player.StartCoroutine(player.BoostSpotlightIntensity(attackType == DamageController.AttackType.Kick));

            TriggerHapticFeedback(0.8f, 0.5f); // amplitude, durée

            PlayImpactSound();

            if (punchCount == 1 && firstPunchSound != null)
                audioSource.PlayOneShot(firstPunchSound);

            if (punchCount == 3 && thirdPunchSound != null)
                audioSource.PlayOneShot(thirdPunchSound);

            canDamage = false;
            StartCoroutine(ResetDamageCooldown());

            if (player.healthPower <= 0)
            {
                player.IsKnockedDown();
                GetComponent<PlayerControllerV2>()?.SetGameStateToFinish();
            }
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

    public void SetCanDamage(bool state)
    {
        canDamage = state;
    }

    private IEnumerator ResetDamageCooldown()
    {
        yield return new WaitForSeconds(damageCooldown);
        canDamage = true;
    }
}
