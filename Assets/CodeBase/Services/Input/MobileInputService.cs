using UnityEngine;

namespace CodeBase.Services.Input
{
    class MobileInputSerivece : InputService
    {
        public override Vector2 Axis => SimpleInputAxis();
    }
}