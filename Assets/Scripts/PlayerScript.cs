using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    public GameObject Player;
    public Rigidbody2D RbPlayer;

    public GameObject Ball;
    public Rigidbody2D RbBall;

    public float InputHorizontal;
    public float InputVertical;

    public float StrengthHorizontal = 20f;
    public float StrengthVertical = 5f;

    public bool isGround;

    public float StrenthKickBall = 20f;


    // Start is called before the first frame update
    void Start()
    {
        //Step 1: get gameobject & rigidbody2d to handle
        Player = GameObject.Find("Player");
        RbPlayer = Player.GetComponent<Rigidbody2D>();
        Ball = GameObject.Find("Ball");
        RbBall= Ball.GetComponent<Rigidbody2D>();
        
    }

    void FixedUpdate()
    {
        //Step 2: get input axis when press key
        InputHorizontal = Input.GetAxis("Horizontal");
        InputVertical = Input.GetAxisRaw("Vertical");

        //Check if press horizontal key
        if (InputHorizontal != 0)
        {
            //Step 3: impact a force horizontal on the player
            Vector2 ForceHorizontal = new Vector2(StrengthHorizontal, 0f);
            RbPlayer.AddForce(ForceHorizontal * InputHorizontal, ForceMode2D.Force);
        }

        //Check if press vertical key
        if ((InputVertical == 1f || InputVertical == -1f) && isGround)
        {
            //Step 4: impact a force vertical on the player
            Vector2 ForceVertical = new Vector2(0f, StrengthVertical);
            RbPlayer.AddForce(ForceVertical * InputVertical, ForceMode2D.Impulse);
        }

        //check if user click mouse
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 ForceKickBall = new Vector2(StrenthKickBall, 0f);
            RbBall.AddForce(ForceKickBall, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isGround = true;
        }

        //prevent the player from falling
        RbPlayer.freezeRotation = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isGround = false;
        }
    }

}
