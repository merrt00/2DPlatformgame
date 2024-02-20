using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyShooter : MonoBehaviour
{

    public GameObject bullet;
    public Transform bulletPos;

    private float timer;
    public GameObject player;
    [SerializeField] private AudioSource bowSound;


    // Update is called once per frame
    void Update()
    {
        

        float distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distance);
        if (distance < 5)
        {
           
            timer += Time.deltaTime;
            if (timer > 1)
            {
                timer = 0;
                Shoot();
            }
        }
        
    }
  

    void Shoot()
    {
        bowSound.Play();
        Instantiate(bullet, bulletPos.position , Quaternion.identity);
    }

}
