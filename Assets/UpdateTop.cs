using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTop : MonoBehaviour {

    public GameObject Camera;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector2(Camera.transform.position.x, transform.position.y);
	}
}
