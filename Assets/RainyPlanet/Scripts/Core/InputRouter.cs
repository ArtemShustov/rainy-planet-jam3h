using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RainyPlanet.Core {
	[Serializable]
	public abstract class PointerDragHandler : MonoBehaviour {
		public virtual bool TryBeginDrag(Vector2 screenPos) { return false; }
		public virtual void Drag(Vector2 screenPos) { }
		public virtual void EndDrag(Vector2 screenPos) { }
	}

	public class InputRouter : MonoBehaviour {
		[SerializeField] private List<PointerDragHandler> _handlers;
		private InputActions _actions;
		private PointerDragHandler _active;

		private Vector2 PointerPosition => _actions.Player.PointerPosition.ReadValue<Vector2>();

		private void Awake() {
			_actions = new InputActions();
		}

		private void Update() {
			_active?.Drag(PointerPosition);
		}
		
		private void OnEnable() {
			_actions.Player.Enable();
			_actions.Player.Pointer.started += OnStarted;
			_actions.Player.Pointer.canceled += OnCanceled;
		}
		
		private void OnDisable() {
			_active?.EndDrag(PointerPosition); 
			_active = null;
			
			_actions.Player.Pointer.started -= OnStarted;
			_actions.Player.Pointer.canceled -= OnCanceled;
			_actions.Player.Disable();
		}

		private void OnStarted(InputAction.CallbackContext context) {
			foreach (var h in _handlers) {
				if (h.TryBeginDrag(PointerPosition)) {
					_active = h;
					return;
				}
			}
		}
		
		private void OnCanceled(InputAction.CallbackContext context) {
			_active?.EndDrag(PointerPosition);
			_active = null;
		}
	}
}
