using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs
{
    public class RepositoryResult<T>
    {
        public bool Successfull { get; set; }
        public string? Message { get; set; }
        public T? Item { get; set; }
    }
}
