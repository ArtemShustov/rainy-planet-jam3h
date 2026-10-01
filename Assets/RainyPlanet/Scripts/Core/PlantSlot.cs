using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace RainyPlanet.Core {
	[GeneratePropertyBag]
	public class PlantSlot : MonoBehaviour {
		[SerializeField] private int _cost;
		[SerializeField] private bool _isUnlocked;
		[SerializeField] private Plant _plant;
		[SerializeField] private Transform _target;
		[SerializeField] private PanelRenderer _ui;

		[CreateProperty] public string Cost => _cost.ToString();
		
		public int MoneyCost => _cost;
		public bool IsUnlocked => _isUnlocked;

		private void Awake() {
			_plant.gameObject.SetActive(_isUnlocked);
			_target.gameObject.SetActive(!_isUnlocked);
		}

		public void SetCost(int cost) {
			_cost = cost;
		}
		
		public void Unlock() {
			_isUnlocked = true;
			_plant.gameObject.SetActive(_isUnlocked);
			_target.gameObject.SetActive(!_isUnlocked);
		}
		
		private void OnEnable() {
			_ui.RegisterUIReloadCallback(OnUIReload);
		}

		private void OnDisable() {
			_ui.UnregisterUIReloadCallback(OnUIReload);
		}

		private void OnUIReload(PanelRenderer renderer, VisualElement root, int version) {
			root.dataSource = this;
		}
	}
}
