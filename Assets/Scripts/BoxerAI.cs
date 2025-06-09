using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxerAI : MonoBehaviour
{
    public Transform[] movePoints; // Définir des positions limites du ring
    public float moveSpeed = 1.5f;
    public float moveInterval = 3f; // Temps entre les changements de direction
    public PlayerControllerV2 controller;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        StartCoroutine(AIRoutine());
    }

    IEnumerator AIRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(moveInterval);

            // Choisir une nouvelle position dans la zone définie
            targetPosition = GetRandomRingPosition();
            isMoving = true;

            // Attaque simulée
            LaunchRandomAttack();
        }
    }

    void Update()
    {
        if (isMoving)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
            {
                isMoving = false;
            }
        }
    }

    Vector3 GetRandomRingPosition()
    {
        // Choisir une position entre deux extrémités définies dans la scène
        Vector3 min = movePoints[0].position;
        Vector3 max = movePoints[1].position;

        float x = Random.Range(min.x, max.x);
        float z = Random.Range(min.z, max.z);
        return new Vector3(x, transform.position.y, z);
    }

    void LaunchRandomAttack()
    {
        if (controller == null) return;

        int attack = Random.Range(0, 4);
        switch (attack)
        {
            case 0:
                controller.Anim.Play("Punch1_L");
                controller.damageController.SetLastAttack(DamageController.AttackType.Punch);
                controller.damageController.SetLastAttackSide(DamageController.AttackSide.Left);
                break;
            case 1:
                controller.Anim.Play("Punch1_R");
                controller.damageController.SetLastAttack(DamageController.AttackType.Punch);
                controller.damageController.SetLastAttackSide(DamageController.AttackSide.Right);
                break;
            case 2:
                controller.Anim.Play("Kick1_L");
                controller.damageController.SetLastAttack(DamageController.AttackType.Kick);
                controller.damageController.SetLastAttackSide(DamageController.AttackSide.Left);
                break;
            case 3:
                controller.Anim.Play("Kick1_R");
                controller.damageController.SetLastAttack(DamageController.AttackType.Kick);
                controller.damageController.SetLastAttackSide(DamageController.AttackSide.Right);
                break;
        }
    }
}
