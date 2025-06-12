using Unity.Collections;
using UnityEngine;

public class Avisos : MonoBehaviour
{
    [Header("Avisos do Jogo/Objeto")]
    [TextArea]
    [SerializeField] private string avisoTexto;
    [Header("Sprite para aparecer junto com o texto")]
    [SerializeField] private Sprite spriteAviso;
    [Header("Cor de aviso")]
    [SerializeField] private Color corAviso = Color.white;
    private bool avisoTemporario = false;

    public string AvisoTexto()
    {
        return avisoTexto;
    }

    public Sprite SpriteAvisos()
    {
        return spriteAviso; 
    }

    public Color CorAviso()
    {
        return corAviso;
    }

    public void DefineTroca(Sprite s, string t, Color c)
    {
        spriteAviso = s;
        avisoTexto = t;
        corAviso = c;
    }

    public bool AvisoTemporario()
    {
        return avisoTemporario;
    }


}
