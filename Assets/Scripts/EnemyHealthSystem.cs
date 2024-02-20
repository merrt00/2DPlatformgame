using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider slider;
    private Animator animator;
    [SerializeField] private AudioSource hurtSound;
    // Start is called before the first frame update
    void Start()
    {

        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = currentHealth;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        hurtSound.Play();
        currentHealth -= damage;
        slider.value = currentHealth;
        animator.SetTrigger("Hit");


        if (currentHealth <= 0)
        {
            Die();
        }

    }
    void Die()
    {
        Debug.Log("EnemyDie!!");
        animator.SetTrigger("Die");



        //GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 0.4f);
    }
}
