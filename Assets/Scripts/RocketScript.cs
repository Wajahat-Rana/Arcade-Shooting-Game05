using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    float speed = 5f;
    private Rigidbody2D myBody;
    // Start is called before the first frame update
    void Awake(){
        myBody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        myBody.velocity = new Vector2(0, speed);   
    }
     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Rocket Destroyer")
        {
            Destroy(gameObject);
        }
        string[] objectName = other.tag.Split();
        if(objectName.Length > 1){
        if(objectName[1] == "Ball"){
            Destroy(gameObject);
        }
        }
    }
}
