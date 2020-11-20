using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Dtos
{
    public class BaseResponseDto<TData>
    {
        public BaseResponseDto()
        {
            Errors = new List<string>();
        }
        public bool HasError => Errors.Any();
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }

        public TData Data { get; set; }
    }
}
