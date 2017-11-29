using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField] private Transform[] gunsTransformList;
    [SerializeField] private float time2Fire = 2.0f;
    [SerializeField] private float bulletVelocity = 8.0f;
    [SerializeField] private GameObject bulletPrefab;
    private int Timer = 5;
    private float life = 1;
    private GameManager gameManager;
    private Animator EnnemyAnimationController;
    // Use this for initialization
    void Start () {
        StartCoroutine(Fire());
        gameManager = FindObjectOfType<GameManager>();
        EnnemyAnimationController = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    { 
       
    }

    private IEnumerator Damage()
    {
        for(int hit = 0;hit < Timer; hit++)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(.1f);
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(.1f);

        }
    }
    private IEnumerator Fire()
    {
        while(true)
        {
            yield return new WaitForSeconds(time2Fire);
            foreach(Transform t in gunsTransformList)
            {
                GameObject bullet = Instantiate(bulletPrefab, t.position, t.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = t.right * bulletVelocity;
                Destroy(bullet, 2);
               
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            life--;
            Destroy(collision.gameObject);
            StartCoroutine(Damage());
            Debug.Log("Message : Enemy Take damage");
            if (life == 0)
            {
                Destroy(this.gameObject);

            }
        }
    }
}
