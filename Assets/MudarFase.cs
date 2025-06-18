using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MudarFase : MonoBehaviour
{
    [SerializeField] private string nomeDaProximaFase;
    [SerializeField] private float tempoDeTransicao;
    [SerializeField] private GameObject efeitoFade;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(!string.IsNullOrEmpty(nomeDaProximaFase))
            {
                StartCoroutine(TransicaoParaProximaFase());
            }
        }
    }

    IEnumerator TransicaoParaProximaFase()
    {
        animator.SetTrigger("MudaFase");
        yield return new WaitForSeconds(tempoDeTransicao);
        SceneManager.LoadScene(nomeDaProximaFase);
    }
}
