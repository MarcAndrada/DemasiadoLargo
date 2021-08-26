using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{

    public GameObject player1Points;
    public GameObject player2Points;
    private int player1Score = 0;
    private int player2Score = 0;

    public float maxSpeed = 2.0f;
    public float baseSpeed = 0.4f;
    public float margin = 0.3f;
    public AudioClip hitSound;

    private float currentSpeedH = 0.0f;
    private float currentSpeedV = 0.0f;
    private float speedAug = 0.1f;
    private Vector3 starterPos;
    private float TimeWaited = 0;

    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        starterPos = transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
        float rand = Random.Range(0.0f, 1.0f);
        if (rand < 0.5)
        {
            currentSpeedH = -baseSpeed;
        }
        else
        {
            currentSpeedH = baseSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        TimeWaited += Time.deltaTime * 1000;
    }

    private void FixedUpdate()
    {//Fisica de Unity
        float delta = Time.fixedDeltaTime * 1000;
        rigidBody.velocity = new Vector2(currentSpeedH * delta, currentSpeedV * delta);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Paddle")
        {
            TimeWaited = 0;
            currentSpeedH = -1 * currentSpeedH;
            float ballY = transform.position.y;
            float paddleY = collision.collider.bounds.center.y;
            float paddleH = collision.collider.bounds.size.y;
            if (ballY < paddleY - (paddleH / 2) * margin)
            {
                currentSpeedV = -baseSpeed;
            }
            else if (ballY > paddleY + (paddleH / 2) * margin)
            {
                currentSpeedV = baseSpeed;
            }
            else
            {
                currentSpeedV = 0f;
            }

            if (currentSpeedH < maxSpeed && currentSpeedH > 0){
                currentSpeedH = currentSpeedH + speedAug;

            }else if (currentSpeedH >= maxSpeed && currentSpeedH > 0)
            {
                currentSpeedH = maxSpeed;

            }

            if (currentSpeedH > -maxSpeed && currentSpeedH < 0)
            {
                currentSpeedH = currentSpeedH - speedAug;

            }
    


        }
        else if (collision.gameObject.tag == "Wall")
        {
            currentSpeedV = -1 * currentSpeedV;
        }
        else if (collision.gameObject.tag == "Goal")
        {
            currentSpeedH = currentSpeedH * (-baseSpeed);
            if (currentSpeedH < baseSpeed && currentSpeedH > 0)
            {
                currentSpeedH = -baseSpeed;
            }
            else{
                currentSpeedH = baseSpeed;
            }
            currentSpeedV = 0;
            transform.position = starterPos;

            float goalX = collision.transform.position.x;
            

            if (goalX < 0) {
                player2Score++;
                player2Points.GetComponent<Text>().text = player2Score.ToString();
                
            }
            else {
                player1Score++;
                player1Points.GetComponent<Text>().text = player1Score.ToString();
                
            }
            
        }
    }



    public bool collisionBotWaited()
    {
        float TimeToWait = 450;

        
        if (TimeToWait <=TimeWaited)
        {
            return true;
        }else{     
            return false;
        }

         
    }
}
