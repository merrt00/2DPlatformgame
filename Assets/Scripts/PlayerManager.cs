using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float health;
    bool dead = false;
    public Animator animator;

    public Transform floatingText;


    public Slider slider;
    bool mouseIsNotOverUI;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = health;
        slider.value = health;
        animator.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        mouseIsNotOverUI = EventSystem.current.currentSelectedGameObject == null;
    }

    public void GetDamage(float damage)
    {
        Instantiate(floatingText,transform.position,Quaternion.identity).GetComponent<TextMesh>().text = damage.ToString();
        if ((health - damage) >= 0)
        {
            animator.SetTrigger("Hurt");
            health -= damage;
        }
        else
        {
            health = 0;
            
        }
        slider.value = health;
        AmIDead();
    }

    void AmIDead()
    {
        if (health <= 0)
        {
            dead = true;
            Destroy(gameObject,15f);
            SceneManager.LoadScene(1);

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            dead = true;
            SceneManager.LoadScene(1);
        }
        if (other.tag == "End")
        {
            SceneManager.LoadScene(2);
        }
        if (other.tag == "End1")
        {
            SceneManager.LoadScene(3);
        }
    }

}