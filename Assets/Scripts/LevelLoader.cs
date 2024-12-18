using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Se precionar a tecla enter 
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)){
            //Muda a cena utilizando a corrotina
            StartCoroutine(CarregarFase("Fase1"));
        }

        //Criando a corrotina
        IEnumerator CarregarFase(string nomeFase)
        {
            //Iniciar a animacao
            transition.SetTrigger("Start");

            //Esperar o tempo de animacao
            yield return new WaitForSeconds(1f);

            //Carregar a cena 
            SceneManager.LoadScene(nomeFase);
        }
    }
}
