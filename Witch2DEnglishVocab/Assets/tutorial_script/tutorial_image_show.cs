using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_image_show : MonoBehaviour
{
    [SerializeField] private List<GameObject> tutorial_images;
    [SerializeField] private List<tutorial_change_direction> tut_change_dir ;
    [SerializeField] private GameObject image_spawn, vocabulary_panel, dialogue_panel, choice_panel;
    [SerializeField] private bool has_tutorial_image;
    [SerializeField] private List<string> directional_tut_img ;
    [SerializeField] private List<int> dialogue_counts, image_marker ;
    
    
    private GameObject tut_image_object;
    private bool is_not_on_dialogue = true, can_browse = true;
    private bool is_showing_image = false;
    private int counter = 0, image_counter = 0, duration = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && is_not_on_dialogue && can_browse) {
            if (is_showing_image) {
                exit_images();
            }
            else {
                is_showing_image = true;
                movement_change(false);
                change_all_direction_texts();
                instantiate_new_image(counter);
            }
            
            Debug.Log("TUTORIAL IMAGES");
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && is_not_on_dialogue && can_browse) {
            if (is_showing_image) {
                exit_images();
            }
        }
        if (Input.GetKeyDown(KeyCode.P) && is_showing_image) {
            int max = tutorial_images.Count - 1;
            if (max > counter) {
                remove_last_image();
                counter++;
                instantiate_new_image(counter);
            }
        }
        else if (Input.GetKeyDown(KeyCode.O) && is_showing_image) {
            int max = tutorial_images.Count - 1;
            if (max > 0) {
                remove_last_image();
                counter--;
                instantiate_new_image(counter);
            }
        }
    }
    void change_all_direction_texts() {
        for (int i = 0 ; i < tutorial_images.Count; i++) {
            change_dir_text(i, "<-Previous(O)    ->Next(P)");
        }
    }
    public void exit_images() {
        is_showing_image = false;
                movement_change(true);
                remove_last_image();
    }
    void set_dialogue_active(bool active) {
        dialogue_panel.SetActive(active);
    }
    void set_choice_active(bool active) {
        choice_panel.SetActive(active);
    }
    public void set_vocabulary_active(bool active) {
        vocabulary_panel.SetActive(active);
    }
    public void set_counter(int count) {
        counter = count;
    }
    public void set_can_browse(bool browse) {
        can_browse = browse;
    }
    public void set_image_counter(int img_counter) {
        image_counter = img_counter;
    }
    public void set_is_not_on_dialogue(bool is_it) {
        is_not_on_dialogue = is_it;
    }
    public void remove_last_image() {
        
        tutorial_images[counter].SetActive(false);
    }
    void instantiate_new_image(int num) {
        
        tutorial_images[num].SetActive(true);
    }
    public void set_is_showing_image(bool show) {
        is_showing_image = show;
    }
    public bool get_has_tutorial_image() {
        return has_tutorial_image;
    }
    public void show_images_within() {
        int num = image_marker[image_counter];
        set_counter(num);
        instantiate_new_image(num);
        is_showing_image = true;
        change_dir_text(num, "Press G to proceed");
        image_counter++;
    }
    void show_tutorial_images() {
       
        instantiate_new_image(counter);
        is_showing_image = true;
       
    }
    public void during_dialogue_trigger_check() {
        //check the duration
        duration_check();
        //check if it's on number
        check_if_on_number();
    }
    void duration_check() {
        if (is_showing_image) {
            duration--;
            if (duration == 0) {
                remove_last_image();
            }
        }
    }
    
    public bool return_is_on_marked_diag(int counter) {
        
        bool is_marked = false;
        if (has_tutorial_image) {
            for (int i = 0; i < dialogue_counts.Count; i++) {
            int num = dialogue_counts[i];
            if (counter == num) {
                is_marked = true;
                break;
            }
        }
        }
        
        return is_marked;
    }
    public void check_if_on_number() {
        bool has_found = false;
        counter++;
        for (int i = 0; i < dialogue_counts.Count; i++) {
            int special_num = dialogue_counts[i];
            if (counter == special_num) {
                has_found = true;
              
                break;
            }
        }
        if (has_found) {
            show_tutorial_images();
        }
    }
    public void change_dir_text(int ind, string text) {
        tut_change_dir[ind].change_dir_text(text);
    }
    void movement_change(bool active) {
        CharacterController2D charsub2d;
        PlayerMovement pmovement ;
        GameObject player_search = GameObject.Find("Player");
        if (player_search != null) {
            
            charsub2d = player_search.GetComponent<CharacterController2D>();
            pmovement = player_search.GetComponent<PlayerMovement>();
            charsub2d.enabled = active;
            pmovement.enabled = active;
        }
    }
}
