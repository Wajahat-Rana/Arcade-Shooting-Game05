using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject rocket;

    [SerializeField]
    private AudioClip shootSound;

    float speed = 8f;
    float maxVelocity = 5f;
    private Rigidbody2D myBody;
    private Animator anim;

    private bool canWalk;
    private bool canShoot;
    void Awake()
    {
       initializeVariables();
    }
    void initializeVariables(){
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canWalk = true;
        canShoot = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Update()
    {
        
    }
      void FixedUpdate()
    {
        playerWalk();
    }

    void playerWalk(){
        var force = 0f;
        var velocity = Mathf.Abs(myBody.velocity.x);
        float h = Input.GetAxis("Horizontal");

        if(h > 0){
            if(velocity < maxVelocity){
            force = speed;
            }
        
            Vector3 temp = transform.localScale;
            temp.x = 1;
            transform.localScale = temp;
            anim.SetBool("Walk", true);
        }else if(h < 0){
           if(velocity < maxVelocity){
            force = -speed;
           }
        
            Vector3 temp = transform.localScale;
            temp.x = -1;
            transform.localScale = temp;
            anim.SetBool("Walk", true);
        }
        else if(h==0){
            anim.SetBool("Walk", false);
        }

        myBody.AddForce(new Vector2(force, 0));
    }
}
