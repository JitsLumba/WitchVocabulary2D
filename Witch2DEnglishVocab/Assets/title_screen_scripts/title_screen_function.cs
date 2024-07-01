using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class title_screen_function : MonoBehaviour
{
    [SerializeField] private GameObject main_panel, tutorial_panel;
    [SerializeField] private play_game play_gm ;
    private bool has_tutorial_panel_on = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && has_tutorial_panel_on) {
            close_tutorial_panel();
        }
    }
    public void close_tutorial_panel() {
        has_tutorial_panel_on = false;
        set_tutorial_panel_active(false);
        set_main_panel_active(true);
    }
    void set_main_panel_active(bool active) {
        this.main_panel.SetActive(active);
    }
    void set_tutorial_panel_active(bool active) {
        tutorial_panel.SetActive(active);
    }
    public void play_notify() {
        play_gm.start_game();
    }
    public void tutorial_show() {
        has_tutorial_panel_on = true;
        set_tutorial_panel_active(true);
        set_main_panel_active(false);
    }
    
}
