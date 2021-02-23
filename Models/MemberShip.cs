using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Models
{
    public class MemberShip
    {   
        [Key]
        public int MemeberId { get; set; }
        [Required]
        [MinLength(14)]
        public string SponserId { get; set; }
        [Required]
        [MinLength(14)]
        public string MemberUniqueId { get; set; }

        [Required]
        public string MemberFirstName { get; set; }
        [Required]
        public string MemberLastName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Format")]
        [DataType(DataType.EmailAddress)]
        public string MemberEmailId { get; set; }
        [Required]
        public string MemberCountry { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "Name Cannot exceed than required characters")]
        public string MemberMobileNumber { get; set; }
        [Required]
        [MaxLength(16, ErrorMessage = "Name Cannot exceed 16 characters")]
        public string MemberAadharCardNumber { get; set; }
        [Required]
        public string MemberPanCardNumber { get; set; }
        [Required]
        public string MemberBankName { get; set; }
        [Required]
        public string MemberAccountHolderName { get; set; }
        [Required]
        public string MemberAccountNumber { get; set; }
        [Required]
        public string MemberBankBranchName { get; set; }
        [Required]
        public string MemberBankIfscCode { get; set; }
        [Required]
        public string MemberNomineeName { get; set; }
        [Required]
        public string MemberNomineeRelation { get; set; }
        [Required]
        [MinLength(8)]
        public string MemberPassword { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime MemberJoinedDate { get; set; }
    }
}
