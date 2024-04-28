using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;
    public Animator Anim;

    private Rigidbody playerRb;
    private bool isOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Punch();
        Kick();
        Move();
    }

    public void Punch()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Anim.Play("Punch1_R");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Anim.Play("Punch1_L");
        }
    }

    public void Kick()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Anim.Play("Kick1_R");
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Anim.Play("Kick1_L");
        }
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-movementSpeed, 0, 0);
            //Anim.Play("MoveBack");
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(movementSpeed, 0, 0);
            //Anim.Play("MoveForw");
        }

        // V�rifier si la touche W est enfonc�e
        if (Input.GetKey(KeyCode.W))
        {
            // D�placer le personnage vers l'avant en fonction de la vitesse de d�placement
            transform.position += new Vector3(0, 0, movementSpeed);
        }
        // V�rifier si la touche S est enfonc�e
        else if (Input.GetKey(KeyCode.S))
        {
            // D�placer le personnage vers l'arri�re en fonction de la vitesse de d�placement
            transform.position += new Vector3(0, 0, -movementSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    // When boxer make a collision 
    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }
}
