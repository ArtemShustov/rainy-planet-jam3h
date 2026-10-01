using UnityEngine;

namespace RainyPlanet.Core {
	public class CameraMovement : PointerDragHandler {
		[SerializeField] private float _limitRadius = 20;

		private Camera _camera;
		private Vector3 _dragOrigin;

		private void Awake() {
			_camera = Camera.main;
		}

		public override bool TryBeginDrag(Vector2 screenPos) {
			_dragOrigin = _camera.ScreenToWorldPoint(screenPos);
			return true;
		}

		public override void Drag(Vector2 screenPos) {
			var delta = _dragOrigin - _camera.ScreenToWorldPoint(screenPos);
			var camPos = _camera.transform.position;
			var newCamPos = (Vector3)Vector2.ClampMagnitude(camPos + delta, _limitRadius);

			_camera.transform.position = newCamPos + Vector3.forward * camPos.z;
		}
	}
}
