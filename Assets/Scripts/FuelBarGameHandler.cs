using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    [SerializeField] private FuelBar FuelBar;

	private void Start () {
        float fuel = 1f;
        if (fuel > .01f) {
            fuel -= .01f;
            FuelBar.SetSize(fuel);

            if (fuel < .3f) {
                // Under 30% health --> color turns white
                if ((int)(fuel * 100f) % 3 == 0) {
                    FuelBar.SetColor(Color.white);
                } else {
                    FuelBar.SetColor(Color.red);
                }
            }
        } else {
            fuel = 1f;
            FuelBar.SetColor(Color.red);
        }
	}
}
