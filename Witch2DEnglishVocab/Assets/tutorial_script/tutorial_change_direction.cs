using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tutorial_change_direction : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text direction_text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void change_dir_text(string dir_text) {
        direction_text.text = dir_text;
    } 
}
