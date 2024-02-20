using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformations : MonoBehaviour
{
    public GameObject Intro;
    public GameObject Jump;
    public GameObject Attack;
    public GameObject Crouch;

    private void Start()
    {
        Intro.SetActive(false);
        Jump.SetActive(false);
        Attack.SetActive(false);    
        Crouch.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Intro")
        {
            Intro.SetActive(true);
        }
        if(collision.tag == "Jump")
        {
            Jump.SetActive(true);
        }
        if(collision.tag == "Attack")
        {
             Attack.SetActive(true);
        }
        if(collision.tag == "CROUCH")
        {
            Crouch.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Intro")
        {
            Intro.SetActive(false);
        }
        if (collision.tag == "Jump")
        {
            Jump.SetActive(false);
        }
        if (collision.tag == "Attack")
        {
            Attack.SetActive(false);
        }
        if (collision.tag == "CROUCH")
        {
            Crouch.SetActive(false);
        }
    }

}
