


namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICategoriesServices _CategoryServices;
        private readonly IDevicesServices _DevicesServices;
        private readonly IGameServices _GameServices ;

        public GamesController(ICategoriesServices categoryServices,
                            IDevicesServices devicesServices,
                            IGameServices gameServices)
        {

            _CategoryServices = categoryServices;
            _DevicesServices = devicesServices;
            _GameServices = gameServices;
        }

        public IActionResult Index()
        {
            var games = _GameServices.GetAll();
            return View(games);
        }



        [HttpGet]
        public IActionResult Create()
        {

            CreateGameFormViewModels viewModel = new()
            {

                Categories = _CategoryServices.GetSelectList(),
                Devices = _DevicesServices.SelectedDevices(),
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModels model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _CategoryServices.GetSelectList();
                model.Devices = _DevicesServices.SelectedDevices();

                return View(model);
            }
            await _GameServices.create(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
