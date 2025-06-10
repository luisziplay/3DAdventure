using System.Collections;
using UnityEngine;

public class Magia : MonoBehaviour
{
    [SerializeField] private int danoMagia;
    [SerializeField] private GameObject destroyMagiaPreFab;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(destroyMagiaPreFab, gameObject.transform.position, gameObject.transform.rotation);
        GetComponent<ParticleSystem>().Stop();
        Destroy(this.gameObject);
    }
}
