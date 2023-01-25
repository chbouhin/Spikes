using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Animator animator;
    [SerializeField] private BuildSpikesManager buildSpikesManager;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private TextMesh score;
    private bool lookRight = true;
    private float speed = 4f;
    private float speedAcceleration = 0.1f;
    private float jumpForce = 6f;
    private bool isPlaying = false;
    private float playerScale;

    private void Start()
    {
        playerScale = transform.localScale.x;
    }

    private void Update()
    {
        if (!isPlaying)
            return;
        Move();
        if (Input.GetMouseButtonDown(0))
            Jump();
        LookForwards();
    }

    public void Init()
    {
        animator.Play("Idle");
        score.text = "0";
        speed = 4f;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = new Vector3(playerScale, playerScale, 1f);
    }

    public void StartMoving()
    {
        audioManager.Play("Play");
        rb.simulated = true;
        rb.velocity = Vector2.zero;
        isPlaying = true;
        lookRight = true;
        particle.Play();
        Jump();
    }

    public void ChangeDirection()
    {
        audioManager.Play("TouchSide");
        speed += speedAcceleration;
        buildSpikesManager.NewSpikes();
        gameManager.level++;
        score.text = gameManager.level.ToString();
        lookRight = !lookRight;
        if (lookRight)
            transform.localScale = new Vector3(playerScale, playerScale, 1f);
        else
            transform.localScale = new Vector3(-playerScale, playerScale, 1f);
    }

    public void Die()
    {
        audioManager.Play("TouchSpikes");
        animator.SetTrigger("Dead");
        rb.simulated = false;
        isPlaying = false;
        particle.Stop();
        gameManager.GameOver();
    }

    private void Move()
    {
        if (lookRight)
            transform.position += Vector3.right * speed * Time.deltaTime;
        else
            transform.position -= Vector3.right * speed * Time.deltaTime;
    }

    private void Jump()
    {
        audioManager.Play("Jump");
        animator.SetTrigger("Close_Open");
        rb.velocity = Vector3.up * jumpForce;
    }

    private void LookForwards()
    {
        Vector2 targetDirection = Vector2.right + rb.velocity / 5;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        if (!lookRight)
            targetAngle = -targetAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
    }
}
