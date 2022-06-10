using System;
using UnityEngine;

public class GeneratePop : MonoBehaviour
{
    [Header("Genetic Algorithm")]
    [SerializeField] private int PopulationSize = 200;

    [SerializeField] private float MutationRate = 0.01f;
    [SerializeField] private int elitism = 5;
    [SerializeField] private GameObject ball;

    private GameObject ballClone;

    //List of genetic algorithm
    private Algorithm ga;

    private System.Random random;
    private Func<char> getRandomGene;
    private Func<float> fitnessFunction;
    private Vector2[] directions;

    private void Start()
    {
        random = new System.Random();

        Vector2 ballLocation = new(-811, 413);

        ga = new Algorithm(PopulationSize, 200, random, GetRandomGene, fitnessFunction, elitism, MutationRate, Instantiate(ball, ballLocation, Quaternion.identity) as GameObject);// Create an instance of the algorithm
    }

    private Vector2 GetRandomGene()
    {
        double newRandomAngle;

        newRandomAngle = random.NextDouble() * (2 * Math.PI);

        Vector2 randomAngle = new((float)newRandomAngle, (float)newRandomAngle);

        return randomAngle;
    }
}