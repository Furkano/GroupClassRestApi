using GroupClass.Core.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Dtos
{
    public class ClassDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string Name { get; set; }
        public string AlphaNumericCode { get; set; }
        public string EducationYear { get; set; }
        public List<MemberDto> Members { get; set; }

        public ClassDto()
        {

        }
        public ClassDto(Class model)
        {
            this.Id = model.Id;
            this.CreatedAt = model.CreatedAt;
            this.ModifiedAt = model.ModifiedAt;
            this.Name = model.Name;
            this.AlphaNumericCode = model.AlphaNumericCode;
            this.EducationYear = model.EducationYear;
            this.Members = model.Members.Adapt<List<MemberDto>>();
            //if (model.Members!=null)
            //{
                
            //}
        }
    }
}
