using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorial_panel_mechanic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject freeze_panel_obj, clue_panel, highlighter_panel, dialogue_panel, vocabulary_panel , press_g_panel , freeze_prompt_panel, press_z_panel ;
    [SerializeField] private Image dialogue_image, freeze_panel_image, highlighter_image, letter_image, vocabulary_image ;
    [SerializeField] private Text dialogue_text, clue_text, vocabulary_text ;
    [SerializeField] private tutorial_definition_check tut_def_check ;
    [SerializeField] private Sprite  normal_dialogue_sprite, freeze_dialogue_spirte, system_sprite, synonym_image, antonym_image, definition_image, example_image;
    [SerializeField] private Sprite S_sprite, A_sprite, D_sprite, E_sprite;
    [SerializeField] private bool has_antonym = false, has_explain = false, has_example = false;
    Color panelcolor;
    private string current_type = "synonym";
    private string original = "";
    private string highlighted_word = "";
    private string color_type = "<color=#09FF00>";
    private bool can_freeze = false, cantrigger = true, can_browse = false, is_img_on = false;
    private int counter = 0;
    private int clue_num = 0;
    private int correct_counter = 0;
    private bool can_a = true, can_d = true, can_f = true, can_tab = false, can_z = true;
    private bool ison = false;
    private int light_counter = 0;

    void Start()
    {
        //purple FF3EF7
        //#40EDF6
        //text color CFBE24
       
    }

    // Update is called once per frame
    void Update()
    {
        string[] words;
        /*if (Input.GetKeyDown(KeyCode.Z)) {
            freeze_or_defreeze();
        }*/
        
        if ((Input.GetKeyDown(KeyCode.D)  || Input.GetKeyDown(KeyCode.RightArrow)) && ison && can_d) {
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
        else if ((Input.GetKeyDown(KeyCode.A) || (Input.GetKeyDown(KeyCode.LeftArrow))) && ison && can_a) {
            words = original.Trim().Split(' ');
            int num = words.Length;
            if (counter > 0)
            {

                counter--;





            }



            this.set_dialogue_box(words, counter);
        }
        
        

        
        
    }
    public void set_counter(int num) {
        this.counter = num;
    }
    public void set_can_tab(bool active) {
        can_tab = active;
    }
    public void key_actives(bool active) {
        set_can_tab(active);
        set_can_f(active);
        set_can_a(active);
        set_can_d(active);
        set_can_z(active);
    }
   
    public void set_can_f(bool can) {
        can_f = can;
    }
    public void set_can_z(bool can) {
        can_z = can;
    }
    public void set_can_d(bool can) {
        can_d = can;
    }
    public void set_can_a(bool can) {
        can_a = can;
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
    public void change_highlighter_panel_active(bool active) {
        highlighter_panel.SetActive(active);
    }
    public void change_freeze_panel_active(bool active) {
        freeze_panel_obj.SetActive(active);
    }
    void change_dialogue_active(bool active) {
        dialogue_panel.SetActive(active);
    }
    public void set_can_browse(bool browse) {
        can_browse = browse;
    }
    public void change_context_highlighter() {
        Debug.Log("is_on " + ison + " can_tab " + can_tab);
        if (can_tab) {
            if (ison) {
            light_counter++;
        if (light_counter == 1 && has_antonym) {

        }
        else if(light_counter == 2 && has_explain) {

        }
        else if(light_counter == 3 && has_example) {

        }
        else {
            light_counter = 0;
            
            
        }
        Debug.Log("light_counter " + light_counter);
        highlighter_context(light_counter);
        
        string[] words = original.Trim().Split(' ');
        change_highlighter_panel_color();
        this.set_dialogue_box(words , counter);
        }
        }
        
        
    }
    public void reset_highlighter() {
        light_counter = 0;
        highlighter_context(light_counter);
        change_highlighter_panel_color();
    }
    public void change_vocab_panel_active(bool active) {
        vocabulary_panel.SetActive(active);
    }
    public void change_vocab_color(bool oncheck) {
       if (oncheck) {
           vocabulary_image.sprite = freeze_dialogue_spirte;
       }
       else {
           vocabulary_image.sprite = system_sprite;
       }
       
    }
    public void change_highlighter_panel_color() {
      
        if (light_counter == 0) {
            highlighter_image.sprite = synonym_image;
            letter_image.sprite = S_sprite;
         
        }
        else if (light_counter == 1) {
          highlighter_image.sprite = antonym_image;
          letter_image.sprite = A_sprite;
           
        }
        else if (light_counter == 2) {
            highlighter_image.sprite = definition_image;
            letter_image.sprite = D_sprite;
        }
        else {
           
            highlighter_image.sprite = example_image;
            letter_image.sprite = E_sprite;
        }
        
        //change color
        /*a_text.text = a_word;
        context_type_text.text = type_word;*/
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
    public void set_clue_panel_active(bool active) {
        this.clue_panel.SetActive(active);
    }
    public void freeze_or_defreeze() {
        string[] words;
        Debug.Log("CAN Z " + can_z + " can freeze" + can_freeze);
        if (can_z) {
            bool is_not_img = !is_img_on;
        if (is_not_img) {
            if (cantrigger) {
            
            if (can_freeze) {
               
                bool switcher = false;
                if (ison) {
                    
                    dialogue_text.text = original;
                }
                else {
                    switcher = true;
                    
                    original = dialogue_text.text;
                    words = original.Trim().Split(' ');
                    counter = 0;
                    set_dialogue_box(words, counter);
                    Debug.Log(original);
                }
                change_panel_sprite(switcher);
                   
                   
                change_vocab_color(switcher);
                freeze_prompt_show_or_hide(switcher);
           
            
                
            }
            
        }
        }
        }
        
        
        
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
        freeze_prompt_panel_switch(freeze_text);
    }
    public void freeze_prompt_panel_switch(bool freeze_text) {
        freeze_prompt_panel.SetActive(freeze_text);
    }
    public void press_z_panel_switch(bool active) {
        press_z_panel.SetActive(active);
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
    public void change_vocab_text(string text) {
        vocabulary_text.text = text;
    }
    public void set_dialogue_active(bool active) {
        Debug.Log("TURNING " + active);
        dialogue_panel.SetActive(active);
    }
    public void set_dialogue_box(string[] words, int beforecounter)
    {
       
        highlighted_word = words[beforecounter];
        string highlight = color_type + "[" + highlighted_word + "]</color>";

        string new_word = "<color=#00D9FF>";
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
        new_word = new_word + "</color>";
        dialogue_text.text = new_word;
    }
    IEnumerator Freeze_Interv() {
        yield return new WaitForSeconds(1.0f);
        cantrigger = true;
    }
}
