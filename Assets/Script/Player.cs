using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public List<GameObject> cardPrefabs;
    private List<GameObject> hand = new List<GameObject>();
    private List<int> selectedCards = new List<int>();

    [SyncVar]
    private int numberOfCards = 10;

    public override void OnStartServer()
    {
        base.OnStartServer();
        GenerateCards();
    }

    private void GenerateCards()
    {
        int tempIndex;
        for (int i = 0; i < numberOfCards; i++)
        {
            do
            {
                tempIndex = Random.Range(0, cardPrefabs.Count);
            } while (selectedCards.Contains(tempIndex));

            selectedCards.Add(tempIndex);
            SpawnCardOnServer(tempIndex, i);
        }
    }

    private void SpawnCardOnServer(int index, int pos)
    {
        // Vector2 position = new Vector2(transform.position.x + pos, transform.position.y);
        Vector2 position = CalculateCardPosition(pos);
        GameObject card = Instantiate(cardPrefabs[index], position, Quaternion.Euler(0, 0, 0));
        NetworkServer.Spawn(card); // Spawn the card on the network
        RpcSpawnCard(card, index, pos);
    }
    private Vector2 CalculateCardPosition(int cardIndex)
    {
        float spacing = 1.0f; // Adjust this value for card spacing
        float totalWidth = (numberOfCards - 1) * spacing;
        Vector2 basePosition = transform.position;

        // Center the cards around the GameObject
        basePosition.x -= totalWidth / 2;

        // Calculate the position of the current card
        Vector2 cardPosition = basePosition;
        cardPosition.x += cardIndex * spacing;

        return cardPosition;
    }

    [ClientRpc]
    private void RpcSpawnCard(GameObject card, int index, int pos)
    {
        if (card == null)
            return;

        card.tag = "Card";  // Ensure the card has the "Card" tag
        hand.Add(card);
        Debug.Log($"Card {index} spawned at position {pos}");
    }

    private void Update()
    {
        HandleCardMovement();
    }

    private void HandleCardMovement()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Card"))
            {
                Vector2 targetPosition = new Vector2(0, 0);
                CmdMoveCard(hit.collider.gameObject, targetPosition, 1.0f); // Move to center with 1-second duration
            }
        }
    }

    [Command]
    private void CmdMoveCard(GameObject card, Vector2 targetPosition, float moveDuration)
    {
        RpcMoveCard(card, targetPosition, moveDuration);
    }

    [ClientRpc]
    private void RpcMoveCard(GameObject card, Vector2 targetPosition, float moveDuration)
    {
        if (card == null)
            return;

        StartCoroutine(MoveCardToPosition(card, targetPosition, moveDuration));
    }

    private IEnumerator MoveCardToPosition(GameObject card, Vector2 targetPosition, float moveDuration)
    {
        Vector2 startPosition = card.transform.position;
        Quaternion startRotation = card.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 180, 0);
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            card.transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            card.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the card is exactly at the target position and rotation at the end
        card.transform.position = targetPosition;
        card.transform.rotation = targetRotation;
    }
}
