using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tutorial_panel_mechanic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject freeze_panel_obj ;
    [SerializeField] private Image dialogue_image, freeze_panel_image ;
    [SerializeField] private Text dialogue_text ;
    [SerializeField] private tutorial_definition_check tut_def_check ;
    [SerializeField] private bool can_antonym = false, can_example = false;
    Color panelcolor;
    private string current_type = "synonym";
    private string original = "";
    private string highlighted_word = "";
    private string color_type = "<color=#09FF00>";
    private bool can_freeze = false;
    private int counter = 0;
    private int correct_counter = 0;
    private bool ison = false;

    void Start()
    {
        //#40EDF6
    }

    // Update is called once per frame
    void Update()
    {
        string[] words;
        if (Input.GetKeyDown(KeyCode.Z)) {
            freeze_or_defreeze();
        }
        if (Input.GetKeyDown(KeyCode.P) && ison) {
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
        else if (Input.GetKeyDown(KeyCode.O) && ison) {
            words = original.Trim().Split(' ');
            int num = words.Length;
            if (counter > 0)
            {

                counter--;





            }



            this.set_dialogue_box(words, counter);
        }

        
    }
    public void freeze_or_defreeze() {
        string[] words;
        if (can_freeze) {
                if (ison) {
                    change_panel_color("#FFFFFF", false);
                    change_freeze_panel_color("#FFFFFF");
                    dialogue_text.text = original;
                }
                else {
                    change_panel_color("#00F8FA", true);
                    change_freeze_panel_color("#40EDF6");
                    
                    original = dialogue_text.text;
                    words = original.Trim().Split(' ');
                    counter = 0;
                    set_dialogue_box(words, counter);
                    Debug.Log(original);
                }
                
            }
    }
    void change_freeze_panel_color(string color) {
        ColorUtility.TryParseHtmlString(color, out panelcolor);
        freeze_panel_image.color = panelcolor;
    }
    public void set_freeze_panel_obj_active(bool active) {
        freeze_panel_obj.SetActive(active);
    }
    public void compare_answers()
    {
        bool not_found = true, can_choice = false, is_not_close = true;
        string show_res = "";
        
       Debug.Log("SOAM1");
            string answer = tut_def_check.get_answer();
            Debug.Log("SOAMMID");
            string clue = tut_def_check.get_type();
      Debug.Log("SOAM2");
        if (answer.Equals(highlighted_word))
        {
            
           
            
            
            if (current_type.Equals(clue)) {
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
    public void change_panel_color(string color, bool oncheck)
    {
        ColorUtility.TryParseHtmlString(color, out panelcolor);

        ison = oncheck;
        dialogue_image.color = panelcolor;
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
}
