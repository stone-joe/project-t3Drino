using UnityEngine;
using System.Collections;

public class ExplosionSelfDestruct : MonoBehaviour {




    ParticleSystem ps;
    float duration;
	// Use this for initialization
	void Start () {
        ps = this.transform.GetComponent<ParticleSystem>();
        duration = ps.duration;
        StartCoroutine(KillDelay());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private IEnumerator KillDelay()
    {
        yield return new WaitForSeconds(duration);
        Destroy(this.gameObject);
    }
   


}
