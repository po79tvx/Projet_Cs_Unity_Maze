using System;
using UnityEngine;

public class GeneratePop : MonoBehaviour
{
    [Header("Genetic Algorithm")]
    [SerializeField] int PopulationSize = 200;
    [SerializeField] float MutationRate = 0.01f;
    [SerializeField] int elitism = 5;

    //List of genetic algorithm
    private Algorithm ga;
    private System.Random random;
    private Func<char> getRandomGene;
    private Func<float> fitnessFunction;
    private Vector2[] directions;

    void Start()
    {
        random = new System.Random();

        ga = new Algorithm(PopulationSize, 200, random, GetRandomGene, fitnessFunction, elitism, MutationRate);// Create an instance of the algorithm
    }

    private Vector2 GetRandomGene()
    {
        double newRandomAngle;

        newRandomAngle = random.NextDouble() * (2 * Math.PI);

        Vector2 randomAngle = new Vector2((float)newRandomAngle, (float)newRandomAngle);

        return randomAngle;
    }
}
