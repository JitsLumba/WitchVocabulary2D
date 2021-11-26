using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interval_door : MonoBehaviour
{
    // Start is called before the first frame update
    bool interv = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setFalseInterv() {
        interv = false;
        StartCoroutine(Doorintervene());
    }
    public bool getInterv() {
        return interv;
    }
    IEnumerator Doorintervene() {
        yield return new WaitForSeconds(0.25f);
        Debug.Log("Boing");
        interv = true;
    }
}
