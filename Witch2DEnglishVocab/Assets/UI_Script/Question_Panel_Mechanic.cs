using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Question_Panel_Mechanic : MonoBehaviour
{
    
    [SerializeField] private Text dialogue_text, result_text;
    [SerializeField] private GameObject dialogue_box, choice_panel, result_panel;
    [SerializeField] private Image diag_panel;
    [SerializeField] private Question_Dialogue_Trigger qtrigger ;
    private List<string> clue_listed;
    private int num_of_clues = 0;
    private bool canfreeze = true;
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
        if (Input.GetKeyDown(KeyCode.Z) && canfreeze) {
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
        //HIGHLIGHTING WORDS
        if (Input.GetKeyDown(KeyCode.P) && is_freeze) {
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
        else if (Input.GetKeyDown(KeyCode.O) && is_freeze) {
            words = original.Trim().Split(' ');
            int num = words.Length - 1;


            if (counter > 0)
            {

                counter--;





            }
            this.set_dialogue_box(words, counter);
        }
        if (Input.GetKeyDown(KeyCode.Tab) && is_freeze) {
            change_context_highlighter();
        }
        if (Input.GetKeyDown(KeyCode.L) && is_freeze)
        {
            check_listed();
        }
    }
    void change_context_highlighter() {
        light_counter++;
        if (light_counter > 2) {
            light_counter = 0;
        }
        highlighter_context(light_counter);
        
        string[] words = original.Trim().Split(' ');
        
        this.set_dialogue_box(words , counter);
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
            string[] words = original.Trim().Split(' ');
        }
        else {
            change_panel_color("#00F8FA", true);
        }
        StartCoroutine(Freeze_Interv());
    }
    void dialogue_switch(bool trigger) {

    }
    void choice_panel_switch(bool trigger) {

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
        string highlight = color_type + highlighted_word + "</color>";

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
        int clue_counter = qtrigger.get_clue_num();
        if (clue_count == clue_counter) {
            can_choice = true;
            clue_list_clear();
        }
        Debug.Log("RESULT IS " + show_res);
        show_result_panel(show_res, can_choice);
        

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
            
            qtrigger.set_can_proc(true);
            clue_count = 0;
            canfreeze = false;
            chang_to_choice();
        }
    }
}
