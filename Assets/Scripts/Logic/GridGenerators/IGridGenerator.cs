using System.Collections.Generic;
using Game.Tiles;
using UnityEngine;
namespace Game.Logic.GridGeneration
{
    public interface IGridGenerator
    {
        public Dictionary<Vector2 , BaseTile> GenerateGrid(Grid grid);
    }
}

