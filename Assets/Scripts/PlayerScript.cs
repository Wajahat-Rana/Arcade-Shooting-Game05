using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
        shoot();
    }
      void FixedUpdate()
    {
        playerWalk();
    }

    void playerWalk(){
        var force = 0f;
        var velocity = Mathf.Abs(myBody.velocity.x);
        float h = Input.GetAxis("Horizontal");

       if(canWalk){
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
       }

        myBody.AddForce(new Vector2(force, 0));
    }

    IEnumerator playerShoot(){
        // canWalk = false;
        anim.Play("Shoot");
        Vector3 temp = transform.position;
        temp.y += 1f;
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
        Instantiate(rocket, temp, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Shoot",false);
        canWalk = true;
        yield return new WaitForSeconds(0.3f);
        canShoot = true;

    }
    void shoot(){
        if(Input.GetMouseButtonDown(0)){
        if(canShoot){
            canShoot = false;
            StartCoroutine(playerShoot());
        }
        }
    }

    IEnumerator KillPlayerAndRestartGame(){
        transform.position = new Vector3(200,200,0);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex);
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        string[] ballName = other.tag.Split();
        if(ballName.Length > 1)
        {
            if(ballName[1] == "Ball")
            {
                StartCoroutine(KillPlayerAndRestartGame());
            }
        }
    }
}
