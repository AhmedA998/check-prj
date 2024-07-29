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
//             RpcSpawnCard(tempIndex, i);
//         }
//     }

//     [ClientRpc]
//     private void RpcSpawnCard(int index, int pos)
//     {
//         Vector2 position = new Vector2(transform.position.x + pos, transform.position.y);
//         GameObject card = Instantiate(cardPrefabs[index], position, Quaternion.Euler(0, isLocalPlayer ? 180 : 0, 0));
//         card.tag = "Card";  // Ensure the card has the "Card" tag
//         hand.Add(card);
//         Debug.Log(index);
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
//         card.transform.position = Vector2.zero;
//         card.transform.rotation = Quaternion.Euler(0, 180, 0);
//     }
// }
