using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_distance_trigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CharacterController2D controller_character ;
    [SerializeField] private PlayerMovement play_move ;
    [SerializeField] private GameObject player, tutor_talker ;
    [SerializeField] private tutorial_script tut_script;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float player_x_dist = player.transform.position.x, tutor_x_dist = tutor_talker.transform.position.x;
        float diff = player_x_dist - tutor_x_dist;
        if (diff >= -1.0f && diff <= 1.0f && Input.GetKeyDown(KeyCode.F)) {
            disable_movement();
            tut_script.send_scripts();
            
        }
    }
    void disable_movement() {
        play_move.enabled = false;
        controller_character.enabled = false;
    }
}
