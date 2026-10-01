using System;
using UnityEngine;

namespace RainyPlanet.Core {
	public class Plant : MonoBehaviour {
		[SerializeField] private float _growTime = 1;
		
		private float _progress;

		public float Progress => _progress;

		public event Action<float> ProgressChanged;
		
		private void Update() {
			if (_progress >= 1) {
				return;
			}
			
			_progress = Mathf.Clamp01(_progress + Time.deltaTime / _growTime);
			ProgressChanged?.Invoke(_progress);

			if (_progress >= 1) {
				Debug.Log("Full grow");
			}
		}

		public void TakeProgress(float value) {
			_progress = Mathf.Clamp01(_progress - value);
			ProgressChanged?.Invoke(_progress);
		}
	}
}
