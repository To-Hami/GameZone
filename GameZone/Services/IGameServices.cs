namespace GameZone.Services
{
    public interface IGameServices
    {
        IEnumerable<Game> GetAll();
        Task create(CreateGameFormViewModels model);
    }
}
