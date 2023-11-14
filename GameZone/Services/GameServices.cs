



namespace GameZone.Services
{
    public class GameServices : IGameServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        private readonly string _imagesPath;

        public GameServices(ApplicationDbContext context,
                             IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _WebHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_WebHostEnvironment.WebRootPath}{SettingsFile.ImagePath}";
        }

        public IEnumerable<Game> GetAll()
        {
            return _context.Games
                .Include(G => G.Category)
                .Include(G=> G.Devices)
                .ThenInclude(D=> D.Device)
                .AsNoTracking().ToList();
        }

      

        public async Task create(CreateGameFormViewModels model)
        {
            var CoverName =await saveCover(model.Cover);

            Game game = new()
            {
                Name = model.Name,
                Cover = CoverName,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()
            };
            _context.Add(game);
            _context.SaveChanges();

        }


        Game? IGameServices.GetById(int id)
        {
            return _context.Games
                  .Include(G => G.Category)
                  .Include(G => G.Devices)
                  .ThenInclude(D => D.Device)
                  .AsNoTracking()
                  .SingleOrDefault(g => g.Id == id );
        }

        public async Task<Game?> update(EditGameFormViewModel model)
        {
            var game = _context.Games
                .Include(g => g.Devices).FirstOrDefault(g => g.Id == model.Id);
            if (game is null)
                return null;

            var hasNewCover = model.Cover is not null;

            var oldCover = game.Cover ;

            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();


            if (hasNewCover)
            {
                game.Cover = await saveCover(model.Cover!);
            }

          var EffectedRows =   _context.SaveChanges();

            if(EffectedRows > 0)
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(_imagesPath, oldCover);
                    File.Delete(cover);
                }

                return game;

            }
            else
            {
               
                var NewCover = Path.Combine(_imagesPath, game.Cover);
                File.Delete(NewCover);
                return null;
            }

            
        }

        private async Task<string> saveCover(IFormFile cover)
        {
            var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_imagesPath, CoverName);

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);

            return CoverName;
        }

        public bool Delete(int id)
        {
            var isDeleted = false;
            var game = _context.Games.Find(id);

            if (game is null)
                return isDeleted;
            var CurrentCover = game.Cover;
            _context.Games.Remove(game);

            var EffectedRows = _context.SaveChanges();
            if(EffectedRows > 0)
            {
                isDeleted = true;
                var Cover = Path.Combine(_imagesPath, CurrentCover);
                File.Delete(Cover);
            }

            return isDeleted;

        }
    }
}
