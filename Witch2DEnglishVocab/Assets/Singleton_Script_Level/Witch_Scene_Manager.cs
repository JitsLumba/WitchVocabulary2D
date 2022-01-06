using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Witch_Scene_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string next_level = "";
    [SerializeField] private bool can_go = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void change_scene() {
        if (can_go) {
            SceneManager.LoadScene(next_level);
        }
        
    }
}
