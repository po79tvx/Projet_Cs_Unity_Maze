using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratePop : MonoBehaviour
{
    /// <summary>
    /// Serialized field and header
    /// </summary>
    /// 
    #region SerializedFiled and Header

    [Header("Genetic Algorithm")]
    [SerializeField] private int PopulationSize = 200;

    [SerializeField] private float MutationRate = 0.001f;
    [SerializeField] private int elitism = 5;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject goal;

    [SerializeField] public Text textElement;

    #endregion SerializedFiled and Header

    /// <summary>
    /// Attributs of the Generate Population classe
    /// </summary>
    /// 
    #region Attributs

    private bool isEveryOneDead;
    private const int NUMBER_OF_GOALS = 11;
    private int numberOfLiving, goalNumber;

    private System.Random random;
    private Func<float> fitnessFunction;
    private Goal tmpGoal;
    private List<GameObject> goals;
    private GameObject goalObject;
    private Algorithm ga;

    private Vector2[] positions = new Vector2[NUMBER_OF_GOALS]
    { new (-783, -207), new (-681, -340), new (-184, -172),
      new (-83, 161), new (-134, 364), new (186, 178),
      new (794, 351), new (650, -7), new (456, -273),
      new (667, -370), new (827, -433) };

    #endregion Attributs

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

        ga = new(PopulationSize, 1000, random, GetRandomGene, fitnessFunction, elitism, MutationRate, ball);// Create an instance of the algorithm

        goals = new List<GameObject>(NUMBER_OF_GOALS);// New goals list
        for (int i = 0; i < NUMBER_OF_GOALS; i++)
        {
            tmpGoal = new();
            goalObject = tmpGoal.CreateNewGoal(i, goal, positions[i]);

            Goal tmpTest = (Goal)goalObject.AddComponent(typeof(Goal));

            goals.Add(goalObject);
        }
        textElement.text = ga.textValue;
    }

    /// <summary>
    /// Create random genes for the dot
    /// </summary>
    /// <returns>A random gene</returns>
    private Vector2 GetRandomGene()
    {
        float randomX = (float)random.NextDouble() * random.Next(-365, 365);
        float randomY = (float)random.NextDouble() * random.Next(-365, 365);

        Vector2 randomAngle = new(randomX, randomY);

        return randomAngle;
    }

    /// <summary>
    /// Update every tick
    /// </summary>
    private void Update()
    {
        // Goals
        foreach (GameObject tmpGoal in goals)
        {
            tmpGoal.SetActive(false);
        }
        goals[goalNumber].SetActive(true);

        if (goals[goalNumber].GetComponent<Goal>().IsActive())
        {
            goalNumber++;
        }

        // Is Everyone's dead ?
        try
        {
            numberOfLiving = 0;

            foreach (GameObject ball in ga.Population)
            {
                if (!ball.GetComponent<DOT>().hitWall)
                {
                    isEveryOneDead = false;
                    numberOfLiving++;
                }
            }
        }
        catch (NullReferenceException) { }

        if (numberOfLiving == 0)
            isEveryOneDead = true;

        if (isEveryOneDead)
            ga.NewGeneration(PopulationSize, true);

        ga.ChangeText(null, null, numberOfLiving);
        textElement.text = ga.textValue;
    }

    #endregion Function
}