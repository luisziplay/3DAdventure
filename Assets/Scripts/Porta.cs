using UnityEngine;

public class Porta : MonoBehaviour
{
    [SerializeField] private int numeroPorta;
    [SerializeField] private bool portaTrancada = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void AbrirPorta(int nChave = 0)
    {
        if(nChave == 0 && !portaTrancada)
        {
            animator.SetTrigger("Abrir");
        }
        else if(nChave == numeroPorta && portaTrancada)
        {
            animator.SetTrigger("Abrir");
            portaTrancada = false;
        }
    }

    public bool EstaTrancada()
    {
        return portaTrancada;
    }

}
