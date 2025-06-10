using UnityEngine;

public class MachadoLancado : MonoBehaviour
{
    [SerializeField] private int dano;
    [SerializeField] private GameObject destroyMachadoPreFab;

    //Ajuste de rotacao inicial
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(destroyMachadoPreFab, gameObject.transform.position, gameObject.transform.rotation);
        GetComponent<ParticleSystem>().Stop();
        Destroy(this.gameObject);
    }
}
