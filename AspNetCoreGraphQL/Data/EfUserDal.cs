using AspNetCoreGraphQL.Core.DataAccess.EntityFramework.Repository;
using AspNetCoreGraphQL.DTO.User;
using AspNetCoreGraphQL.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreGraphQL.Data
{
    public class EfUserDal : EfEntityRepositoryBase<User, EFContext>, IUserDal
    {
        private readonly IMapper _mapper;
       
        public EfUserDal(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IQueryable<User> All()
        {
            return GetAll();
        }

        public async Task<IEnumerable<User>> IncludeUserGroupsAsync()
        {
            return await IncludeEnumerable(x => x.Group);
        }

        public IQueryable<UserListResponseDTO> IncludeUserGroupsDTO()
        {
            return Including(x => x.Group).ProjectTo<UserListResponseDTO>(_mapper.ConfigurationProvider);
        }

        public User GetId(int id)
        {
            //return Get(id);
            return FindBy(x=> x.Id==id).Include(x=> x.Group).FirstOrDefault();
        }


        public IQueryable<User> GetUsersWithByUserGroupId(long GroupID)
        {
          return FindBy(x => x.Groupid == GroupID);
        }

        public IQueryable<UserListResponseDTO> List(UserListRequestDTO src)
        {
            // 1
            /*
            return Including(x => x.Group)
                .Where(e => src.Id == 0 || e.Id == src.Id)
                .Where(e => src.Groupid == 0 || e.Groupid == src.Groupid)
                .Where(e => src.Usertypeid == 0 || e.Usertypeid == src.Usertypeid)
                .ProjectTo<UserListResponseDTO>(_mapper.ConfigurationProvider);
            */

            //2
            return Including(x => x.Group).Where(c => ((src.Id == 0) || c.Id == src.Id) && ((src.Groupid == 0) || c.Groupid == src.Groupid) && ((src.Usertypeid == 0) || c.Usertypeid == src.Usertypeid) && (string.IsNullOrEmpty(src.FirstName) || c.Email.Contains(src.FirstName)) && (string.IsNullOrEmpty(src.LastName) || c.Email.Contains(src.LastName)) && (string.IsNullOrEmpty(src.Email) || c.Email.Contains(src.Email)) && (src.Id == 0 || c.Id == src.Id)).ProjectTo<UserListResponseDTO>(_mapper.ConfigurationProvider);
        }

    }
}
