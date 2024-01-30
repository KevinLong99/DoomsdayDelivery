using UnityEngine;
using Actuate;
using ActuateClient.Messages;
using UnityEngine.UI;

public class ExtraData : MonoBehaviour, IActuateExtrasSource {
	public Slider fuelSlider;
	public Slider temperatureSlider;
	public Toggle oilLightToggle;
	public Toggle leftIndicatorLightToggle;
	public Toggle rightIndicatorLightToggle;
	public Toggle brakeSystemLightToggle;
	public Toggle batteryLightToggle;
	public Toggle espLightToggle;
	public Toggle dipBeamLightToggle;
	public Toggle mainBeamLightToggle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public ActuateExtraData GetExtras() {
		DashLights dashLights = (DashLights)0;

		if (oilLightToggle.isOn)
			dashLights |= DashLights.OilLight;

		if (leftIndicatorLightToggle.isOn)
			dashLights |= DashLights.LeftIndicatorLight;
		
		if (rightIndicatorLightToggle.isOn)
			dashLights |= DashLights.RightIndicatorLight;
		
		if (brakeSystemLightToggle.isOn)
			dashLights |= DashLights.BrakeSystemLight;
		
		if (batteryLightToggle.isOn)
			dashLights |= DashLights.BatteryLight;
		
		if (espLightToggle.isOn)
			dashLights |= DashLights.EspLight;
		
		if (dipBeamLightToggle.isOn)
			dashLights |= DashLights.DippedBeamLight;
		
		if (mainBeamLightToggle.isOn)
			dashLights |= DashLights.MainBeamLight;

		return new ActuateExtraData {
			dashLights = dashLights,
			engineRpm = 0f,
			fuelLevel = fuelSlider.value,
			gearIndicator = ' ',
			temperature = temperatureSlider.value,
			tripCounter = 0f
		};
	}
}
