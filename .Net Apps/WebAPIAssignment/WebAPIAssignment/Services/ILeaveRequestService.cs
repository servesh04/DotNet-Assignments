using System.Collections.Generic;
using WebAPIAssignment.Dtos;

namespace WebAPIAssignment.Services
{
    public interface ILeaveRequestService
    {
        LeaveRequestResponseDto CreateLeaveRequest(LeaveRequestCreateDto requestDto);
        IEnumerable<LeaveRequestResponseDto> GetAllLeaveRequests();
        LeaveRequestResponseDto? GetLeaveRequestById(int id);
    }
}
