using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class question_next_level : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Witch_Scene_Manager witch_scene_manager ;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void next_level_notify() {
        witch_scene_manager.change_scene();
    }
}
