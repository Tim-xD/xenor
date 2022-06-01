using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PotionMaker : MonoBehaviour
{
    public class Potion : Items
    {


        public life vie;//the player that fight
        public player joueur;
        public Monsters monstre;

        /*
         * Add Object hitbox in children of Potion
         */


        //Type of differents potions
        enum Type
        {
            Damage,
            Heal,
            Stregth
        }

        Type type;

        //initiate the value of their type
        float damage = 0;
        float heal = 0;
        float streng = 0;   

        Potion(Type type, float x)
        {
            this.type = type;

            switch (type)
            {
                case Type.Damage:
                    damage = x;
                    break;
                case Type.Heal:
                    heal = x;
                    break;
                case Type.Stregth:
                    streng= x;
                    break;
                default:
                    break;
            }
        }

        // Make effect of potion when used
        public void Effect(Collider2D obj)
        {
            System.Random rand = new System.Random();
            int random_number=rand.Next(3);
            switch (type)
            {
                case Type.Damage:
                    if (random_number % 2==0)
                    {
                         vie.Reduce4(1);//A function of the life class that reduce by 1/4 the life of the player 
                    }
                    else
                    {
                        monstre.GetDamage(15);//A function of the Monsters class that reduce the life of the monster 
                    }
                    break;
                case Type.Heal:
                    if (random_number % 2==0)
                    {
                        vie.HealMax(); //A function of the life class that goes up to the max the life of the player 
                    }
                    else
                    {
                        monstre.Heal(); //A function of the Monsters class that goes up to the max the life of the monster
                    }
                    break;
                /*case Type.Stregth:
                    if (obj.tag == "Player")
                    {
                        joueur.Strength += 5;//Add more strength to the player 
                    }
                    else
                    {
                        monstre.attack += 3;//Add more strength to the monster
                    }
                    break;*/
                default:
                    break;
            }
        }
    }
}
