using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinishLine : MonoBehaviour
{
    [SerializeField] float delay = 2f;
    [SerializeField] ParticleSystem FinishEffect;
    
    public Boolean isFinished = false;
    public void Start()
    {
            FinishEffect.Stop();
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(isFinished) return;
        if(collider.tag == "Player")
        {
            Debug.Log("The winner is " + collider.gameObject.tag + " .You won");
            FinishEffect.Play();
        }else if (collider.tag == "Enemy"){
            Debug.Log("The winner is " + collider.gameObject.tag + " .You Lost");
            FinishEffect.Play();
        }
        isFinished = true;
        GetComponent<AudioSource>().Play();
        Invoke("onLoadScene", delay);
    }
    public void onLoadScene()
    {
        // this method gets you the scean you want by passing the name or the index of the scean
        // SceneManager.LoadScene("Level1");
        SceneManager.LoadScene(0);
    }
}
