using System.Collections;
using UnityEngine;

public class RewardGateGenerator : MonoBehaviour
{
    [SerializeField] private GameObject rewardGate;
    [SerializeField] private Transform spawnPoints;
    private int numOfGates;
    private float initialGateStartTime = 1f;
    private float minTimeBetnObjs = 1f;
    private float maxTimeBetnObjs = 3f;
    private Vector3 position = new Vector3(0, 1.75f, 1);

    private void OnEnable() => GameStateManager.OnStateChange += OnGameStateChanged;

    private void OnDisable() => GameStateManager.OnStateChange -= OnGameStateChanged;

    private void OnGameStateChanged(GameState gameState)
    {
        if (gameState == GameState.Game)
        {
            StartCoroutine(GenerateGates());
        }
    }

    private void Start()
    {
        numOfGates = LevelDataContainer.Instance.numOfGates;
        initialGateStartTime = LevelDataContainer.Instance.initialGateStartTime;
        minTimeBetnObjs = LevelDataContainer.Instance.minTimeBetnGates;
        maxTimeBetnObjs = LevelDataContainer.Instance.maxTimeBetnGates;
    }

    private IEnumerator GenerateGates()
    {
        yield return new WaitForSeconds(initialGateStartTime);
        float timeBetweenGates = Random.Range(minTimeBetnObjs, maxTimeBetnObjs);
        for (int i = 0; i < numOfGates; i++)
        {
            Vector3 combinedPosition = spawnPoints.position + position;
            Instantiate(rewardGate, combinedPosition, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenGates);
        }
    }
}
