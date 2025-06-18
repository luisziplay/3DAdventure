using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SistemaInterativo : MonoBehaviour
{
    [Header("Objeto do Canvas que o Icone")]
    [SerializeField] private Image spriteInterface;
    [Header("Objeto do Canvas que o Texto")]
    [SerializeField] private TextMeshProUGUI textoAviso;
    [SerializeField] private float tempoExibir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ProcuraReferencias();
    }

    void Update()
    {
        ProcuraReferencias();
    }

    private void ProcuraReferencias()
    {
        if (spriteInterface == null || textoAviso == null)
        {
            spriteInterface = GameObject.Find("SpriteInterface").GetComponent<Image>();
            spriteInterface.enabled = false;
            textoAviso = GameObject.Find("Text (texto avisos)").GetComponent<TextMeshProUGUI>(); 
            textoAviso.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Avisos>(out Avisos a))
        {
            StartCoroutine(ExibirAvisos(a.SpriteAvisos(), a.AvisoTexto(), a.CorAviso()));
            if (a.AvisoTemporario())
            {
                StartCoroutine(TimerAvisoTemporario(other.gameObject));
            }
        }
    }

    IEnumerator TimerAvisoTemporario(GameObject g)
    {
        yield return new WaitForSeconds(tempoExibir);
        Destroy(g);
    }
    

    IEnumerator ExibirAvisos(Sprite s, string t, Color c)
    {
        //Ativando os objetos
        spriteInterface.enabled = true;
        textoAviso.enabled = true;

        //Passando a sprite e definindo a cor do icone
        spriteInterface.sprite = s;
        spriteInterface.color = c;

        //Passando a sprite e definindo a cor 
        textoAviso.text = t;
        textoAviso.color = c;

        //Esperando um tempo para desativar os objetos 
        yield return new WaitForSeconds(tempoExibir);
        spriteInterface.enabled = false;
        textoAviso.enabled = false;
    }

}
