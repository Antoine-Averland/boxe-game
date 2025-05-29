using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRHandDamage : MonoBehaviour
{
    public DamageController.AttackType attackType = DamageController.AttackType.Punch;
    public DamageController.AttackSide attackSide = DamageController.AttackSide.Left;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoxerBlueSide"))
        {
            DamageController dc = other.GetComponent<DamageController>();
            if (dc != null)
            {
                dc.SetLastAttack(attackType);
                dc.SetLastAttackSide(attackSide);
                dc.SetCanDamage(true);
            }
        }
    }
}

