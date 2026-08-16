using System;
using System.ComponentModel.DataAnnotations;

namespace AI_Ecommerce.Data.Models.Masters
{
    /// <summary>Login audit trail — one row written on every successful employee login.</summary>
    public class EmployeeLogTable
    {
        [Key]
        public long LogId { get; set; }

        public long? EmployeeId { get; set; }
        public string? Token { get; set; }
        public DateTime? LogDateTime { get; set; }
        public TimeSpan? LogTime { get; set; }

        [MaxLength(100)]
        public string? IPAddress { get; set; }
        [MaxLength(100)]
        public string? CompName { get; set; }
        [MaxLength(100)]
        public string? MacAddress { get; set; }
        public string? GeoLocation { get; set; }
        [MaxLength(100)]
        public string? Latitude { get; set; }
        [MaxLength(100)]
        public string? Longitude { get; set; }
        [MaxLength(100)]
        public string? OSFamily { get; set; }
        [MaxLength(100)]
        public string? OSVersion { get; set; }
        [MaxLength(100)]
        public string? BrowserFamily { get; set; }
        [MaxLength(100)]
        public string? BrowserVersion { get; set; }

        public EmployeeMaster? Employee { get; set; }
    }
}
