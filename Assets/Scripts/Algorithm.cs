using System;
using System.Collections.Generic;
using UnityEngine;

public class Algorithm
{
    // Attributs
    public List<GameObject> Population { get; private set; }// List of population
    public int Generation { get; private set; }// How many generations has past
    public float BestFitness { get; private set; }// Fitness of the best individu
    public Vector2[] BestGenes { get; private set; }// Best genes

    public int Elitism;// How many elements will be kept
    public float MutationRate;// Rate of the mutation

    private List<GameObject> newPopulation;// Create a new population
    private System.Random random;// Random number
    private float fitnessSum;// Sum of the fitness
    private int dotSize;// Size of DOT
    private Func<Vector2> getRandomGene;// Function to get a random gene
    private Func<float> fitnessFunction;// Function to get the fitness

    /// <summary>
    /// Create a new Genetic Algorithm
    /// </summary>
    /// <param name="populationSize">Size of the population</param>
    /// <param name="dotSize">Size of the dot for each elements of the population</param>
    /// <param name="random">A new Random</param>
    /// <param name="getRandomGene">Get a random gene</param>
    /// <param name="fitnessFunction">Function to calculate the fitness</param>
    /// <param name="elitism">How many elements will be kept</param>
    /// <param name="mutationRate">Initial mutation rate</param>
    public Algorithm(int populationSize, int dotSize, System.Random random, Func<Vector2> getRandomGene, Func<float> fitnessFunction,
        int elitism, float mutationRate = 0.01f)
    {
        Generation = 1;// First generation
        Elitism = elitism;
        MutationRate = mutationRate;
        Population = new List<GameObject>(populationSize);// New population
        newPopulation = new List<GameObject>(populationSize);// List of population in the algorithm

        this.random = random;
        this.dotSize = dotSize;
        this.getRandomGene = getRandomGene;
        this.fitnessFunction = fitnessFunction;

        BestGenes = new Vector2[dotSize];

        for (int i = 0; i < populationSize; i++)
        {
            Debug.Log("Create new ball => Algorithm (Constructor)");

            DOT tmpDot = new(new(dotSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));
            newPopulation.Add(tmpDot.CreateNewBall(i));
        }

        Debug.Log("New Algorithm => Algorithm (Constructor)");
    }


    /// <summary>
    /// Calculate the fitness
    /// </summary>
    private void CalculateFitness()
    {
        Debug.Log("Calculate Fitness => Algorithm");

        fitnessSum = 0;
        DNA best = Population[0].GetComponent<DOT>().Brain;

        for (int i = 0; i < Population.Count; i++)
        {
            fitnessSum += Population[i].GetComponent<DOT>().Brain.fitnessFunction();

            if (Population[i].GetComponent<DOT>().Brain.Fitness > best.Fitness)
            {
                best = Population[i].GetComponent<DOT>().Brain;
            }
        }
    }


    /// <summary>
    /// Create a new generation
    /// </summary>
    /// <param name="numNewDOT"></param>
    /// <param name="crossoverNewDOT"></param>
    public void NewGeneration(int numNewDOT, bool crossoverNewDOT)
    {
        int finalCount = Population.Count + numNewDOT;

        Debug.Log($"Start creation of new gen ({numNewDOT.ToString()}) => Algorithm");
        Debug.Log($"Final count ({finalCount.ToString()}) => Algorithm");

        // Return nothing if the population is dead
        if (finalCount <= 0)
        {
            Debug.Log("Final count <= 0 return; => Algorithm");
            return;
        }

        // If the population is not totally dead
        if (Population.Count > 0)
        {
            Debug.Log($"Population ({Population.Count.ToString()}) => Algorithm");
            CalculateFitness();
            Population.Sort();

        }
        newPopulation.Clear();// Clear the old list

        Debug.Log("Clear population => Algorithm");

        // Loop inside the population size
        for (int i = 0; i < Population.Count; i++)
        {
            Debug.Log($"For loop ({i.ToString()})=> Algorithm");

            // How many elements we want to keep & not more elements than the original population
            if (i < Elitism && i < Population.Count)
            {
                newPopulation.Add(Population[i]);
            }
            else if (i < Population.Count || crossoverNewDOT)// Create new kids
            {
                // New parents
                DOT parent1 = ChooseParent();
                DOT parent2 = ChooseParent();

                // New child
                DNA child = parent1.Brain.Crossover(parent2.Brain);

                // Mutate the child according to the mutation rate
                child.Mutate(MutationRate);

                DOT newChild = new(child);

                // Add the child to the population
                newPopulation.Add(newChild.CreateNewBall(i));
            }
            else
            {
                // Replace the actual generation with an other generation

                Debug.Log("Create New Ball => Algoritm");

                DOT tmpDot = new(new(dotSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));
                newPopulation.Add(tmpDot.CreateNewBall(i));

            }
        }

        // Switch between old and new population
        List<GameObject> tmpList = Population;
        Population = newPopulation;
        newPopulation = tmpList;

        Generation++;// Increase the generation counter

        Debug.Log("New Generation => Algorithm");
    }

    private DOT ChooseParent()
    {
        double randomNumber = random.NextDouble() * fitnessSum;

        for (int i = 0; i < Population.Count; i++)
        {
            if (randomNumber < Population[i].GetComponent<DOT>().Fitness)
            {
                return Population[i].GetComponent<DOT>();
            }

            randomNumber -= Population[i].GetComponent<DOT>().Fitness;
        }

        Debug.Log("Choose Parent => Algorithm");

        return null;

    }

    private int CompareDNA(DNA a, DNA b)
    {
        if (a.Fitness > b.Fitness)
        {
            return -1;
        }
        else if (a.Fitness < b.Fitness)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

}
