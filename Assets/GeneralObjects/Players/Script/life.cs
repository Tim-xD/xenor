using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class life : MonoBehaviour //life class 
{
    //canvas of hearts
    public  Image cooldown;
    public  Image cooldown1;
    public  Image cooldown2;
    public  Image cooldown3;
    public  Image cooldown4;
    public bool die = false;
    public float waitTime = 15.0f;
    private GameManager gameManager;
    PhotonView view;
        
    public life(Image heart1, Image heart2, Image heart3, Image heart4, Image heart5)
    {
        cooldown = heart1;
        cooldown1 = heart2;
        cooldown2 = heart3;
        cooldown3 = heart4;
        cooldown4 = heart5;
    }

    void Start()
    {

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        view = GetComponent<PhotonView>();


        if (!PhotonNetwork.IsMasterClient) return;

        if (view.IsMine)
        {
            view.RPC("Reduce2Bis", RpcTarget.All, gameManager.hearths[0]);
        }
        else
        {
            view.RPC("Reduce2Bis", RpcTarget.All, gameManager.hearths[1]);
        }
    }



    /*
     *Function that swith to another heart canvas 
     */
    public Image SwitchImage(int number)
    {
        switch (number)
        {
            case 1:
                return cooldown1;
            case 2:
                return cooldown2;
            case 3:
                return cooldown3;
            case 4:
                return cooldown4;
            default:
                return cooldown;
        }
    }


    /*
     *Function that reduce over the number of 1/2
     */
    [PunRPC]
    public void Reduce2(int hearts) //Function that reduce by 1/2 hearts
    {
        if (gameManager == null)
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        gameManager.QuarterHeartLost += hearts*2;

        Image cd = cooldown;

        int j = 1;//count of round to switch to the next canvas of heart
        for (int i = 0; i < hearts; i++)
        {
            while (cd.fillAmount <= 0 && j<5)
            {
                cd = SwitchImage(j);//Change the image of heart when the filAmount is at 0
                j++;
            }
            
            cd.fillAmount -= .5f;

            if (!PhotonNetwork.IsMasterClient) return;

            if (view.IsMine)
            {
                gameManager.hearths[0] += 1;
            }
            else
            {
                gameManager.hearths[1] += 1;
            }
        }
    }

    [PunRPC]
    public void Reduce2Bis(int hearts) //Function that reduce by 1/2 hearts
    {
        Image cd = cooldown;

        int j = 1;//count of round to switch to the next canvas of heart
        for (int i = 0; i < hearts; i++)
        {
            while (cd.fillAmount <= 0 && j < 5)
            {
                cd = SwitchImage(j);//Change the image of heart when the filAmount is at 0
                j++;
            }

            cd.fillAmount -= .5f;
        }
    }
    /*
     *Function that reduce over the number of 1/4
     */

    public void Reduce4(int hearts) //Function that reduce by 1/4 hearts
    {
        if (gameManager == null)
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        gameManager.QuarterHeartLost += hearts;

        Image cd = cooldown;

        float val = 1;//the value which reduces the filAmount 
        int j = 1;//count of round to switch to the next canvas of heart
        //Reduces by a number of 1/2 heart the filAmount
        for (int i = 0; i < hearts; i++)
        {
            val -= 0.25f;
            while (cd.fillAmount <= 0 && j < 5)
            {
                cd = SwitchImage(j);//Change the image of heart when the filAmount is at 0
                j++;
            }
            
            cd.fillAmount -= val;
            
        }
    }

    //Make the fillAmount of all the canvas to 1 
    public void HealMax()
    {
        if (die) // revive and heal 1 heart
        {
            cooldown4.fillAmount = 1;
            die = false;

            if (!PhotonNetwork.IsMasterClient) return;

            if (view.IsMine)
                gameManager.hearths[0] = 8;
            else
                gameManager.hearths[1] = 8;
        }
        else // heal 5 hearts
        {
            cooldown.fillAmount = 1;
            cooldown1.fillAmount = 1;
            cooldown2.fillAmount = 1;
            cooldown3.fillAmount = 1;
            cooldown4.fillAmount = 1;

            if (!PhotonNetwork.IsMasterClient) return;

            if (view.IsMine)
                gameManager.hearths[0] = 0;
            else
                gameManager.hearths[1] = 0;
        }
    }



    void Update()//verify if the player is not dead
    {

        if(cooldown4.fillAmount <= 0)
        {
            die = true;
        }
    }
}
