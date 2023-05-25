using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class HierarchyElement
    {
        private VisualElement _element;
        private HierarchyElement[] _subElements;

        public HierarchyElement(VisualElement element, HierarchyElement[] subElements)
        {
            _element = element;
            _subElements = new HierarchyElement[subElements.Length];
            for (int i = 0; i < _subElements.Length; i++)
            {
                _subElements[i] = subElements[i];
            }
        }

        public void RegisterOnClickButton(string buttonName, Action method)
        {
            // register clicked event on button in current VisualElement or child's VisualElement
        }
    }
}