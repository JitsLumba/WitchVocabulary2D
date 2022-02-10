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
    [SerializeField] private bool has_tutorial = false;
    private bool can_interact = true;
    
    void Start()
    {
        if (has_tutorial) {
            start_sequence();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float player_x_dist = player.transform.position.x, tutor_x_dist = tutor_talker.transform.position.x;
        float diff = player_x_dist - tutor_x_dist;
        if (diff >= -1.0f && diff <= 1.0f && Input.GetKeyDown(KeyCode.F) && can_interact) {
            
            start_sequence();
        }
    }
    public void set_can_interact(bool interact) {
        can_interact = interact;
    }
    void start_sequence() {
        set_can_interact(false);
        disable_movement();
        tut_script.send_scripts();
    }
    void disable_movement() {
        CharacterController2D charsub2d;
        PlayerMovement pmovement ;
        GameObject player_search = GameObject.Find("Player");
        if (player_search != null) {
            
            charsub2d = player_search.GetComponent<CharacterController2D>();
            pmovement = player_search.GetComponent<PlayerMovement>();
            charsub2d.enabled = false;
            pmovement.enabled = false;
        }
        /*play_move.enabled = false;
        controller_character.enabled = false;*/
    }
}
