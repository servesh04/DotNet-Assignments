using System;
using System.Collections.Generic;
using System.Linq;
using WebAPIAssignment.Dtos;   // Crucial: Connects the DTOs
using WebAPIAssignment.Models; // Crucial: Connects the core model

namespace WebAPIAssignment.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        // Internal in-memory data store using the database Model
        private readonly List<LeaveRequest> _leaveRequests = new();
        private int _nextId = 1;

        // 1. Matches interface return type: LeaveRequestResponseDto
        public LeaveRequestResponseDto CreateLeaveRequest(LeaveRequestCreateDto requestDto)
        {
            // Automated TotalDays calculation
            int totalDays = (requestDto.EndDate.Date - requestDto.StartDate.Date).Days + 1;
            if (totalDays <= 0) totalDays = 0;

            var leaveRequest = new LeaveRequest
            {
                LeaveRequestId = _nextId++,
                EmployeeName = requestDto.EmployeeName,
                EmployeeEmail = requestDto.EmployeeEmail,
                MobileNumber = requestDto.MobileNumber,
                LeaveType = requestDto.LeaveType,
                StartDate = requestDto.StartDate,
                EndDate = requestDto.EndDate,
                Reason = requestDto.Reason,
                TotalDays = totalDays,
                Status = "Pending",
                CreatedOn = DateTime.UtcNow
            };

            _leaveRequests.Add(leaveRequest);

            // Converts the model back to the expected DTO type before returning
            return MapToResponseDto(leaveRequest);
        }

        // 2. Matches interface return type: IEnumerable<LeaveRequestResponseDto>
        public IEnumerable<LeaveRequestResponseDto> GetAllLeaveRequests()
        {
            // Explicitly maps each entity into a Response DTO
            return _leaveRequests.Select(MapToResponseDto);
        }

        // 3. Matches interface return type: LeaveRequestResponseDto?
        public LeaveRequestResponseDto? GetLeaveRequestById(int id)
        {
            var request = _leaveRequests.FirstOrDefault(r => r.LeaveRequestId == id);
            return request == null ? null : MapToResponseDto(request);
        }

        // 4. Mapping helper to handle the structural translation safely
        private static LeaveRequestResponseDto MapToResponseDto(LeaveRequest model)
        {
            return new LeaveRequestResponseDto
            {
                LeaveRequestId = model.LeaveRequestId,
                EmployeeName = model.EmployeeName,
                EmployeeEmail = model.EmployeeEmail,
                LeaveType = model.LeaveType,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Reason = model.Reason,
                TotalDays = model.TotalDays,
                Status = model.Status,
                CreatedOn = model.CreatedOn
            };
        }
    }
}