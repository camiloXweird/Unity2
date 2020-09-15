using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class global : MonoBehaviour
{
    public static int colision =0;
	public static int fin =0;

    public static void setColision(int n)
    {
        colision = n;
    }
    public static int getColision()
    {
        return colision;
    }
}
