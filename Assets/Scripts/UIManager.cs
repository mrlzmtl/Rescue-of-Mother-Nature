using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider playerHealthBar;
    

    // Imagens do Player
    public Image playerImage;

    // UI do Inimigo
    public GameObject enemyUI;
    public Slider enemyHealthBar;
    public Image enemyImage;

    // O que vai armazenar os dados do player
    private PlayerController player;

    // Timers e controles do enemyUI
    [SerializeField] private float enemyUITimer = 4f;
    private float enemyTimer;

    void Start()
    {
        // Obtem os dados do Player
        player = FindAnyObjectByType<PlayerController>();

        // Definir o barra de vida ao maximo do jogador

        playerHealthBar.maxValue = player.maxHealth;
        playerHealthBar.value = playerHealthBar.maxValue;



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Atualiza a vida do player com base no total de ataques que ele irá sofrer
    public void UpdatePlayerHealth(int amount)
    {
        playerHealthBar.value = amount;
    }

    // Para atualizar os elementos UI do inimigo
    public void UpdateEnemyUI(int maxHealth, int currentHealth, Sprite Image)
    {
        // Atualiza os dados do inimigo de acordo com o inimigo atacado
        enemyHealthBar.maxValue = maxHealth;
        enemyHealthBar.value = currentHealth;
        enemyImage.sprite = Image;

        // Zera o timer para começar a conta os 4 segundos
        enemyTimer = 0;

        // enemyUI fica habilitada, deixando ela visível
        enemyUI.SetActive(true);
    }



}
