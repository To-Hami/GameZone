namespace GameZone.Services
{
    public interface IGameServices
    {
        IEnumerable<Game> GetAll();
        Game? GetById (int id);
       
        Task create(CreateGameFormViewModels model);
        Task<Game?> update(EditGameFormViewModel model);

        bool Delete(int id);


    }
}
