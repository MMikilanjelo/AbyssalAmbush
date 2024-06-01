using System.Collections.Generic;
using UnityEngine;
using Game.Managers;
using System.Linq;
namespace Game.Tiles
{
    public abstract class BaseTile : MonoBehaviour
    {
        #region Coordinates and Initialization
        public HexCoords Coords { get; private set; }
        public List<BaseTile> Neighbors { get; protected set; }
        public void Initialize(HexCoords coords , Vector2 pos) {
            Coords = coords;
            transform.position = pos;
        }
        public void CacheNeighbors() {
            Neighbors = GridManager.Instance.TilesInGrid.Where(t => Coords.GetDistance(t.Value.Coords) == 1).Select(t=>t.Value).ToList();
        }
        public virtual void OnMouseDown(){
            Debug.Log($"{Coords.q_} column +  {Coords.r_} row");
        }
        #endregion
        #region Tile Data
        public abstract  bool IsTraversable();
        #endregion
        #region  PathFinding
        public BaseTile Connection { get; private set; }
        public float G { get; private set; }
        public float H { get; private set; }
        public float F => G + H;
        public void SetConnection(BaseTile nodeBase) => Connection = nodeBase;
        public void SetG(float g) => G = g;
        public float GetDistance(BaseTile other) => Coords.GetDistance(other.Coords);
        public void SetH(float h) => H = h;
        #endregion
    }
    public interface ICoords
    {
        public float GetDistance(ICoords other);
    }
    public struct HexCoords : ICoords
    {
        public readonly int q_;
        public readonly int r_;

        public HexCoords(int q, int r){
            q_ = q;
            r_ = r;
        }
        public float GetDistance(ICoords other) => (this - (HexCoords)other).AxialLength();
        private int AxialLength()
        {
            if (q_ == 0 && r_ == 0) return 0;
            if (q_ > 0 && r_ >= 0) return q_ + r_;
            if (q_ <= 0 && r_ > 0) return -q_ < r_ ? r_ : -q_;
            if (q_ < 0) return -q_ - r_;
            
            return -r_ > q_ ? -r_ : q_;
        }

        public static HexCoords operator - (HexCoords a, HexCoords b)
        {
            return new HexCoords(a.q_ - b.q_, a.r_ - b.r_);
        }
    }
}