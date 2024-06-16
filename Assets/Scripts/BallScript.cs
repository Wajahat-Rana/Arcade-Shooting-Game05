using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private float forceX, forceY;
    private Rigidbody2D myBody;

    [SerializeField]
    private bool moveLeft,moveRight;

    [SerializeField]
    private GameObject nextBall;

    private GameObject ball1, ball2;


    private BallScript ball1Script, ball2Script;

    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        setBallSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        moveBall();
    }

    void setBallSpeed(){
        forceX = 2.5f;
        switch (this.gameObject.tag)
        {
            case "Largest Ball":
            forceY = 11.5f;
            break;
            case "Large Ball":
            forceY = 10.5f;
            break;
            case "Medium Ball":
            forceY = 9f;
            break;
            case "Small Ball":
            forceY = 8f;
            break;
            case "Smallest Ball":
            forceY = 7f;
            break;
        }
    }
    void moveBall(){
        if(moveLeft){
            Vector3 temp = transform.position;
            temp.x -= forceX*Time.deltaTime;
            transform.position = temp;
        }
        if(moveRight){
            Vector3 temp = transform.position;
            temp.x += forceX*Time.deltaTime;;
            transform.position = temp;

        }
    }

    void instantiateBalls(){
        if(gameObject.tag != "Smallest ball"){
        ball1 = Instantiate(nextBall);
        ball2 = Instantiate(nextBall);

        ball1.name = nextBall.name;
        ball2.name = nextBall.name;

        ball1Script = ball1.GetComponent<BallScript>();

        ball2Script = ball2.GetComponent<BallScript>();
        }

    }
    void instantiateNextBallAndDestroyExisting(){
        instantiateBalls();

        Vector3 temp = transform.position;

        ball1.transform.position = temp;
        ball1Script.SetMoveLeft(true);
        ball2.transform.position = temp;
        ball2Script.SetMoveRight(true);

        ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0,2.5f);
        ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0,2.5f);
        gameObject.SetActive(false);
    }
    void SetMoveLeft(bool canMoveLeft){
        this.moveLeft = canMoveLeft;
        this.moveRight = !canMoveLeft;
    }
    
    void SetMoveRight(bool canMoveRight){
        this.moveRight = canMoveRight;
        this.moveLeft = !canMoveRight;
    }
     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Ground"){
            myBody.velocity = new Vector2(0,forceY);
        }
        if(other.tag == "Left Wall"){
            SetMoveRight(true);
        }
        
        if(other.tag == "Right Wall"){
            SetMoveLeft(true);
        }
        
        if(other.tag == "Rocket"){
            if(gameObject.tag != "Smallest Ball"){
            instantiateNextBallAndDestroyExisting();
            }
            else{
                gameObject.SetActive(false);
            }
        }
    }
}
