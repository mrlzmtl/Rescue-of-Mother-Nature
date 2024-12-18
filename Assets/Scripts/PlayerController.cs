using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class PlayerController : MonoBehaviour
{
    // variavel que controla o animador do player
    private Animator playerAnimator;

    // variavel de trigger de animação
    private bool isWalking;
    private bool isDefending;

    // variavel golpe especial kick
    private int punchCount;
    private bool comboControl;
    //tempo do combo kick
    private float timeCross = 2.12f;

    // variavel para o personagem olhar para o lado certo
    private bool isFacingRight = true;

    // Variavel que verifica se o player está morto
    public bool isDead;

    // variavel para controlar o rigidBody (usado para movimentação do player)
    private Rigidbody2D playerRigidbody;

    // variaveis auxiliares para movimentação do player
    public Vector2 playerDirection;
    private float maxSpeed = 1f;
    private float currentSpeed;

    // variaveis para contagem de vida
    public int maxHealth = 100;
    public int currentHealth;

    // variaveis para sprite do personagem
    public Sprite playerImage;



    void Start()
    {
        // pegando o Animator e RigidBody do player
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();

        // setando a velocidade e vida atual com base em suas maximas
        currentSpeed = maxSpeed;

    }


    void Update()
    {
        // atualizando a movimentação do player
        PlayerMove();
        // atualizando a animação da movimentação do player
        UpdateAnimator();

        PlayerDefending();

        // golpe especial jab/kick
        if (Input.GetKeyDown(KeyCode.F))
        {
            // se o punchCount variavel de contagem para o golpe especial,
            // for menor que dois ele irá somar, quando ela chegar em dois o golpe especial (kick) é ativado
            if (punchCount < 2)
            {
                PlayerPunch();
                punchCount++;

                if (!comboControl)
                {
                    StartCoroutine(Combo());
                }
            }
            else
            {
                PlayerKick();
                punchCount = 0;
            }
            StopCoroutine(Combo());
        }
        //Se eu estiver pressionando a tecla g ele defende 
        if (Input.GetKey(KeyCode.K))
        {
            isDefending = true;
            ZeroSpeed();

        }
        else
        {
            isDefending = false;
            ResetSpeed();
        }


    }
    //Corrotina golpe especial kick
    IEnumerator Combo()
    {
        comboControl = true;
        yield return new WaitForSeconds(timeCross);
        punchCount = 0;
        comboControl = false;
    }

    void FixedUpdate()
    {
        // definindo se o personagem está andando ou não, Para atualizar a animação
        if (playerDirection.x != 0 || playerDirection.y != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        // finalmente movimentando o personagem, adicionando a ele a velocidade atual com uma matematica
        playerRigidbody.MovePosition(playerRigidbody.position + currentSpeed * Time.fixedDeltaTime * playerDirection);

    }

    void PlayerMove()
    {
        // recebendo qual tecla o Jogador esta clicando e adicionando ao PlayerDirection
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // ativando função de flipar dependendo de qual lado ele esta olhando
        if (playerDirection.x < 0 && isFacingRight)
        {
            Flip();
        }
        else if (playerDirection.x > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    // Função para virar o inimigo para o lado oposto
    void Flip()
    {
        isFacingRight = !isFacingRight;

        transform.Rotate(0, 180, 0);
    }

    void UpdateAnimator()
    {
        playerAnimator.SetBool("isWalking", isWalking);
    }


    //void player punch
    void PlayerPunch()
    {
        playerAnimator.SetTrigger("punch");
    }
    // void do golpe especial kick
    void PlayerKick()
    {
        playerAnimator.SetTrigger("kick");
    }

    void PlayerDefending()
    {
        playerAnimator.SetBool("isDefending", isDefending);
    }

    public void TakeDamage(int damage)
    {
        //se o player estiver vivo e ele nao estiver defendendo ele recebe dano
        if (!isDead && isDefending == false)
        {
            currentHealth -= damage;
            playerAnimator.SetTrigger("hitDamage");


            FindFirstObjectByType<UIManager>().UpdatePlayerHealth(currentHealth);

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


}
