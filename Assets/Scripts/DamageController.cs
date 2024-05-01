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

    public void SetLastAttack(AttackType attackType)
    {
        lastAttack = attackType;
    }

    public void SetLastAttackSide(AttackSide attackSide)
    {
        lastAttackSide = attackSide;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BoxerBlueSide")
        {
            switch (lastAttack)
            {
                case AttackType.Punch:
                    Debug.Log("Punch fait");
                    other.GetComponent<PlayerControllerV2>().healthPower -= punchPower;
                    other.GetComponent<PlayerControllerV2>().IsHit(lastAttack, lastAttackSide);
                    break;
                case AttackType.Kick:
                    Debug.Log("Kick fait");
                    other.GetComponent<PlayerControllerV2>().healthPower -= kickPower;
                    other.GetComponent<PlayerControllerV2>().IsHit(lastAttack, lastAttackSide);
                    break;
                default:
                    break;
            }
            
            Debug.Log("Collision sur boxerblueside");
            Debug.Log(other.GetComponent<PlayerControllerV2>().healthPower);
        }
    }
}
