using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;
    public Animator Anim;
    public DamageController damageController;
    public bool canMove;
    public AudioClip audioPunchR = null;
    public AudioClip audioPunchL = null;
    public AudioClip audioKick = null;
    public AudioClip audioHit = null;
    public int healthPower = 100;
    private int previousHealth;

    private Rigidbody playerRb;
    private bool isOnGround = true;
    private AudioSource boxerAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        boxerAudioSource = GetComponent<AudioSource>();
        previousHealth = healthPower;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Punch();
            Kick();
            Move();
        }
        if (previousHealth != healthPower)
        {
            // Si la sant� pr�c�dente est sup�rieure � la sant� actuelle, cela signifie que le joueur a perdu des points de vie
            if (previousHealth > healthPower)
            {
                // Jouer le son de perte de vie
                boxerAudioSource.PlayOneShot(audioHit);
            }

            // Mettez � jour la sant� pr�c�dente avec la sant� actuelle pour la prochaine it�ration
            previousHealth = healthPower;
        }
    }

    public void Punch()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Anim.Play("Punch1_R");
            boxerAudioSource.PlayOneShot(audioPunchR);
            damageController.SetLastAttack(DamageController.AttackType.Punch);
            damageController.SetLastAttackSide(DamageController.AttackSide.Right);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Anim.Play("Punch1_L");
            boxerAudioSource.PlayOneShot(audioPunchL);
            damageController.SetLastAttack(DamageController.AttackType.Punch);
            damageController.SetLastAttackSide(DamageController.AttackSide.Left);
        }
    }

    public void Kick()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Anim.Play("Kick1_R");
            boxerAudioSource.PlayOneShot(audioKick);
            damageController.SetLastAttack(DamageController.AttackType.Kick);
            damageController.SetLastAttackSide(DamageController.AttackSide.Right);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Anim.Play("Kick1_L");
            boxerAudioSource.PlayOneShot(audioKick);
            damageController.SetLastAttack(DamageController.AttackType.Kick);
            damageController.SetLastAttackSide(DamageController.AttackSide.Left);
        }
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(-movementSpeed, 0, 0);
            //Anim.Play("MoveBack");
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(movementSpeed, 0, 0);
            //Anim.Play("MoveForw");
        }

        // V�rifier si la touche A est enfonc�e
        if (Input.GetKey(KeyCode.A))
        {
            // D�placer le personnage vers la gauche en fonction de la vitesse de d�placement
            transform.position += new Vector3(0, 0, movementSpeed);
        }
        // V�rifier si la touche D est enfonc�e
        else if (Input.GetKey(KeyCode.D))
        {
            // D�placer le personnage vers la droite en fonction de la vitesse de d�placement
            transform.position += new Vector3(0, 0, -movementSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    public void IsHit(DamageController.AttackType attackType, DamageController.AttackSide attackSide)
    {
        if (attackType == DamageController.AttackType.Kick)
        {
            if (attackSide == DamageController.AttackSide.Left)
            {
                Anim.Play("GetHitBody1_R");
                
            }
            else
            {
                Anim.Play("GetHitBody1_L");
                
            }
        } else if (attackType == DamageController.AttackType.Punch)
        {
            if (attackSide == DamageController.AttackSide.Left)
            {
                Anim.Play("GetHitHead1_R");
                
            } else
            {
                Anim.Play("GetHitHead1_L");
                
            }
        }
        
    }

    // When boxer make a collision 
    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }
}
