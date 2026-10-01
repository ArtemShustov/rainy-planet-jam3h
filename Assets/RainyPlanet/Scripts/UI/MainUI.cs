using RainyPlanet.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace RainyPlanet.UI {
	public class MainUI : MonoBehaviour {
		[SerializeField] private PanelRenderer _ui;
		[SerializeField] private Player _player;
		
		private MainViewModel _viewModel;
		
		private void Awake() {
			_viewModel = new MainViewModel(_player.Money);
		}
		
		private void OnEnable() {
			_ui.RegisterUIReloadCallback(OnUIReload);
		}

		private void OnDisable() {
			_ui.UnregisterUIReloadCallback(OnUIReload);
		}

		private void OnUIReload(PanelRenderer renderer, VisualElement root, int version) {
			root.dataSource = _viewModel;
		}
	}
}
