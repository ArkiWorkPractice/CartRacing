using System.Threading.Tasks;

namespace Infrastructure.StateMachine.States
{
    public interface IGameState
    {
        Task Enter();
        void Exit();
    }
}