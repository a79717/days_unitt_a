using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Animator aniPlayer;
    private SpriteRenderer sprPlayer;
    private float hp = 100;
    private float hpMax;
    private Image hpbar;
    private bool dead;
    private GameManager gm;

    [Header("移動速度"), Range(0, 1000)]
    public float speed = 10;

    /// <summary>
    /// 移動方法:角色移動、動畫與翻轉
    /// </summary>
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");

        transform.Translate(speed * h * Time.deltaTime, 0, 0);

        aniPlayer.SetBool("跑步開關", h != 0);

        if (Input.GetKeyDown(KeyCode.A))
        {
            sprPlayer.flipX = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            sprPlayer.flipX = false;
        }
    }
    
    /// <summary>
    /// 死亡方法:播放死亡動畫、關閉腳本
    /// </summary>
    private void Dead()
    {
        aniPlayer.SetTrigger("死亡觸發");
        this.enabled = false;
        dead = true;
        gm.GameOver();
    }

    private void Start()
    {
        aniPlayer = GetComponent<Animator>();
        sprPlayer = GetComponent<SpriteRenderer>();
        hpbar = GameObject.Find("血條").GetComponent<Image>();
        gm = FindObjectOfType<GameManager>();

        hpMax = hp;

    }
    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dead) return;

        if (collision.tag == "陷阱")              //如果碰到物件.標籤 為 "陷阱"
        {
            hp -= 20;                             //血量 -= 20
            hpbar.fillAmount = hp / hpMax;        //血條.長度 = 血量 / 血量最大值

            if (hp <= 0) Dead();
        }
        if (collision.tag == "鑽石")              //如果 碰到物件.標籤為"鑽石"
        {
            hp += 5;                              //血量 += 5
            hp = Mathf.Clamp(hp, 0, hpMax);       //數學.夾住(血量.0.血量最大值)
            hpbar.fillAmount = hp / hpMax;        //血條.長度 = 血量 / 血量最大值)
        }

        Destroy(collision.gameObject);             //刪除(碰到物件.遊戲物件)
    }
}
