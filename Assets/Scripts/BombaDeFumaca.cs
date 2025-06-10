using System.Collections;
using TMPro;
using UnityEngine;

public class BombaDeFumaca : MonoBehaviour
{
    [SerializeField] private GameObject fumacaPrefab;
    [SerializeField] private float tempoDetonar = 3f;
    [SerializeField] private TextMeshPro tempoTexto;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

   IEnumerator ContadorTempo()
    {
        float tempoRestante = tempoDetonar;
        while (tempoRestante > 0)
        {
            tempoTexto.text = tempoRestante.ToString();
            yield return new WaitForSeconds(1f);
            tempoRestante -= 1f;
        }

        Detonar();
    }

    private void Detonar()
    {
        Instantiate(fumacaPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void IniciarContagem()
    {
        StartCoroutine(ContadorTempo());
    }

    private void OnCollisionEnter(Collision collision)
    {
        IniciarContagem();
    }

}
