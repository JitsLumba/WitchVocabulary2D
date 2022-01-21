using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorial_panel_mechanic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject freeze_panel_obj, clue_panel, highlighter_panel, dialogue_panel, vocabulary_panel ;
    [SerializeField] private Image dialogue_image, freeze_panel_image, highlighter_image, vocabulary_image ;
    [SerializeField] private Text dialogue_text, clue_text, a_text, context_type_text ;
    [SerializeField] private tutorial_definition_check tut_def_check ;
    [SerializeField] private Sprite  normal_dialogue_sprite, freeze_dialogue_spirte, system_sprite;
    [SerializeField] private bool has_antonym = false, has_example = false;
    Color panelcolor;
    private string current_type = "synonym";
    private string original = "";
    private string highlighted_word = "";
    private string color_type = "<color=#09FF00>";
    private bool can_freeze = false, cantrigger = true, can_browse = false, is_img_on = false;
    private int counter = 0;
    private int clue_num = 0;
    private int correct_counter = 0;
    private bool can_o = true, can_p = true, can_l = true, can_tab = false, can_f = true;
    private bool ison = false;
    private int light_counter = 0;

    void Start()
    {
        //#40EDF6
        /*if (Input.GetKeyDown(KeyCode.X)) {
            Debug.Log(can_browse + " CHECK");
            if (can_browse) {
                Debug.Log("CAN BROWSE PANEL " + is_img_on);
                if (is_img_on) {
                    is_img_on = false;
                    Debug.Log("REOPEN DIALOGUE");
                    change_dialogue_active(true);
                    if (can_freeze) {
                        change_freeze_panel_active(true);
                        change_highlighter_panel_active(true);
                    }
                    
                }
                else {
                    
                    is_img_on = true;
                    
                    change_dialogue_active(false);
                    if (can_freeze) {
                        change_freeze_panel_active(false);
                        change_highlighter_panel_active(false);
                    }
                    
                    Debug.Log("CLOSE DIALOGUE " + is_img_on + " " + can_browse);
                }
            }
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        string[] words;
        if (Input.GetKeyDown(KeyCode.Z)) {
            freeze_or_defreeze();
        }
        
        if (Input.GetKeyDown(KeyCode.P) && ison && can_p) {
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
        else if (Input.GetKeyDown(KeyCode.O) && ison && can_o) {
            words = original.Trim().Split(' ');
            int num = words.Length;
            if (counter > 0)
            {

                counter--;





            }



            this.set_dialogue_box(words, counter);
        }
        if (Input.GetKeyDown(KeyCode.Tab)) {
            change_context_highlighter();
        }
        

        
        
    }
    public void key_actives(bool active) {
        can_tab = active;
        can_l = active;
        can_o = active;
        can_p = active;
        can_f = active;
    }
    public void tutorial_dialogue_show() {
        if (can_browse) {
                
                if (is_img_on) {
                    is_img_on = false;
                    Debug.Log("REOPEN DIALOGUE");
                    change_dialogue_active(true);
                    change_vocab_panel_active(true);
                    if (can_freeze) {
                        change_freeze_panel_active(true);
                        change_highlighter_panel_active(true);
                        
                    }
                    
                }
                else {
                    
                    is_img_on = true;
                    
                    change_dialogue_active(false);
                    change_vocab_panel_active(false);
                    if (can_freeze) {
                        change_freeze_panel_active(false);
                        change_highlighter_panel_active(false);
                    }
                    
                    Debug.Log("CLOSE DIALOGUE " + is_img_on + " " + can_browse);
                }
        }
    }
    void change_highlighter_panel_active(bool active) {
        highlighter_panel.SetActive(active);
    }
    void change_freeze_panel_active(bool active) {
        freeze_panel_obj.SetActive(active);
    }
    void change_dialogue_active(bool active) {
        dialogue_panel.SetActive(active);
    }
    public void set_can_browse(bool browse) {
        can_browse = browse;
    }
    public void change_context_highlighter() {
        if (can_tab) {
            if (ison) {
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
        change_highlighter_text_color();
        this.set_dialogue_box(words , counter);
        }
        }
        
        
    }
    void change_vocab_panel_active(bool active) {
        vocabulary_panel.SetActive(active);
    }
    public void change_vocab_color(bool oncheck) {
       if (oncheck) {
           vocabulary_image.sprite = freeze_dialogue_spirte;
       }
       else {
           vocabulary_image.sprite = system_sprite;
       }
       vocabulary_image.color = panelcolor;
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
    public void set_clue_panel_active(bool active) {
        this.clue_panel.SetActive(active);
    }
    public void freeze_or_defreeze() {
        string[] words;
        if (can_f) {
            bool is_not_img = !is_img_on;
        if (is_not_img) {
            if (cantrigger) {
            
            if (can_freeze) {
                cantrigger = false;
                if (ison) {
                    change_panel_sprite(false);
                    change_freeze_panel_color("#FFFFFF");
                    change_highlighter_panel_color("#FFFFFF");
                    change_vocab_color(false);
                    dialogue_text.text = original;
                }
                else {
                    change_panel_sprite(true);
                    change_freeze_panel_color("#40EDF6");
                    change_highlighter_panel_color("#40EDF6");
                    change_vocab_color(true);
                    original = dialogue_text.text;
                    words = original.Trim().Split(' ');
                    counter = 0;
                    set_dialogue_box(words, counter);
                    Debug.Log(original);
                }
           
                StartCoroutine(Freeze_Interv());
                
            }
            
        }
        }
        }
        
        
        
    }
    public void change_highlighter_panel_color(string color) {
        ColorUtility.TryParseHtmlString(color, out panelcolor); 
       highlighter_image.color = panelcolor;
    }
    public void set_clue_number(int num) {
        clue_num = num;
        clue_text.text = "Clues: " + num;
    }
    public void change_freeze_panel_color(string color) {
        ColorUtility.TryParseHtmlString(color, out panelcolor);
        freeze_panel_image.color = panelcolor;
    }
    public void set_highlighter_panel_active(bool active) {
        highlighter_panel.SetActive(active);
    }
    public void set_freeze_panel_obj_active(bool active) {
        freeze_panel_obj.SetActive(active);
    }
    public void compare_answers()
    {
        bool not_found = true, can_choice = false, is_not_close = true;
        string show_res = "";
        
      
            string answer = tut_def_check.get_answer();
         
            string clue = tut_def_check.get_type();
 
        if (answer.Equals(highlighted_word))
        {
            
           
            
            
            if (current_type.Equals(clue)) {
                clue_num--;

                set_clue_number(clue_num);
                set_clue_panel_active(false);
                show_res = "Correct";
                correct_counter = 0;
                not_found = false;

          


            }
            else {
                correct_counter = 2;
                is_not_close = false; 
                show_res = "Close...";
            }
         
        }
         Debug.Log("SOAM3");
        
        
        if (not_found && is_not_close) {
            correct_counter = 1;
            show_res = "Incorrect";
        }
        ison = false;
        //if it is correct 
        //if it is not
      
        
        

    }
    public int get_correct_counter() {
        return correct_counter;
    }
    public bool get_ison() {
        return ison;
    }
    public void set_can_freeze(bool freeze) {
        can_freeze = freeze;
    }
    public void change_panel_sprite(bool oncheck)
    {
        if (oncheck) {
            dialogue_image.sprite = freeze_dialogue_spirte;
        }
        else {
            dialogue_image.sprite = normal_dialogue_sprite;
        }

        ison = oncheck;
      
    }
    public void set_dialogue_active(bool active) {
        dialogue_panel.SetActive(active);
    }
    public void set_dialogue_box(string[] words, int beforecounter)
    {
       
        highlighted_word = words[beforecounter];
        string highlight = color_type + "[" + highlighted_word + "]</color>";

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
    IEnumerator Freeze_Interv() {
        yield return new WaitForSeconds(1.0f);
        cantrigger = true;
    }
}
