using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject dialogue_panel, invisi_button, g_text, traverse_text ;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private Dialogue_Manager dialogue_Manager;
    [SerializeField] private sublevel_backend slevel_backend;
    
    [SerializeField] private Panel_Mechanic pmech;
    private bool can_traverse = false;
    private int stop_at = 0;
    private bool can_z = true, can_f = true;
    private string play_name = "Elaina";
    private string direction = "forward";
    private List<string> choice_list;
    bool canproc = false;
    bool is_active = false;
    
    bool cantrigger = false;
    bool canfreeze = false;
    bool is_on_choice = false;
    bool can_browse = false;
    bool is_bef_choice = false;
    bool after_result = false;
    bool is_correct = false;
    bool is_img_on = false;
    int counter = 0;
    int multiple = 0, button_select = 0;

    void Start()
    {
        //red
        //E90000
        //pink
        //E900E3
        choice_list = new List<string>();
        dialogue.start_list();
    }

    // Update is called once per frame
    void Update()
    {

        //canfreeze = pmech.get_can_freeze();
     
        if (Input.GetKeyDown(KeyCode.G) && cantrigger)
        {

            Debug.Log("canproc " + canproc);
            direction = "forward";
            Trigger_Dialogue();



        }
        if (Input.GetKeyDown(KeyCode.B) && cantrigger)
        {

            Debug.Log("canproc " + canproc);
            direction = "backward";
            Trigger_Dialogue();



        }
       
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("canfreezeZZZ " + canfreeze);
            
            freeze_command();
            Debug.Log("cantrigger2 " + cantrigger);
            

        }
    }
    public void set_two_texts_prompt(bool active) {
        traverse_text.SetActive(active);
    }
    public void set_can_traverse(bool can) {
        this.can_traverse = can;
    }
    public void set_g_text_prompt(bool active) {
        g_text.SetActive(active);
    }
    void key_change_active() {
        bool activate = true;
        if (is_img_on) {
            is_img_on = false;
           
        }
        else {
            is_img_on = true;
            activate = false;
        }
        can_f = activate;
        can_z = activate;
        pmech.key_actives(activate);
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
                /*cantrigger = false;
                can_browse = false;
                StartCoroutine(Freeze_Interv());*/
            }
        }
        
           
    }
    public void set_canproc(bool proc) {
        canproc = proc;
    }
    public string get_play_name() {
        return play_name;
    }
    public void choice_trigger(List<string> clues, List<string> clue_type) {
        
        dialogue_Manager.indicate_context(clues, clue_type, play_name);
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
    public void set_can_proc(bool proc) {
        canproc = proc;
    }
    
    public void Trigger_Dialogue()
    {

        Debug.Log("canproceed " + canproc + " is before choice " + is_bef_choice);
        if (canproc)
        {
            if (is_on_choice)
            {
            
            }
            else if (is_bef_choice) {
                //indicating context clues
                is_on_choice = true;
                is_bef_choice = false;
                dialogue_Manager.set_active_dialogue(true, true);
            }
            else
            {
                counter++;
                Debug.Log("COUNTER " + counter + " MULTIPLE");
                if (after_result)
                {
                    if (counter == multiple) {
                        //show result
                        bool can_return = false;
                        after_result = false;
                        cantrigger = false;
                        canproc = false;
                        counter = 0;
                        dialogue_Manager.set_diag_counter(counter);
                        if (is_correct) {
                            
                            can_return = true;
                            dialogue_panel.SetActive(false);
                            is_correct = false;
                        }
                        
                        Debug.Log("END OF SENTENCE");
                       
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
                    counter--;
                    if (direction.Equals("forward")) {
                        next_button_func();
                    }
                    else {
                        back_button_func();
                    }
                    /*if (counter != stop_at)
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
                    }*/
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
        dialogue.clear_clue_lists();
        dialogue.add_clues(clues, clue_type);
    }
    public void change_dial_vals(List<string> sentences, List<string> names, List<string> choices, List<string> results, List<string> remarks, List<string> name_res)
    {
        is_active = true;
        canfreeze = true;
        can_browse = true;
        set_can_traverse(true);
        set_g_text_prompt(false);
        set_two_texts_prompt(true);
        set_freeze(true);
        pmech.set_has_all_clues(false);
        this.canproc = true;
        this.cantrigger = true;
        
        
        dialogue.clear_sentences();
      
        dialogue.add_remarks(remarks);
        dialogue.add_dialogues(sentences, names);
        dialogue.add_result_dialogues(results, name_res);
       
        dialogue_panel.SetActive(true);
        invisi_button.SetActive(true);
        choice_list.Clear();
      
        for (int i = 0; i < choices.Count; i++) {
            choice_list.Add(choices[i]);
        }
        dialogue_Manager.set_choices(choices);
        dialogue_Manager.set_mode(1);
        
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
    public void set_dialogue_stoppers(List<int> stoppers) {
        dialogue.clear_result_stoppers();
        dialogue.add_result_stoppers(stoppers);
    }
    public void next_button_func() {
        if (can_traverse) {
            if (canproc) {
            int last_ind = stop_at - 1;
            if (counter < last_ind) {
                counter++;
                dialogue_Manager.set_diag_counter(counter);
                dialogue_Manager.show_dialogue_sentence();
            }
        }
        }
        
        
    }
    public void back_button_func() {
        if (can_traverse) {
            if (canproc) {
            if (counter != 0) {
                counter--;
                dialogue_Manager.set_diag_counter(counter);
                dialogue_Manager.show_dialogue_sentence();
            }
        }
        }
        
        
    }
    public void can_trigger_again(int num_button)
    {
        int start = 0, end = 0;
        is_on_choice = false;
        is_bef_choice = false;
        after_result = true;
        button_select = num_button;
        string remark = dialogue.get_remark(num_button);
        counter = 0;
        Debug.Log("YEAH BOY");
        string message = "Player selected \"" + choice_list[num_button] + "\" choice\n";
        string res_msg = "Result is \"" + remark + "\"\n\n";
        slevel_backend.append_file_log(message);
        slevel_backend.append_file_log(res_msg);
        Debug.Log("YEAH BOY2");
        if (remark.Equals("Correct")) {
            is_correct = true;
        }
        if (num_button == 0) {
            end = dialogue.get_stopper(num_button);
        }
        else if (num_button == 1) {
            start = dialogue.get_stopper(0) + 1;
            end = dialogue.get_stopper(num_button);
        }
        else {
            Debug.Log("YEAH BOY 3");
            start = dialogue.get_stopper(num_button - 1) + 1; 
            end = dialogue.get_num_results() - 1;
            Debug.Log("YEAH BOY 4");
        }

        multiple = end - start + 1;
        Debug.Log("start " + start + " end " + end);
        dialogue_Manager.set_start_end(start, end);
        dialogue_Manager.set_mode(2);
        dialogue_Manager.set_active_dialogue(true, false);
        dialogue_Manager.StartDialogue(dialogue);
        Debug.Log("FLUSH");
        
    }
    IEnumerator Dialogue_Interv(bool can_return)
    {
        yield return new WaitForSeconds(1.0f);
        can_browse = true;
       
        canproc = true;
        is_active = true;
        if (!can_return) {
            List<string> clue_list = new List<string>();
            List<string> clue_type = new List<string>();
            canproc = true;
            int clue_num = dialogue.get_clue_num();
           
            for (int i = 0; i < clue_num; i++) {
                string clue_word = dialogue.get_clues(i);
                string type_clue = dialogue.get_clue_type(i);
                clue_list.Add(clue_word);
                clue_type.Add(type_clue);
            }
            cantrigger = true ;
            choice_trigger(clue_list, clue_type);
        }
        else {
            is_active = false;
         
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
