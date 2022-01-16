using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_dialogue_trigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private tutorial_dialogue_manager tut_diag_manager ;
    [SerializeField] private tutorial_panel_mechanic tut_panel_mech ;
    private List<string> result_dialogue;
    private int counter = 1;
    private int after_counter = 0;
    private int after_counter_max = 0;
    private int max_count = 0;
    private bool cango = false;
    private bool after_choose = false;
    private bool is_correct = false;
    void Start()
    {
        result_dialogue = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) {
            
            if (cango) {
               if (after_choose) {
                   if (is_correct) {
                       //proceed to the next dialogues
                       
                       if (after_counter == after_counter_max) {
                           //end it here
                           after_counter = 0;
                           after_choose = false;
                           is_correct = false;
                           tut_diag_manager.set_active_dialogue_box(false);
                           enable_movement();
                       }
                       else {
                           
                           tut_diag_manager.next_after_dialogue(after_counter);
                           after_counter++;
                       }
                   }
                   else {
                       //last dialogue
                       tut_panel_mech.set_can_freeze(true);
                       tut_diag_manager.next_dialogue(counter - 1);
                       after_choose = false;
                   }
                   
               }
               else {
                   next_dialogue();
               }
                
            }
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            bool is_on = tut_panel_mech.get_ison();
            if (is_on) {
                check_answer();
            }
        }
    }
    void check_answer() {
        tut_panel_mech.change_freeze_panel_color("#FFFFFF");
        tut_panel_mech.set_freeze_panel_obj_active(false);
        tut_panel_mech.change_panel_color("#FFFFFF", false);
        tut_panel_mech.compare_answers();
        int cor_counter = tut_panel_mech.get_correct_counter();
        if (cor_counter == 0) {
            is_correct = true;
        }
        else {
            is_correct = false;
        }
        after_choose = true;
        tut_panel_mech.set_can_freeze(false);
        tut_diag_manager.next_result(cor_counter);
    }
    public void next_dialogue() {
        
        if (max_count > counter) {
            
            counter++;
            if (max_count == counter) {
                tut_panel_mech.set_can_freeze(true);
                tut_panel_mech.set_freeze_panel_obj_active(true);
            }
            int index = counter - 1;
            tut_diag_manager.next_dialogue(index);
        }
       
    }
    public void store_result_diag(List<string> result_diag , string speaker) {
        tut_diag_manager.initialize_result_dialogues(result_diag, speaker);
    }
    public void store_after_diag(List<string> aft_diag, List<string> aft_name) {
        after_counter = 0;
        after_counter_max = aft_diag.Count;
        tut_diag_manager.initialize_after_dialogues(aft_diag, aft_name);
    }
    public void initialize_dialogue(List<string> dialogue, List<string> names) {
        cango = true;
        is_correct = false;
        counter = 1;
        max_count = dialogue.Count ;
        tut_diag_manager.start_dialogue(dialogue, names);

    }
    void trigger_dialogue() {

    }
    void enable_movement() {
        CharacterController2D charsub2d;
        PlayerMovement pmovement ;
        GameObject player_search = GameObject.Find("Player");
        if (player_search != null) {
            charsub2d = player_search.GetComponent<CharacterController2D>();
            pmovement = player_search.GetComponent<PlayerMovement>();
            charsub2d.enabled = true;
            pmovement.enabled = true;
        }
    }
}
