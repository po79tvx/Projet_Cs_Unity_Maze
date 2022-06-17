using System;
using UnityEngine;

/// <summary>
/// A DNA
/// </summary>
public class DNA : MonoBehaviour
{
    /// <summary>
    /// Attributs of the DNA 
    /// </summary>
    #region Attributs
    public Vector2[] Genes { get; private set; }// Array of genes

    public float Fitness { get; set; }// Calculation time of the evaluation function

    private System.Random random;// Random
    public Func<Vector2> getRandomGene;// Random gene
    public Func<float> fitnessFunction;// Function of the fitness
    public int step = 0;

    #endregion

    /// <summary>
    /// Constructor of the DNA 
    /// </summary>
    #region Constructor

    /// <summary>
    /// Create a new constructor
    /// </summary>
    /// <param name="size">Size of the dna</param>
    /// <param name="random">A random number</param>
    /// <param name="getRandomGene">Function to get random genes</param>
    /// <param name="fitnessFunction">Function to get the fitness</param>
    /// <param name="shouldInitGenes">Can the dna init genes ?</param>
    public DNA(int size, System.Random random, Func<Vector2> getRandomGene, Func<float> fitnessFunction, bool shouldInitGenes = false)
    {
        // Assign values to DNA
        Genes = new Vector2[size];
        this.random = random;
        this.getRandomGene = getRandomGene;
        this.fitnessFunction = fitnessFunction;

        // Don't create genes every time, only when he his allowed to
        if (shouldInitGenes)
        {
            Randomize();
        }

        //Debug.Log("New DNA => DNA");
    }

    #endregion

    /// <summary>
    /// Function of the DNA 
    /// </summary>
    #region Functions

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

        //Debug.Log("Mutate => DNA");
    }

    /// <summary>
    /// /* Generates random vectors for the directions array.These are unit vectors(mag.
    /// of 1) with a random angle. */
    /// </summary>
    private void Randomize(){
        for (int i = 0; i < Genes.Length; i++)
        {
            Genes[i] = getRandomGene();
        }
    }

    #endregion
}