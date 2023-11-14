



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
            var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";
            var path = Path.Combine(_imagesPath, CoverName);

            using var stream = File.Create(path);
            await model.Cover.CopyToAsync(stream);

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

     
    }
}
