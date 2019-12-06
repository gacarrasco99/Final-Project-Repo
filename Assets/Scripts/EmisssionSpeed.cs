using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmisssionSpeed : MonoBehaviour
{
    public Text winText;
    private ParticleSystem stars;
    


    void Start()
    {
        stars = GetComponent<ParticleSystem>();
    }

    
    void Update()
    {
        var emission = stars.emission;
        var starsSpeed = stars.main;
       
        

        if (winText.text == "You win! Game created by Gary Carrasco!")
        {
            StartCoroutine(starFast());
        }

        IEnumerator starFast()
        {
            
            yield return new WaitForSeconds(1);
            starsSpeed.simulationSpeed += 5;
        }
    }
}
