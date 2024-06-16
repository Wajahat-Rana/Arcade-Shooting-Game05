using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private float forceX, forceY;
    private Rigidbody2D myBody;

    [SerializeField]
    private bool moveLeft,moveRight;

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
     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Ground"){
            myBody.velocity = new Vector2(0,forceY);
        }
        string[] objectName = other.tag.Split();
        if(objectName.Length>=1){
        if(objectName[0] == "Left"){
            moveLeft = false;
            moveRight = true;
        }
        if(objectName[0] == "Right"){
            moveLeft = true;
            moveRight = false;
        }
        }
    }
}
