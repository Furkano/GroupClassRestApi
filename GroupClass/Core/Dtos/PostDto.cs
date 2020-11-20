using GroupClass.Core.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int Userid { get; set; }
        public int Classid { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public virtual UserDto User { get; set; }
        public virtual ClassDto Class { get; set; }

        public PostDto()
        {

        }
        public PostDto(Post model)
        {
            this.Id = model.Id;
            this.CreatedAt = model.CreatedAt;
            this.ModifiedAt = model.ModifiedAt;
            this.Userid = model.Userid;
            this.Classid = model.Classid;
            this.Title = model.Title;
            this.Body = model.Body;
            this.User = model.User.Adapt<UserDto>();
            this.Class = model.Class.Adapt<ClassDto>();

        }
    }
}
