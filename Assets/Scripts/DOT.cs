using UnityEngine;

/// <summary>
/// Create a new dot
/// </summary>
/// <typeparam name="T">Any type of param</typeparam>
public class DOT : MonoBehaviour
{
    /// <summary>
    /// Attributs of the DOT
    /// </summary>
    #region Attributs

    // Vectors
    public Vector2 pos = new (0f, 0f);// Position
    public Vector2 acc = new (1f, 1f);// Acceleration

    [Range (0,50)]
    public Vector2 vel = new(0f, 0f);// Velocity

    public bool hitWall;
    private bool isOnGoal, isBest;
    public float Fitness = 0.0f;
    public int index;

    public GameObject ballObject;
    public DNA Brain;
    public GameObject Ball;
    private Rigidbody2D rb;
    private Algorithm algo;

    #endregion

    /// <summary>
    /// Constructor of the DOT
    /// </summary>
    #region Constructor
    public DOT(DNA brain)
    {
        Brain = brain;
        brain.fitnessFunction = CalculateFitness;

        pos = new Vector2(0f, 0f);

        acc = new Vector2(1f, 1f);

        vel = new Vector2(0f, 0f);

        //Debug.Log("New DOT => DOT");
    }
    #endregion

    /// </summary>
    /// Functions of the DOT
    /// </summary>
    #region Functions

    /// <summary>
    /// Passing parameters to the constructor
    /// </summary>
    /// <param name="brain">The new brain of the DOT</param>
    public void DotInteractConstructor(DNA brain, Algorithm algo, int index)
    {
        Brain = brain;
        this.algo = algo;
        this.index = index;
        brain.fitnessFunction = CalculateFitness;
    }

    /// <summary>
    /// When the dot start
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        Brain.step = 0;
    }

    /// <summary>
    /// Move the dot
    /// </summary>
    public void MoveDot()
    {
        if (Brain.Genes.Length > Brain.step)
         {
             acc = Brain.Genes[Brain.step];
             Brain.step += 1;
         }
         else
         {
             CalculateFitness();
         }

        // Add the acceleration to velocity
        //vel -= new Vector2(acc.x, acc.y) * Time.deltaTime;

        rb.velocity -= new Vector2(acc.x, acc.y) * Time.deltaTime;

        // Update the position of the dot.
        rb.AddRelativeForce(pos,ForceMode2D.Force);
    }


    /// <summary>
    /// Moves the dot and checks if the dot has hit a wall etc. If so, it is killed.
    /// Also checks to see if it has reached the goal.
    /// </summary>
    private void Update()
    {
        /* Only update if the dot is still moving. */
        if (!isOnGoal && !hitWall)
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
    /// Is the DOT collided ?
    /// </summary>
    /// <param name="collision">With what the ball collided</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null || collision.gameObject.CompareTag("Ball"))
        {
            hitWall = false;
        }
        else
        {
            /*
            gameObject.SetActive(false);

            algo.oldPopulation.Add(this);

            hitWall = true;
            */

            algo.oldPopulation[index] = this;

            gameObject.SetActive(false);

            rb.velocity = new Vector2(0f, 0f);
            acc = new Vector2(0f, 0f);
            pos = new Vector2(0f, 0f);

            GameObject maze = GameObject.Find("Maze/IA");

            Vector2 ballLocation = new(maze.transform.position.x + -806, maze.transform.position.y + 328);

            rb.position = ballLocation;

        }

        if (!isActiveAndEnabled)
        {
            hitWall = true;
            CalculateFitness();
            //Brain.Genes[Brain.step] = Brain.getRandomGene();

            //Debug.Log($"Fitness de ({index}) == ({Brain.Fitness}) => DOT");
        }
    }

    /// <summary>
    /// Create a new ball
    /// </summary>
    /// <param name="i">Index in the array</param>
    /// <param name="ball">The ball to create</param>
    /// <returns></returns>
    public GameObject CreateNewBall(int i ,GameObject ball)
    {
        try
        {
            GameObject maze = GameObject.Find("Maze/IA");

            Vector2 ballLocation = new(maze.transform.position.x + -806, maze.transform.position.y + 328);

            GameObject tmpBall = Instantiate(ball,ballLocation, Quaternion.identity,maze.transform);
            
            tmpBall.name = "BallClone" + (i + 1);

            //Debug.Log($"Create New Ball ({i}) => DOT");

            ballObject = tmpBall;

            return tmpBall;
        }
        catch (System.NullReferenceException)
        {
            //Debug.Log($"Create New Ball ({i}) [Error] => DOT");

            return null;
        }
    }

    /// <summary>
    /// Is the ball collided
    /// </summary>
    /// <returns>Is the ball collided ?</returns>
    private bool IsCollided()
    {
        return hitWall;
    }

    /// <summary>
    /// This is the fitness function for this algorithm. It uses the distance to the goal,
    /// along with the speed at which it reached the goal.
    /// </summary>
    /// <returns></returns>
    public float CalculateFitness()
    {
        if (Brain.Fitness == 0)
        {
            float distanceToGoal = Vector2.Distance(transform.position, GameObject.Find("Goal").transform.position);
        
            /* If the dot has just run out of steps to make, or it has actually reached the goal then calculate the fitness. */
            if (hitWall || isOnGoal)
            {
                //return Brain.Fitness = 1.0f / (distanceToGoal * distanceToGoal) + ((Brain.step / Brain.genesSize)*100);
                //return Brain.Fitness = 1.0f / Mathf.Pow(distanceToGoal, 2) * Brain.step;
                //return Brain.Fitness = 1.0f / Mathf.Pow(distanceToGoal ,2) + Mathf.Pow(2, Brain.step);
                //return Brain.Fitness = 1.0f / ((distanceToGoal * distanceToGoal) / (int)Mathf.Pow(Brain.step, 2));
                return Brain.Fitness = 1.0f / (distanceToGoal * distanceToGoal + (int)Mathf.Pow(Brain.step, 2));

            }
             /*If the dot has hit a wall or obstacle, then its fitness is calculated as a number
               effectively 0 in size. I can't remember why I didn't use fitness = 0, but there was
               a good reason for me to make it an unrealistically small number instead. */
            else
            {
                return Brain.Fitness = 0.00000000001f;
            }
        }

        //Debug.Log($"Brain Fitness ({index}) --> ({Brain.Fitness})=> DOT");

        return Brain.Fitness = 0f;
    }
    #endregion
}