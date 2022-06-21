using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    Vector2[] positions;
    GameObject[] goals;
    const int NUMBER_OF_GOALS = 11;

    public GameObject goal;

    // Start is called before the first frame update
    void Start()
    {
        positions = new Vector2[NUMBER_OF_GOALS] { new(-631, 10), new(-681, -340), new(-184, -172), 
            new(-83, 161), new(-134, 364), new(186, 178), 
            new(794, 351), new(650, -7), new(456, -273), 
            new(667, -370), new(806, 433) };

        for (int i = 0; i < NUMBER_OF_GOALS; i++)
        {
            GameObject mazeGoal = GameObject.Find("Maze/Goal");

            GameObject tmpBall = Instantiate(goal, positions[i], Quaternion.identity, mazeGoal.transform);

            tmpBall.name = "BallClone" + (i + 1);

            //Debug.Log($"Create New Ball ({i}) => DOT");

            goal = tmpBall;

            goals[i] = goal;

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
