using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreGraphQL.DTO.User
{
    public class UserListRequestDTO 
    {
        public int Id { get; set; }
        public int Groupid { get; set; }
        public int Usertypeid { get; set; }
        //[Required]
        //[Display(Name = "PO")]
        //[RegularExpression(@"^[0-9]+$", ErrorMessage = "Debe ser un número.")]
        public string Email { get; set; }
        //[Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
