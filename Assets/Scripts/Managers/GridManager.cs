using Game.Tiles;
using UnityEngine;
using System.Collections.Generic;
using Game.Utilities.Singletons;
using Game.Logic.GridGeneration;
namespace Game.Managers
{
    public class GridManager : Singleton<GridManager>
    {
        [SerializeField] private Grid grid_;
        private IGridGenerator gridGenerator_;
        public IReadOnlyDictionary<Vector2, BaseTile> TilesInGrid { get; private set; }
        protected override void Awake() {
            base.Awake();
            gridGenerator_ = GetComponent<IGridGenerator>();
        }       
        private void Start(){
            GenerateGrid();
        }
        public void GenerateGrid() {
            TilesInGrid = gridGenerator_.GenerateGrid(grid_);
            foreach(var tile in TilesInGrid.Values){
                tile.CacheNeighbors();
            }
        }
        public BaseTile GetTile(Vector2 worldPos) {
            if (TilesInGrid.TryGetValue(worldPos, out BaseTile tile)) {
                return tile;
            }
            return null;
        }
    }
}

