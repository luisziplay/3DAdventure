using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float inputH;
    private float inputV;
    private Animator animator;
    private bool estaNoChao = true;
    private float velocidadeAtual;
    private bool morrer = true;
    private SistemaDeVida sVida;
    private Vector3 anguloRotacao = new Vector3(0, 90, 0);
    [SerializeField] private float velocidadeAndar;
    [SerializeField] private float velocidadeCorrer;
    [SerializeField] private float forcaPulo;
    [SerializeField] private TextMeshProUGUI textoPontos;
    [SerializeField] private int pontosTotais = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        sVida = GetComponent<SistemaDeVida>();
        velocidadeAtual = velocidadeAndar;
        textoPontos = GameObject.Find("Pontos").GetComponent<TextMeshProUGUI>();
        textoPontos.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (sVida.EstaVivo())
        {
            Andar();
            Girar();
            Pular();
            Correr();
            Atacar();
            Magia();
        }
        else if(!sVida.EstaVivo() && morrer)
        {
            Morrer();
        }
     
    }

    private void Andar()
    {
        inputV = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.forward * inputV;
        Vector3 moveForward = rb.position + moveDirection * velocidadeAtual * Time.deltaTime;
        rb.MovePosition(moveForward);

        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Andar", true);
            animator.SetBool("AndarTras", false);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            animator.SetBool("AndarTras", true);
            animator.SetBool("Andar", false);
        }
        else
        {
            animator.SetBool("AndarTras", false);
            animator.SetBool("Andar", false);
        }
    }

    private void Girar()
    {
        inputH = Input.GetAxis("Horizontal");
        Quaternion deltaRotation =
            Quaternion.Euler(anguloRotacao * inputH * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

        if(Input.GetKey(KeyCode.A) ||
                    Input.GetKey(KeyCode.D) ||
                        Input.GetKey(KeyCode.LeftArrow) ||
                            Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("Andar", true);
        }
    }

    private void Pular()
    {
        if(Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            rb.AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
            animator.SetTrigger("Pular");
            StartCoroutine(TempoPulo());
        }
    }

    IEnumerator TempoPulo()
    {   estaNoChao = false;
        yield return new WaitForSeconds(1.0f);
        estaNoChao = true;
    }

    private void Correr()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            velocidadeAtual = velocidadeCorrer;
            animator.SetBool("Correr", true);
        }
        else
        {
            velocidadeAtual = velocidadeAndar;
            animator.SetBool("Correr", false);
        }
    }

    private void Morrer()
    {
        morrer = false;
        animator.SetBool("EstaVivo", false);
        animator.SetTrigger("Morrer");
        rb.Sleep();
    }

    private void Interagir()
    {
        animator.SetTrigger("Interagir");
    }

    private void Pegar()
    {
        animator.SetTrigger("Pegar");
    }

    private void Atacar()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Atacar");
        }
    }

    private void Magia()
    {
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Magia");
        }
    }

    public void Hit()
    {
        animator.SetTrigger("Hit");
    }
    IEnumerator ContaPonto()
    {
        pontosTotais++;
        textoPontos.text = pontosTotais.ToString();
        yield return new WaitForSeconds(0.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Quebra"))
        {
            Atacar();
            //Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            estaNoChao = true;
            animator.SetBool("EstaNoChao", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            estaNoChao = false;
            animator.SetBool("EstaNoChao", false);
        }
    
    } private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Item") && Input.GetKey(KeyCode.E))
        {
            Pegar();
            StartCoroutine(ContaPonto());
            Destroy(other.gameObject);
            
        }
        else if (other.CompareTag("Porta") && Input.GetKey(KeyCode.E))
        {
            Interagir();
            other.gameObject.GetComponent<Animator>().SetTrigger("Abrir");
        }
    }
}
