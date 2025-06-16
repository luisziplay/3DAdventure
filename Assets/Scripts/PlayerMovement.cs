using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float inputH;
    private float inputV;
    private Animator animator;
    private bool estaNoChao = true;
    private float velocidadeAtual;
    private bool contato = false;
    private bool morrer = true;
    private SistemaDeVida sVida;
    private SistemaInterativo sInterativo;
    private Vector3 anguloRotacao = new Vector3(0, 90, 0);
    private bool temChave = false;
    private int numeroChave = 0;
    private bool temMana = true;
    private bool estaFechada = false;
    [SerializeField] private float velocidadeAndar;
    [SerializeField] private float velocidadeCorrer;
    [SerializeField] private float forcaPulo;
    [SerializeField] private GameObject magiaPreFab;
    [SerializeField] private GameObject quebraPreFab;
    [SerializeField] private GameObject miraMagia;
    [SerializeField] private int forcaArremeco;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        sInterativo = GetComponent<SistemaInterativo>();
        sVida = GetComponent<SistemaDeVida>();
        velocidadeAtual = velocidadeAndar;
    }

    // Update is called once per frame
    void Update()
    {
        if(sVida.EstaVivo())
        {
            Andar();
            Girar();
            Pular();
            Correr();
            Magia();
        }
        else if(!sVida.EstaVivo() && morrer)
        {
            Morrer();
        }

        if(Input.GetMouseButtonDown(0))
        {
            contato = true;
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
        else if (Input.GetKey(KeyCode.S))
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

        if (Input.GetKey(KeyCode.A) ||
                    Input.GetKey(KeyCode.D) ||
                        Input.GetKey(KeyCode.LeftArrow) ||
                            Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("Andar", true);
        }
    }

    private void Pular()
    {
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            rb.AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
            animator.SetTrigger("Pular");
            StartCoroutine(TempoPulo());
        }
    }

    public void TemMana()
    {
        temMana = false;
        //if (sVida.CargaMana()) ;
    }

    IEnumerator TempoPulo()
    {
        estaNoChao = false;
        yield return new WaitForSeconds(1.5f);
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

    private int Atacar()
    {
        animator.SetTrigger("Atacar");
        Instantiate(quebraPreFab, miraMagia.transform.position, miraMagia.transform.rotation);
        contato = false;
        return 10;
    }

    public void Magia()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(LancarMagia());
            animator.SetTrigger("Magia");
        }
    }


    IEnumerator LancarMagia()
    {
        if (temMana)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject magia = Instantiate(magiaPreFab, miraMagia.transform.position, miraMagia.transform.rotation);
            //magia.transform.rotation *= Quaternion.Euler(0, -90, 0); //Machado
            magia.transform.rotation *= Quaternion.Euler(90, 0, 180); //Flecha
            Rigidbody rbMagia = magia.GetComponentInChildren<Rigidbody>();
            rbMagia.AddForce(miraMagia.transform.forward * forcaArremeco, ForceMode.Impulse);
            sVida.UsarMana();
        }
    }

    public void Hit()
    {
        animator.SetTrigger("Hit");
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            estaNoChao = true;
            animator.SetBool("EstaNoChao", true);
        }

        if (collision.gameObject.CompareTag("Quebra") && Input.GetMouseButtonDown(0))
        {
            Atacar();
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            estaNoChao = false;
            animator.SetBool("EstaNoChao", false);
        }
        if (collision.gameObject.CompareTag("Quebra"))
        {
            
            contato = false;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Mana") && Input.GetKey(KeyCode.E))
        {
            sVida.CargaMana(50);
            Pegar();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Vida") && Input.GetKey(KeyCode.E))
        {
            sVida.CargaVida(50);
            Pegar();
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("Porta") && Input.GetKey(KeyCode.E))
        {
            if(other.gameObject.GetComponent<Porta>().EstaTrancada())
            {
                estaFechada = false;
                Interagir();
                other.gameObject.GetComponent<Porta>().AbrirPorta(numeroChave);
                estaFechada = true;
            }
            else if (!other.gameObject.GetComponent<Porta>().EstaTrancada())
            {
                estaFechada = false;
                Interagir();
                other.gameObject.GetComponent<Porta>().AbrirPorta();
                estaFechada = true;
            }    
        }
        else if (other.CompareTag("Porta") && Input.GetKey(KeyCode.E) && estaFechada)
        {
            if (other.gameObject.GetComponent<Porta>().EstaTrancada())
            {
                Interagir();
                other.gameObject.GetComponent<Porta>().FechaPorta();
            }
            else if (!other.gameObject.GetComponent<Porta>().EstaTrancada())
            {
                Interagir();
                other.gameObject.GetComponent<Porta>().FechaPorta();
            }
        }
        else if (other.CompareTag("Chave") && Input.GetKey(KeyCode.E))
        {
            Pegar();
            temChave = true;
            numeroChave = other.gameObject.GetComponent<Chave>().NumeroPorta();
            other.gameObject.GetComponent<Chave>().PegarChave();
        }
        if (other.gameObject.CompareTag("Quebra"))
        {
            if(contato)
            {
                other.gameObject.GetComponent<ObjetoQuebra>().Quebrar(Atacar());
            }
        }
    }
}
