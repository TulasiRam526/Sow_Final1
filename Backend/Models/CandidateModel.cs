using System;
using System.ComponentModel.DataAnnotations;

namespace CandidateSoW.Models
{ 
    public class CandidateModel
    {
        [Key]
        [Required]
        public int CandidateId { get; set; }
        public string? CandidateName { get; set; }
        public string? MobileNo { get; set; }
        public string? Gender { get; set; }
        public DateTime DOB{ get; set; }
        public string? Email { get; set; }
        public string? Location { get; set; }
        public string? Skills { get; set; }
        public DateTime JoiningDate { get; set; }
        public string? Address { get; set; }
        public string? Status { get; set; }
        public string? Pincode { get; set; }
        public bool isInternal { get; set; }
    }
    public class CandidateData
    {
        public IList<CandidateModel>? candidates { get; set; }
    }
}
