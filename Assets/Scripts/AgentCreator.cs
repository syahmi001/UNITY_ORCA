using UnityEngine;
using System.Collections;
using Pathfinding;
using System.Collections.Generic;

public class AgentCreator : MonoBehaviour {

    [SerializeField]
    GameObject _prefab;

    [SerializeField]
    int _amount;

    [SerializeField]
    Transform _targetPosition;

    // Use this for initialization
    void Start () {
        StartCoroutine(GenerateAgent());
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator GenerateAgent()
    {
        int count = 0;
        while (count != _amount)
        {
            GameObject agentGameObject = (GameObject)Instantiate(_prefab, transform.position, Quaternion.Euler(0,270,0));
            agentGameObject.name = _prefab.name + "C" + count;
            yield return new WaitForSeconds(1);
            count++;
        }
    }
}