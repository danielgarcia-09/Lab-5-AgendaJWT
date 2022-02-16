using AgendaManager.Bl.Dto;
using AgendaManager.Model.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaManager.Services.Service
{
    public interface IBaseService<T, TDto, TContext> where T : IBaseEntity where TDto: BaseDto where TContext : DbContext
    {
        IQueryable<TDto> Get();

        Task<TDto> GetById (int id);

        Task<TDto> Create( TDto dto );

        Task<TDto> Update( int id, TDto dto );

        Task<bool> Delete( int id );
    }
    public class BaseService<T, TDto, TContext> : IBaseService<T, TDto, TContext> 
        where T : BaseEntity
        where TDto : BaseDto
        where TContext : DbContext
    {
        protected readonly TContext _context;

        protected readonly DbSet<T> _dbSet;

        protected readonly IMapper _mapper;

        public BaseService(TContext context, IMapper mapper)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _mapper = mapper;
        }

        public virtual async Task<TDto> Create(TDto dto)
        {
            var entity = _mapper.Map<T>(dto);
            _dbSet.Add(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map(entity, dto);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            
            if (entity is null) return false;

            entity.Deleted = true;

            _dbSet.Update(entity);
            
            await _context.SaveChangesAsync();

            return true;
        }

        public virtual IQueryable<TDto> Get()
        {
            var entities = _dbSet.AsQueryable();
            return _mapper.Map<List<TDto>>(entities).AsQueryable();
        }

        public async Task<TDto> GetById(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity is null) return null;

            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> Update(int id, TDto dto)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity is null) return null;

            var update = _mapper.Map(dto, entity);

            _dbSet.Update(update);

            await _context.SaveChangesAsync();

            return _mapper.Map<TDto>(update);
        }
    }
}
