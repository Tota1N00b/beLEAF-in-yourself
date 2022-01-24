using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercode : MonoBehaviour
{
    Rigidbody Player;
    float Speed = 5;
    float JumpForce = 450;
    [SerializeField] Transform Feet;
    bool PlayerIsGrounded = false;

    [SerializeField] GameObject[] Ref1;
    [SerializeField] GameObject[] Ref2;

    public AudioClip _jump;
    private AudioSource _audioSource;

    void Start()
    {
        Player = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (PublicVars.CamView == 0 & !PublicVars.EndGame)
        {
            float xSpeed = Input.GetAxis("Horizontal") * Speed;
            Player.velocity = new Vector3(xSpeed, Player.velocity.y, 0);

            if (xSpeed < 0 && (int)Mathf.Abs(transform.eulerAngles.y) == 0)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
            }
            if (xSpeed > 0 && (int)Mathf.Abs(transform.eulerAngles.y) != 0)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
            }
        }

        if (PublicVars.CamView == 1 && !PublicVars.EndGame)
        {
            float zSpeed = Input.GetAxis("Horizontal") * -Speed;
            Player.velocity = new Vector3(0, Player.velocity.y, zSpeed);

            if (zSpeed < 0 && (int)Mathf.Abs(transform.eulerAngles.y) == 270)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90, transform.eulerAngles.z);
            }
            if (zSpeed > 0 && (int)Mathf.Abs(transform.eulerAngles.y) != 270)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 270, transform.eulerAngles.z);
            }
        }

        PlayerIsGrounded = GroundCheck();
        if (Input.GetKeyDown(KeyCode.Space) && PlayerIsGrounded)
        {
            Player.velocity = new Vector3(Player.velocity.x, 0, 0);
            Player.AddForce(new Vector3(0, JumpForce, 0));
            _audioSource.PlayOneShot(_jump);
        }

        if (!PublicVars.CamShifting && !PublicVars.CamStartShifting)
        {
            if (PublicVars.CamView == 0)
                for (int i = 0; i < Ref1.Length - 1; i++)
                {
                    //if (i == 0)
                    //    print("Feet: " + Feet.transform.position.y + " Ref: " + Ref1[i].transform.position.y);
                    if (Feet.transform.position.y >= Ref1[i].transform.position.y &&
                        Feet.transform.position.y < Ref1[i + 1].transform.position.y)
                    {
                        //print("y interval: "+i+" to "+(i+1));
                        if (transform.position.x >= Ref1[i].transform.position.x - 10 * Ref1[i].transform.localScale.x / 2 &&
                            transform.position.x < Ref1[i].transform.position.x + 10 * Ref1[i].transform.localScale.x / 2)
                        {
                            transform.position = new Vector3(transform.position.x, transform.position.y, Ref1[i].transform.position.z);
                        }
                    }
                }
            else if (PublicVars.CamView == 1)
            {
                for (int i = 0; i < Ref2.Length - 1; i++)
                {
                    //if (i == 0)
                    //    print("Feet: " + Feet.transform.position.y + " Ref: " + Ref1[i].transform.position.y);
                    bool interval = false;
                    if (i != 3 && Feet.transform.position.y >= Ref2[i].transform.position.y &&
                        Feet.transform.position.y < Ref2[i + 1].transform.position.y)
                        interval = true;
                    if (i == 3 && Feet.transform.position.y >= Ref2[i].transform.position.y &&
                        Feet.transform.position.y < Ref2[i + 2].transform.position.y)
                        interval = true;
                    if (interval)
                    {
                        if (transform.position.z >= Ref2[i].transform.position.z - 10 * Ref2[i].transform.localScale.z / 2 &&
                            transform.position.z < Ref2[i].transform.position.z + 10 * Ref2[i].transform.localScale.z / 2)
                        {
                            transform.position = new Vector3(Ref2[i].transform.position.x, transform.position.y, transform.position.z);
                        }
                    }
                }
                //get to the tree
                if (transform.position.z >= 12.31)
                {
                    transform.position = new Vector3(6.35f, transform.position.y, transform.position.z);
                }
            }
        }
    }

    bool GroundCheck()
    {
        RaycastHit hit;
        float distance = .1f;
        Vector3 dir = new Vector3(0, -1, 0);

        if (Physics.Raycast(Feet.position, dir, out hit, distance))
        {
            //print("grounded");
            return true;
        }
        else
        {
            //print("not grounded");
            return false;
        }
    }
}
