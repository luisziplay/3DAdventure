using UnityEngine;

public class ObjetoQuebra : MonoBehaviour
{
    [SerializeField] private int vidaObj;
    [SerializeField] private GameObject efeitoQuebra;

    public void Quebrar(int dano)
    {
        vidaObj -= dano;

        if (vidaObj <= 0)
        {
            Instantiate(efeitoQuebra, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
