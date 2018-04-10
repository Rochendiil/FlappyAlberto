using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    // The Target
    public Transform target;
    public float x = 0;
    void Start()
    {
        // Fly towards the right
        //GetComponent<Rigidbody2D>().velocity = Vector2.right * 2.0f;
        x = 0;
    }


    // Update is called once per frame
    void Update()
    {
        x += 0.06f;
        transform.position = new Vector3(x,
                                         transform.position.y,
                                         transform.position.z);
        
    }

        void LateUpdate()
    {
        transform.position = new Vector3(target.position.x,
                                         transform.position.y,
                                         transform.position.z);
    }
    
}