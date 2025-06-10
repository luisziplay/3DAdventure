using UnityEngine;

public class PlataformaMove : MonoBehaviour
{
   
    [SerializeField] private GameObject ponto1;
    [SerializeField] private GameObject ponto2;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private bool dirX;
    [SerializeField] private bool dirY;
    [SerializeField] private bool dirZ;
    private int dirP = 1;


    // Update is called once per frame
    void Update()
    {
        if (dirX)
        {
            transform.position += new Vector3(Time.deltaTime * speed * dirP, 0, 0);
        }
        else if (dirY)
        {
            transform.position += new Vector3(0, Time.deltaTime * speed * dirP, 0);
        }
        else if (dirZ)
        {
            transform.position += new Vector3(0, 0, Time.deltaTime * speed * dirP);
        }
        else
        {
            transform.position += new Vector3(0, 0, 0);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plataforma"))
        {
            if (dirP == 1)
            {
                dirP = -1;
            }
            else
            {
                dirP = 1;
            }

        }
    }
}

