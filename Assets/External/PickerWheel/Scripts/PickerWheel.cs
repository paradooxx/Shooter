using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections.Generic;

namespace EasyUI.PickerWheelUI
{
    public class PickerWheel : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private GameObject linePrefab;
        [SerializeField] private Transform linesParent;

        [Space]
        [SerializeField] private Transform PickerWheelTransform;
        [SerializeField] private Transform wheelCircle;
        [SerializeField] private GameObject wheelPiecePrefab;
        [SerializeField] private Transform wheelPiecesParent;

        [Space]
        [Header("Sounds:")]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip tickAudioClip;
        [SerializeField] [Range(0f, 1f)] private float volume = .5f;
        [SerializeField] [Range(-3f, 3f)] private float pitch = 1f;

        [Space]
        [Header("Picker wheel settings:")]
        [Range(1, 20)] public int spinDuration = 8;
        [SerializeField] [Range(0.2f, 2f)] private float wheelSize = 1f;

        [Space]
        [Header("Picker wheel pieces:")]
        public WheelPiece[] wheelPieces;

        // Events
        private UnityAction onSpinStartEvent;
        private UnityAction<WheelPiece> onSpinEndEvent;

        private bool _isSpinning = false;
        public bool IsSpinning { get { return _isSpinning; } }

        private Vector2 pieceMinSize = new Vector2(81f, 146f);
        private Vector2 pieceMaxSize = new Vector2(144f, 213f);
        private int piecesMin = 2;
        private int piecesMax = 12;

        private float pieceAngle;
        private float halfPieceAngle;
        private float halfPieceAngleWithPaddings;

        private double accumulatedWeight;
        private System.Random rand = new System.Random();

        private List<int> nonZeroChancesIndices = new List<int>();

        private string targetLabel = null;  // The label you want to stop at

        private void Start()
        {
            pieceAngle = 360f / wheelPieces.Length;
            halfPieceAngle = pieceAngle / 2f;
            halfPieceAngleWithPaddings = halfPieceAngle - (halfPieceAngle / 4f);

            Generate();

            CalculateWeightsAndIndices();
            if (nonZeroChancesIndices.Count == 0)
                Debug.LogError("You can't set all pieces chance to zero");

            SetupAudio();
        }

        private void SetupAudio()
        {
            audioSource.clip = tickAudioClip;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
        }

        private void Generate()
        {
            wheelPiecePrefab = InstantiatePiece();

            RectTransform rt = wheelPiecePrefab.transform.GetChild(0).GetComponent<RectTransform>();
            float pieceWidth = Mathf.Lerp(pieceMinSize.x, pieceMaxSize.x, 1f - Mathf.InverseLerp(piecesMin, piecesMax, wheelPieces.Length));
            float pieceHeight = Mathf.Lerp(pieceMinSize.y, pieceMaxSize.y, 1f - Mathf.InverseLerp(piecesMin, piecesMax, wheelPieces.Length));
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, pieceWidth);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pieceHeight);

            for (int i = 0; i < wheelPieces.Length; i++)
                DrawPiece(i);

            Destroy(wheelPiecePrefab);
        }

        private void DrawPiece(int index)
        {
            WheelPiece piece = wheelPieces[index];
            Transform pieceTrns = InstantiatePiece().transform.GetChild(0);

            pieceTrns.GetChild(0).GetComponent<Image>().sprite = piece.Icon;
            pieceTrns.GetChild(2).GetComponent<Text>().text = piece.Amount.ToString();

            // Line
            Transform lineTrns = Instantiate(linePrefab, linesParent.position, Quaternion.identity, linesParent).transform;
            lineTrns.RotateAround(wheelPiecesParent.position, Vector3.back, (pieceAngle * index) + halfPieceAngle);

            pieceTrns.RotateAround(wheelPiecesParent.position, Vector3.back, pieceAngle * index);
        }

        private GameObject InstantiatePiece()
        {
            return Instantiate(wheelPiecePrefab, wheelPiecesParent.position, Quaternion.identity, wheelPiecesParent);
        }

        public void SetTargetLabel(string label)
        {
            targetLabel = label;
        }

        private int GetPieceIndexByLabel(string label)
        {
            for (int i = 0; i < wheelPieces.Length; i++)
            {
                if (wheelPieces[i].Label == label)
                {
                    return i;
                }
            }
            return -1;  // Return -1 if not found
        }

        public void Spin()
        {
            if (!_isSpinning)
            {
                _isSpinning = true;
                onSpinStartEvent?.Invoke();

                int index;
                if (!string.IsNullOrEmpty(targetLabel))
                {
                    index = GetPieceIndexByLabel(targetLabel);
                    if (index == -1)
                    {
                        Debug.LogError("Target label not found!");
                        _isSpinning = false;
                        return;
                    }
                }
                else
                {
                    index = 0;
                }

                WheelPiece piece = wheelPieces[index];

                // Ensure non-zero chance pieces are selected when available
                if (piece.Chance == 0 && nonZeroChancesIndices.Count != 0)
                {
                    index = nonZeroChancesIndices[Random.Range(0, nonZeroChancesIndices.Count)];
                    piece = wheelPieces[index];
                }

                // Calculate the base angle for the selected piece
                float angle = -(pieceAngle * index);

                // Calculate offsets to add some randomness to the stop point within the piece
                float rightOffset = (angle - halfPieceAngleWithPaddings) % 360;
                float leftOffset = (angle + halfPieceAngleWithPaddings) % 360;

                // Randomize the exact stopping point within the piece
                float randomAngle = Random.Range(leftOffset, rightOffset);

                // Add extra rotations (e.g., 5 full rotations)
                int extraRotations = 5; // Number of extra full rotations before stopping

                // Calculate the target rotation, including extra rotations
                Vector3 targetRotation = Vector3.back * (randomAngle + 360 * extraRotations);

                // Initialize variables for tracking the rotation during the animation
                float prevAngle = wheelCircle.eulerAngles.z;
                float currentAngle = prevAngle;
                bool isIndicatorOnTheLine = false;

                // Start the wheel spinning animation
                wheelCircle
                    .DORotate(targetRotation, spinDuration, RotateMode.FastBeyond360)
                    .SetEase(Ease.InOutQuart)
                    .SetUpdate(true) // Ensures the animation runs even when Time.timeScale is 0
                    .OnUpdate(() =>
                    {
                        float diff = Mathf.Abs(prevAngle - currentAngle);
                        if (diff >= halfPieceAngle)
                        {
                            if (isIndicatorOnTheLine)
                            {
                                audioSource.PlayOneShot(audioSource.clip);
                            }
                            prevAngle = currentAngle;
                            isIndicatorOnTheLine = !isIndicatorOnTheLine;
                        }
                        currentAngle = wheelCircle.eulerAngles.z;
                    })
                    .OnComplete(() =>
                    {
                        _isSpinning = false;
                        onSpinEndEvent?.Invoke(piece);
                        onSpinStartEvent = null;
                        onSpinEndEvent = null;
                    });

                // After spinning, reset the targetLabel
                targetLabel = null;
            }
        }

        public void OnSpinStart(UnityAction action)
        {
            onSpinStartEvent = action;
        }

        public void OnSpinEnd(UnityAction<WheelPiece> action)
        {
            onSpinEndEvent = action;
        }

        private int GetRandomPieceIndex()
        {
            double r = rand.NextDouble() * accumulatedWeight;

            for (int i = 0; i < wheelPieces.Length; i++)
                if (wheelPieces[i]._weight >= r)
                    return i;

            return 0;
        }

        private void CalculateWeightsAndIndices()
        {
            for (int i = 0; i < wheelPieces.Length; i++)
            {
                WheelPiece piece = wheelPieces[i];

                // Add weights:
                accumulatedWeight += piece.Chance;
                piece._weight = accumulatedWeight;

                // Add index:
                piece.Index = i;

                // Save non-zero chance indices:
                if (piece.Chance > 0)
                    nonZeroChancesIndices.Add(i);
            }
        }

        private void OnValidate()
        {
            if (PickerWheelTransform != null)
                PickerWheelTransform.localScale = new Vector3(wheelSize, wheelSize, 1f);

            if (wheelPieces.Length > piecesMax || wheelPieces.Length < piecesMin)
                Debug.LogError("[PickerWheel] pieces length must be between " + piecesMin + " and " + piecesMax);
        }
    }
}
