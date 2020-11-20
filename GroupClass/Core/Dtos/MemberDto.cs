using GroupClass.Core.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Dtos
{
    public class MemberDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int Classid { get; set; }
        public int Userid { get; set; }

        public ClassDto Class { get; set; }
        public UserDto User { get; set; }

        public MemberDto()
        {

        }
        public MemberDto(Member model)
        {
            this.Id = model.Id;
            this.CreatedAt = model.CreatedAt;
            this.ModifiedAt = model.ModifiedAt;
            this.Userid = model.Userid;
            this.Classid = model.Classid;
            this.Class = model.Class.Adapt<ClassDto>();
            this.User = model.User.Adapt<UserDto>();
        }
    }
}
