


using Microsoft.AspNetCore.Http.HttpResults;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICategoriesServices _CategoryServices;
        private readonly IDevicesServices _DevicesServices;
        private readonly IGameServices _GameServices;

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

        public IActionResult Details(int id)
        {
            var game = _GameServices.GetById(id);

            if (game is null)
                return NotFound();


            return View(game);
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _GameServices.GetById(id);

            if (game is null)
                return NotFound();


            EditGameFormViewModel viewModel = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                CurrentCover = game.Cover,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _CategoryServices.GetSelectList(),
                Devices = _DevicesServices.SelectedDevices(),
            };

            return View(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _CategoryServices.GetSelectList();
                model.Devices = _DevicesServices.SelectedDevices();

                return View(model);
            }

            var game =   await _GameServices.update(model);
            if(game is  null)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index)) ;


        }

       // [HttpDelete]
        public IActionResult Delete(int id)
        {
            var IsDeleted = _GameServices.Delete(id);
            return IsDeleted  ? Ok() : BadRequest();
            
        }
    }
}
