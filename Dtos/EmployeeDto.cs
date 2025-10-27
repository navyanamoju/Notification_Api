﻿namespace NotificationsApi.Dtos
{
    public class EmployeeDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public DateTime Created_At { get; set; } = DateTime.UtcNow;
    }
}
