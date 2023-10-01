using UnityEngine;

public enum ProjectileType
{
    Chair,
    Bird
}

public class Projectile : MonoBehaviour
{
    protected Transform _transform;
    protected Rigidbody _rb;

    [SerializeField] protected float _speed = 5f;
    [SerializeField] protected int _damage;

    [SerializeField] protected GameObject _explosion;
    
    protected Vector3 _trajectory;

    protected Vector3 _targetPosition;

    [SerializeField] protected float _lifeTime = 3f;
    private float _livingTime = 0f;
    //
    // [SerializeField] protected float _minRotationValue = 100f;
    // [SerializeField] protected float _maxRotationValue = 300f;
    //
    public int Damage => _damage;

    protected ProjectileType _projectileType;
    private SoundPlayer _soundPlayer;

    protected virtual void Awake()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        _soundPlayer = FindObjectOfType<SoundPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        _livingTime += Time.deltaTime;

        if (_livingTime >= _lifeTime)
        {
            Destroy(this.gameObject);
        }
        
        Anim();
    }

    protected virtual void FixedUpdate()
    {
        _rb.velocity = _trajectory;
    }

    protected virtual void Anim()
    {
        transform.Rotate(transform.right,  Random.Range(100f, 300f) * Time.deltaTime);
        transform.Rotate(transform.forward, Random.Range(100f, 300f) * Time.deltaTime);
    }
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Obstacle>() || other.gameObject.GetComponent<Player>())
        {
            switch (_projectileType)
            {
                case ProjectileType.Chair:
                    _soundPlayer.PlaySound(SoundType.BreakChair);
                    break;
                case ProjectileType.Bird:
                    _soundPlayer.PlaySound(SoundType.BreakBird);
                    break;
            }
            
            Instantiate(_explosion.gameObject, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}
