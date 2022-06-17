using System;
using System.Collections.Generic;
using UnityEngine;

public class Algorithm
{
    /// <summary>
    /// Attributs of the Algorithm
    /// </summary>
    # region Attributs
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
    public GameObject Ball;
    public Collonie tmpCollonie;
    private DOT tmpDot;
    private GameObject tmpBall;

    public List<DOT> oldPopulation;
    #endregion

    /// <summary>
    /// Constructor of the algorithm
    /// </summary>
    #region Constructor
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
        int elitism, float mutationRate, GameObject ball)
    {
        Generation = 1;// First generation
        Elitism = elitism;
        MutationRate = mutationRate;
        Population = new List<GameObject>(populationSize);// New population
        newPopulation = new List<GameObject>(populationSize);// List of population in the algorithm
        oldPopulation = new List<DOT>(populationSize);

        // Passing attributs of 
        Ball = ball;
        this.random = random;
        this.dotSize = dotSize;
        this.getRandomGene = getRandomGene;
        this.fitnessFunction = fitnessFunction;

        // Create a new array with the bests genes
        BestGenes = new Vector2[dotSize];

        for (int i = 0; i < populationSize; i++)
        { 
            tmpDot = new(new(dotSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));

            tmpBall = tmpDot.CreateNewBall(i, ball);

            DOT tmpTest = (DOT)tmpBall.AddComponent(typeof(DOT));

            tmpTest.DotInteractConstructor(tmpDot.Brain, this, i);

            Population.Add(tmpBall);

            oldPopulation.Add(tmpTest);
        }

        Debug.Log($"Pop size : ({Population.Count}) = Algorithm (Constructor)");

        Debug.Log("New Algorithm => Algorithm (Constructor)");
    }

    #endregion

    /// </summary>
    /// Functions of the algorithm
    /// </summary>
    #region Functions

    /// <summary>
    /// Calculate the fitness of the population
    /// </summary>
    private void CalculatePopulationFitness()
    {
        // Debug.Log("Calculate Fitness => Algorithm");

        fitnessSum = 0;
        DNA best = oldPopulation[0].Brain;

        for (int i = 0; i < Population.Count; i++)
        {
            fitnessSum += oldPopulation[i].Brain.Fitness;

            //fitnessSum += Population[i].GetComponent<DOT>().CalculateFitness();

            if (oldPopulation[i].Brain.Fitness > best.Fitness)
            {
                best = oldPopulation[i].Brain;
            }
        }
    }

    /// <summary>
    /// Create a new generation of dot
    /// </summary>
    /// <param name="numNewDOT">Number of new dot to create</param>
    /// <param name="crossoverNewDOT">Can the dot make a crossover ?</param>
    public void NewGeneration(int numNewDOT, bool crossoverNewDOT)
    {
        int finalCount = Population.Count + numNewDOT;
        /*
        Debug.Log($"Old Population ({oldPopulation.Count}) => Algorithm");

        Debug.Log($"Start creation of new gen ({numNewDOT.ToString()}) => Algorithm");
        Debug.Log($"Final count ({finalCount.ToString()}) => Algorithm");
        */

        // If the population is totally dead
        if (Population.Count > 0)
        {
            //Debug.Log($"Population ({Population.Count.ToString()}) => Algorithm");
            CalculatePopulationFitness();
            //Population.Sort();
        }
        newPopulation.Clear();// Clear the old list

        //Debug.Log("Clear population => Algorithm");

        // Loop inside the population size
        for (int i = 0; i < oldPopulation.Count; i++)
        {
           //Debug.Log($"For loop ({i.ToString()})=> Algorithm");

            // How many elements we want to keep & not more elements than the original population
            if (i < Elitism && i < oldPopulation.Count)
            {
                tmpDot = oldPopulation[i];

                tmpBall = tmpDot.CreateNewBall(i, Ball);

                DOT tmpTest = (DOT)tmpBall.AddComponent(typeof(DOT));

                tmpTest.DotInteractConstructor(tmpDot.Brain, this, i);

                newPopulation.Add(tmpBall);

                Debug.Log($"Elitism ({i}) => Algorithm");

                //newPopulation.Add(oldPopulation[i]);
            }
            else if (i < oldPopulation.Count || crossoverNewDOT)// Create new kids
            {
                // New parents
                DOT parent1 = ChooseParent();
                DOT parent2 = ChooseParent();

                // New child
                DNA child = Crossover(parent1.Brain, parent2.Brain);

                // Mutate the child according to the mutation rate
                child.Mutate(MutationRate);
                
                DOT newChild = new(child);

                // Add the child to the population
                /*newPopulation.Add(newChild.CreateNewBall(i, Ball));
                */

                tmpDot = newChild;

                tmpBall = tmpDot.CreateNewBall(i, Ball);

                DOT tmpTest = (DOT)tmpBall.AddComponent(typeof(DOT));

                tmpTest.DotInteractConstructor(newChild.Brain, this, i);

                newPopulation.Add(tmpBall);

                Debug.Log($"Crossover ({i}) => Algorithm");
            }
            else
            {
                // Replace the actual generation with an other generation

                //Debug.Log("Create New Ball => Algoritm");

                tmpDot = new(new(dotSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));

                tmpBall = tmpDot.CreateNewBall(i, Ball);

                DOT tmpTest = (DOT)tmpBall.AddComponent(typeof(DOT));

                tmpTest.DotInteractConstructor(tmpDot.Brain, this, i);

                newPopulation.Add(tmpBall);

                Debug.Log($"New Ball ({i} => Algorithm");
            }
        }

        // Switch between old and new population
        List<GameObject> tmpList = Population;
        Population = newPopulation;
        newPopulation = tmpList;

        Generation++;// Increase the generation counter

        /*
        Debug.Log($"New Generation {Generation} => Algorithm");
        Debug.Log($"Pop size : {Population.Count} => Algorithm");
        Debug.Log($"New pop size : {newPopulation.Count} => Algorithm");
        */
    }

    /// <summary>
    /// Make a crossover between two parents
    /// </summary>
    /// <param name="firstParent">First parent</param>
    /// <param name="secondParent">Second parent</param>
    /// <returns>Return the child</returns>
    public DNA Crossover(DNA firstParent, DNA secondParent)
    {
        // Create a new dna (Child of the two parents)
        DNA child = new(firstParent.Genes.Length, random, getRandomGene, fitnessFunction, shouldInitGenes: false);

        // Reach the genes of the child
        for (int i = 0; i < firstParent.Genes.Length; i++)
        {
            // Have a chance to replace them with genes of first or second parent
            if (random.NextDouble() < 0.5)
                child.Genes[i] = firstParent.Genes[i];
            else
                child.Genes[i] = secondParent.Genes[i];
        }

        //Debug.Log("Crossover => DNA");

        return child;
    }

    /// <summary>
    /// Choose which parent will make a crossover
    /// </summary>
    /// <returns>Which parent ?</returns>
    private DOT ChooseParent()
    {
        double randomNumber = random.NextDouble() * fitnessSum;

        // Loop into all the parents
        for (int i = 0; i < oldPopulation.Count; i++)
        {
            if (randomNumber < oldPopulation[i].Brain.Fitness)
            {
                return oldPopulation[i];
            }

            randomNumber -= oldPopulation[i].Brain.Fitness;
        }

        //Debug.Log("Choose Parent => Algorithm");

        return null;
    }

    #endregion
}