using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;
using TMPro;
using UnityEngine;

public class CreateBehaviourLanesLabels : MonoBehaviour
{
    public TMP_FontAsset AvailableAsset;
    public TMP_FontAsset UtilityAsset;
    public TMP_FontAsset RedAsset;
    public GameObject LayerInstantiate;
    public Transform[] EventGrid;
    public RotationCallbackController RotationCallback;

    private readonly List<LaneInfo> laneObjs = new List<LaneInfo>();

    // Start is called before the first frame update
    void Start()
    {
        UpdateLabels();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateLabels(int _page = 0)
    {
        foreach (Transform children in LayerInstantiate.transform.parent.transform)
        {
            if (children.gameObject.activeSelf)
                Destroy(children.gameObject);
        }

        //laneObjs.Clear();

        foreach (var trans in EventGrid)
        {
            var localScale = trans.localScale;
            localScale = new Vector3(1, localScale.y, localScale.z);
            trans.localScale = localScale;
        }

        if (_page < 0 || _page > 10) return;

        for (int i = 0; i < 10; i++)
        {
            GameObject instantiate = Instantiate(LayerInstantiate, LayerInstantiate.transform.parent);
            instantiate.SetActive(true);
            instantiate.transform.localPosition = new Vector3(i, 0, 0);

            try
            {
                var textMesh = instantiate.GetComponentInChildren<TextMeshProUGUI>();
                textMesh.font = AvailableAsset;
                textMesh.text = "Lane " + ((_page * 10) + i + 1);
            }
            catch { }
        }
    }
}
