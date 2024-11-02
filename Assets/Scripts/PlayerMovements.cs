using System;
using System.Collections;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    // [SerializeField]: This allows you to expose the TorqeAmount variable in Unity's Inspector without making it public. 
    // It helps in adjusting values directly in the editor without changing the script.
    [SerializeField] float TorqeAmount = 10f;
    [SerializeField] Vector3 jumpForce = new Vector3(0, 10f, 0);
    Rigidbody2D rb2d;
    private bool permission = true;
    [SerializeField] private float Cooldown = 10f;
    SurfaceEffector2D surfaceEffector2D;
    [SerializeField] float boostSpeed = 40f;
    [SerializeField] float baseSpeed = 20f;
    private int score = 0;
   [SerializeField] float boostAmount = 10f; // Boost amount that decreases
    [SerializeField] float depletionRate = 1f; // Speed at which boostAmount depletes

    void Start()
    {
        // GetComponent<Component name>() this method get you the component inside the gameobject where you store your script
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindAnyObjectByType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        RespondToBoost();
    }

    void RespondToBoost()
    {
        // Check if boost is allowed and the player is pressing F
        if (permission && boostAmount > 0 && Input.GetKey(KeyCode.F))
        {
            surfaceEffector2D.speed = boostSpeed;

            // Gradually reduce boostAmount over time
            boostAmount -= depletionRate * Time.deltaTime;

            if (boostAmount <= 0)
            {
                boostAmount = 0; // Clamp to 0 to avoid negative values
                permission = false; // Stop boosting
                StartCoroutine(CooldownCoroutine());
            }
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb2d.AddTorque(-TorqeAmount);
            // Track the total rotation of the object
            IncreaseScore();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb2d.AddTorque(TorqeAmount);
            // Track the total rotation of the object
            IncreaseScore();
        }

        // Check if the player can jump and space bar is pressed
        if (permission && Input.GetKeyDown(KeyCode.Space))
        {
            rb2d.AddForce(jumpForce, ForceMode2D.Impulse);
            permission = false; // Disable jumping

            // Start the cooldown coroutine
            StartCoroutine(CooldownCoroutine());
        }
    }

    // Coroutine to handle the jump cooldown
    IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(Cooldown); // Wait for cooldown

        // Refill boostAmount after cooldown
        boostAmount = 10f;
        permission = true; // Allow boosting again
    }

    // Increments the score and logs it
    void IncreaseScore()
    {
        score++;
        Debug.Log("Score: " + score);
    }

}
