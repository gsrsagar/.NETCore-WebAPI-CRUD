using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SampleAPI.Models
{
    public class MemberSponserRelationTable
    {   [Key]
        public int RelationId { get; set; }
        [Required]
        public string MemberUniqueId { get; set; }
        [Required]
        public ICollection<MemberShip> LowerMembers { get; set; }

    }
}
