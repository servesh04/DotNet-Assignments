using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPIAssignment.Dtos;
using WebAPIAssignment.Services;

namespace WebAPIAssignment.Controllers
{
    [ApiController] // <-- CRUCIAL: Tells Swagger this is an API Controller
    [Route("api/leaverequests")] // <-- CRUCIAL: Maps the base route 
    public class LeaveRequestsController : ControllerBase
    {
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestsController(ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        [HttpPost]
        public ActionResult<LeaveRequestResponseDto> CreateLeaveRequest([FromBody] LeaveRequestCreateDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _leaveRequestService.CreateLeaveRequest(requestDto);
            return CreatedAtAction(nameof(GetLeaveRequestById), new { id = response.LeaveRequestId }, response);
        }

        [HttpGet]
        public ActionResult<IEnumerable<LeaveRequestResponseDto>> GetAllLeaveRequests()
        {
            var requests = _leaveRequestService.GetAllLeaveRequests();
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public ActionResult<LeaveRequestResponseDto> GetLeaveRequestById(int id)
        {
            var request = _leaveRequestService.GetLeaveRequestById(id);
            if (request == null)
            {
                return NotFound(new { message = $"Leave request with ID {id} not found." });
            }
            return Ok(request);
        }
    }
}