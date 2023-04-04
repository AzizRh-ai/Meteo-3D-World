using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class WorldCoordonatesManager : MonoBehaviour
{
    #region Variables
    public Camera camera;

    public GameManager gameManager;

    public GameObject displayedPointObject;

    [Serializable]
    public class SelectedCoordonatesData
    {
        public LocationData.CoordonatesData Coordonates = new LocationData.CoordonatesData();

    }
    public SelectedCoordonatesData SelectedCoordonates = new SelectedCoordonatesData();

    [Serializable]
    public class DisplayedCoordonatesData
    {
        public LocationData.CoordonatesData Coordonates = new LocationData.CoordonatesData();

    }
    public DisplayedCoordonatesData DisplayedCoordonates = new DisplayedCoordonatesData();

 //   public RaycastHit hit/* = new RaycastHit()*/;

    #endregion Variables


    private void OnEnable()
    {
        DisplayLocationByCoordonates();
    }

    private void Update()
    {
    //    DisplayLocationByCoordonates();

        CastRay();
    }



    private void DisplayLocationByCoordonates()
    {
        Vector3 point = new Vector3(DisplayedCoordonates.Coordonates.radius, 0, 0);
        point = Quaternion.Euler(0, DisplayedCoordonates.Coordonates.latitude, DisplayedCoordonates.Coordonates.longitude) * point;

        displayedPointObject.transform.position = point;

    }

   
    private void CastRay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
    
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            Debug.Log(Input.mousePosition.x.ToString());

            if (Physics.Raycast(ray, out hit))
            {
                AssignSelectedCoordonates(hit.point);
                MoveDisplayPointObjectToSelectedCoordonates();

                // TO BE MODIFIED
                displayedPointObject.transform.position = hit.point;
            }

            Debug.DrawLine(camera.transform.position, hit.point, Color.blue);
        }
    }

    void AssignSelectedCoordonates(Vector3 newCoordonates)
    {
        SelectedCoordonates.Coordonates.latitude = newCoordonates.y;
        SelectedCoordonates.Coordonates.longitude = newCoordonates.z;
    //    SelectedCoordonates.Coordonates.height = newCoordonates.z;

    }

    void MoveDisplayPointObjectToSelectedCoordonates()
    {
        Vector3 coordonates = new Vector3(SelectedCoordonates.Coordonates.latitude, 
            SelectedCoordonates.Coordonates.longitude, 
            SelectedCoordonates.Coordonates.height);

        displayedPointObject.transform.position = coordonates;
    }



}
