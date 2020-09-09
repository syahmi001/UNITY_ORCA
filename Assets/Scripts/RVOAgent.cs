using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using RVO;
using Pathfinding;

public class RVOAgent : MonoBehaviour {

    [SerializeField]
    Transform target = null;
    
    Seeker agentSeeker;
    private List<Vector3> pathNodes = null;
    RVO2Simulator simulator = null;
    int agentIndex = -1;
    int currentNodeInThePath = 0;
    bool isAbleToStart = false;

    // Use this for initialization
    IEnumerator Start() {
        currentNodeInThePath = 0;
        simulator = GameObject.FindGameObjectWithTag("RVOSim").GetComponent<RVO2Simulator>();
        pathNodes = new List<Vector3>();
        yield return StartCoroutine(StartPaths());
        agentIndex = simulator.addAgentToSim(transform.position, gameObject, pathNodes);
        isAbleToStart = true;
    }
    IEnumerator StartPaths() {
        agentSeeker = gameObject.GetComponent<Seeker>();
        var path = agentSeeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return StartCoroutine(path.WaitForPath());
    }
    public void OnPathComplete(Path p){
        //We got our path back
        if (p.error) {
            Debug.Log("" + this.gameObject.name + " ---- -" + p.errorLog);
        }
        else {
            pathNodes = p.vectorPath;
        }
    }
    // Update is called once per frame
    void Update() {
        if (isAbleToStart && agentIndex != -1) {
            transform.position = toUnityVector(simulator.getAgentPosition(agentIndex));
        }
    }
    public RVO.Vector2 calculateNextStation() {
        Vector3 station;
        if (currentNodeInThePath < pathNodes.Count)
        {
            station = pathNodes[currentNodeInThePath];
            float distance = Vector3.Distance(station, transform.position);
            if (distance >= 0 && distance < 1) {
                station = pathNodes[currentNodeInThePath];
                currentNodeInThePath++;
            }
        }
        else {
            station = pathNodes[pathNodes.Count - 1];
        }
        return toRVOVector(station);
    }
    Vector3 toUnityVector(RVO.Vector2 param)
    {
        return new Vector3(param.x(), transform.position.y, param.y());
    }

    RVO.Vector2 toRVOVector(Vector3 param)
    {
        return new RVO.Vector2(param.x, param.z);
    }


}
