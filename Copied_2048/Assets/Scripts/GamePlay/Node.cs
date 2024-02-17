using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle_2048
{
    [RequireComponent(typeof(Image))]
    public class Node : MonoBehaviour
    {
        public bool isEmpty => value == 0;

        private void Awake()
        {            
            _image = GetComponent<Image>();        
        }

        public int value
        {
            get => _value;
            set
            {
                if (value == _value)
                    return;

                _value = value;

                if (_image != null)
                {
                    _image.sprite = BlockSpritesRepository.instance.GetSprite(value);
                }
                
                onValueChanged?.Invoke(this, value);
            }
        }

        public int _value;
        public event Action<Node, int> onValueChanged;
        public Animator animator;
        private Image _image;
    }
}
