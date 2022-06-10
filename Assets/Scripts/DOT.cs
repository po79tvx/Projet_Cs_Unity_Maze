using UnityEngine;

/// <summary>
/// Create a new dot
/// </summary>
/// <typeparam name="T">Any type of param</typeparam>
public class DOT : MonoBehaviour
{
    // Vectors
    public Vector2 pos;// Position

    public Vector2 vel;// Velocity

    public Vector2 acc;// Acceleration

    //public GameObject ball;
    public GameObject ballObject;

    private GameObject ballClone;

    //private GameObject ballContainer;
    private DOT newScript;

    private DNA _brain;

    public DNA Brain
    { get { return _brain; } set { _brain = value; } }

    private bool hitWall = false;

    private bool isDead = false;
    private bool isOnGoal = false;

    private bool isBest = false;

    public float Fitness = 0.0f;

    public GameObject Ball;

    /// <summary>
    /// Create a DOT with a Brain
    /// <param name="dna">Brain of the dot</param>
    public DOT(DNA brain)
    {
        Brain = brain;
        brain.fitnessFunction = CalculateFitness;

        pos = new Vector2(-811f, 413f);

        vel = new Vector2(1f, 1f);
        acc = new Vector2(1f, 1f);

        Debug.Log("New DOT => DOT");
    }

    public void MoveDot()
    {
        /*if (Brain.Genes.Length > Brain.step)
         {
             acc = Brain.Genes[Brain.step];
             Brain.step++;
         }
         else
         {
             isDead = true;
             CalculateFitness();
         }*/

        /* Add the acceleration to velocity*/
        vel += acc;

        //Debug.Log($"Velocity : ({vel}) => Dot");

        /* Update the position of the dot. */
        pos += vel;

        //Debug.Log($"Position : ({pos}) => Dot");

        this.transform.position = pos;
    }

    /* Moves the dot and checks if the dot has hit a wall etc. If so, it is killed.
     Also checks to see if it has reached the goal. */

    private void Update()
    {
        /* Only update if the dot is still moving. */
        if (!isDead && !isOnGoal && !hitWall)
        {
            MoveDot();
            if (IsCollided())
            {
                CalculateFitness();
            }
            /* This checks to see if the dot has reached the goal. */
            else if (Vector2.Distance(this.pos, GameObject.Find("Goal").transform.position) < 5)
            {
                isOnGoal = true;
                CalculateFitness();
            }
        }

        // Debug.Log("Calculate Fitness => DNA");
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
            hitWall = false;
        else
            hitWall = true;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="i"></param>
    /// <param name="newDot"></param>
    /// <returns></returns>
    public GameObject CreateNewBall(int i, GameObject ball)
    {
        ballClone = ball;
        try
        {
            //ballClone.transform.parent = ballContainer.transform;
            ballClone.name = "BallClone" + (i);
            //ballClone.AddComponent<DOT>();

            Debug.Log($"Create New Ball ({i}) => DOT");

            return ballClone;
        }
        catch (System.NullReferenceException)
        {
            Debug.Log($"Create New Ball ({i}) [Error] => DOT");

            return null;
        }
    }

    /// <summary>
    /// Is the ball collided
    /// </summary>
    /// <returns></returns>
    private bool IsCollided()
    {
        return hitWall;
    }

    /* This is the fitness function for this algorithm. It uses the distance to the goal,
     along with the speed at which it reached the goal. */

    public float CalculateFitness()
    {
        if (Fitness == 0)
        {
            float distanceToGoal = Vector2.Distance(this.pos, GameObject.Find("Goal").transform.position);

            /* If the dot has just run out of steps to make, or it has actually reached the goal then calculate the fitness. */
            if (isDead || isOnGoal)
            {
                Fitness = 1.0f / (distanceToGoal * distanceToGoal + (int)Mathf.Pow(Brain.step, 2));
            }
            /* If the dot has hit a wall or obstacle, then its fitness is calculated as a number
               effectively 0 in size. I can't remember why I didn't use fitness = 0, but there was
               a good reason for me to make it an unrealistically small number instead. */
            else
            {
                Fitness = 0.001f;
            }
        }

        return Fitness;
    }
}