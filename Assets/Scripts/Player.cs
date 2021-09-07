using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5;

    private float x = 0;
    private float y = 0;
    private float _x = 0;
    private float _y = 0;

    public Direction direction = Direction.down;

    private bool isWalk = false;
    private bool isFirstFrame = true;

    public Texture2D moveSprite;
    private Sprite[,] moveSpriteList;
    private SpriteRenderer spriteRenderer;

    Flowchart flowchart;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameData.scene = SceneManager.GetActiveScene().name;
        flowchart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        Slice();
        transform.position = GameData.vector;
        direction = GameData.direction;
        UpadteSprite();
        NextFrame();
    }

    // 切割人物图
    private void Slice()
    {
        moveSpriteList = new Sprite[4, 3];
        int width = moveSprite.width / 3;
        int height = moveSprite.height / 4;
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                Sprite temp = Sprite.Create(moveSprite, new Rect(j * width, i * height, width, height), new Vector2(0.5f, 0.5f), Mathf.Min(width, height));
                moveSpriteList[3 - i, 2 - j] = temp;
            }
        }
    }

    void GetInpuit()
    {
        if (y == 0)
        {
            x = Input.GetAxisRaw("Horizontal");
            y = 0;
        }
        if (x == 0)
        {
            y = Input.GetAxisRaw("Vertical");
            x = 0;
        }
    }

    void SetDirection()
    {
        if (x != 0 || y != 0)
        {
            GameData.direction = direction;
        }

        if (x == 1)
        {
            direction = Direction.right;
        }
        if (x == -1)
        {
            direction = Direction.left;

        }
        if (y == 1)
        {
            direction = Direction.up;
        }
        if (y == -1)
        {
            direction = Direction.down;
        }

    }

    void UpadteSprite()
    {
        if (!isWalk)
        {
            if (direction == Direction.down)
            {
                spriteRenderer.sprite = moveSpriteList[0, 1];
            }
            else if (direction == Direction.left)
            {
                spriteRenderer.sprite = moveSpriteList[1, 1];
            }
            else if (direction == Direction.right)
            {
                spriteRenderer.sprite = moveSpriteList[2, 1];
            }
            else
            {
                spriteRenderer.sprite = moveSpriteList[3, 1];
            }
            isFirstFrame = true;
        }
        else
        {
            if (isFirstFrame)
            {
                if (direction == Direction.down)
                {
                    spriteRenderer.sprite = moveSpriteList[0, 0];
                }
                else if (direction == Direction.left)
                {
                    spriteRenderer.sprite = moveSpriteList[1, 0];
                }
                else if (direction == Direction.right)
                {
                    spriteRenderer.sprite = moveSpriteList[2, 0];
                }
                else
                {
                    spriteRenderer.sprite = moveSpriteList[3, 0];
                }
                isFirstFrame = false;
            }

            _animTimer += Time.deltaTime;
            if (_animTimer > animTime)
            {
                _animTimer -= animTime;
                NextFrame();
            }
        }

        if (y == -1)
        {
            Walk(Direction.down);
        }
        else if (x == -1)
        {
            Walk(Direction.left);
        }
        else if (x == 1)
        {
            Walk(Direction.right);
        }
        else if (y == 1)
        {
            Walk(Direction.up);
        }
        else
        {
            isWalk = false;
        }
    }

    public float animTime = 0.25f;   // 多少秒播放一帧动画
    private float _animTimer = 0f;
    private int _direction = 0;
    private int _animIndex = 0;

    void Walk(Direction d)
    {
        isWalk = true;
        int _d;
        if (d == Direction.down)
        {
            _d = 0;
        }
        else if (d == Direction.left)
        {
            _d = 1;
        }
        else if (d == Direction.right)
        {
            _d = 2;
        }
        else
        {
            _d = 3;
        }
        _direction = Mathf.Clamp(_d, 0, 3);
    }

    private void NextFrame()
    {
        if (isWalk)
        {
            _animIndex = (_animIndex + 1) % 3;
            spriteRenderer.sprite = moveSpriteList[_direction, _animIndex];
        }
    }

    void CheckCollision()
    {
        if (x == _x)
        {
            x = 0;
        }
        if (y == _y)
        {
            y = 0;
        }

        Vector2 d;
        float l;
        if (direction == Direction.down)
        {
            d = Vector2.down;
            l = 0.3f;
        }
        else if (direction == Direction.left)
        {
            d = Vector2.left;
            l = 0.2f;
        }
        else if (direction == Direction.right)
        {
            d = Vector2.right;
            l = 0.2f;
        }
        else
        {
            d = Vector2.up;
            l = 0.2f;
        }
        RaycastHit2D rayhit = Physics2D.Raycast(transform.position, d, l, 1 << LayerMask.NameToLayer("Map"));

        if (rayhit.collider != null)
        {
            if (direction == Direction.right && x == 1)
            {
                x = 0;
            }
            if (direction == Direction.left && x == -1)
            {
                x = 0;
            }
            if (direction == Direction.up && y == 1)
            {
                y = 0;
            }
            if (direction == Direction.down && y == -1)
            {
                y = 0;
            }
            isWalk = false;
        }

        RaycastHit2D rayhit2 = Physics2D.Raycast(transform.position, d, l, 1 << LayerMask.NameToLayer("Item"));

        if (rayhit2.collider != null)
        {
            
            if (Input.GetButtonDown("Submit") && flowchart.HasBlock(rayhit2.collider.name))
            {
                flowchart.ExecuteBlock(rayhit2.collider.name);
            }
        }
    }

    void Update()
    {
        if (GameData.isBusy)
        {
            return;
        }

        GetInpuit();
        SetDirection();
        UpadteSprite();
        CheckCollision();

        Vector2 p = transform.position;
        p.x += x * Time.deltaTime * speed;
        p.y += y * Time.deltaTime * speed;

        if (!p.Equals(transform.position))
        {
            transform.position = p;

            GameData.vector = transform.position;
        }
    }

    private void OnCollisionStay2D(UnityEngine.Collision2D collision)
    {
        if (Mathf.Abs(collision.contacts[0].normal.x + 1) < Time.deltaTime * speed)
        {
            _x = 1;
        }
        else if (Mathf.Abs(collision.contacts[0].normal.x - 1) < Time.deltaTime * speed)
        {
            _x = -1;
        }
        else if (Mathf.Abs(collision.contacts[0].normal.y + 1) < Time.deltaTime * speed)
        {
            _y = 1;
        }
        else if (Mathf.Abs(collision.contacts[0].normal.y - 1) < Time.deltaTime * speed)
        {
            _y = -1;
        }
        isWalk = false;
    }

    private void OnCollisionExit2D()
    {
        _x = 0;
        _y = 0;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 p = transform.position;
        Vector2 d;
        float l;
        if (direction == Direction.down)
        {
            d = Vector2.down;
            l = 0.3f;
        }
        else if (direction == Direction.left)
        {
            d = Vector2.left;
            l = 0.2f;
        }
        else if (direction == Direction.right)
        {
            d = Vector2.right;
            l = 0.2f;
        }
        else
        {
            d = Vector2.up;
            l = 0.2f;
        }
        Gizmos.DrawLine(transform.position, p + d * l);
    }
}
