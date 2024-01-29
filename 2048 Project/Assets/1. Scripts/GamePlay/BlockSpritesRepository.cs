using UnityEngine;

namespace Puzzle_2048
{
    public class BlockSpritesRepository
    {
        public static BlockSpritesRepository instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BlockSpritesRepository();
                    _instance._sprites = Resources.LoadAll<Sprite>("n"); // Resource 폴더 내의 n에서 sprite를 가져옴
                }
                return _instance;
            }
        }
        private static BlockSpritesRepository _instance;
        private Sprite[] _sprites; // 2, 4, 8 .. 

        public Sprite GetSprite(int value) // 2 블록 두 개 또는 2, 4 블록을 하나씩 가져와야함
        {
            return value > 0 ?
                _sprites[(int)Mathf.Log(value, 2) - 1] : null;
        }

        public int GetSpriteValue(Sprite sprite)
        {
            for (int i = 0; i < _sprites.Length; i++)
            {
                if (_sprites[i] == sprite)
                {
                    return (int)Mathf.Pow(2, i + 1); // Mathf.Pow(2, i+1) = 2^(i+1) => GetSprite의 value
                }
            }

            return -1; // sprite가 같지않다면 -1을 반환
        }
    }
}