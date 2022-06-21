using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody2D rb;
    private float movementSpeed;
    private float moveHorizontal;
    private float moveVertical;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        movementSpeed = 3f;
    }

    private void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb.rotation += movementSpeed;
        }
            //rb.AddForce(new Vector2(moveHorizontal * movementSpeed, 0f), ForceMode2D.Impulse);

        if (moveVertical > 0.1f || moveVertical < -0.1f)
        {
            rb.rotation -= movementSpeed;
        }
            //rb.AddForce(new Vector2(0f, moveVertical * movementSpeed), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        Debug.Log("Hit detected !!!");
        Destroy(collision.gameObject);
        this.gameObject.SetActive(false);

        moveHorizontal = 0f;
        moveVertical = 0f;
        movementSpeed = 0f;

        */
    }
}