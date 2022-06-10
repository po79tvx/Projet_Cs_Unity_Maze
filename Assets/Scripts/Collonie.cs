using System;
using UnityEngine;

public class Collonie : MonoBehaviour
{
    public GameObject[] DotCollonie;

    private System.Random RndNumber;

    private int NumberOfGenes = 0;
    private Func<Vector2> getRandomGene;
    private Func<float> fitnessFunction;

    public Collonie(int numberOfDot)
    {
        /*DotCollonie = new GameObject[numberOfDot];

        for (int i = 0; i < numberOfDot; i++)
        {
            DOT tmpDot = new(new(NumberOfGenes, RndNumber, getRandomGene, fitnessFunction, true));
            DotCollonie[i] = tmpDot.CreateNewBall(i);
        }

        Debug.Log("New Collonie => Collonie");*/
    }
}