using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private GameObject goalObject;
    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    { }

    /// <summary>
    /// Create a new ball
    /// </summary>
    /// <param name="i">Index in the array</param>
    /// <param name="ball">The ball to create</param>
    /// <returns></returns>
    public GameObject CreateNewGoal(int i, GameObject goal, Vector2 position)
    {
        try
        {
            //Vector2 ballLocation = new(maze.transform.position.x + -806, maze.transform.position.y + 328);
            GameObject mazeGoal = GameObject.Find("Maze/Goal");

            GameObject tmpGoal = Instantiate(goal, new Vector2(mazeGoal.transform.position.x + position.x, mazeGoal.transform.position.y + position.y), Quaternion.identity, mazeGoal.transform);

            tmpGoal.name = "Goal" + (i + 1);

            goalObject = tmpGoal;

            return tmpGoal;
        }
        catch (System.NullReferenceException)
        {
            return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null || collision.gameObject.CompareTag("Goal"))
            isActive = false;
        else
            isActive = true;

    }

    public bool IsActive()
    {
        return isActive;
    }

}
