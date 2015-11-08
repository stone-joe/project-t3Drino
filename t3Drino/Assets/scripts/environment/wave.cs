using UnityEngine;
using System.Collections;


public enum MotionType { FLOOR, SIDE, FRONT};

public class wave : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        y = transform.position.y;
        x = transform.position.x ; /// 2;
        z = transform.position.z ;/// 2;
                                  /// 

    }



    

    public MotionType _motion;
    float amplitudeY = 5.0f;
    float omegaY = 1.0f;
    float index;
    float x;
    float z;
    float y;
    public void Update()
    {

        if (_motion == MotionType.FLOOR)  floorwave();
        else
            if (_motion == MotionType.SIDE) sidewallwave();
        else
                if (_motion == MotionType.FRONT) frontwallwave();
    }

    public void floorwave()
    {
        index += Time.deltaTime;
        float Y = (amplitudeY * Mathf.Sin(omegaY * index + (x + z) / 5)) - 20;
        transform.localPosition = new Vector3(x, Y+y, z);
    }

    public void sidewallwave()
    {
        index += Time.deltaTime;
        float X = (amplitudeY * Mathf.Sin(omegaY * index + (y + z) / 5)) - 20;
        transform.localPosition = new Vector3(x+X, y, z);
    }


    public void frontwallwave()
    {
        index += Time.deltaTime;
        float Z = (amplitudeY * Mathf.Sin(omegaY * index + (y + x) / 5)) - 20;
        transform.localPosition = new Vector3(x , y, z+Z);
    }
}
