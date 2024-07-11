using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_1_Initialize : MonoBehaviour
{
    // Start is called before the first frame update
    
    //variable to initialize game
    [SerializeField] private tutorial_distance_trigger tut_dis_trigger;
    void Start()
    {
        start_game_sequence();
    }
    void start_game_sequence() {
        tut_dis_trigger.start_sequence();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
