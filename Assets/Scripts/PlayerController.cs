using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRB;
    Animator playerAnimator;
    public float moveSpeed = 1f;
    public float jumpSpeed = 1f, jumpFrequency = 1f, nextJumpTime;
    public BoxCollider2D player;


    [Header("Face")]
    bool facingRight = true;

    [Header("Animations")]
    public bool isGrounded = false;
    public bool isAttacking = false;
    public bool isCrouch = false;
    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;

    [Header("Attack")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;

    [Header("Audio")]
    [SerializeField] private AudioSource walkSound;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource attackSound;

    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        player = player.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMove();
        OnGroundCheck();
        Attack();
        Crouch();
        if (playerRB.velocity.x < 0 && facingRight)
        {
            FlipFace();
        }
        else if (playerRB.velocity.x > 0 && !facingRight)
        {
            FlipFace();
        }
        if (Input.GetAxis("Vertical") > 0 && isGrounded && (nextJumpTime < Time.timeSinceLevelLoad) && isAttacking == false && isCrouch == false)
        {
            nextJumpTime = Time.timeSinceLevelLoad + jumpFrequency;
            Jump();
        }
    }

    void FixedUpdate()
    {

    }

    void HorizontalMove()
    {
        if(isCrouch == false)
        {
            playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, playerRB.velocity.y);
            playerAnimator.SetFloat("playerSpeed", Mathf.Abs(playerRB.velocity.x));
        }
       
    }

    void Jump()
    {
        jumpSound.Play();
        playerRB.AddForce(new Vector2(0f, jumpSpeed));
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.E) && isGrounded)
        {
            attackSound.Play();
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            isAttacking = true;
            playerAnimator.SetBool("isAttacking", true);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealthSystem>().TakeDamage(attackDamage);
                Debug.Log("Hit Enemies" +enemy.name);
            }
            
        }
        else
        {
            isAttacking = false;
            playerAnimator.SetBool("isAttacking", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    void Crouch()
    {

        if (Input.GetKey(KeyCode.S) && isGrounded || Input.GetAxis("Vertical") < 0 && isGrounded)
        {
            player.size = new Vector2(0.2418499f, 0.2426018f);
            player.offset = new Vector2(0.03058147f, -0.2f);
            
            isCrouch = true;
            playerAnimator.SetBool("isCrouch", true);
            playerAnimator.SetFloat("playerSpeed", 0f);
        }
        else
        {
            player.size = new Vector2(0.2418499f, 0.4451104f);
            player.offset = new Vector2(0.03058147f, -0.09513968f);
            isCrouch = false;
            playerAnimator.SetBool("isCrouch", false);
        }
    }

    void FlipFace()
    {
        if(gameObject.tag == "Player")
        {
            facingRight = !facingRight;
            Vector3 tempLocalScale = transform.localScale;
            tempLocalScale.x *= -1;
            transform.localScale = tempLocalScale;
        }
        
    }

    void OnGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer);
        playerAnimator.SetBool("isGroundedAnim", isGrounded);

    }
}