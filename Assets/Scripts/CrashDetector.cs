using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrachDetector : MonoBehaviour
{
    CircleCollider2D playerHead;
    [SerializeField] float delay = 0.5f;
    [SerializeField] ParticleSystem CrashEffect;
    private void Start()
    {
        CrashEffect.Stop();
        playerHead = GetComponent<CircleCollider2D>();
    }  
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" && playerHead.IsTouching(other.collider))
        {
            CrashEffect.Play();
            GetComponent<AudioSource>().Play();
            Invoke("onLoadScene", delay);
        }
    }

    public void onLoadScene()
    {
        // this method gets you the scean you want by passing the name or the index of the scean
        // SceneManager.LoadScene("Level1");
        SceneManager.LoadScene(0);
    }
}
