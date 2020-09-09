using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreGraphQL.DTO.User
{
    public class UserListResponseDTO
    {
        public int Id { get; set; }
        public int Groupid { get; set; }
        public string UserTypeName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool Isactive { get; set; }
        public bool Isdelete { get; set; }
        public virtual UserGroupMapperUserDTO Group { get; set; }
        public virtual UserMapperUserTypeDTO Usertype { get; set; }
    }

    public class UserGroupMapperUserDTO
    {
        public string Name { get; set; }
    }

    public class UserMapperUserTypeDTO
    {
        public string Name { get; set; }
    }
}
