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

    private System.Random random;
    private Func<char> getRandomGene;
    private Func<float> fitnessFunction;
    private Vector2[] directions;
    private Algorithm ga;
    private bool isEveryOneDead;
    

    private void Start()
    {
        random = new System.Random();

        GameObject maze = GameObject.Find("Maze/IA");

        Vector2 ballLocation = new(maze.transform.position.x + -806, maze.transform.position.y + 328);

        ga = new(PopulationSize, 200, random, GetRandomGene, fitnessFunction, elitism, MutationRate, Instantiate(ball, ballLocation, Quaternion.identity) as GameObject);// Create an instance of the algorithm
    }

    private Vector2 GetRandomGene()
    {
        float randomX = (float)random.NextDouble() * 365;
        float randomY = (float)random.NextDouble() * 365;

        //float randomAngle = random.Next(2 * MathF.PI);
        Vector2 randomAngle = new (randomX, randomY);

        return randomAngle;
    }

    private void Update()
    {/*
        for (int i = 0; i < ga.Population.Count; i++)
        {
            isEveryOneDead = true;

            DOT ballAtributs2 = ga.Population[i].GetComponent(typeof(DOT)) as DOT;

            if (ballAtributs2.isDead == false)
                isEveryOneDead = false;
        }

        
        foreach(GameObject tmpBall in ga.Population)
        {
            isEveryOneDead = true;

            DOT ballAttributs = tmpBall.GetComponent(typeof(DOT)) as DOT;

            //(DOT)tmpBall.AddComponent(typeof(DOT));
            if (ballAttributs.isDead == false)
            {
                isEveryOneDead = false;
            }
        }*/

        foreach(GameObject ball in ga.Population)
        {
            if(ball.activeSelf)
                isEveryOneDead = false;
            else
                isEveryOneDead = true;
        }

        if (isEveryOneDead)
            ga.NewGeneration(PopulationSize,true);
    }
}