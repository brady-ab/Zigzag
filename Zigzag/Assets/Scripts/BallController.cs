using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public GameObject particle;

    [SerializeField]
    private float speed;
    bool started;
    bool gameOver;
    int diamondCounter;

    Rigidbody rb;

    //On startup, get the Rigidbody of the ball
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    // Start is called before the first frame update
    void Start()
    {
        diamondCounter = 0;
        started = false;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
      
        if (!started)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.velocity = new Vector3(speed, 0, 0);
                started = true;
            }
        }

        //VIEW RAYCAST Debug.DrawRay(transform.position, Vector3.down, Color.red);

        if(!Physics.Raycast(transform.position, Vector3.down, 1f))
        {
            gameOver = true;
            rb.velocity = new Vector3(0, -12f, 0);
            Camera.main.GetComponent<cameraFollow>().gameOver = true;
        }

        //use switchDirection on MouseDown when gameOver boolean is not true.
        if (Input.GetMouseButtonDown(0) && !gameOver)
        {
            switchDirection();
        }
    }

    //Function to switch from x to z direction and back.
    void switchDirection()
    {
        if(rb.velocity.z > 0)
        {
            rb.velocity = new Vector3(speed, 0, 0);
        }
        else if(rb.velocity.x > 0)
        {
            rb.velocity = new Vector3(0, 0, speed);
        }
    }

    //Check for diamonds collided with. Play particle effect, increase ball speed every 5 diamonds.
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Diamond")
        {
            
            GameObject part = Instantiate(particle, col.gameObject.transform.position, Quaternion.identity) as GameObject;

            diamondCounter++;

            if (diamondCounter % 5 == 0)
            {
                speed = speed + 1;
                if (rb.velocity.z > 0)
                {
                    rb.velocity = new Vector3(0, 0, speed);
                }
                else if (rb.velocity.x > 0)
                {
                    rb.velocity = new Vector3(speed, 0, 0);
                }
            }


            Destroy(col.gameObject);
            Destroy(part, 3f);
        }
    }
}
