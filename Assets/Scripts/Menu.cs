using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
[SerializeField] private string nomeCena;
    public void Scene1()
    {
        SceneManager.LoadScene(nomeCena);    
    }
    public void Voltar()
    {
        SceneManager.LoadScene(nomeCena);
    }
}
