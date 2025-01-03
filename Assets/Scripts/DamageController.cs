using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public int kickPower = 20;
    public int punchPower = 10;
    public enum AttackType { None, Punch, Kick };
    public enum AttackSide { None, Left, Right };

    private AttackType lastAttack = AttackType.None;
    private AttackSide lastAttackSide = AttackSide.None;
    private bool canDamage = true;

    public void SetLastAttack(AttackType attackType)
    {
        lastAttack = attackType;
    }

    public void SetLastAttackSide(AttackSide attackSide)
    {
        lastAttackSide = attackSide;
    }

    public void SetCanDamage(bool state)
    {
        canDamage = state;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BoxerBlueSide" && canDamage)
        {
            switch (lastAttack)
            {
                case AttackType.Punch:
                    Debug.Log("Punch fait");
                    other.GetComponent<PlayerControllerV2>().healthPower -= punchPower;
                    other.GetComponent<PlayerControllerV2>().IsHit(lastAttack, lastAttackSide);
                    other.GetComponent<PlayerControllerV2>().SetHealthPowerText();
                    other.GetComponent<PlayerControllerV2>().StartCoroutine(other.GetComponent<PlayerControllerV2>().BoostSpotlightIntensity(false));
                    SetCanDamage(false);
                    break;
                case AttackType.Kick:
                    Debug.Log("Kick fait");
                    other.GetComponent<PlayerControllerV2>().healthPower -= kickPower;
                    other.GetComponent<PlayerControllerV2>().IsHit(lastAttack, lastAttackSide);
                    other.GetComponent<PlayerControllerV2>().SetHealthPowerText();
                    other.GetComponent<PlayerControllerV2>().StartCoroutine(other.GetComponent<PlayerControllerV2>().BoostSpotlightIntensity(true));
                    SetCanDamage(false);
                    break;
                default:
                    break;
            }
            
            Debug.Log("Collision sur boxerblueside");
            Debug.Log(other.GetComponent<PlayerControllerV2>().healthPower);

            if (other.GetComponent<PlayerControllerV2>().healthPower <= 0)
            {
                other.GetComponent<PlayerControllerV2>().IsKnockedDown();
                GetComponent<PlayerControllerV2>().SetGameStateToFinish();
            }
        }
    }
}
