using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechDataInput.Client.Services;
using TechDataInput.Data;
using TechDataInput.Data.DataClasses;
using TechDataInput.DB;

namespace TechDataInput.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InputController : ControllerBase
    {
        private readonly AppDbContext db;

        public InputController(AppDbContext DB)
        {
            db = DB;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitMeasurement([FromBody] MeasurementSessionForm form)
        {
            var session = new MeasurementSession
            {
                EquipmentId = form.EquipmentId,
                UserRoleId = form.UserRoleId,
                EnteredBy = form.EnteredBy,
                Timestamp = DateTime.Now,
                Values = form.Values.Select(v => new ParameterValue
                {
                    ParameterDefinitionId = v.ParameterDefinitionId,
                    Value = v.Value
                }).ToList()
            };

            db.MeasurementSessions.Add(session);
            await db.SaveChangesAsync();

            return Ok();
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class UserRolesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public UserRolesController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IEnumerable<UserRole>> Get()
        {
            try
            {
                return await _db.UserRoles.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentGroupController : ControllerBase
    {
        private readonly AppDbContext _db;

        public EquipmentGroupController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IEnumerable<EquipmentGroup>> Get() => await _db.EquipmentGroups.ToListAsync();

    }

    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentController : ControllerBase
    {
        private readonly AppDbContext _db;

        public EquipmentController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IEnumerable<Equipment>> Get() => await _db.Equipments.ToListAsync();

    }


    [ApiController]
    [Route("api/[controller]")]
    public class ParameterDefinitionsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ParameterDefinitionsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IEnumerable<ParameterDefinition>> Get([FromQuery] int roleId, [FromQuery] int equipmentId)
        {
            return await _db.ParameterDefinitions
                .Where(p => p.UserRoleId == roleId && p.EquipmentGroupId == equipmentId)
                .ToListAsync();
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly AppDbContext _db;

        public LogController(AppDbContext db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClientLogEntry log)
        {
            // здесь можешь сохранить в БД или в файл
            Console.WriteLine($"[{log.Timestamp}] {log.Level}: {log.Message}");

            // Пример сохранения, если у тебя есть таблица в БД
            // _db.ClientLogs.Add(log);
            // await _db.SaveChangesAsync();

            return Ok();
        }

    }
}
