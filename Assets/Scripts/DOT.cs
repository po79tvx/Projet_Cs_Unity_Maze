using UnityEngine;

/// <summary>
/// Create a new dot
/// </summary>
/// <typeparam name="T">Any type of param</typeparam>
public class DOT : MonoBehaviour
{
    // Vectors
    public Vector2 pos = new (0f, 0f);// Position

    public Vector2 acc = new (1f, 1f);// Acceleration

    //public GameObject ball;
    public GameObject ballObject;

    private GameObject ballClone;

    //private GameObject ballContainer;
    private DOT newScript;

    public DNA Brain;

    private bool hitWall = false;

    public bool isDead = false;
    private bool isOnGoal = false;

    private bool isBest = false;

    public float Fitness = 0.0f;

    public GameObject Ball;

    private Rigidbody2D rb;

    public DOT(DNA brain)
    {
        Brain = brain;
        brain.fitnessFunction = CalculateFitness;

        pos = new Vector2(0f, 0f);

        acc = new Vector2(1f, 1f);

        Debug.Log("New DOT => DOT");
    }

    public void DotInteractConstructor(DNA brain)
    {
        Brain = brain;
        brain.fitnessFunction = CalculateFitness;

    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void MoveDot()
    {
        if (Brain.Genes.Length > Brain.step)
         {
             acc = Brain.Genes[Brain.step];
             Brain.step += 1;
         }
         else
         {
             isDead = true;
             CalculateFitness();
         }

        // Add the acceleration to velocity
        //rb.velocity = acc;
        //rb.AddRelativeForce(acc);

        //rb.angularVelocity = acc.y;

        rb.velocity = new Vector2(acc.x, acc.y);

        //Debug.Log($"Velocity : ({vel}) => Dot");

        // Update the position of the dot.
        //rb.position += pos;
        rb.AddRelativeForce(pos,ForceMode2D.Force);

        //Debug.Log($"Position : ({pos}) => Dot");
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
        if (collision == null  || collision.gameObject.CompareTag("Ball"))
        {
            hitWall = false;
        }
        else
        {
            hitWall = true;

            this.gameObject.SetActive(false);

            rb.velocity = new Vector2(0f,0f);
            acc = new Vector2(0f, 0f);
            pos = new Vector2(0f, 0f);

            GameObject maze = GameObject.Find("Maze/IA");

            Vector2 ballLocation = new(maze.transform.position.x + -806, maze.transform.position.y + 328);

            rb.position = ballLocation;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="i"></param>
    /// <param name="newDot"></param>
    /// <returns></returns>
    public GameObject CreateNewBall(int i ,GameObject ball)
    {
        try
        {
            GameObject tmpBall = GameObject.Instantiate(ball);

            tmpBall.name = "BallClone" + (i + 1);

            tmpBall.transform.SetParent(GameObject.Find("Maze/IA").transform);

            Debug.Log($"Create New Ball ({i}) => DOT");

            ballObject = tmpBall;

            return tmpBall;
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
        if (Brain.Fitness == 0)
        {
            float distanceToGoal = Vector2.Distance(this.pos, GameObject.Find("Goal").transform.position);
        
            /* If the dot has just run out of steps to make, or it has actually reached the goal then calculate the fitness. */
            if (isDead || isOnGoal)
            {
                Brain.Fitness = 1.0f / (distanceToGoal * distanceToGoal + (int)Mathf.Pow(Brain.step, 2));
            }
             /*If the dot has hit a wall or obstacle, then its fitness is calculated as a number
               effectively 0 in size. I can't remember why I didn't use fitness = 0, but there was
               a good reason for me to make it an unrealistically small number instead. */
            else
            {
                Brain.Fitness = 0.001f;
            }
        }

        return Brain.Fitness = 0.00001f;
    }
}