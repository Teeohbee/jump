using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver;
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip deathSound;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                if (isOnGround && !gameOver) 
                {
                    PerformJump();
                }            
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver) {
            PerformJump();
        }
    }

    private void PerformJump() {
        playerAudio.PlayOneShot(jumpSound, 1.0f);
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isOnGround = true;
        } else if (collision.gameObject.CompareTag("Rope")) {
            gameOver = true;
            playerAudio.PlayOneShot(deathSound, 1.0f);
            Debug.Log("Game Over!");
        }
    }
}
