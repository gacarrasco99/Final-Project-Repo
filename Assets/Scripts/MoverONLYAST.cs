using UnityEngine;
using System.Collections;

public class MoverONLYAST : MonoBehaviour
{
    public float speed;
    public bool hardMode = false;

    private Rigidbody rb;

    public void hard()
    {
        speed = -15;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = -5;

        rb.velocity = transform.forward * speed;


        if (hardMode == true)
        {
            rb.velocity = transform.forward * speed * 3;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speed = -15;
            rb.velocity = transform.forward * speed * 3;
        }
    }

}
