using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Paddle : MonoBehaviour
{
    public enum Controller { NONE, PLAYER1, PLAYER2, AI };
    public enum Direction { NONE, UP, DOWN };
    public Controller paddleController = Controller.NONE;
    public Direction paddleDirection = Direction.NONE;

    public float baseSpeed = 0.3f;

    private float currentSpeedV = 0.0f;
    private Rigidbody2D rigidBody;
    private BallController ballCont;
   
    GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        ball = GameObject.FindGameObjectWithTag("Ball");
        ballCont = ball.GetComponent<BallController>();
    }

    // Update is called once per frame
    private void Update()
    {
        float delta = Time.deltaTime * 1000;
        KeyCode upButton = KeyCode.None;
        KeyCode downButton = KeyCode.None;
        paddleDirection = Direction.NONE;
        switch (paddleController){
            default: break;
            case Controller.PLAYER1:
                upButton = KeyCode.W;
                downButton = KeyCode.S;
                break;
            case Controller.PLAYER2:
                upButton = KeyCode.UpArrow;
                downButton = KeyCode.DownArrow;
                break;
            case Controller.AI:
                float ballY = ball.transform.position.y;
                float paddleY = transform.position.y;
                if (ballCont.collisionBotWaited())
                {
                    if (ballY <= paddleY && ballY >= paddleY)
                    {
                        paddleDirection = Direction.NONE;
                    }
                    else if (ballY < paddleY && ballY - paddleY <= 0.25 && ballY - paddleY <= -0.25)
                    {
                        paddleDirection = Direction.DOWN;
                    }
                    else if (ballY > paddleY && ballY - paddleY >= 0.25 && ballY - paddleY >= -0.25)
                    {
                        paddleDirection = Direction.UP;
                    }

                }
                else
                {
                    paddleDirection = Direction.NONE;

                }
                break;

        }

       
        if(upButton != KeyCode.None && downButton != KeyCode.None){
            if (Input.GetKey(upButton)){
                paddleDirection = Direction.UP;
            } else if (Input.GetKey(downButton)){
                paddleDirection = Direction.DOWN;
            }
        }
    }

    void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime * 1000;
        currentSpeedV = 0;
        if(paddleDirection == Direction.UP) {
            currentSpeedV = baseSpeed;
        }else if (paddleDirection == Direction.DOWN) {
            currentSpeedV = -baseSpeed;
        }
        rigidBody.velocity = new Vector2(0, currentSpeedV * delta);
    }
}
