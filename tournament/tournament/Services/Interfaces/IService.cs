using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tournament.Models;

namespace tournament.Services.Interfaces
{
    public interface IService<TDto> where TDto : BaseDto
    {
        Task<ICollection<TDto>> GetAll();
        Task<TDto> Create(TDto newItem);
        Task<TDto> GetById(int id);
        Task Update(int id, TDto updateData);
        Task<bool> Delete(int id);
    }
}
