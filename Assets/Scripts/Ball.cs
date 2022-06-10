using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject ball;
    private GameObject ballClone;

    private void Start()
    {
        ball = Instantiate(ball, transform.position, Quaternion.identity) as GameObject;
    }
}