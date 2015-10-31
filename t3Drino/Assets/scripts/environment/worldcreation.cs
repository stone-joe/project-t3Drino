using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class worldcreation : MonoBehaviour {

    private Object[] _prefabs;
    public GameObject cubi1;
    private int coef = 10;
    private int numberOfcubes ;

    public  List<GameObject> listofblocks = new   List<GameObject>();

    void BuildBottomTop(int zpos)
    {
    
        //floor
        for (int i = 0; i <= numberOfcubes; i++)
        {
            Vector3 where = new Vector3(-40 + i* coef  , -12f, zpos * coef);
            GameObject g = Instantiate(cubi1, where, Quaternion.identity) as GameObject;
            g.transform.parent = this.transform;
            g.transform.localScale = new Vector3(coef, coef, coef);
            g.AddComponent<wave>();
            g.GetComponent<wave>()._motion = 0;
            listofblocks.Add(g);

        }
      
        

        //ceiling
        for (int i = 0; i <= numberOfcubes; i++)
        {
            Vector3 where = new Vector3(-40 + i * coef, 60f, zpos * coef);
            GameObject g = Instantiate(cubi1, where, Quaternion.identity) as GameObject;
            g.transform.parent = this.transform;
            g.transform.localScale = new Vector3(coef, coef, coef);
            g.AddComponent<wave>();
            g.GetComponent<wave>()._motion = 0;
            listofblocks.Add(g);
        }



    }

    void BuildLeftRight(int zpos)
    {

        //leftwall
        for (int i = 0; i <= numberOfcubes; i++)
        {
            Vector3 where = new Vector3(-30,  -26 + i * coef, zpos * coef);
            GameObject g = Instantiate(cubi1, where, Quaternion.identity) as GameObject;
            g.transform.parent = this.transform;
            g.transform.localScale = new Vector3(coef, coef, coef);
             g.AddComponent<wave>();
             g.GetComponent<wave>()._motion = 1;
             listofblocks.Add(g);
        }


    
        //ceiling
        for (int i = 0; i <= numberOfcubes; i++)
        {
            Vector3 where = new Vector3(70, -26 + i * coef, zpos * coef);
            GameObject g = Instantiate(cubi1, where, Quaternion.identity) as GameObject;
            g.transform.parent = this.transform;
            g.transform.localScale = new Vector3(coef, coef, coef);
            g.AddComponent<wave>();
            g.GetComponent<wave>()._motion = 1;
            listofblocks.Add(g);
        }

      

    }

    void BuildFrontBAck(int zpos)
    {

        //front
        for (int i = 0; i <= numberOfcubes; i++)
        {
            Vector3 where = new Vector3(-40 + i * coef,  zpos * coef, 100);
            GameObject g = Instantiate(cubi1, where, Quaternion.identity) as GameObject;
            g.transform.parent = this.transform;
            g.transform.localScale = new Vector3(coef, coef, coef);
            g.AddComponent<wave>();
            g.GetComponent<wave>()._motion = 2;   
            listofblocks.Add(g);
        }


       
        //back
        for (int i = 0; i <= numberOfcubes; i++)
        {
             Vector3 where = new Vector3(-40 + i * coef,  zpos * coef, -80);
            GameObject g = Instantiate(cubi1, where, Quaternion.identity) as GameObject;
            g.transform.parent = this.transform;
            g.transform.localScale = new Vector3(coef, coef, coef);
            g.AddComponent<wave>();
            g.GetComponent<wave>()._motion = 2;
            listofblocks.Add(g);

        }

       

    }


    void Start()
    {
        numberOfcubes = 80 / coef; // from -40 to 40 on the x axis  , if the cubes are coef=1 we want 80 cubes, if coef is 2 we wat 80/2 
        for (int zz = -10; zz <= numberOfcubes; zz++) {
               BuildBottomTop(zz);
             BuildLeftRight(zz);
            BuildFrontBAck(zz);
        }
           
    }

    float every = 1f;
    int cnt = 0;
    void Update() {

        if (every < 0) { 
            every = 1f;

            int sizeoflist = listofblocks.Count;
            int howmanytodelete = Random.RandomRange(1, 50);
            print(howmanytodelete + " deletye ");

            int indextodelete;

            if (sizeoflist > 0)
            {
                for (int i = 0; i < howmanytodelete; i++)
                {

                    indextodelete = Random.RandomRange(0, sizeoflist - 1);
                    Destroy(listofblocks[indextodelete]);
                    listofblocks.RemoveAt(indextodelete);
                }
            }
             
           
        }
        //print(every + " seconds ");
        every = every - Time.deltaTime;
    
    }



}