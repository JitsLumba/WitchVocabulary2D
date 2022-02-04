using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question_Dialogue_Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<Question_Script> question_list;
    [SerializeField] private definition_check dcheck;
    [SerializeField] private Question_Dialogue_Manager question_manager;
    [SerializeField] private Question_Panel_Mechanic qpanel_mech;
    [SerializeField] private question_next_level qnext_level;
    [SerializeField] private tutorial_image_show tut_img_show;
    [TextArea(3, 10)]
    [SerializeField] private List<string> tutorial_cloze_diag ;
    [SerializeField] private List<string> tutorial_cloze_speaker;
    private List<string> choices, clues, clue_typing;
    private string orig_question = "", orig_speaker = "";
    private bool canproc = true;
    private bool can_g = true, can_tab = true, can_x = true;
    private bool is_showing_img = false;
    private bool is_on_choice = false;
    private bool is_on_question = false;
    private int button_select = 0;
    private bool after_result = false;
    private bool has_finished = false;
    private bool is_on_tutorial = true;
    private string play_name = "Elaina";
    private int counter = 0, tut_counter = 0;

    void Start()
    {
        choices = new List<string>();
        clues = new List<string>();
        clue_typing = new List<string>();
        tut_img_show.set_is_not_on_dialogue(false);
        tutorial_trigger_dialogue();
    }
    void clear_lists()
    {
        choices.Clear();
        clues.Clear();
        clue_typing.Clear();
    }
    void initialize_script()
    {
        canproc = false;
        clear_lists();
        qpanel_mech.set_can_freeze(true);
        tut_img_show.set_is_not_on_dialogue(false);

        add_definitions();
        qpanel_mech.set_clue_number(clues.Count);
        qpanel_mech.set_clue_panel_active(true);
        qpanel_mech.set_freeze_panel_active(true);
        qpanel_mech.set_highlighter_panel_active(true);
        choice_button_edit();
        trigger_dialogue();
    }
    void add_definitions()
    {
        dcheck.clear_lists();
        int clue_count = question_list[counter].get_clue_count();
        for (int i = 0; i < clue_count; i++)
        {
            string clue_word = question_list[counter].get_clue(i);
            string clue_type = question_list[counter].get_clue_type(i);
            clues.Add(clue_word);
            clue_typing.Add(clue_type);
        }
        dcheck.set_answer(clues, clue_typing);
    }
    void choice_button_edit()
    {
        clear_choice_list();
        int choice_num = question_list[counter].get_choice_count();
        for (int i = 0; i < choice_num; i++)
        {
            choices.Add(question_list[counter].get_choice(i));
        }
        question_manager.set_choices(choices);
    }
    void clear_choice_list()
    {
        choices.Clear();
    }
    public void set_can_proc(bool proc)
    {
        canproc = proc;
    }
    public int get_clue_num()
    {
        return dcheck.get_count();
    }
    public string get_clue(int num)
    {
        return dcheck.get_answer(num);
    }
    public string get_clue_type(int num)
    {
        return dcheck.get_clue(num);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && canproc && can_g)
        {
            if (is_on_tutorial) {
                tutorial_trigger_dialogue();
            }
            else if (has_finished)
            {
                qnext_level.next_level_notify();
            }
            else
            {
                if (after_result)
                {
                    canproc = false;
                    after_result = false;
                    question_manager.show_result(question_list[counter].get_result_diag(button_select));
                    can_x = false;
                    Debug.Log("is_choice " + is_on_choice);
                    StartCoroutine(Result_Interv());
                }
                else
                {
                    new_dialogue(orig_question, orig_speaker);
                    is_on_choice = true;
                    question_manager.set_active_dialogue(true, true);
                    canproc = false;
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            show_tut_images();
        }
    }
    public void show_tut_images()
    {
        bool activate = true;
        Debug.Log("KUNA");
        if (can_x)
        {
            if (is_showing_img)
            {
                is_showing_img = false;

                //disable other keys


            }
            else
            {
                is_showing_img = true;
                activate = false;
            }
            can_g = activate;
            qpanel_mech.set_can_l(activate);
            qpanel_mech.set_can_o(activate);
            qpanel_mech.set_can_p(activate);
            qpanel_mech.set_can_z(activate);
            if (is_on_choice)
            {
                qpanel_mech.set_choice_active(activate);
            }
            qpanel_mech.set_can_freeze(activate);
            qpanel_mech.set_dialogue_active(activate);
            if (is_on_question)
            {
                qpanel_mech.set_freeze_panel_active(activate);
                qpanel_mech.set_highlighter_panel_active(activate);

            }
            tut_img_show.show_img_sequence();
        }

    }

    public void choice_trigger(List<string> clues)
    {
        //trigger dialogue
        is_on_question = false;
        canproc = true;
        qpanel_mech.set_clue_panel_active(false);
        question_manager.indicate_context(clues, play_name);


    }
    public int get_counter()
    {
        return counter;
    }

    public void trigger_dialogue()
    {
        is_on_question = true;
        string question = question_list[counter].get_main_script();

        string speaker = question_list[counter].get_speaker();
        orig_question = question;
        orig_speaker = speaker;
        new_dialogue(question, speaker);
    }
    public void tutorial_trigger_dialogue() {
        if (tut_counter == tutorial_cloze_diag.Count) {
            is_on_tutorial = false;
            initialize_script();
        }
        else {
            string dialogue = tutorial_cloze_diag[tut_counter];
            string speaker = tutorial_cloze_speaker[tut_counter];
            new_dialogue(dialogue, speaker);
            tut_counter++;
        }
    }
    void new_dialogue(string question, string speaker)
    {
        question_manager.new_dialogue(question, speaker);
    }
    public void choice_click_event(int num)
    {
        qpanel_mech.set_can_freeze(false);
        is_on_choice = false;
        button_select = num;
        after_result = true;
        canproc = true;
        string result_diag = question_list[counter].get_remark_diag(num);

        string talker = question_list[counter].get_speaker();
        question_manager.set_active_dialogue(true, false);
        question_manager.new_dialogue(result_diag, talker);
    }
    void next_question()
    {
        counter++;
        Debug.Log("JUMPERS " + counter);
        int max_count = question_list.Count;
        if (counter == max_count)
        {
            can_x = false;
            Debug.Log("END");
            end_of_question();
        }
        else
        {
            Debug.Log("NEW");
            initialize_script();
        }
    }
    void end_of_question()
    {
        string end_words = "Well done my student. You have finally completed all the trials!";
        string talker = "Sensei";
        canproc = true;
        set_has_finished(true);
        new_dialogue(end_words, talker);
    }
    void set_has_finished(bool fin)
    {
        has_finished = fin;
    }
    IEnumerator Result_Interv()
    {
        yield return new WaitForSeconds(1.5f);
        can_x = true;
        next_question();
    }
}
