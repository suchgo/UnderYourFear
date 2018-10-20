using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour {

    public Image healthbar, mindbar, satietybar;
    public int health, mind, satiety, mindDamage;
    public GameObject mainCharacter;
    public bool doorIsSelected = false, ghostIsActive = false;
    public Rigidbody2D rb2D;
    Animator animator;
    bool stuck = false;
    Vector2 targetPosition;
    float speed = 150f, healthTimer = 0, mindTimer = 0, satietyTimer = 0;

    // Use this for initialization
    void Start () {
        rb2D = mainCharacter.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        IsStuck();
        healthTimer = mindTimer = satietyTimer = Time.time;
        health = mind = satiety = 100;
    }
	
	// Update is called once per frame
	void Update () {
        HealthUpdate();
        HealthBarUpdate();
        SatietyUpdate();
        SatietyBarUpdate();
        MindUpdate();
        MindBarUpdate();
        BringigngStats();
        Die();
        CharacterAnimation();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    void HealthUpdate()
    {
        if (mind == 0 || satiety == 0)
        {
            if (Time.time - healthTimer >= 5)
            {
                health -= 5;
                healthTimer = Time.time;
            }
        }
        else
            healthTimer = Time.time;
    }

    void MindUpdate()
    {
        if (ghostIsActive)
        {
            if (mind > 0)
            {
                if (Time.time - mindTimer >= 5)
                {
                    mind -= mindDamage;
                    mindTimer = Time.time;
                }
            }
        }
        else
            mindTimer = Time.time;
    }

    void SatietyUpdate()
    {
        float fastingRate = 20f;
        if (satiety > 0)
        {
            if (Time.time - satietyTimer >= fastingRate)
            {
                satiety -= 5;
                satietyTimer = Time.time;
            }
        }
    }

    void HealthBarUpdate()
    {
        healthbar.fillAmount = health / 100f;
    }

    void MindBarUpdate()
    {
        mindbar.fillAmount = mind / 100f;
    }

    void SatietyBarUpdate()
    {
        satietybar.fillAmount = satiety / 100f;
    }

    void BringigngStats()
    {
        if (health < 0)
            health = 0;
        else
        if (health > 100)
            health = 100;
        if (mind < 0)
            mind = 0;
        else
        if (mind > 100)
            mind = 100;
        if (satiety < 0)
            satiety = 0;
        else
        if (satiety > 100)
            satiety = 100;
    }

    void Die()
    {
        if (health == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void CharacterAnimation()
    {
        if (rb2D.velocity == Vector2.zero)
            animator.SetBool("isMove", false);
        else
            animator.SetBool("isMove", true);
        if (GetComponent<Animator>().GetBool("isMove") == false)
            GetComponent<SpriteRenderer>().flipX = false;
    }

    void MoveCharacter()
    {
        if (Mathf.Abs(mainCharacter.transform.position.x - targetPosition.x) <= 0.05f)
        {
            rb2D.velocity = Vector2.zero;
            return;
        }
        if (!stuck)
        {
            if (targetPosition.x < mainCharacter.transform.position.x)
            {
                rb2D.velocity = Vector2.left * Time.deltaTime * speed;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                rb2D.velocity = Vector2.right * Time.deltaTime * speed;
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        IsStuck();
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        stuck = false;
    }

    public void MoveTo(Vector2 target)
    {
        targetPosition = target;
        stuck = false;
    }

    public void IsStuck()
    {
        stuck = true;
        targetPosition = new Vector2(mainCharacter.transform.position.x, mainCharacter.transform.position.y);
        rb2D.velocity = Vector2.zero;
    }
}
