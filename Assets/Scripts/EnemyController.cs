using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _enemySpeed;
    [SerializeField] private float _enemyRotationSpeed;
    [SerializeField] private Vector3 _relativePosition;


    private Vector3 startPosition;
    private Vector3 endPosition;


    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + _relativePosition;
    }

   
    void Update()
    {
        if (_enemySpeed > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, _enemySpeed * Time.deltaTime);
            if (transform.position == endPosition)
            {
                endPosition = startPosition;
                startPosition = transform.position;
            }

            if (_enemyRotationSpeed > 0)
            {
                transform.Rotate(new Vector3(0, 0, _enemyRotationSpeed * Time.deltaTime));
            }
        }
    }
}
