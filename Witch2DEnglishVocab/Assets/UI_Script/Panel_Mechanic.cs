using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Panel_Mechanic : MonoBehaviour
{
    [SerializeField] private GameObject result_panel;
    [SerializeField] private Image panel;

    [SerializeField] private Text text_dialogue, result_text;
    [SerializeField] private definition_check dcheck;
    [SerializeField] private level_return lreturn;
    [SerializeField] private Dialogue_Trigger dtrigger ;
    [SerializeField] private bool has_antonym = false, has_example = false; 
    private List<string> clue_listed;
    private bool ison = false, canfreeze = false, hashighlight = false, cantrigger = true;
    private string original = "";
    private string color_type = "<color=#09FF00>";
    private string current_type = "synonym";
    int counter = 0, clue_count = 0, light_counter = 0;
    private string highlighted_word = "";
    Color panelcolor;
    // Start is called before the first frame update
    void Start()
    {
        clue_listed = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        string[] words;
        char[] delimiterChars = { ' ', '.', ':', '\t', '!', '?' };

        if (Input.GetKeyDown(KeyCode.Z) && cantrigger)
        {
            if (canfreeze)
            {
                if (ison)
                {
                    change_panel_color("#FFFFFF", false);

                    text_dialogue.text = original;
                }
                else
                {
                    change_panel_color("#00F8FA", true);

                    original = text_dialogue.text;
                    words = original.Trim().Split(' ');
                    counter = 0;
                    set_dialogue_box(words, counter);


                }
                cantrigger = false;
                StartCoroutine(Freeze_Interv());
            }

        }

        if (Input.GetKeyDown(KeyCode.P) && ison)
        {

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
            int beforecounter = counter - 1;


        }
        else if (Input.GetKeyDown(KeyCode.O) && ison)
        {

            words = original.Trim().Split(' ');
            int num = words.Length;
            if (counter > 0)
            {

                counter--;





            }



            this.set_dialogue_box(words, counter);

        }
        if (Input.GetKeyDown(KeyCode.Tab) && ison) {
            change_context_highlighter();
        }
        if (Input.GetKeyDown(KeyCode.L) && ison)
        {
            check_listed();
        }
    }
    void clue_list_clear() {
        clue_listed.Clear();
    }
    void change_context_highlighter() {
        light_counter++;
        if (light_counter == 1 && has_antonym) {

        }
        else if(light_counter == 2 && has_example) {

        }
        else {
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
    public void set_can_freeze(bool freeze) {
        canfreeze = freeze;
    }
    void change_panel_color(string color, bool oncheck)
    {
        ColorUtility.TryParseHtmlString(color, out panelcolor);

        ison = oncheck;
        panel.color = panelcolor;
    }
    public void set_freeze(bool freeze) {
        this.canfreeze = freeze;
    }
    public bool get_can_freeze() {
        return canfreeze;
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
        int list_num = dcheck.get_count();
        for (int i = 0; i < list_num; i++) {
            string answer = dcheck.get_answer(i);
            string clue = dcheck.get_clue(i);
     
        if (answer.Equals(highlighted_word))
        {
            
           
            
            
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
        int clue_counter = dcheck.get_count();
        if (clue_count == clue_counter) {
            can_choice = true;
            clue_list_clear();
        }
      
        show_result_panel(show_res, can_choice);
        

    }
    
    void show_result_panel(string result, bool resultcond)
    {
        this.result_text.text = result;
        this.result_panel.SetActive(true);
        //return or no
        StartCoroutine(erase_result(resultcond));
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
        
        text_dialogue.text = new_word;
    }
    IEnumerator Freeze_Interv()
    {
        yield return new WaitForSeconds(1.0f);
        cantrigger = true;
       
    }
    public void chang_to_choice() {
        int list_num = dcheck.get_count();
        List<string> clue_words = new List<string>();
        for (int i = 0; i < list_num; i++) {
            clue_words.Add(dcheck.get_answer(i));
        }
        dtrigger.choice_trigger(clue_words);
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
            dtrigger.set_freeze(false);
            dtrigger.set_canproc(true);
            clue_count = 0;
            canfreeze = false;
            chang_to_choice();
        }
    }

}
