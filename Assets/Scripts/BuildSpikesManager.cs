using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BuildSpikesManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject spike;
    [SerializeField] private Transform mapTop;
    [SerializeField] private Transform mapGround;
    [SerializeField] private Transform leftWall;
    [SerializeField] private Transform rightWall;
    private List<GameObject> spikes = new List<GameObject>();
    private int spikesCount = 2;
    private bool spikeIsOnRight = true;
    private int topScreen;
    private int groundScreen;

    private void Start()
    {
        topScreen = (int)mapTop.transform.position.y; // Get maxExclusive
        groundScreen = (int)mapGround.transform.position.y + 1; // Get minInclusive
    }

    public void Init()
    {
        spikeIsOnRight = true;
        spikesCount = 2;
        DestroySpikes();
    }

    public void NewSpikes()
    {
        spikeIsOnRight = !spikeIsOnRight;
        if (spikesCount < 9 && gameManager.level == spikesCount * spikesCount)
            spikesCount++;
        DestroySpikes();
        BuilSpikes();
    }

    private void BuilSpikes()
    {
        Vector2 spawSpikePos = new Vector2();
        spawSpikePos.x = spikeIsOnRight ? rightWall.position.x : leftWall.position.x;
        Quaternion spawSpikeRot = spikeIsOnRight ? Quaternion.Euler(0f, 0f, 90f) : Quaternion.Euler(0f, 0f, 270f);
        List<int> tempListRandom = new List<int>();
        for (int i = 0; i < spikesCount; i++) {
            spawSpikePos.y = Random.Range(groundScreen, topScreen);
            while (tempListRandom.Contains((int)spawSpikePos.y)) {
                spawSpikePos.y++;
                if (spawSpikePos.y >= topScreen)
                    spawSpikePos.y = groundScreen;
            }
            tempListRandom.Add((int)spawSpikePos.y);
            spikes.Add(Instantiate(spike, spawSpikePos, spawSpikeRot));
        }
    }

    private void DestroySpikes()
    {
        while (0 < spikes.Count) {
            Destroy(spikes[0]);
            spikes.RemoveAt(0);
        }
    }
}
