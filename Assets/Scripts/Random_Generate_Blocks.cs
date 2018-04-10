using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Random_Generate_Blocks : MonoBehaviour {
    public GameObject Muur;
    public GameObject Camera;
    public GameObject RekenPunt;
    public Text scoreText;
    private int spawn_position = 13;
    List<GameObject> MuurList;
	// Use this for initialization
	void Start () {
        MuurList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = Convert.ToString((int)Camera.transform.position.x);
        if (Camera.transform.position.x + 17 >= spawn_position)
        {
            //Vector3 position, Quaternion rotation);
            MuurList.Add(Instantiate<GameObject>(Muur, new Vector3(spawn_position, UnityEngine.Random.Range(-5, 1),0), new Quaternion(0,0,0,0)));
            spawn_position += 6;
            for(int i = 0; i < MuurList.Count; i++)
            {
                if (MuurList[i] != null && ((MuurList[i].transform.position.x >= Camera.transform.position.x && MuurList[i].transform.position.x < Camera.transform.position.x + 6) || (MuurList[i].transform.position.x >= Camera.transform.position.x && MuurList[i].transform.position.x == 13)))
                {
                    RekenPunt.transform.Translate(MuurList[i].transform.position.x + (float)0.7 - RekenPunt.transform.position.x, MuurList[i].transform.position.y + (float)5 - RekenPunt.transform.position.y, 0);
                  
                }
                Console.WriteLine(MuurList);
                if (MuurList[i] != null && (MuurList[i].transform.position.x <= Camera.transform.position.x - 15))
                {
                    Destroy(MuurList[i]);
                    MuurList.Remove(MuurList[i]);
                    
                    Console.WriteLine("destroyed object");
                }
                
                
            }


        }
        //scoreText.text = "Score: " + (int)Camera.transform.position.x;
    }
    public void clearMuren()
    {
       
        for (int i = MuurList.Count - 1; i >= 0; i--)
        {
            if (MuurList[i] != null)
            {
               
                Destroy(MuurList[i]);
                MuurList.RemoveAt(i);
            }
           
        }
        
        spawn_position = 13;
    }
}
