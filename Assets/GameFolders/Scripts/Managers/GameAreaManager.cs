using UnityEngine;

namespace LevelSystem
{
    public class GameAreaManager : MonoBehaviour
    {
        [SerializeField] private Transform _nextAreaPlacer;

        public Vector3 GetNextAreaPosition()
        {
            return _nextAreaPlacer.position;
        }

        public void MoveArea(Vector3 position)
        {
            transform.position = position;
        }
    }
}

