using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Abstract;
using DtoLayer.MenuTableDto;
using EntityLayer.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuTablesController : ControllerBase
    {
        private readonly IMenuTableService _menuTableService;

        public MenuTablesController(IMenuTableService menuTableService)
        {
            _menuTableService = menuTableService;
        }

        [HttpGet]
        public async Task<IActionResult> TableList()
        {
            return Ok(_menuTableService.TGetListAll());
        }

        [HttpGet("MenuTableCount")]
        public IActionResult MenuTableCount()
        {
            return Ok(_menuTableService.MenuTableCount());
        }

        [HttpPost]
        public IActionResult CreateTable(CreateMenuTableDto createMenuTableDto)
        {
            _menuTableService.TAdd(new MenuTable
            {
                Name = createMenuTableDto.Name,
                Status = false
            });
            return Ok("Addition successfuly");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTable(int id)
        {
            var value = _menuTableService.TGetByID(id);
            _menuTableService.TDelete(value);
            return Ok("Deletion successfuly");
        }
    }
}
