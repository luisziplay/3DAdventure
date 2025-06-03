using System.Collections;
using TMPro;
using UnityEngine;

public class SistemaDeVida : MonoBehaviour
{
    [SerializeField] private int vida = 100;
    private bool estaVivo = true;
    private bool levarDano = true;
    private PlayerMovement pMove;
    [SerializeField] private TextMeshProUGUI textoVida;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pMove = GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        AtualizarTextoVida();
    }

    public bool EstaVivo()
    {
        return estaVivo;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fatal") && estaVivo && levarDano)
        {
            StartCoroutine(LevarDano(10));
        }
    }

    IEnumerator LevarDano(int dano)
    {
        levarDano = false;
        if (vida > 0)
        {
            pMove.Hit(); //Chama a animacao do metodo Hit do PlayerMoviment 
            vida -= dano;
            AtualizarTextoVida();
            VerificarVida();
            yield return new WaitForSeconds(0.5f);
            levarDano = true;
        
        }
    }

    private void VerificarVida()
    {
        if (vida <= 0)
        {
            vida = 0;
            estaVivo = false;
        }
    }

    private void AtualizarTextoVida()
    {
        textoVida.text = "Vida: " + vida.ToString();
    }


}
