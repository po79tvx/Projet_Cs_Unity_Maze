using System;
using UnityEngine;

/// <summary>
/// A DNA of type 'T'
/// </summary>
/// <typeparam name="T">Any type of param</typeparam>
public class DNA : MonoBehaviour
{
    // Attributs
    public Vector2[] Genes { get; private set; }// Array of genes

    public float Fitness { get; private set; }// Calculation time of the evaluation function

    private System.Random random;// Random
    public Func<Vector2> getRandomGene;// Random gene
    public Func<float> fitnessFunction;// Function of the fitness
    public int step = 0;

    public DNA(int size, System.Random random, Func<Vector2> getRandomGene, Func<float> fitnessFunction, bool shouldInitGenes)
    {
        // Assign values to DNA
        Genes = new Vector2[size];
        this.random = random;
        this.getRandomGene = getRandomGene;
        this.fitnessFunction = fitnessFunction;

        // Don't create genes every time, only when he his allowed to
        if (shouldInitGenes)
        {
            // Create a new random gene for each gene of the dna
            for (int i = 0; i < Genes.Length; i++)
            {
                Genes[i] = this.getRandomGene();
            }
        }

        Debug.Log("New DNA => DNA");
    }

    private void Start()
    {
        for (int i = 0; i < Genes.Length; i++)
        {
        }
    }

    /// <summary>
    /// Crosses DNA from both parents
    /// </summary>
    /// <param name="otherParent">The second parent</param>
    /// <returns>Return the child</returns>
    public DNA Crossover(DNA otherParent)
    {
        // Create a new dna (Child of the two parents)
        DNA child = new(Genes.Length, random, getRandomGene, fitnessFunction, shouldInitGenes: false);

        // Reach the genes of the child
        for (int i = 0; i < Genes.Length; i++)
        {
            // Have a chance to replace them with genes of first or second parent
            child.Genes[i] = random.NextDouble() < 0.5 ? Genes[i] : otherParent.Genes[i];
        }

        Debug.Log("Crossover => DNA");

        return child;
    }

    /// <summary>
    /// Has a chance to mutate the dna
    /// </summary>
    /// <param name="mutationRate">Rate of the mutation</param>
    public void Mutate(float mutationRate)
    {
        // Reach the genes of dna
        for (int i = 0; i < Genes.Length; i++)
        {
            // Have a chance to mutate
            if (random.NextDouble() < mutationRate)
            {
                Genes[i] = getRandomGene();
            }
        }

        Debug.Log("Mutate => DNA");
    }
}