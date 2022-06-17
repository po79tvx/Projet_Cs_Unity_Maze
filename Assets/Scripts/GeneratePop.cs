using System;
using UnityEngine;

public class GeneratePop : MonoBehaviour
{

    /// <summary>
    /// Serialized field and header
    /// </summary>
    #region SerializedFiled and Header

    [Header("Genetic Algorithm")]
    [SerializeField] private int PopulationSize = 200;

    [SerializeField] private float MutationRate = 0.001f;
    [SerializeField] private int elitism = 5;
    [SerializeField] private GameObject ball;

    #endregion

    /// <summary>
    /// Attributs of the Generate Population classe
    /// </summary>
    #region Attributs

    private GameObject ballClone;

    private System.Random random;
    private Func<char> getRandomGene;
    private Func<float> fitnessFunction;
    private Vector2[] directions;
    private Algorithm ga;
    private bool isEveryOneDead;

    private int numberOfLiving;

    #endregion

    /// <summary>
    /// Functions of the Generate Population classe
    /// </summary>
    #region Function

    /// <summary>
    /// When the program start
    /// </summary>
    private void Start()
    {
        random = new System.Random();

        GameObject maze = GameObject.Find("Maze/IA");

        Vector2 ballLocation = new(maze.transform.position.x + -806, maze.transform.position.y + 328);

        ga = new(PopulationSize, 200, random, GetRandomGene, fitnessFunction, elitism, MutationRate, Instantiate(ball, ballLocation, Quaternion.identity) as GameObject);// Create an instance of the algorithm
    }

    /// <summary>
    /// Create random genes for the dot
    /// </summary>
    /// <returns>A random gene</returns>
    private Vector2 GetRandomGene()
    {
        float randomX = (float)random.NextDouble() * random.Next(-365,365);
        float randomY = (float)random.NextDouble() * random.Next(-365,365);

        //float randomAngle = random.Next(2 * MathF.PI);
        Vector2 randomAngle = new (randomX, randomY);

        return randomAngle;
    }

    /// <summary>
    /// Update every tick
    /// </summary>
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

        try
        {
            numberOfLiving = 0;

            foreach (GameObject ball in ga.Population)
            {
                if (!ball.GetComponent<DOT>().hitWall)
                {
                    isEveryOneDead = false;
                    numberOfLiving++;
                    //Debug.Log(ball.name);
                }
            }
        }catch (NullReferenceException) { }


       // Debug.Log(numberOfLiving);

        if (numberOfLiving == 0)
            isEveryOneDead = true;

        if (isEveryOneDead)
            ga.NewGeneration(PopulationSize, true);

        /*
        Debug.Log("Update => Pop");

        if (ga.Popul)
            isEveryOneDead = true;

        if (isEveryOneDead)
        {
            ga.NewGeneration(PopulationSize, true);
            isEveryOneDead = false;
        }
        */

    }

    #endregion
}