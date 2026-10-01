using UnityEngine;

namespace RainyPlanet.Core {
	public class Cloud : MonoBehaviour {
		[SerializeField] private RainDrop[] _dropPrefabs;
		[SerializeField] private Transform _spawnPoint;
		[SerializeField] private float _delay = 1;
		[SerializeField] private float _rotationSpeed = 45f;

		private float _timer;

		private void Update() {
			transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
			
			_timer += Time.deltaTime;

			if (_timer >= _delay) {
				_timer = 0;
				SpawnDrop();
			}
		}

		private void SpawnDrop() {
			var prefab = _dropPrefabs[UnityEngine.Random.Range(0, _dropPrefabs.Length)];
			Instantiate(prefab, _spawnPoint.position, _spawnPoint.rotation);
		}
	}
}
