using Unity.VisualScripting;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    [SerializeField] private float velocidadeInimigo;
    [SerializeField] private GameObject player;
    [SerializeField]private GameObject inimigo;
    private Rigidbody rb;
    private PlayerMovement pMove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pMove = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        AndarInimigo();
    }
    
    private void AndarInimigo(/*Inimigo rInimigo*/)
    {
        
        inimigo.GetComponent<Rigidbody>().rotation = player.transform.rotation;
        //Vector3 p = ;
        rb.AddForce((player.transform.forward).normalized *  velocidadeInimigo); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject)
        {
            rb.AddForce(inimigo.transform.position.normalized, ForceMode.Impulse);
        }
    }


}
