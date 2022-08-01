using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScripts : MonoBehaviour
{
    public UnityEngine.UI.Button button; 
    public UnityEngine.UI.Text Times, Life,Status;
    private Rigidbody rigidBody;
    float timeCounter = 30;
    int lifeCounter = 3;
    bool gameContinue = true;
    bool gameEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        Life.text = lifeCounter + "";
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameContinue && !gameEnd)
        {
            timeCounter -= Time.deltaTime;
            Times.text = (int)timeCounter + "";

        }
        else if (!gameEnd)
        {
            Status.text = "Game Over";
            button.gameObject.SetActive(true);
        }

        
        if (timeCounter < 0)
        {
            gameContinue = false;
        }
    }
    void FixedUpdate()
    {   if (gameContinue && !gameEnd)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 force = new Vector3(vertical, 0, -horizontal);
            rigidBody.AddForce(force);
        }else
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero; 

        }
        
    }
    void OnCollisionEnter(Collision collision)
    {
        string objName = collision.gameObject.name;
        if (objName.Equals("End"))
        {
            gameEnd = true;
            Status.text = "Game Completed! Congratulations";
            button.gameObject.SetActive(true);
        }
        else if (!objName.Equals("Ground") && !objName.Equals("GameFloor"))
        {
            lifeCounter -= 1;
            Life.text = lifeCounter + "";
            if (lifeCounter == 0)
            {
                gameContinue = false;
            }

        }
    }
}
