using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDamageable
{
    public GameObject bulletPrefab;
    public GameManager manager;
    public PathHandler handler;
    SpriteRenderer rend;
    public bool stickToPath;
    public float baseSpeed = 2f;
    public float dashSpeed = 8f;
    public bool finished = false;

    int posId = -1;

    //stat that changes  a lot
    public float speed = 2f;

    Vector2 targetPos;
    Rigidbody2D rb;

    public float bulletSpeed = 3f;
    public float shootDelay = 1f;

    public float dashDelay = 2f;
    public float dashDur = 0.2f;

    //timer class/struct?
    float lastDash;
    float lastShot;

    public Rigidbody2D GetRB()
    {
        return rb;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPos = rb.position;
        lastShot = Time.time;
        lastDash = Time.time;
        rend = GetComponent<SpriteRenderer>();
    }

    public IEnumerator DoDash()
    {
        speed = dashSpeed;
        rend.color = Color.red;
        yield return new WaitForSeconds(dashDur);
        speed = baseSpeed;
        rend.color = Color.white;
    }

    public void OnRoundStart(Vector2 startPos)
    {
        rb.position = startPos;
        targetPos = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && manager.roundActive && Time.time > lastShot + shootDelay)
        {
            lastShot = Time.time;
            Shoot();
        }
        if(Input.GetKeyDown(KeyCode.Space) && manager.roundActive && Time.time > lastDash + dashDelay)
        {
            lastDash = Time.time;
            StartCoroutine(DoDash());
        }
    }

    void Shoot()
    {
        Bullet newBullet = Object.Instantiate(bulletPrefab, this.transform.position, Quaternion.identity, null).GetComponent<Bullet>();
        Vector2 shootDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        shootDir = shootDir.normalized * bulletSpeed;
        newBullet.SetStats(1f, 0);
        newBullet.Shoot(shootDir, this, new IDamageable.AlignmentMask(IDamageable.Alignment.Enemy));
    }

    private void FixedUpdate()
    {
        float movementLeft = speed;

        if(!finished && stickToPath && handler != null && handler.pathReady)
        {
            while(Vector2.Distance(rb.position,targetPos) < movementLeft * Time.fixedDeltaTime)
            {
                rb.position = targetPos;
                if (posId + 1 < handler.path.Length)
                {
                    posId += 1;
                    targetPos = handler.path[posId];
                }
                else
                {
                    finished = true;
                    manager.roundActive = false;
                    rb.velocity = Vector2.zero;
                    return;
                }
            }
            rb.velocity = (targetPos - rb.position).normalized * movementLeft;
        }
    }

    public IDamageable.Alignment GetAlignment()
    {
        return IDamageable.Alignment.Player;
    }

    public void Damage(float amount, IDamageable source)
    {
        Debug.Log("player damaged by: "+amount);
    }
}
