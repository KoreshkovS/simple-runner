using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 0;
    [SerializeField] private float _jumpSpeed = 0;
    [SerializeField] private GameObject _winnerText;


    private Rigidbody2D body;
    private bool _facingRight = true;
    private Animator animator;
    private bool isGameOvered;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

 
    void Update()
    {
        var horizontalMovement = Input.GetAxis("Horizontal");

        if (horizontalMovement != 0)
        {
            animator.SetInteger("Speed", 1);
            body.position += new Vector2(horizontalMovement * _speed * Time.deltaTime, 0);
        }
        else
        {
            animator.SetInteger("Speed", 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            
            body.AddForce(new Vector2(0, _jumpSpeed), ForceMode2D.Impulse);
        }
        animator.SetBool("Jump", !IsGrounded());
        if (Input.GetAxis("Horizontal") > 0 && !_facingRight)
        {
            Flip();
        }
        else if (Input.GetAxis("Horizontal") < 0 && _facingRight)
        {
            Flip();
        }

        if (transform.position.y < -4)
        {
            GameOver();
        }

    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private bool IsGrounded()
    {
        var raycast = Physics2D.Raycast(transform.position - transform.localScale / 2, Vector2.down, 0.1f);
        return raycast.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameOver();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameWinner();
    }
    private void GameOver()
    {
        if (isGameOvered)
        {
            return;
        }
        isGameOvered = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GameWinner()
    {
        _winnerText.SetActive(true);
        Destroy(gameObject, 3);
    }
    private void OnDestroy()
    {
        GameOver();
    }
}
