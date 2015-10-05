//Nabil spawner
//this is just a test
using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {

    //this script is attached to an empty GameObject called TheSpawner
    // if you click on TheSpawner in the Hierarchy column you will see this script as 
    // a component of TheSpawner in the Innspector column

    // created an array of GameObjects , since this array is public
    //you can see it in the  inspector
    // I dragged my tetrino prefabs in the array and we now have  ref to each one of the 
    public GameObject[] golist = new GameObject[6];

    // Use this for initialization
    void Start () {
        

    }

    private float nextActionTime = 0.0f;
    public float period = 3f; //every 3 seconds
    private int index = 0;    
    private int tetrinoCount = 0;
    void FixedUpdate()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;


            tetrinoCount++;
            if (tetrinoCount < 12)
            {
                index = tetrinoCount % 5;
                GameObject go = golist[index];
                Instantiate(go, transform.position, transform.rotation); //this will instanciate a tetrino at the location and rotation of the object on which this script is attached (TheSpawner)
            }


        }
    }
}
