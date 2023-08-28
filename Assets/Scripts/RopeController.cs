using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{

    private int rotationCount = 0;
    public float rotatedAroundX;
    public Vector3 lastUp;
    public float speed = 100.0f;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        rotatedAroundX = 0;
        lastUp = transform.up;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false) {
            transform.Rotate(new Vector3(-speed, 0, 0) * Time.deltaTime);
            CountRotations();
        }
    }

    private void CountRotations()
    {
        var rotationDifference = Vector3.SignedAngle(transform.up, lastUp, transform.right);

        rotatedAroundX += rotationDifference;

        lastUp = transform.up;

        if (rotatedAroundX >= 360.0f)
        {
            rotationCount++;
            rotatedAroundX -= 360.0f;

            if (rotationCount % 2 == 0) {
                speed *= 1.1f;
            }
        }
    }
}
