using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Panel_Mechanic : MonoBehaviour
{
    [SerializeField] private GameObject result_panel, freeze_panel, clue_panel, highlighter_panel, dialogue_panel , press_g_panel, freeze_prompt_panel ;
    [SerializeField] private Image panel, freeze_image, highlighter_image, vocabulary_image, letter_image ;
    [SerializeField] private Sprite dialogue_sprite, system_sprite, freeze_sprite, synonym_sprite, antonym_sprite, definition_sprite, example_sprite;
    [SerializeField] private Sprite S_sprite, A_sprite, D_sprite, E_sprite ;

    [SerializeField] private Text text_dialogue, result_text, clue_text ;
    [SerializeField] private definition_check dcheck;
    [SerializeField] private level_return lreturn;

    [SerializeField] private sublevel_backend slevel_backend ;
    [SerializeField] private Dialogue_Trigger dtrigger ;
    
    [SerializeField] private bool has_antonym = false, has_explain = false, has_example = false; 
    
    private int correct = 0, incorrect = 0, close = 0;
    private List<string> clue_listed;
    private bool ison = false, canfreeze = false, hashighlight = false, cantrigger = true, is_img_on = false, can_move = true, has_all_clues = false;
    private string original = "";
    private string color_type = "<color=#09FF00>";
    private string current_type = "synonym";
    int counter = 0, clue_count = 0, light_counter = 0, clue_num = 0;
    private bool can_f = true, can_tab = true, can_l = true, can_o = true, can_p = true;
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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            string direct = slevel_backend.get_dir();
            Debug.Log(direct + " LANS");
            freeze_or_not();

        }

        if (((Input.GetKeyDown(KeyCode.RightArrow)) )  && ison && can_move)
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
        else if (((Input.GetKeyDown(KeyCode.LeftArrow)) ) && ison && can_move)
        {

            words = original.Trim().Split(' ');
            int num = words.Length;
            if (counter > 0)
            {

                counter--;





            }



            this.set_dialogue_box(words, counter);

        }
        if (Input.GetKeyDown(KeyCode.Tab) && can_tab) {
            change_context_highlighter();
        }
        if (Input.GetKeyDown(KeyCode.F) && ison && can_l)
        {
            check_listed();
        }
    }
    public void key_actives(bool active) {
        can_tab = active;
        can_l = active;
        can_o = active;
        can_p = active;
        can_f = active;
    }
    public void dialogue_show() {
        if (is_img_on) {
                    is_img_on = false;
                 
                    change_dialogue_active(true);
                    bool should_show = !has_all_clues;
                    if (should_show) {
                        set_freeze_panel_active(true);
                        set_highlighter_panel_active(true);
                    }
                        
                        
                    
                    
                }
                else {
                    
                    is_img_on = true;
                    
                    change_dialogue_active(false);
                   
                        set_freeze_panel_active(false);
                        set_highlighter_panel_active(false);
                    
                    
                  
                }
    }
    void change_dialogue_active(bool active) {
        this.dialogue_panel.SetActive(active);
    }
    public void set_has_all_clues(bool all) {
        has_all_clues = all;
    }
    public void set_can_move(bool move) {
        can_move = move;
    }
    
    void change_vocab_color() {
        if (ison) {
            vocabulary_image.sprite = freeze_sprite;
        }
        else {
            vocabulary_image.sprite = system_sprite;
        }
    }
    public void set_clue_panel_active(bool active) {
        this.clue_panel.SetActive(active);
    }
    public void set_clue_number(int num) {
        clue_num = num;
        clue_text.text = "Clues left: " + num;
    }
    public void set_highlighter_panel_active(bool active) {
        highlighter_panel.SetActive(active);
    }
    public void freeze_or_not() {
        if (can_tab) {
            string[] words;
        if (cantrigger) {
            if (canfreeze)
            {
                bool switcher = false;
                if (ison)
                {
                   
                   Debug.Log("IS CURRENTLY ON");
                    ison = false;
                    dtrigger.set_canproc(true);
                    text_dialogue.text = original;
                }
                else
                {
                    switcher = true;
                    Debug.Log("IS CURRENTLY OFF");
                   ison = true;
                    dtrigger.set_canproc(false);
                    
                    original = text_dialogue.text;
                    words = original.Trim().Split(' ');
                    counter = 0;
                    set_dialogue_box(words, counter);


                }
                change_panel_color();
                change_vocab_color();
                freeze_prompt_show_or_hide(switcher);
                /*cantrigger = false;
                StartCoroutine(Freeze_Interv());*/
            }
        }
        }
        
        
    }
    
    void clue_list_clear() {
        clue_listed.Clear();
    }
    public void change_context_highlighter() {
        
        if (can_tab) {
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
        highlighter_context(light_counter);
        
        string[] words = original.Trim().Split(' ');
        
        if (ison) {
            this.set_dialogue_box(words , counter);
        }
        change_highlighter_panel_color();
        
        }
            
        
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
        else {
           
            highlighter_image.sprite = example_sprite;
            letter_image.sprite = E_sprite;
        }
       
        
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
    public void set_can_freeze(bool freeze) {
        canfreeze = freeze;
    }
  
    
    void change_panel_color()
    {
        if (ison) {
            panel.sprite = freeze_sprite;
        }
        else {
            panel.sprite = dialogue_sprite;
        }
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
        string lowered = highlighted_word.ToLower();
        if (answer.Equals(lowered))
        {
            
           
            
            
            if (current_type.Equals(clue)) {
                show_res = "Correct";
                correct++;
            clue_num--;
            set_clue_number(clue_num);
            clue_count++;
            not_found = false;
            clue_listed.Add(highlighted_word);


            }
            else {
                is_not_close = false; 
                show_res = "Close...";
                close++;
            }
            break;
        }
        
        
        }
        if (not_found && is_not_close) {
            show_res = "Incorrect";
            incorrect++;
        }
        int clue_counter = dcheck.get_count();

        string message = "Player selected word: \"" + highlighted_word + "\" with the " + current_type + " highlighter\n";
        slevel_backend.append_file_log(message);
        string result = "Result is \"" + show_res+ "\"\n\n";
        slevel_backend.append_file_log(result);

        if (clue_count == clue_counter) {
            has_all_clues = true;
            can_choice = true;
            canfreeze = false;

            slevel_backend.append_file_log("\nResults:\n\n");
            
            string correct_message = "Number of times gotten correct: " + correct + "\n";
            string incorrect_message = "Number of times gotten incorrect: " + incorrect + "\n";
            string close_message = "Number of times gotten close: " + close + "\n\n";

            slevel_backend.append_file_log(correct_message);
            slevel_backend.append_file_log(incorrect_message);
            slevel_backend.append_file_log(close_message);

            correct = 0;
            incorrect = 0;
            close = 0;
            clue_list_clear();
        }
        
        show_result_panel(show_res, can_choice);
        

    }
    
    void show_result_panel(string result, bool resultcond)
    {
        this.result_text.text = result;
        this.result_panel.SetActive(true);
        cantrigger = false;
        //return or no
        StartCoroutine(erase_result(resultcond));
    }
    public void set_dialogue_box(string[] words, int beforecounter)
    {
        int index = beforecounter;
        highlighted_word = words[beforecounter];
        string highlight = color_type + "" +  highlighted_word + "</color>";

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
        text_dialogue.text = new_word;
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
    IEnumerator Freeze_Interv()
    {
        yield return new WaitForSeconds(1.0f);
        cantrigger = true;
       
    }
    public void set_freeze_panel_active(bool active) {
        freeze_panel.SetActive(active);
    }
    public void chang_to_choice() {
        int list_num = dcheck.get_count();
        List<string> clue_words = new List<string>();
        List<string> clue_type = new List<string>();
        
        dtrigger.set_two_texts_prompt(false);
        dtrigger.set_g_text_prompt(true);
        dtrigger.set_can_traverse(false);
        for (int i = 0; i < list_num; i++) {
            clue_words.Add(dcheck.get_answer(i));
            clue_type.Add(dcheck.get_clue(i));
        }
        dtrigger.choice_trigger(clue_words, clue_type);
    }
    IEnumerator erase_result(bool can_choice)
    {
        //return to the original place
        yield return new WaitForSeconds(1.0f);
        this.result_panel.SetActive(false);
        cantrigger = true;
        if (can_choice)
        {
            //have this be on 
            set_clue_panel_active(false);
            ison = false;
            change_panel_color();
           
            change_vocab_color();
            freeze_prompt_show_or_hide(false);
            set_freeze_panel_active(false);
            set_highlighter_panel_active(false);
            dtrigger.set_freeze(false);
            dtrigger.set_canproc(true);
            
            clue_count = 0;
            canfreeze = false;
            set_clue_panel_active(false);
            chang_to_choice();
        }
    }

}
