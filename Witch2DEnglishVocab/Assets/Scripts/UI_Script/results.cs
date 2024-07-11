using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class results : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] private List<string> sentences;
    int counter = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void return_diag() {
        counter++;
        Debug.Log(counter);
    }
}
