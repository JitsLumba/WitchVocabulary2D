using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Question_Panel_Mechanic : MonoBehaviour
{
    
    [SerializeField] private Text dialogue_text, result_text, clue_text, a_text, context_type_text ;
    [SerializeField] private GameObject dialogue_box, choice_panel, result_panel, clue_panel, freeze_panel_obj, highlighter_panel;
    [SerializeField] private Image diag_panel, freeze_image, highlighter_image;
    [SerializeField] private Question_Dialogue_Trigger qtrigger ;
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
        if (Input.GetKeyDown(KeyCode.P) && is_freeze && can_p) {
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
        else if (Input.GetKeyDown(KeyCode.O) && is_freeze && can_o) {
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
        if (Input.GetKeyDown(KeyCode.L) && is_freeze && can_l)
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
                if (is_freeze) {
                    dialogue_text.text = original;
                }
                else {
                    original = dialogue_text.text;
                    words = original.Trim().Split(' ');
                    this.set_dialogue_box(words, counter);
                }
                freeze_panel();
                
                
                

            }
        }
        
        }
        
    }
    public void set_can_freeze(bool freeze) {
        canfreeze = freeze;
    }
    public void change_context_highlighter() {
        if (is_freeze) {
            light_counter++;
        if (light_counter > 2) {
            light_counter = 0;
        }
        highlighter_context(light_counter);
        
        string[] words = original.Trim().Split(' ');
        change_highlighter_text_color();
        this.set_dialogue_box(words , counter);
        }
        
    }
    public void set_freeze_panel_active(bool active) {
        freeze_panel_obj.SetActive(active);
    }
    public void set_highlighter_panel_active(bool active) {
        highlighter_panel.SetActive(active);
    }
    void change_highlighter_text_color() {
        string a_word = "";
        string type_word =  color_type;
        if (light_counter == 0) {
            
            type_word = type_word + "Synonym";
        }
        else if (light_counter == 1) {
          
            type_word = type_word + "Antonym";
        }
        else {
            a_word = "<color=#A42BE0>[A]";
            type_word = type_word + "Example";
        }
        a_word = color_type + "[A]</color>";
        type_word = type_word + "</color>";
        a_text.text = a_word;
        context_type_text.text = type_word;
    }
    void highlighter_context(int hlight) {
        Debug.Log("HLIGHT " + hlight);
        if (hlight == 0) {
            current_type = "synonym";
            color_type = "<color=#09FF00>";
        }
        else if (hlight == 1) {
            current_type = "antonym";
            color_type = "<color=#E90000>";
        }
        else if (hlight == 2) {
            current_type = "example";
            color_type = "<color=#A42BE0>";
        }
    }
    void freeze_panel() {
        if (is_freeze) {
            counter = 0;
            change_panel_color("#FFFFFF", false);
            change_freeze_panel_color("##FFFFFF");
            change_highlighter_panel_color("#FFFFFF");
            string[] words = original.Trim().Split(' ');
        }
        else {
            change_panel_color("#00F8FA", true);
            change_freeze_panel_color("#40EDF6");
             change_highlighter_panel_color("#40EDF6");
        }
        StartCoroutine(Freeze_Interv());
    }

    void dialogue_switch(bool trigger) {

    }
    void choice_panel_switch(bool trigger) {

    }
    void change_freeze_panel_color(string color) {
       ColorUtility.TryParseHtmlString(color, out panelcolor); 
       freeze_image.color = panelcolor;
    }
    void change_highlighter_panel_color(string color) {
        ColorUtility.TryParseHtmlString(color, out panelcolor); 
       highlighter_image.color = panelcolor;
    }
    void change_panel_color(string color, bool oncheck)
    {
        ColorUtility.TryParseHtmlString(color, out panelcolor);

        is_freeze = oncheck;
        diag_panel.color = panelcolor;
    }
    public void set_num_of_clues (int num) {
        num_of_clues = num;
    }
    public void set_dialogue_box(string[] words, int beforecounter)
    {
        
        highlighted_word = words[beforecounter];
        string highlight = color_type + "[" +  highlighted_word + "]</color>";

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
        for (int i = 0; i < list_num; i++) {
            clue_words.Add(qtrigger.get_clue(i));
        }
        qtrigger.choice_trigger(clue_words);
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
            change_panel_color("#FFFFFF", false);
            change_freeze_panel_color("##FFFFFF");
            change_highlighter_panel_color("#FFFFFF");
            qtrigger.set_can_proc(true);
            
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
