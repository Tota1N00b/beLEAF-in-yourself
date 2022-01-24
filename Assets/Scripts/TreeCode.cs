using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCode : MonoBehaviour
{
    [SerializeField] GameObject Leave0;
    [SerializeField] GameObject Leave1;
    [SerializeField] GameObject Leave2;
    [SerializeField] GameObject Leave3;
    [SerializeField] GameObject Stair1;
    [SerializeField] GameObject Stair2;
    [SerializeField] GameObject Stair3;
    [SerializeField] GameObject Stair4;
    [SerializeField] GameObject AirWallTree;

    void Start()
    {
        GameObject[] Stairs = { Stair1, Stair2, Stair3, Stair4 };
        for (int j = 0; j < Stairs.Length; j++)
            for (int i = 0; i < Stairs[j].transform.childCount; i++)
            {
                Color c = Stairs[j].transform.GetChild(i).GetComponent<MeshRenderer>().material.color;
                c.a = 0;
                Stairs[j].transform.GetChild(i).GetComponent<MeshRenderer>().material.color = c;
            }
        PublicVars.NextStage = 3;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TreeGrow();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            TreeFall();
        }

        if (PublicVars.NextStage > PublicVars.TreeStage)
            TreeGrow();
        else if (PublicVars.NextStage < PublicVars.TreeStage)
            TreeFall();

        if (PublicVars.TreeStage == 4)
            AirWallTree.active = false;
    }

    void TreeGrow()
    {
        if (PublicVars.TreeStage >= 0 && PublicVars.TreeStage < 4)
        {
            switch (PublicVars.TreeStage)
            {
                case 0:
                    StartCoroutine(LGrow(Leave0));
                    StartCoroutine(StairUp(Stair1));
                    break;
                case 1:
                    StartCoroutine(LGrow(Leave1));
                    StartCoroutine(StairUp(Stair2));
                    break;
                case 2:
                    StartCoroutine(LGrow(Leave2));
                    StartCoroutine(StairUp(Stair3));
                    break;
                case 3:
                    StartCoroutine(LGrow(Leave3));
                    StartCoroutine(StairUp(Stair4));
                    break;
            }
            PublicVars.TreeStage++;
        }
    }

    void TreeFall()
    {
        if (PublicVars.TreeStage > 0 && PublicVars.TreeStage <= 4)
        {
            switch (PublicVars.TreeStage)
            {
                case 4:
                    StartCoroutine(LFall(Leave3));
                    StartCoroutine(StairDown(Stair4));
                    break;
                case 3:
                    StartCoroutine(LFall(Leave2));
                    StartCoroutine(StairDown(Stair3));
                    break;
                case 2:
                    StartCoroutine(LFall(Leave1));
                    StartCoroutine(StairDown(Stair2));
                    break;
                case 1:
                    StartCoroutine(LFall(Leave0));
                    StartCoroutine(StairDown(Stair1));
                    break;
            }
            PublicVars.TreeStage--;
        }
    }

    IEnumerator LGrow(GameObject Leaves)
    {
        for(float t = 0; t <= 1; t+=0.05f)
        {
            Leaves.transform.localPosition = new Vector3(0, 0, 0);
            for (int i = 0; i < Leaves.transform.GetChild(0).transform.childCount; i++)
            {
                Color c = Leaves.transform.GetChild(0).transform.GetChild(i).GetComponent<MeshRenderer>().material.color;
                c.a = t;
                Leaves.transform.GetChild(0).transform.GetChild(i).GetComponent<MeshRenderer>().material.color = c;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator LFall(GameObject Leaves)
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            for (int i = 0; i < Leaves.transform.GetChild(0).transform.childCount; i++)
            {
                Color c = Leaves.transform.GetChild(0).transform.GetChild(i).GetComponent<MeshRenderer>().material.color;
                c.a = 1-t;
                Leaves.transform.GetChild(0).transform.GetChild(i).GetComponent<MeshRenderer>().material.color = c;
            }
            float y = - t * 7.61f;
            Leaves.transform.localPosition = new Vector3(0, y, 0);
            yield return new WaitForSeconds(0.05f);
        }

        for (int i = 0; i < Leaves.transform.GetChild(0).transform.childCount; i++)
        {
            Color c = Leaves.transform.GetChild(0).transform.GetChild(i).GetComponent<MeshRenderer>().material.color;
            c.a = 0;
            Leaves.transform.GetChild(0).transform.GetChild(i).GetComponent<MeshRenderer>().material.color = c;
        }
    }

    IEnumerator StairUp(GameObject Stairs)
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            for (int i = 0; i < Stairs.transform.childCount; i++)
            {
                Color c = Stairs.transform.GetChild(i).GetComponent<MeshRenderer>().material.color;
                c.a = t;
                Stairs.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = c;
            }
            Stairs.transform.position = Stairs.transform.position + new Vector3(0, 0.05f * 5, 0);
            yield return new WaitForSeconds(0.05f);
        }

        for (int i = 0; i < Stairs.transform.childCount; i++)
        {
            Color c = Stairs.transform.GetChild(i).GetComponent<MeshRenderer>().material.color;
            c.a = 1;
            Stairs.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = c;
        }
    }

    IEnumerator StairDown(GameObject Stairs)
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            for (int i = 0; i < Stairs.transform.childCount; i++)
            {
                Color c = Stairs.transform.GetChild(i).GetComponent<MeshRenderer>().material.color;
                c.a = 1 - t;
                Stairs.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = c;
            }
            Stairs.transform.position = Stairs.transform.position - new Vector3(0, 0.05f * 5, 0);
            yield return new WaitForSeconds(0.05f);
        }

        for (int i = 0; i < Stairs.transform.childCount; i++)
        {
            Color c = Stairs.transform.GetChild(i).GetComponent<MeshRenderer>().material.color;
            c.a = 0;
            Stairs.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = c;
        }
    }
}
