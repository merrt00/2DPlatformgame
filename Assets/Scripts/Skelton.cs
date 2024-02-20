using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelton : MonoBehaviour
{
    #region Public Variables
    public Transform raycast;
    public LayerMask raycastMask;
    public float raycastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public Transform rightLimit;
    public Transform leftLimit;
    #endregion
    [Header("Audio")]
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private AudioSource walkSound;
 

    #region Private Variables
    private RaycastHit2D hit;
    private Transform target;
    private Animator animator;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    #endregion

    void Awake()
    {
        SelectTarget();
        intTimer = timer;
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!attackMode)
        {
            Move();
        }

        if (!InsideOfLimits() && !inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyAttack"))
        {
            SelectTarget();
        }

        if (inRange)
        {
            hit = Physics2D.Raycast(raycast.position, transform.right, raycastLength, raycastMask);
            RaycastDebugger();
        }

        if (hit.collider != null)
        {
            EnemyLogic();
        }
        else if (hit.collider == null)
        {
            inRange = false;
        }

        if (inRange == false)
        {
            StopAttack();
        }
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            target = trig.transform;
            inRange = true;
            Flip();
        }


    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }
        if (cooling)
        {
            CoolDown();
            animator.SetBool("Attack", false);
        }
    }

    void Move()
    {
        
        animator.SetBool("canWalk", true);
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyAttack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        attackSound.Play();
        timer = intTimer;
        attackMode = true;

        animator.SetBool("canWalk", false);
        animator.SetBool("Attack", true);
    }

    void CoolDown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }
    
    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        animator.SetBool("Attack", false);

    }

    void RaycastDebugger()
    {
        if (distance > attackDistance)
        {
            Debug.DrawRay(raycast.position, transform.right * raycastLength, Color.red);  
        }
        else if (attackDistance > distance)
        {
            Debug.DrawRay(raycast.position, transform.right * raycastLength, Color.green);

        }

    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    private void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight) 
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();

    }

    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180;
        }
        else
        {
            rotation.y = 0;
        }

        transform.eulerAngles = rotation;
    }
}
