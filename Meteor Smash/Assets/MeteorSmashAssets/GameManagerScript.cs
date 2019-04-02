using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    [SerializeField] private GameObject gameThrowHandler;
    [SerializeField] private GameObject placementHandler;


    private bool stageIsDone = false;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.MeteorSmash.ON_SPAWN_TARGET_DONE, this.AllowThrowing);
        
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.MeteorSmash.ON_SPAWN_TARGET_DONE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AllowThrowing(Parameters param)
    {
        this.ActivateGameThrowingHandler();
        this.DeactivatePlacementHander();

        this.targetPosition = new Vector3(param.GetFloatExtra(EventNames.MeteorSmash.VALUE_TARGET_POSITION_X, -1),
                                          param.GetFloatExtra(EventNames.MeteorSmash.VALUE_TARGET_POSITION_Y, -1),
                                          param.GetFloatExtra(EventNames.MeteorSmash.VALUE_TARGET_POSITION_Z, -1));
        //TEST PURPOSES.
        Parameters paramCoor = new Parameters();
        string coordinates = " " + this.targetPosition.x + " " + this.targetPosition.y + " " + this.targetPosition.z;
        paramCoor.PutExtra(EventNames.MeteorSmash.VALUE_TARGET_COORDINATES, coordinates);
        EventBroadcaster.Instance.PostEvent(EventNames.MeteorSmash.ON_PRINT_TARGET_POSITION, paramCoor);

    }

    private void ActivateGameThrowingHandler()
    {
        this.gameThrowHandler.SetActive(true);
    }

    private void DeactivateGameHander()
    {
        this.gameThrowHandler.SetActive(false);
    }

    private void ActivatePlacementHandler()
    {
        this.placementHandler.SetActive(true);
    }

    private void DeactivatePlacementHander()
    {
        this.placementHandler.SetActive(false);
    }
}
