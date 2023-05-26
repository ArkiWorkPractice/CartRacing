using EventBusModule.Interfaces;

namespace EventBusModule
{
    public class SingleIntParameterEventBusEventArgs : IEventBusEventArgs
    {
        public int Number;

        public SingleIntParameterEventBusEventArgs(int number) : base()
        {
            Number = number;
        }
    }
}