using EventBusModule.Interfaces;

namespace EventBusModule
{
    public class SingleIntParameterEventBusArgs : IEventBusArgs
    {
        public int Number { get; }

        public SingleIntParameterEventBusArgs(int number) : base()
        {
            Number = number;
        }
    }
}