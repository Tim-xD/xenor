using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour
{

    public int nb = -1;
    public GameObject wire;
    public WiresManager wireManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       

    }


    //unplug a wire
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (nb != -1)//nb = -1 by default, 0 if it a left plug and 1 if it is the right plug
        {
            if (nb == 1)//is right plug
            {
                Debug.Log("you are a success");
            }
            else
            {
                Debug.Log("you smell bad");
                //make player lose life
                //reset all
            }
            Destroy(wire);//remove wire unpluged
        }
    }
  
}
