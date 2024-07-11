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
    [SerializeField] private tutorial_adaptor tut_adapt ;
    [SerializeField] private bool has_tutorial = false;
    private bool can_interact = true;
    
    void Start()
    {
        if (has_tutorial) {
            //start_sequence();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void set_can_interact(bool interact) {
        can_interact = interact;

        if (interact) {
            bool negate_interact = !interact;
            tut_adapt.switch_empty_tutorials(negate_interact);
        }
    }
    public void start_sequence() {
        //sequence to start the game by disabling character movement
        set_can_interact(false);
        disable_movement();
        tut_script.send_scripts();
    }
    public void activate_game_level_tut(bool activate) {

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
