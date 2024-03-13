using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 10f; // speed of the player's movement
    private float maxSpeed = 20f; // maximum speed of the player
    private float jumpForce = 6f; // force of the player's jump
    private Rigidbody rb;
    [SerializeField] private bool isGrounded;
    public Transform cameraTransform;
    public Animator animator;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public float searchRange = 100f;

    public int health = 0;
    public int maxHealth = 1000;
    public Slider healthBar;
    private GameManager gameManager;
    //public bool isGameOver = false;
    public TextMeshProUGUI healthText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
        healBarUpdate(1000);
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !gameManager.isGameOver)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            animator.SetBool("IsJump", true);

            
        }
        if (gameManager.isGameOver)
        {
            animator.SetTrigger("Die");
            // Check if the "Die" animation is finished playing
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (gameManager.isGameOver && stateInfo.IsName("Die") && stateInfo.normalizedTime >= 1.0f)
            {
                // The "Die" animation has finished playing, perform game over actions
                // Your logic here

                // Stop the game (you can replace this with your own game over logic)
                Time.timeScale = 0f;
            }
        }
    }
    private void FixedUpdate()
    {
        // Get the player's input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputVector = new Vector3(horizontalInput, 0, verticalInput);
        if (inputVector.magnitude > 0&& !gameManager.isGameOver)
        {
           
            // Move the player

            float targetAngle = Mathf.Atan2(inputVector.x, inputVector.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.AddForce(moveDir * moveSpeed);
            
            // Limit the player's maximum speed
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
            
        }
        if (rb.velocity.magnitude > 0.1&&isGrounded)
        {
            animator.SetBool("IsRun", true);
            AudioManager.instance.PlayWalkSound();
        }
        else
        {
            animator.SetBool("IsRun", false);
            AudioManager.instance.StopWalkSound();
        }
        animator.SetFloat("Speed", rb.velocity.magnitude);


        // look at enemy 
        
        GameObject nearestEnemy = FindNearestEnemyWithTag("Enemy", searchRange);
        //GameObject nearestS_Enemy = FindNearestEnemyWithTag("S_Enemy", searchRange);
        //GameObject targetEnemy = (nearestS_Enemy != null) ? nearestS_Enemy : nearestEnemy;
        GameObject targetEnemy = nearestEnemy;

        if (targetEnemy != null)
        {
            Vector3 direction = targetEnemy.transform.position - transform.position;
            direction.Normalize();
            direction.y = 0;
            //if (Mathf.Abs(targetEnemy.transform.position.y - transform.position.y) < 2)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("IsJump", false);
            rb.velocity = Vector3.zero;
        }
        

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
          //Debug.Log("updatehp");
          //int amount = Random.Range(30, 200);
            healBarUpdate(-1);
        }
    }
    private GameObject FindNearestEnemyWithTag(string tag, float maxDistance)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);

        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < maxDistance && distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
    public void healBarUpdate(int amount)
    {
        health += amount;
        if (health > 1000)
        {
            health = 1000;
        }
        else if (health < 0)
        {
            health = 0;
        }
        healthBar.value = health;
        if (health < 1) { gameManager.isGameOver = true; }
        healthText.text = health + "/" + maxHealth;
    }
}
