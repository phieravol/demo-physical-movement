using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //declare some properties
    public GameObject Player;
    public Rigidbody2D rb;
    public float horizontalInput { get; set; }
    public float verticalInput { get; set; }
    public bool isGround;


    // Start is called before the first frame update
    void Start()
    {
        //get game object
        Player = GameObject.Find("Player");
        //get init rigidbody2D for game object
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //move player horizontal
        if (horizontalInput == 1f || horizontalInput == -1f)
        {
            Vector2 forceHorizontal = new Vector2(10f, 0f);
            rb.AddForce(forceHorizontal * horizontalInput);
        }

        //move player vertical, player can only jump when they are ground
        if((verticalInput == 1f || verticalInput == -1f) && isGround)
        {
            Vector2 forceVertical = new Vector2(0f, 5f);
            rb.AddForce(forceVertical * verticalInput, ForceMode2D.Impulse);
        }

    }
    /**
     * check if player is grounding
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ground")
        {
            isGround = true;
        }
    }

    /**
     * check if player is not grounding
     */
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isGround = false;
        }
    }
}
