using UnityEngine;

namespace PCMaker.Services
{
    public class InputEvent
    {
        public bool Handled { get; private set; }

        public void MarkHandled()
        {
            Handled = true;
        }
    }


    public class ClickLMBEvent : InputEvent { }
    public class UpLMBEvent : InputEvent { }
    public class ClickRMBEvent : InputEvent { }
    public class ClickESCEvent : InputEvent { }
    public class ClickEKeyEvent : InputEvent { }
    public class ClickUpArrowEvent : InputEvent { }
    public class ClickDownArrowEvent : InputEvent { }
    public class SwitchGridSnappingEvent : InputEvent { }
    public class SwitchAlternativePlacementEvent : InputEvent { }
    public class MoveMouseEvent : InputEvent
    {
        public Vector2 Delta { get; }
        public MoveMouseEvent(Vector2 delta) => Delta = delta;
    }
    public class DragLMBEvent : InputEvent
    {
        public Vector2 Delta { get; }
        public DragLMBEvent(Vector2 delta) => Delta = delta;
    }

    public class MovePlayerEvent : InputEvent
    {
        public Vector2 Move { get; }
        public MovePlayerEvent(Vector2 move) => Move = move;
    }

    public class CrouchPlayerEvent : InputEvent { }

    public class ZoomPlayerEvent : InputEvent { }
    public class ScrollEvent : InputEvent
    {
        public float Value { get; }
        public ScrollEvent(float value) => Value = value;
    }
    public class SwitchConsoleEvent : InputEvent { }
    public class EnableCircleMenuEvent : InputEvent { }
    public class DisableCircleMenuEvent : InputEvent { }
}