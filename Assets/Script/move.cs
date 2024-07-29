using System.Collections;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject lastCardPlayed;
    public float moveDuration = 1.0f;  // Duration of the move animation in seconds

    void OnMouseDown()
    {
        Vector2 targetPosition = new Vector2(0, 0);
        Quaternion targetRotation = Quaternion.Euler(0, 180, 0);

        StartCoroutine(MoveCardToPosition(targetPosition, targetRotation));

        lastCardPlayed = gameObject;
        Debug.Log("name " + gameObject.name);
    }

    private IEnumerator MoveCardToPosition(Vector2 targetPosition, Quaternion targetRotation)
    {
        Vector2 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the card is exactly at the target position and rotation at the end
        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }
}
