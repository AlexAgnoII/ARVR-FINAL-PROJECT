using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    [SerializeField] private GameObject gameThrowHandler;
    [SerializeField] private GameObject placementHandler;


    private bool stageIsDone = false;

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

    private void AllowThrowing()
    {
        this.ActivateGameThrowingHandler();
        this.DeactivatePlacementHander();
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
