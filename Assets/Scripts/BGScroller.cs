using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGScroller : MonoBehaviour
{

    public float scrollSpeed;
    public float tileSizeZ;
    public Text winText;

  

    private Vector3 startPosition;
    
    void Start()
    {
        startPosition = transform.position;

    }

   
    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.forward * newPosition;

       if (winText.text == "You win! Game created by Gary Carrasco!")
        {
            StartCoroutine(bgFast());
        }

        IEnumerator bgFast()
        {
            yield return new WaitForSeconds(1);
            scrollSpeed += -0.005f;

            if (scrollSpeed < -10)
            {
                scrollSpeed = -10;
            }

        }

    }
}
