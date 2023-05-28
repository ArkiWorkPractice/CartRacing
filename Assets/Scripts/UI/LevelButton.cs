using UnityEngine.UIElements;

namespace UI
{
    public class LevelButton : Button
    {
        public int LevelNumber { get; set; }

        public new class UxmlFactory : UxmlFactory<LevelButton, UxmlTraits>
        {
        }

        public new class UxmlTraits : Button.UxmlTraits
        {
            private UxmlIntAttributeDescription _levelNumber = new UxmlIntAttributeDescription { name = "Level-number" };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                LevelButton button = (LevelButton)ve;
                button.LevelNumber = _levelNumber.GetValueFromBag(bag, cc);
            }
        }
    }
}