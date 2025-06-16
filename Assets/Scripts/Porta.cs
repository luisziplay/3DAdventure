using UnityEngine;

public class Porta : MonoBehaviour
{
    [SerializeField] private int numeroPorta;
    [SerializeField] private bool portaTrancada = false;
    [SerializeField]private Sprite spriteAvisoPorta;
    //private bool portaAberta = false;
    [Header("Caso trancado, defina o Sprite de asviso")]
   
    private Animator animator;
    private Avisos avisoPorta;

    void Start()
    {
        animator = GetComponent<Animator>();
        if(portaTrancada)
        {
            avisoPorta = GetComponent<Avisos>();
        }
    }
    public void AbrirPorta(int nChave = 0)
    {
        if(nChave == 0 && !portaTrancada)
        {
            animator.SetTrigger("Abrir");
            //portaAberta = true;
        }
        else if(nChave == numeroPorta && portaTrancada)
        {
            animator.SetTrigger("Abrir");
            portaTrancada = false;
            avisoPorta.DefineTroca(spriteAvisoPorta, "Porta Destrancada", Color.green);
            //portaAberta = true;
        }
    }

    public void FechaPorta(int nChave = 0)
    {
        if (nChave == numeroPorta && !portaTrancada)
        {
            animator.SetTrigger("Fechar");
        }
    }

    public bool EstaTrancada()
    {
        return portaTrancada;
    }

}
