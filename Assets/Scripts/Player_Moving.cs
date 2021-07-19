using UnityEngine;
using Zenject;


public class Player_Moving : MonoBehaviour
{
    [SerializeField] int speed = 5;

    Transform startPoint;
    Transform finishPoint;

    Rigidbody2D _rigidbody;
    AudioSource _audioSourse;
    Animator _animator;

    GameState _gameState;

    [Inject]
    private void Construct(Transform[] points, GameState gameState)
    {
        startPoint = points[0];
        finishPoint = points[1];
        _gameState = gameState;
    }

    private void Start()
    {
        _audioSourse = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (_gameState.state)
        {
            case State.Start:
                transform.position = startPoint.position;
                _gameState.ChangeState(State.Game);
                break;
            case State.Game: Move(); break;
            case State.Finish: MoveToFinish(); break;
        }
    }

    public void Move()
    {
        float X = Input.GetAxis("Horizontal");
        float Y = Input.GetAxis("Vertical");
        Vector2 move = transform.right * X + transform.up * Y;
        _rigidbody.velocity = (move * speed);

        if (X != 0 || Y != 0)
            PlaySoundAndAnimation(true);
        else
            PlaySoundAndAnimation(false);
    }

    void MoveToFinish()
    {
        transform.position = Vector2.MoveTowards(transform.position, finishPoint.position, speed * Time.deltaTime);
        PlaySoundAndAnimation(true);
        if (Vector2.Distance(transform.position, finishPoint.position) <= 0)
        {
            PlaySoundAndAnimation(false);
        }
    }

    void PlaySoundAndAnimation(bool isPlay)
    {
        if (isPlay)
        {
            if (!_audioSourse.isPlaying) _audioSourse.PlayOneShot(_audioSourse.clip);
        }
        else
        {
            _audioSourse.Stop();
        }
        _animator.SetBool("isMove", isPlay);
    }
}
