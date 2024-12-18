using UnityEngine;

public class LixoController : MonoBehaviour
{

    private Animator lixoAnimator;


    private Rigidbody2D lixoRigidbody;


    public Sprite lixoSprite;


    private Transform target;


    private bool isDead = false;


    private int maxHealth = 100;
    private int currentHealth;


    private float maxSpeed = 0.7f;
    private float currentSpeed;


    private float horizontalForce;
    private float verticalForce;


    private float walkTimer;


    private float stunTime = 0.5f;
    private float stunTimer;

    private bool isTakingDamage;

    void Start()
    {

        lixoAnimator = GetComponent<Animator>();


        lixoRigidbody = GetComponent<Rigidbody2D>();


        target = FindAnyObjectByType<PlayerController>().transform;


        currentHealth = maxHealth;


        currentSpeed = maxSpeed;
    }

    
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            isTakingDamage = true;
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                isDead = true;
                ZeroSpeed();
                lixoAnimator.SetTrigger("Dead");
            }
        }
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    void ResetSpeed()
    {
        currentSpeed = maxSpeed;
    }

    public void DisableLixo()
    {
        this.gameObject.SetActive(false);
    }
}
