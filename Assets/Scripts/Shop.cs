using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void PurchaseStandardTurret() //for the regular turret 
    {
        print("Turret purchases");
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }
    public void PurchaseMissileLauncher() //for the missile
    {
        print("MissileLauncher purchased");
       buildManager.SetTurretToBuild(buildManager.missileLauncherPrefab);
    }
}
