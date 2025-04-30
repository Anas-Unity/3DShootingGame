using UnityEngine;
using UnityEngine.UI;

public class GunsController : MonoBehaviour
{
    public float gunRange = 100f;
    public float gunDamage = 10f;
    public int megCapacity = 30;
    public int gunBullets;

    public Text bulletCountText;
    public Camera Camera;
    public ParticleSystem muzzleFlash;

    /*public int gunLevel = 1;
    public GunProperties gunProperties;*/

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gunBullets = megCapacity;
        bulletCountText.text = gunBullets.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Shoot();
        }
    }


    public void Shoot()
    {

        /*Debug.Log("Gun Level: "+ gunLevel+" Gun Damage: " + gunProperties.levels[gunLevel - 1].gunDamage);
      if (gunLevel < gunProperties.levels.Length)
      {
          gunLevel++;
      }*/

        if (gunBullets > 0)
        {
            muzzleFlash.Play();
            RaycastHit hit;
            if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, gunRange))
            {
                Debug.Log("Hitted Object: " + hit.transform.name);
                HealthManager _healthManager = hit.transform.GetComponent<HealthManager>();
                if (_healthManager != null)
                {
                    _healthManager.UpdateHealth(-gunDamage);
                }
            }
            gunBullets--;
            if (gunBullets <= 0) 
            {
                ReloadMeg();
            }

            bulletCountText.text = gunBullets.ToString();
        }
    }


    public void ReloadMeg()
    {
        Debug.Log("Reloading...");
        gunBullets = megCapacity;
    }
}
/*[System.Serializable]
public class GunProperties
{
    public string gunName;
    public float recoilValue;
    public GunLevel[] levels;
}
[System.Serializable]
public class GunLevel
{
    public float gunDamage;
    public float gunRange;
    public float reloadTime;
}*/