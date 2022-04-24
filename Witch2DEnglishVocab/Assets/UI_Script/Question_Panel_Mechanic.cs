using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Question_Panel_Mechanic : MonoBehaviour
{
    
    [SerializeField] private Text dialogue_text, result_text, clue_text ;
    [SerializeField] private GameObject dialogue_box, choice_panel, result_panel, clue_panel, freeze_panel_obj, highlighter_panel;
    [SerializeField] private GameObject press_g_panel , freeze_prompt_panel ;
    [SerializeField] private Image diag_panel,  highlighter_image, letter_image ;
    [SerializeField] private Sprite dialogue_sprite, system_sprite, freeze_sprite, synonym_sprite, antonym_sprite, definition_sprite, example_sprite;
    [SerializeField] private Sprite S_sprite, A_sprite, D_sprite, E_sprite;
    [SerializeField] private Question_Dialogue_Trigger qtrigger ;
    [SerializeField] private bool has_antonym = false, has_definition = false, has_example = false;
    private List<string> clue_listed;
    private int num_of_clues = 0;
    private bool can_l = true, can_o = true , can_p = true, can_z = true;
    private bool canfreeze = false;
    private bool cantrigger = true;
    private bool is_freeze = false;
    private string original = "";
    private string highlighted_word = "";
    private int counter = 0;
    private int light_counter = 0;
    private int clue_count = 0;
    Color panelcolor;
    private string color_type = "<color=#09FF00>";
    private string current_type = "synonym";
    // Start is called before the first frame update
    void Start()
    {
        clue_listed = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        string[] words;
        if (Input.GetKeyDown(KeyCode.Z)) {
            freeze_or_defreeze();
            
        }
        //HIGHLIGHTING WORDS
        if (((Input.GetKeyDown(KeyCode.RightArrow)) ) && is_freeze && can_p) {
            words = original.Trim().Split(' ');
            int num = words.Length - 1;


            if (counter >= 0)
            {

                if (counter < num)
                {
                    counter++;
                }




            }
            this.set_dialogue_box(words, counter);
        }
        else if (((Input.GetKeyDown(KeyCode.LeftArrow)) ) && is_freeze && can_o) {
            words = original.Trim().Split(' ');
            int num = words.Length - 1;


            if (counter > 0)
            {

                counter--;





            }
            this.set_dialogue_box(words, counter);
        }
        if (Input.GetKeyDown(KeyCode.Tab)) {
            change_context_highlighter();
        }
        if (Input.GetKeyDown(KeyCode.F) && is_freeze && can_l)
        {
            check_listed();
        }
    }
    public void set_can_l(bool can) {
        can_l = can;
    }
    public void set_can_o(bool can) {
        can_o = can;
    }
    public void set_can_p(bool can) {
        can_p = can;
    }
    public void set_can_z(bool can) {
        can_z = can;
    }
    public void set_dialogue_active(bool active) {
        dialogue_box.SetActive(active);
    }
    public void set_choice_active(bool active) {
        choice_panel.SetActive(active);
    }
    public void freeze_or_defreeze() {
        string[] words;
        if (can_z) {
            if (canfreeze) {
            if (cantrigger) {
                
                cantrigger = false;
                

                words = original.Trim().Split(' ');
                bool switcher = false;
                if (is_freeze) {
                    dialogue_text.text = original;
                }
                else {
                    switcher = true;
                    original = dialogue_text.text;
                    words = original.Trim().Split(' ');
                    this.set_dialogue_box(words, counter);
                }
                freeze_prompt_show_or_hide(switcher);
                freeze_panel();
                
                
                

            }
        }
        
        }
        
    }
    public void set_can_freeze(bool freeze) {
        canfreeze = freeze;
    }
    public void change_context_highlighter() {
       
            light_counter++;
        if (light_counter == 1 && has_antonym) {

        }
        else if (light_counter == 2 && has_definition) {

        }
        else if (light_counter == 3 && has_example) {

        }
        else {
            light_counter = 0;
        }
        highlighter_context(light_counter);
        change_highlighter_panel_color();
        string[] words = original.Trim().Split(' ');
        if (is_freeze) {
        this.set_dialogue_box(words , counter);
        }
        
    }
    public void set_freeze_panel_active(bool active) {
        freeze_panel_obj.SetActive(active);
    }
    public void set_highlighter_panel_active(bool active) {
        highlighter_panel.SetActive(active);
    }
    
    void highlighter_context(int hlight) {
        
        if (hlight == 0) {
            current_type = "synonym";
            color_type = "<color=#09FF00>";
        }
        else if (hlight == 1) {
            current_type = "antonym";
            color_type = "<color=#FB7E4F>";
        }
        else if (hlight == 2) {
            current_type = "definition";
            color_type = "<color=#FFA0D2>";
        }
        else if (hlight == 3) {
            current_type = "example";
            color_type = "<color=#CE64FF>";
        }
    }
    void freeze_panel() {
        if (is_freeze) {
            is_freeze = false;
            counter = 0;
            
            string[] words = original.Trim().Split(' ');
        }
        else {
            is_freeze = true;
            
        }
        change_panel_color();
            
            
        StartCoroutine(Freeze_Interv());
    }

    void dialogue_switch(bool trigger) {

    }
    void choice_panel_switch(bool trigger) {

    }
    
    void change_highlighter_panel_color() {
        if (light_counter == 0) {
            highlighter_image.sprite = synonym_sprite;
            letter_image.sprite = S_sprite;
        }
        else if (light_counter == 1) {
            highlighter_image.sprite = antonym_sprite;
            letter_image.sprite = A_sprite;
        }
        else if (light_counter == 2) {
            highlighter_image.sprite = definition_sprite;
            letter_image.sprite = D_sprite;
        }
        else if (light_counter == 3) {
            highlighter_image.sprite = example_sprite;
            letter_image.sprite = E_sprite;
        }
    }
    void change_panel_color()
    {
        if (is_freeze) {
            diag_panel.sprite = freeze_sprite;
        }
        else {
            diag_panel.sprite = dialogue_sprite;
        }
    }
    public void set_num_of_clues (int num) {
        num_of_clues = num;
    }
    public void set_dialogue_box(string[] words, int beforecounter)
    {
        
        highlighted_word = words[beforecounter];
        string highlight = color_type + "" +  highlighted_word + "</color>";

        string new_word = "";
        int reduce = words.Length - 1;
        for (int i = 0; i < words.Length; i++)
        {
            if (i != beforecounter)
            {
                new_word = new_word + words[i];
            }
            else
            {
                new_word = new_word + highlight;
            }

            if (i != reduce)
            {
                new_word = new_word + " ";
            }

        }
        
        dialogue_text.text = new_word;
    }
    void check_listed() {
        Debug.Log("Checking");
        bool not_found = true;
        for (int i = 0; i < clue_listed.Count; i++) {
            if (highlighted_word.Equals(clue_listed[i])) {
                not_found = false;
                show_result_panel("Already found", false);
                break;
            }
        }
        if (not_found) {
            compare_answers();
        }
    }
    void compare_answers()
    {
        bool not_found = true, can_choice = false, is_not_close = true;
        string show_res = "";
        int list_num = qtrigger.get_clue_num();
        int clue_counter = qtrigger.get_clue_num();
        for (int i = 0; i < list_num; i++) {
            string answer = qtrigger.get_clue(i);
            string clue = qtrigger.get_clue_type(i);
            //Debug.Log("COMPARE " + answer + " HIGHLIGHT " + highlighted_word);
        if (answer.Equals(highlighted_word))
        {
            
            //canfreeze = false;
            
            
            if (current_type.Equals(clue)) {
                show_res = "Correct";
            clue_count++;
            not_found = false;
            int remain = clue_counter - clue_count;
            set_clue_number(remain);
            clue_listed.Add(highlighted_word);


            }
            else {
                is_not_close = false; 
                show_res = "Close...";
            }
            break;
        }
        
        
        }
        if (not_found && is_not_close) {
            show_res = "Incorrect";
        }
        
        if (clue_count == clue_counter) {
            counter = 0;
            clue_count = 0;
            can_choice = true;
            clue_list_clear();
        }
        Debug.Log("RESULT IS " + show_res);
        show_result_panel(show_res, can_choice);
        

    }
    public void freeze_prompt_show_or_hide(bool is_on) {
        bool g_text = false;
        bool freeze_text = false;
        if (is_on) {
            freeze_text =  true;
        }
        else {
            g_text = true;
        }
        press_g_panel.SetActive(g_text);
        freeze_prompt_panel.SetActive(freeze_text);
    }
    public void set_clue_number(int num) {
        clue_text.text = "Clues: " + num;
    }
    public void set_clue_panel_active(bool active) {
        clue_panel.SetActive(active);
    }
    void clue_list_clear() {
        clue_listed.Clear();
    }
    public void chang_to_choice() {
        int list_num = qtrigger.get_clue_num();
        List<string> clue_words = new List<string>();
        List<string> clue_type = new List<string>();
        for (int i = 0; i < list_num; i++) {
            clue_words.Add(qtrigger.get_clue(i));
            clue_type.Add(qtrigger.get_clue_type(i));
        }
        qtrigger.choice_trigger(clue_words, clue_type);
    }
    void show_result_panel(string result, bool resultcond)
    {
        this.result_text.text = result;
        this.result_panel.SetActive(true);
        //return or no
        StartCoroutine(erase_result(resultcond));
    }
    IEnumerator Freeze_Interv() {
        yield return new WaitForSeconds(1.0f);
        cantrigger = true;
    }
    IEnumerator erase_result(bool can_choice)
    {
        //return to the original place
        yield return new WaitForSeconds(1.0f);
        this.result_panel.SetActive(false);
        
        if (can_choice)
        {
            //have this be on 
            is_freeze = false;
            change_panel_color();
            
            
            qtrigger.set_can_proc(true);
            freeze_prompt_show_or_hide(false);
            set_freeze_panel_active(false);
            set_highlighter_panel_active(false);
            set_highlighter_panel_active(false);
            set_clue_panel_active(false);
            clue_count = 0;
            canfreeze = false;
            chang_to_choice();
        }
    }
}
