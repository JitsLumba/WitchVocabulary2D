using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Question_Dialogue_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject dialogue_panel, choice_panel, remark_panel;
    
    [SerializeField] private Text name_field, dialogue_field,  remark_text, choice1, choice2, choice3;
    private string orig_question = "";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void indicate_context(List<string> clues, List<string> clue_type, string speaker) {
       
        string dialog_text = "The context clues for this dialogue are";
        int stopper = clues.Count - 1;
        for (int i = 0; i < clues.Count; i++) {
            string color = "";
            string type_clue = clue_type[i];
            Debug.Log("ASD " + type_clue);
            if (type_clue.Equals("synonym")) {
                color = "<color=#09FF00>";
            }
            else if (type_clue.Equals("antonym")) {
                color = "<color=#FB7E4F>";
            }
            else if (type_clue.Equals("definition")) {
                color = "<color=#FFA0D2>";
            }
            else {
                color = "<color=#CE64FF>";
            }
            string clue_colored = color + clues[i] + "</color>";
            if (i == stopper) {
                dialog_text = dialog_text + " and " +  clue_colored;
            }
            else {
                dialog_text = dialog_text + " " + clue_colored + ","; 
            }
        }
        dialog_text = dialog_text + ". Choose one of the definitions that is correct.";
      
     
      
        name_field.text = speaker;
        dialogue_field.text = dialog_text;
    }
    
    public void set_active_dialogue(bool dialogue_active, bool choice_active) {
        dialogue_panel.SetActive(dialogue_active);
        choice_panel.SetActive(choice_active);
    }
    
    public void set_orig_question(string question) {
        orig_question = question;
    }
    public void new_dialogue(string dialogue, string speaker) {
        name_field.text = speaker;
        dialogue_field.text = dialogue;
    }
    public void show_result(string result) {
        remark_text.text = result;
        set_result_panel(false, true);
        StartCoroutine(next_question());
    }
    void set_result_panel(bool diag_active, bool result_active) {
        dialogue_panel.SetActive(diag_active);
        remark_panel.SetActive(result_active);

    }
    public void set_choices(List<string> choices) {
        choice1.text = choices[0];
        choice2.text = choices[1];
        choice3.text = choices[2];
    }
    IEnumerator next_question() {
        yield return new WaitForSeconds(1.5f);
        set_result_panel(true, false);
    }
}
