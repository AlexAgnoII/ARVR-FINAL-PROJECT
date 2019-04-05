using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTargetRandomizer : MonoBehaviour    
{

    [SerializeField] private List<GameObject> targetList;

    // Start is called before the first frame update
    void Start()
    {
        int real_target_index = Random.Range(0, 5);
        this.targetList[real_target_index].gameObject.tag = TagNames.TARGET;

        for (int i = 0; i < targetList.Count; i++)
        {
            if (i != real_target_index)
            {
                this.targetList[i].gameObject.tag = TagNames.FAKE_TARGET;
            }
        }
    }


}
