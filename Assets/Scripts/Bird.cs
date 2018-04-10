using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour
{
    // Movement speed
    public float speed = 2.0f;

    public AudioSource FlyingSound;
    public GameObject Camera;
    public AudioSource DyingSound;

    public AudioSource StartingSound;
    public bool dead = false;
    
    private int restart = 0;
    private int fitness;

    private int NoConstantSound = 0;
    // Flap force
    public float force = 5;

    // Use this for initialization
    void Start()
    {
        // Fly towards the right
       // GetComponent<Rigidbody2D>().velocity = Vector2.right * 2;
        StartingSound.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Camera.transform.position.x, transform.position.y);
        // Flap
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
        else
        {
            NoConstantSound = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // Restart
        restart = 1;
        if (!dead)
        {
            fitness = (int)this.transform.position.x;
        }
        dead = true;
        DyingSound.Play(0);
        


    }

 
    public int getFitness()
    {
        return fitness;
    }

    public void jump()
    {
        GetComponent<Rigidbody2D>().velocity = (Vector2.down * 0) ;
        GetComponent<Rigidbody2D>().velocity = (Vector2.up * 0);
        
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);

        if (NoConstantSound == 0)
        {
            FlyingSound.Play(0);
            NoConstantSound = 1;
        }
    }
}
