using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public static Witch_Manager Instance { get ; private set ;}

    //public data
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    public void sample() {
        
    }
}
