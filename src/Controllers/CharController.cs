using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {
    public int maxHealth = 100;
    public bool isAfflicted = false;
    public bool isGrounded = false;
    public bool isJumping = false;
    public bool facingRight = true;
    public bool canMove = true, canShoot = true, knockedBack = false;
    public float timeScale = 1;
    public LayerMask groundLayer; // Insert the layer here

    //public Dictionary<string, bool> keyDict;

    protected int health;
    protected bool isDead = false;
    protected bool isHealing = false;
    protected float moveSpeed = 5f;
    protected float jumpHeight = 11f;
    protected float jumpMaxSpeed;

    protected Animator anim;
    protected Rigidbody2D rb;

    public void Start()
    {
        //anim = GetComponent<Animator> ();
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    public void Update()
    {
        GameManager.ins.checkPlayerVisibility(transform);
    }

    public void OnDestroy()
    {
        
    }

    public void FaceRight(bool right)
    {
        facingRight = right;
        GetComponent<SpriteRenderer>().flipX = !right;
    }

	public void moveHorizontal(float amount, bool faceDirection, float reactivityPercent = 1.0F)
    {
        if (canMove && !knockedBack)
        {
			rb.velocity = new Vector2(moveSpeed * (amount * reactivityPercent), rb.velocity.y);
			           
        }
    }

    public void setHealth(int newHealth)
    {
		if (newHealth >= maxHealth) // cannot go over max health
			health = maxHealth;
		else
        	health = newHealth;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public virtual void takeDamage(int damage, bool right)
    {
        setHealth(health - damage);
    }

    public void Die()
    {
        isDead = true;
        health = 0;
        /*Vector3 currentVelocity = rb.velocity;
        GameObject deadBody = Instantiate((GameObject)Resources.Load("Prefabs/DeadChar"), transform.position, transform.rotation);
        if (!facingRight)
        {
            deadBody.GetComponent<SpriteRenderer>().flipX = true;
        }
        deadBody.GetComponent<Rigidbody2D>().velocity = currentVelocity;
        deadBody.layer = gameObject.layer;
        if (this.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.ins.gameOver = true;
            Camera.main.GetComponent<Camera2DFollow>().target = deadBody.transform;
            deadBody.AddComponent<AudioListener>();
        }
        else if (this.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            GameManager.ins.level.enemies.Remove(gameObject);
        }*/
        Destroy(this.gameObject);
    }

    public int getHealth()
    {
        return health;
    }

	public int getMaxHealth() {
		return maxHealth;
	}

    public bool isAlive()
    {
        return !isDead;
    }

	public bool isFacingRight() {
		return facingRight;
	}

    public IEnumerator HealDelay(int amount, float seconds)
    {
        if (!isHealing)
        {
            isHealing = true;
            yield return new WaitForSeconds(seconds);
            if (isHealing)
            {
                setHealth(health + amount);
                isHealing = false;
            }
        }
    }

    public IEnumerator DelayKnockback(float recoil)
    {
        knockedBack = true;
        yield return new WaitForSeconds(recoil);
        knockedBack = false;
    }
}
