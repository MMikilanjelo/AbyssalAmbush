using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using Game.Managers;
namespace Game.Tiles
{
    public class GroundTile : BaseTile
    {
        public override bool IsTraversable() => true;
        public override void OnMouseDown()
        {
            base.OnMouseDown();
            if (Neighbors.Count > 0) {
                foreach (var tile in Neighbors) {
                    Debug.Log($"{tile.Coords.q_} , {tile.Coords.r_}");
                    tile.gameObject.name = $"SelectedTile{tile.Coords.q_} q , {tile.Coords.r_}r";
                    tile.GetComponent<SpriteRenderer>().color = Color.black;
                }
            }
        }
    }
}
