using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Abstract;
using DtoLayer.ExpenseDto;
using EntityLayer.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public IActionResult ExpenseList()
        {
            var value = _expenseService.TGetListAll().OrderByDescending(x=>x.ExpenseDate);
            return Ok(value);
        }

        [HttpPost] 
        public IActionResult CreateExpense(CreateExpenseDto createExpenseDto)
        {
            _expenseService.TAdd(new Expense
            {
                ExpenseAmount = createExpenseDto.ExpenseAmount,
                ExpenseDate = Convert.ToDateTime(DateTime.Now.ToLongTimeString()),
                ExpenseDescription = createExpenseDto.ExpenseDescription,
            });
            return Ok("Addition successfuly");
        }
    }
}
