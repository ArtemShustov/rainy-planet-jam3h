using UnityEngine;

namespace RainyPlanet.Core {
	[RequireComponent(typeof(Plant))]
	public class PlantView : MonoBehaviour {
		[SerializeField] private SpriteRenderer _renderer;
		[SerializeField] private Sprite _smallSprite;
		[SerializeField] private Sprite[] _fullSprites;
		[SerializeField] private float _frameDuration = 0.2f;
       
		private Plant _plant;
		private bool _isFull;
		private float _timer;
		private int _frame;

		private void Awake() {
			_plant = GetComponent<Plant>();
		}

		private void OnEnable() {
			_plant.ProgressChanged += OnProgressChanged;
			_renderer.sprite = _plant.Progress >= 1f ? _fullSprites[_frame] : _smallSprite;
		}

		private void OnDisable() {
			_plant.ProgressChanged -= OnProgressChanged;
		}

		private void Update() {
			if (!_isFull || _fullSprites.Length == 0) {
				return;
			}

			_timer += Time.deltaTime;
			if (_timer < _frameDuration) {
				return;
			}

			_timer -= _frameDuration;
			_frame = (_frame + 1) % _fullSprites.Length;
			_renderer.sprite = _fullSprites[_frame];
		}
       
		private void OnProgressChanged(float value) {
			var isFull = value >= 1f;

			if (isFull == _isFull) {
				return;
			}

			_isFull = isFull;
			_timer = 0f;
			_frame = 0;

			if (_isFull && _fullSprites.Length > 0) {
				_renderer.sprite = _fullSprites[0];
			} else {
				_renderer.sprite = _smallSprite;
			}
		}
	}
}
