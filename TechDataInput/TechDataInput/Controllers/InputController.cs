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
            return await _db.UserRoles.ToListAsync();
        }

        // Получить одну роль по Id
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRole>> Get(int id)
        {
            var role = await _db.UserRoles.FindAsync(id);
            if (role == null) return NotFound();
            return role;
        }

        // Добавить новую роль
        [HttpPost]
        public async Task<ActionResult<UserRole>> Create(UserRole newRole)
        {
            _db.UserRoles.Add(newRole);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = newRole.Id }, newRole);
        }

        // Обновить существующую роль
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserRole updatedRole)
        {
            if (id != updatedRole.Id)
                return BadRequest("Id в URL и в теле запроса не совпадают.");

            var role = await _db.UserRoles.FindAsync(id);
            if (role == null) return NotFound();

            role.Name = updatedRole.Name;

            await _db.SaveChangesAsync();
            return NoContent(); // 204, без тела
        }

        // Удалить роль
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _db.UserRoles.FindAsync(id);
            if (role == null) return NotFound();

            _db.UserRoles.Remove(role);
            await _db.SaveChangesAsync();
            return NoContent();
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

        // Получить одну группу оборудования по Id
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentGroup>> Get(int id)
        {
            var eqGroup = await _db.EquipmentGroups.FindAsync(id);
            if (eqGroup == null) return NotFound();
            return eqGroup;
        }

        // Добавить новую группу оборудования
        [HttpPost]
        public async Task<ActionResult<EquipmentGroup>> Create(EquipmentGroup newEqGroup)
        {
            _db.EquipmentGroups.Add(newEqGroup);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = newEqGroup.Id }, newEqGroup);
        }

        // Обновить существующую группу оборудования
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EquipmentGroup updatedEqGroup)
        {
            if (id != updatedEqGroup.Id)
                return BadRequest("Id в URL и в теле запроса не совпадают.");

            var role = await _db.EquipmentGroups.FindAsync(id);
            if (role == null) return NotFound();

            role.Name = updatedEqGroup.Name;

            await _db.SaveChangesAsync();
            return NoContent(); // 204, без тела
        }

        // Удалить группу оборудования
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eqGroup = await _db.EquipmentGroups.FindAsync(id);
            if (eqGroup == null) return NotFound();

            _db.EquipmentGroups.Remove(eqGroup);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentController : ControllerBase
    {
        private readonly AppDbContext _db;

        public EquipmentController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IEnumerable<Equipment>> Get() => await _db.Equipments.ToListAsync();

        // Получить одну единицу оборудования по Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Equipment>> Get(int id)
        {
            var eq = await _db.Equipments.FindAsync(id);
            if (eq == null) return NotFound();
            return eq;
        }

        // Добавить новую единицу оборудования
        [HttpPost]
        public async Task<ActionResult<Equipment>> Create(Equipment newEq)
        {
            _db.Equipments.Add(newEq);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = newEq.Id }, newEq);
        }

        // Обновить существующую единицу оборудования
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Equipment updatedEq)
        {
            if (id != updatedEq.Id)
                return BadRequest("Id в URL и в теле запроса не совпадают.");

            var role = await _db.Equipments.FindAsync(id);
            if (role == null) return NotFound();

            role.Name = updatedEq.Name;
            role.EquipmentGroupId = updatedEq.EquipmentGroupId;

            await _db.SaveChangesAsync();
            return NoContent(); // 204, без тела
        }

        // Удалить группу оборудования
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eq = await _db.Equipments.FindAsync(id);
            if (eq == null) return NotFound();

            _db.Equipments.Remove(eq);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }


    [ApiController]
    [Route("api/[controller]")]
    public class ParameterDefinitionsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ParameterDefinitionsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IEnumerable<ParameterDefinition>> Get()
        {
            return await _db.ParameterDefinitions.ToListAsync();
        }

        // Добавить новую единицу оборудования
        [HttpPost]
        public async Task<ActionResult<ParameterDefinition>> Create(ParameterDefinition pd)
        {
            _db.ParameterDefinitions.Add(pd);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = pd.Id }, pd);
        }

        // Обновить существующую единицу оборудования
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ParameterDefinition pd)
        {
            if (id != pd.Id)
                return BadRequest("Id в URL и в теле запроса не совпадают.");

            var foundPD = await _db.ParameterDefinitions.FindAsync(id);
            if (foundPD == null) return NotFound();

            foundPD.Name = pd.Name;
            foundPD.Unit = pd.Unit;
            foundPD.EquipmentGroupId = pd.EquipmentGroupId;
            foundPD.UserRoleId = pd.UserRoleId;
            foundPD.PageNumber = pd.PageNumber;
            foundPD.OrderOnPage = pd.OrderOnPage;

            await _db.SaveChangesAsync();
            return NoContent(); // 204, без тела
        }

        // Удалить группу оборудования
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pd = await _db.ParameterDefinitions.FindAsync(id);
            if (pd == null) return NotFound();

            _db.ParameterDefinitions.Remove(pd);
            await _db.SaveChangesAsync();
            return NoContent();
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
