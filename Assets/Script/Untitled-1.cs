// using System.Collections;
// using System.Collections.Generic;
// using Mirror;
// using UnityEngine;

// public class Player : NetworkBehaviour
// {
//     public List<GameObject> cardPrefabs;
//     private List<GameObject> hand = new List<GameObject>();
//     private List<int> selectedCards = new List<int>();

//     [SyncVar]
//     private int numberOfCards = 10;

//     public override void OnStartServer()
//     {
//         base.OnStartServer();
//         GenerateCards();
//     }

//     private void GenerateCards()
//     {
//         int tempIndex;
//         for (int i = 0; i < numberOfCards; i++)
//         {
//             do
//             {
//                 tempIndex = Random.Range(0, cardPrefabs.Count);
//             } while (selectedCards.Contains(tempIndex));

//             selectedCards.Add(tempIndex);
//             SpawnCardOnServer(tempIndex, i);
//         }
//     }

//     private void SpawnCardOnServer(int index, int pos)
//     {
//         Vector2 position = new Vector2(transform.position.x + pos, transform.position.y);
//         GameObject card = Instantiate(cardPrefabs[index], position, Quaternion.Euler(0,0,0));
//         NetworkServer.Spawn(card); // Spawn the card on the network
//         RpcSpawnCard(card, index, pos);
//     }

//     [ClientRpc]
//     private void RpcSpawnCard(GameObject card, int index, int pos)
//     {
//         if (card == null)
//             return;

//         card.tag = "Card";  // Ensure the card has the "Card" tag
//         hand.Add(card);
//         Debug.Log($"Card {index} spawned at position {pos}");
//     }

//     private void Update()
//     {
//         HandleCardMovement();
//     }

//     private void HandleCardMovement()
//     {
//         if (!isLocalPlayer)
//         {
//             return;
//         }

//         if (Input.GetMouseButtonDown(0))
//         {
//             Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//             RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

//             if (hit.collider != null && hit.collider.gameObject.CompareTag("Card"))
//             {
//                 CmdMoveCard(hit.collider.gameObject);
//             }
//         }
//     }

//     [Command]
//     private void CmdMoveCard(GameObject card)
//     {
//         RpcMoveCard(card);
//     }

//     [ClientRpc]
//     private void RpcMoveCard(GameObject card)
//     {
//         if (card == null)
//             return;

//         card.transform.position = Vector2.zero;
//         card.transform.rotation = Quaternion.Euler(0, 180, 0);
//     }
// }


// move 

// using System.Collections;
// using UnityEngine;

// public class Move : MonoBehaviour
// {
//     public GameObject lastCardPlayed;
//     public float moveDuration = 1.0f;  // Duration of the move animation in seconds

//     void OnMouseDown()
//     {
//         Vector2 targetPosition = new Vector2(0, 0);
//         Quaternion targetRotation = Quaternion.Euler(0, 180, 0);

//         StartCoroutine(MoveCardToPosition(targetPosition, targetRotation));

//         lastCardPlayed = gameObject;
//         Debug.Log("name " + gameObject.name);
//     }

//     private IEnumerator MoveCardToPosition(Vector2 targetPosition, Quaternion targetRotation)
//     {
//         Vector2 startPosition = transform.position;
//         Quaternion startRotation = transform.rotation;
//         float elapsedTime = 0f;

//         while (elapsedTime < moveDuration)
//         {
//             transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
//             transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / moveDuration);
//             elapsedTime += Time.deltaTime;
//             yield return null;
//         }

//         // Ensure the card is exactly at the target position and rotation at the end
//         transform.position = targetPosition;
//         transform.rotation = targetRotation;
//     }
// }
