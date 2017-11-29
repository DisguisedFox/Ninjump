using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class PlayerController : MonoBehaviour {

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    Rigidbody2D rb;

    [Header("Physics")]
    [SerializeField] private float force = 10;
    [Range(0.0f,10.0f)  ]
    [Header("Jumps")]
    
    [SerializeField]private float forceJump = 5;
    [SerializeField] private float vitMax = 10;
    [SerializeField] private Transform positionRaycastJump; // Position du détecteur de collision
    [SerializeField] private float radiusRaycastJump; // Rayon de la détection de collision
    [SerializeField] private LayerMask layerMaskJump; // Layout physique de l'objet

    [Header("sounds")]
    [SerializeField]
    private AudioClip[] Audioclip;
    [Header("Fire gun")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform GunTransform; // Pour la sortie du tir
    [SerializeField] private float bulletVelocity = 5.0f; // Vitesse de la balle 
    [SerializeField] private float time2Fire = 0.0f; // Cooldown de tir
    private float lastTimeFire = 0; // Dernier moment ou on a tiré pour la derniere fois 

    private Rigidbody2D rigid;
    private Transform spawnTransform;
    private Animator PlayerAnimationController;
    private SpriteRenderer render;

    private GameManager gameManager;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        spawnTransform = GameObject.Find("Spawn").transform;
        gameManager = FindObjectOfType<GameManager>();
        PlayerAnimationController = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();

    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        float horizontalInput = Input.GetAxis("Horizontal");
        render.flipX = horizontalInput < 0;
        PlayerAnimationController.SetFloat("SpeedX", Mathf.Abs(horizontalInput));
        PlayerAnimationController.SetFloat("SpeedY", rigid.velocity.y);
        
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
            else if (rb.velocity.y > 0 && !Input.GetButton ("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        
        
      Vector2 forceDirection = new Vector2(horizontalInput, 0);
        forceDirection *= 10;
        rigid.AddForce(forceDirection);
        
        if (Mathf.Abs(rigid.velocity.x) > vitMax)
            {
                rigid.velocity = new Vector2(5.0f * Mathf.Sign(rigid.velocity.x), rigid.velocity.y);
            }
      
        bool touchFloor = Physics2D.OverlapCircle(positionRaycastJump.position, radiusRaycastJump, layerMaskJump);
        if (Input.GetAxis("Jump") > 0 && touchFloor)
        {
            PlayerAnimationController.SetTrigger("Jumped");
            rigid.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
        }else
        {
            PlayerAnimationController.SetBool("IsGrounded", touchFloor);
        }
        if(Input.GetAxis("Fire1") > 0)
        {
            PlayerAnimationController.SetTrigger("Attack");
            Fire();
        }
        PlayerAnimationController.SetBool("IsGrounded", touchFloor);
	}
       
    private  void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Heart")
        {
            gameManager.Vie();
            Debug.Log("Message : you win one life");
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Coin")
        {
            gameManager.Coins();
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Finish")
        {
            gameManager.PlayerWin();
        }

        if (collision.tag == "Limit")
        {
            new WaitForSeconds(2);
            transform.position = spawnTransform.position;
            gameManager.PlayerDie();
        }
        if (collision.tag == "BulletEnemy")
        {
            gameManager.PlayerDie();
        }
    }

    private void Fire()
    {
        if(Time.realtimeSinceStartup - lastTimeFire > time2Fire)
        { 
            GameObject bullet = Instantiate(bulletPrefab, GunTransform.position, GunTransform.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = GunTransform.right * bulletVelocity; 
            Destroy(bullet, 5);
            lastTimeFire = Time.realtimeSinceStartup;
        }
    }
}
