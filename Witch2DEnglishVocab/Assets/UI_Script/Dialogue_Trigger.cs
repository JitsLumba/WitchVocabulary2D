using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject dialogue_panel, invisi_button;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private Dialogue_Manager dialogue_Manager;
    [SerializeField] private tutorial_image_show tut_img_show ;

    [SerializeField] private Panel_Mechanic pmech;
    
    private int stop_at = 0;
    private string play_name = "Elaina";
    bool canproc = false;
    bool is_active = false;
    
    bool cantrigger = false;
    bool canfreeze = false;
    bool is_on_choice = false;
    bool can_browse = false;
    bool is_bef_choice = false;
    bool after_result = false;
    bool is_correct = false;
    int counter = 0;
    int multiple = 0, button_select = 0;
    void Start()
    {
        dialogue.start_list();
    }

    // Update is called once per frame
    void Update()
    {

        canfreeze = pmech.get_can_freeze();

        if (Input.GetKeyDown(KeyCode.G) && cantrigger)
        {

            Debug.Log("canproc " + canproc);
            Trigger_Dialogue();



        }
        if (Input.GetKeyDown(KeyCode.X) && can_browse && is_active) {
            
            bool is_showing = tut_img_show.return_is_showing_image();
            bool result = false;
            Debug.Log("X goes here " + is_showing + " canfreeze " + canfreeze);
            if (is_showing) {
                result = true;
                cantrigger = true;
            }
            else {
                cantrigger = false;
            }
            set_freeze(result);
            Debug.Log("FREEZE " + canfreeze);
            tut_img_show.show_img_sequence();
            pmech.set_can_move(is_showing);
            pmech.dialogue_show();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && can_browse && is_active) {
            bool is_showing = tut_img_show.return_is_showing_image();
            if (is_showing) {
                tut_img_show.exit_images();
                canfreeze = true;
                pmech.set_can_move(true);
            }
            
            pmech.dialogue_show();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("canfreezeZZZ " + canfreeze);
            
            freeze_command();
            Debug.Log("cantrigger2 " + cantrigger);
            

        }
    }
    public void freeze_command() {
        Debug.Log("FREEZERICE " + canfreeze);
        if (cantrigger) {
            if (canfreeze)
            {

                if (canproc)
                {
                    

                    canproc = false;
                    Debug.Log("STOP ");
                }
                else
                {
                    canproc = true;
                }
                cantrigger = false;
                can_browse = false;
                StartCoroutine(Freeze_Interv());
            }
        }
        
           
    }
    public void set_canproc(bool proc) {
        canproc = proc;
    }
    public string get_play_name() {
        return play_name;
    }
    public void choice_trigger(List<string> clues) {
        
        dialogue_Manager.indicate_context(clues, play_name);
        after_result = false;
        is_bef_choice = true;
    }
    public void set_freeze(bool freeze)
    {
        this.canfreeze = freeze;
        pmech.set_freeze(freeze);
    }
    public void set_is_on_choice(bool choice) {
        is_on_choice = choice;
    }
    public void Trigger_Dialogue()
    {

     
        if (canproc)
        {
            if (is_on_choice)
            {
            
            }
            else if (is_bef_choice) {
                //indicating context clues
                is_on_choice = true;
                is_bef_choice = false;
                dialogue_Manager.set_active_dialogue(false, true);
            }
            else
            {
                counter++;
                if (after_result)
                {
                    if (counter == multiple) {
                        //show result
                        bool can_return = false;
                        after_result = false;
                        cantrigger = false;
                        canproc = false;
                        counter = 0;
                        if (is_correct) {
                            
                            can_return = true;
                            dialogue_panel.SetActive(false);
                            is_correct = false;
                        }
                        
                        Debug.Log("END OF SENTENCE");
                        tut_img_show.set_can_browse(false);
                        can_browse = false;
                        is_active = false;
                        StartCoroutine(Dialogue_Interv(can_return));
                        dialogue_Manager.show_result(dialogue.remark_list[button_select], can_return);

                    }
                    else {
                        Debug.Log("AFTER RESULT " + counter);
                        this.dialogue_Manager.Display_Next_Sentence();
                    }
                }
                else 
                {
                    if (counter != stop_at)
                    {
                        //GOES TO THE NEXT SENTENCE
                        this.dialogue_Manager.Display_Next_Sentence();
                    }
                    else
                    {
                      //this is when it has reached the end of the last dialogue
                        //is_on_choice = true;
                        counter = 0;
                        dialogue_Manager.StartDialogue(dialogue);
                        //dialogue_Manager.set_active_dialogue(false, true);
                    }
                }

            }

        }



        //StartCoroutine(Dialogue_Interv());

    }
    public void clues_add(List<string> clues, List<string> clue_type) {
        int num = clues.Count;
        pmech.set_clue_number(num);
        pmech.set_clue_panel_active(true);
        pmech.set_highlighter_panel_active(true);
        dialogue.add_clues(clues, clue_type);
    }
    public void change_dial_vals(List<string> sentences, List<string> names, List<string> choices, List<string> results, List<string> remarks, List<string> name_res, int mult)
    {
        is_active = true;
        can_browse = true;
        tut_img_show.set_is_not_on_dialogue(false);
        set_freeze(true);
        pmech.set_has_all_clues(false);
        this.canproc = true;
        this.cantrigger = true;
        this.multiple = mult;
        
        dialogue.clear_sentences();
        dialogue.add_remarks(remarks);
        dialogue.add_dialogues(sentences, names);
        dialogue.add_result_dialogues(results, name_res);
        dialogue_panel.SetActive(true);
        invisi_button.SetActive(true);
        dialogue_Manager.set_choices(choices);
        dialogue_Manager.set_mode(1);
        Debug.Log("CLEAR");
        this.dialogue_Manager.StartDialogue(dialogue);

    }
    
    public void set_trigger()
    {
        this.cantrigger = true;
    }
    public void set_stop_at(int stopper)
    {
        this.stop_at = stopper;
    }
    
    public void can_trigger_again(int num_button)
    {
        is_on_choice = false;
        is_bef_choice = false;
        after_result = true;
        button_select = num_button;
        string remark = dialogue.get_remark(num_button);
        counter = 0;
        if (remark.Equals("Correct")) {
            is_correct = true;
        }
        dialogue_Manager.set_start_end(multiple * num_button, multiple * num_button + multiple);
        dialogue_Manager.set_mode(2);
        dialogue_Manager.set_active_dialogue(true, false);
        dialogue_Manager.StartDialogue(dialogue);
        
        
    }
    IEnumerator Dialogue_Interv(bool can_return)
    {
        yield return new WaitForSeconds(1.0f);
        can_browse = true;
        tut_img_show.set_can_browse(true);
        canproc = true;
        is_active = true;
        if (!can_return) {
            List<string> clue_list = new List<string>();
            int clue_num = dialogue.get_clue_num();
            for (int i = 0; i < clue_num; i++) {
                string clue_word = dialogue.get_clues(i);
                clue_list.Add(clue_word);
            }
            choice_trigger(clue_list);
        }
        else {
            is_active = false;
            tut_img_show.set_is_not_on_dialogue(true);
        }
    }
    IEnumerator Freeze_Interv()
    {
        yield return new WaitForSeconds(1.0f);
        cantrigger = true;
        can_browse = true;
        Debug.Log("Canproc " + canproc);
    }
}
