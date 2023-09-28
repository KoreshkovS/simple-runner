using UnityEngine;

public class CameraCotroller : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    
    void Update()
    {
        transform.position = new Vector3(_player.transform.position.x, transform.position.y, transform.position.z);
    }
}
