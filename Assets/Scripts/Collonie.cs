using System;
using System.Collections.Generic;
using UnityEngine;

public class Collonie : MonoBehaviour
{
    public List<DOT> DotCollonie;

    private System.Random RndNumber;

    private int NumberOfGenes = 0;
    private Func<Vector2> getRandomGene;
    private Func<float> fitnessFunction;
    private GameObject Ball;
    /*
    public Collonie(int numberOfDot, GameObject ball)
    {
        this.Ball = ball;


        for (int i = 0; i < numberOfDot; i++)
        {
            DOT tmpDot = new(new(NumberOfGenes, RndNumber, getRandomGene, fitnessFunction, true));
            DotCollonie[i] = tmpDot.CreateNewBall(i);
        }

        Debug.Log("New Collonie => Collonie");
    }


    public List<DOT> CreateCollonie(int numberDot, GameObject ball)
    {
        for (int i = 0; i < numberDot; i++)
        {
            DOT tmpDot = new Dot
        }


        return DotCollonie;
    }*/
}