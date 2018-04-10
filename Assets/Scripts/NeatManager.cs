using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class NeatManager : MonoBehaviour
{


    private List<Bird> birdList;
    private List<Network> netList;
    private int timer = 39;
    private bool[] count;
    private bool[] checkcount;
    private int birdsAlive = 10;
    public Camera camera;
    public Random_Generate_Blocks random_generated_blocks;
    // Use this for initialization
    void Start()
    {
        checkcount = new bool[10];
        count = new bool[10];
        for (int i = 0; i < count.Length; i++)
        {
            count[i] = true;
            checkcount[i] = false;
        }
        
        birdList = new List<Bird>();
        netList = new List<Network>();
        birdList.Add(GameObject.Find("bird_0").GetComponent<Bird>());
        for (int i = 0; i < 9; i++)
        {
            birdList.Add(Instantiate(GameObject.Find("bird_0").GetComponent<Bird>()));
        }
        for (int i = 0; i < birdList.Count; i++)
        {
            netList.Add(new Network(new int[4] { 2, 6,6, 1 }));
        }




    }
    void SendInput()
    {

        timer++;
        if (timer > 1)
        {


            for (int i = 0; i < netList.Count; i++)
            {   
                if (!birdList[i].dead)
                {

                    GameObject punt = GameObject.Find("Rekenpunt");
                    double x = Math.Tanh(punt.transform.position.x - birdList[i].transform.position.x);
                    double y = Math.Tanh(punt.transform.position.y - birdList[i].transform.position.y);
                    double[] output = netList[i].feedforward(new double[2] { x, y });
                    if (output[0] >= 0.5f)
                    {
                        birdList[i].jump();
                    }
                }
                else if(count[i])
                { 
                    birdsAlive--;
                    count[i] = false;
                }

            }
            timer = 0;

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (birdsAlive != 0)
        {
            SendInput();
        }
        else
        {
            Random_Generate_Blocks blocks = (GameObject.Find("GameLogic").GetComponent<Random_Generate_Blocks>());
            blocks.clearMuren();
            for (int i = 0; i < birdList.Count; i++)
            {
                netList[i].fitness = birdList[i].getFitness();
                birdList[i].transform.position = new Vector2(0, 0);
                birdList[i].GetComponent<Rigidbody2D>().velocity = Vector2.right * birdList[i].speed;
            }

            int sum = 0;
            netList.Sort();
            foreach (Network item in netList)
            {
                Debug.Log(item.fitness);
                sum += item.fitness;
            }
            int ran = UnityEngine.Random.Range(0, sum);
            int partialsum = 0;
            Network parent;
            foreach (Network item in netList)
            {
                partialsum += item.fitness;
                if(partialsum >= ran)
                {
                    parent = (Network)item.Clone();

                        
                    netList.RemoveAt(9);
                    parent.mutateWeights();
                    netList.Add(parent);
                    break;
                }

            }

            birdsAlive = 10;
            camera.transform.position = new Vector3(0,0, -10);
            CameraFollow camtest = (GameObject.Find("Main Camera").GetComponent<CameraFollow>());
            camtest.x = 0;
            for (int i = 0; i < 10; i++)
            {
                birdList[i].dead = false;
                count[i] = true;
            }
            //TODO netlist parents bepalen en children mutaten
        }


    }

}