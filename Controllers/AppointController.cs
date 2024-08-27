using AutomobileServiceCenter_MasterDetailsInAPI.DAL;
using AutomobileServiceCenter_MasterDetailsInAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace AutomobileServiceCenter_MasterDetailsInAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointController : ControllerBase
    {
        private readonly MyDbContext _dbContext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AppointController(MyDbContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            _dbContext = dbContext;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult GetAppointments()
        {
            var appointments = _dbContext.AppointMasters.Include(a => a.AppointDetail).ToList();
            return Ok(appointments);
        }

        [HttpGet("{appointId}")]
        public IActionResult GetAppointment(int appointId)
        {
            var appointment = _dbContext.AppointMasters.Include(a => a.AppointDetail).FirstOrDefault(a => a.AppointId == appointId);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> PostAppointment()
        {
            if (ModelState.IsValid)
            {
                var customerName = HttpContext.Request.Form["CustomerName"];
                var appointDate = DateTime.Parse(HttpContext.Request.Form["AppointDate"]);
                var isComplete = bool.Parse(HttpContext.Request.Form["IsComplete"]);

                var appointment = new AppointMaster
                {
                    CustomerName = customerName,
                    AppointDate = appointDate,
                    IsComplete = isComplete
                };

                IFormFile imageFile = HttpContext.Request.Form.Files["ImageFile"];
                if (imageFile != null)
                {
                    var filename = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var path = Path.Combine(_hostEnvironment.WebRootPath ?? string.Empty, "uploads", filename);

                    appointment.ImagePath = filename;
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                }

                string appointDetailJson = HttpContext.Request.Form["AppointDetail"];
                if (!string.IsNullOrEmpty(appointDetailJson))
                {
                    List<AppointDetail> appointDetailList = JsonConvert.DeserializeObject<List<AppointDetail>>(appointDetailJson);
                    appointment.AppointDetail.AddRange(appointDetailList);
                }

                _dbContext.AppointMasters.Add(appointment);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, [FromForm] AppointMaster appointMaster)
        {
            var appointment = _dbContext.AppointMasters.Include(a => a.AppointDetail).FirstOrDefault(a => a.AppointId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var customerName = HttpContext.Request.Form["CustomerName"];
                var appointDate = DateTime.Parse(HttpContext.Request.Form["AppointDate"]);
                var isComplete = bool.Parse(HttpContext.Request.Form["IsComplete"]);

                appointment.CustomerName = customerName;
                appointment.AppointDate = appointDate;
                appointment.IsComplete = isComplete;

                IFormFile imageFile = HttpContext.Request.Form.Files["ImageFile"];
                if (imageFile != null)
                {
                    var filename = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var path = Path.Combine(_hostEnvironment.WebRootPath ?? string.Empty, "uploads", filename);

                    appointment.ImagePath = filename;
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                }

                string appointDetailJson = HttpContext.Request.Form["AppointDetail"];
                if (!string.IsNullOrEmpty(appointDetailJson))
                {
                    var updatedAppointDetails = JsonConvert.DeserializeObject<List<AppointDetail>>(appointDetailJson);
                    foreach (var updatedAppointDetail in updatedAppointDetails)
                    {
                        var existingAppointDetail = appointment.AppointDetail.FirstOrDefault(ad => ad.AppointDetailId == updatedAppointDetail.AppointDetailId);
                        if (existingAppointDetail != null)
                        {
                            existingAppointDetail.ServiceId = updatedAppointDetail.ServiceId;
                            existingAppointDetail.Quantity = updatedAppointDetail.Quantity;
                            existingAppointDetail.Price = updatedAppointDetail.Price;
                        }
                        else
                        {
                            appointment.AppointDetail.Add(new AppointDetail
                            {
                                ServiceId = updatedAppointDetail.ServiceId,
                                Quantity = updatedAppointDetail.Quantity,
                                Price = updatedAppointDetail.Price
                            });
                        }
                    }
                }

                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{appointId}")]
        public async Task<IActionResult> DeleteAppointment(int appointId)
        {
            var appointment = _dbContext.AppointMasters.FirstOrDefault(a => a.AppointId == appointId);
            if (appointment == null)
            {
                return NotFound();
            }

            _dbContext.AppointMasters.Remove(appointment);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}