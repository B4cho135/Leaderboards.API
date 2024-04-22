using Application.Factories;
using Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Factories
{
    public class RepositoryResultFactory<T> : IRepositoryResultFactory<T> where T : class
    {
        public RepositoryResult<T> Create(bool successfull, string? message = null, T? item = null)
        {
            return new RepositoryResult<T> { Successfull = successfull, Item = item, Message = message };
        }
    }
}
