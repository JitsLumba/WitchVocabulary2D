using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_transport : MonoBehaviour
{
    // Start is called before the first frame update
   
    [SerializeField] private GameObject camera, player, door;
    [SerializeField] private string checl;
    [SerializeField] private string door_num;
    [SerializeField] private level_return lreturn;
    float playerdistx, doordistx, diff;
    [SerializeField] private input_transport inp_transpy;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerdistx = player.transform.position.x;
        doordistx = door.transform.position.x;
        diff = playerdistx - doordistx;
        bool can_return = lreturn.get_can_return(door_num);
    
        if ((diff <= 0.5f && diff >= -0.5f) && can_return)
        {
            inp_transpy.set_transp_bool(true);
            
        }
        else
        {
            //transp_lp.set_false_transport();
            inp_transpy.set_transp_bool(false);
        }
    }
}
