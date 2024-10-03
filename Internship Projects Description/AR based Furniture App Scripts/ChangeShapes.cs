using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShapes : MonoBehaviour
{
    public GameObject[] Objectss;
    private int counter = 0;

//Functions For Objectss
    public void nextChair()
    {
        counter++;
        if(counter == Objectss.Length)
        {
            counter = 0;
        }
        for(int i = 0 ; i<Objectss.Length ; i++)
        {
            Objectss[i].gameObject.SetActive(false);
        }
        Objectss[counter].gameObject.SetActive(true);
    }

    public void backChair()
    {
        counter--;
        if(counter == -1)
        {
            counter = Objectss.Length-1 ;
        }
        for(int i = 0 ; i<Objectss.Length ; i++)
        {
            Objectss[i].gameObject.SetActive(false);
        }
        Objectss[counter].gameObject.SetActive(true);
    }
}
