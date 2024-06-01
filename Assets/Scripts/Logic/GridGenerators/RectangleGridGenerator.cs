using System.Collections.Generic;
using Game.Tiles;
using UnityEngine;

namespace Game.Logic.GridGeneration
{
    public class RectangleGridGenerator : MonoBehaviour , IGridGenerator 
    {
        [SerializeField] BaseTile tile_;
        public Dictionary<Vector2, BaseTile> GenerateGrid(Grid grid) {
            var tilesInGrid = new Dictionary<Vector2, BaseTile>();
            for(int x = 0 ; x < 10 ; x ++) {
                for(int y = 0 ; y < 10 ; y ++) {
                    
                    var worldPos = grid.GetCellCenterWorld(new Vector3Int(x , y));
                    var tileInstance = Instantiate(tile_ , worldPos , Quaternion.identity);
                  
                    tileInstance.Initialize(new HexCoords(y , x) , worldPos);
                    tileInstance.gameObject.name = $"{tileInstance.Coords.q_ } q  {tileInstance.Coords.r_ } r";
                    tilesInGrid.Add(worldPos , tileInstance);     
                }
            } 
            return tilesInGrid;      
        }   
    }
}

