using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_character_transport : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private tutorial_adaptor tut_adapt ;
    [SerializeField] private GameObject F_key_obj, player, door;
    [SerializeField] private GameObject sensei, camera;

    [SerializeField] private float camera_x_pos, player_x_pos, sensei_x_pos;

    private bool can_enter = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float playerdist = player.transform.position.x, door_dist = door.transform.position.x;
        float dist = playerdist - door_dist;

        if (dist >= -0.5f && dist <= 0.5f) {
            set_f_key_obj_active(true);
            can_enter = true;
        }
        else {
            set_f_key_obj_active(false);
            can_enter = false;
        }

        if (Input.GetKeyDown(KeyCode.F) && can_enter) {
            switch_movement(false);
            play_dialogues_on_door();
        }
    }
    void play_dialogues_on_door() {
        tut_adapt.switch_empty_tutorials(true);
        tut_adapt.play_dialogues_on_door();
    }
    public void transport() {
        this.camera.transform.position = new Vector3(camera_x_pos, this.camera.transform.position.y, this.camera.transform.position.z);
        this.sensei.transform.position = new Vector3(sensei_x_pos, sensei.transform.position.y, sensei.transform.position.z);

        this.player.transform.position =  new Vector3(player_x_pos, player.transform.position.y, player.transform.position.z);
    }
    void set_f_key_obj_active(bool active) {
        F_key_obj.SetActive(active);
    }
    void switch_movement(bool active) {
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
