using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public DamageController damageController;

    public void OnAnimationDamageFinish()
    {
        damageController.SetCanDamage(true);
        damageController.SetLastAttack(DamageController.AttackType.None);
        damageController.SetLastAttackSide(DamageController.AttackSide.None);
    }
}
