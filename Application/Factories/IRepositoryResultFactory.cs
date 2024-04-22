using Application.Models.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Factories
{
    public interface IRepositoryResultFactory<T> where T : class
    {
        RepositoryResult<T> Create(bool successfull, string? message = null, T? item = null);
    }
}
