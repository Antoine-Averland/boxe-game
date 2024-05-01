using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public int kickPower = 20;
    public int punchPower = 10;
    public enum AttackType { None, Punch, Kick };

    private AttackType lastAttack = AttackType.None;

    public void SetLastAttack(AttackType attackType)
    {
        lastAttack = attackType;
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
                    break;
                case AttackType.Kick:
                    Debug.Log("Kick fait");
                    other.GetComponent<PlayerControllerV2>().healthPower -= kickPower;
                    break;
                default:
                    break;
            }
            
            Debug.Log("Collision sur boxerblueside");
            Debug.Log(other.GetComponent<PlayerControllerV2>().healthPower);
        }
    }
}
