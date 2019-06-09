using UnityEngine;
using System.Collections;
using Zenject;

public class CameraFollow : MonoBehaviour 
{
	[SerializeField] private float _xMargin = 1f;	
	[SerializeField] private float _xSmooth = 3f;  
    [SerializeField] private float _minX = -28f;

    [Inject] private Player _player;

    private Transform _playerTransform;
    private bool _isPlayerNotNull;
    private float _targetY;

    void Awake ()
    {
        
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _isPlayerNotNull = _playerTransform != null;
        _targetY = transform.position.y;
    }


	bool CheckXMargin()
	{
		return Mathf.Abs(transform.position.x - _playerTransform.position.x) > _xMargin;
	}


	void LateUpdate ()
	{
        if (_isPlayerNotNull && !_player.IsDead)
        TrackPlayer();
	}
	
	
	void TrackPlayer ()
	{
        var position = transform.position;
        float targetX = position.x;
		

		if(CheckXMargin())
			targetX = Mathf.Lerp(position.x, _playerTransform.position.x, _xSmooth * Time.deltaTime);

        targetX = Mathf.Clamp(targetX, _minX, Mathf.Infinity);

        transform.position = new Vector3(targetX, _targetY, position.z);
	}
}
