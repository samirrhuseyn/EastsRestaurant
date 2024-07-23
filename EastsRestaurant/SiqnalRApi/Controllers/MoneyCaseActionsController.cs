using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Abstract;
using DtoLayer.MoneyCaseActionDto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyCaseActionsController : ControllerBase
    {
        private readonly IMoneyCaseActionService _moneyCaseActionService;
        private readonly IMapper _mapper;
        public MoneyCaseActionsController(IMoneyCaseActionService moneyCaseActionService, IMapper mapper)
        {
            _moneyCaseActionService = moneyCaseActionService;
            _mapper = mapper;
        }

        [HttpGet("GetMoneyCaseActionList")]
        public IActionResult GetMoneyCaseActionList() 
        {
            var value = _mapper.Map<List<ResultMoneyCaseActionDto>>(_moneyCaseActionService.TGetListAll());
            return Ok(value);
        }
    }
}
