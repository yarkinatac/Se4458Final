﻿using AutoMapper.Execution;
using Donor.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBank.Data.Repository.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        DonorDBContext _context = new DonorDBContext();

        private DbSet<T> query { get; set; }
        public BaseRepository()
        {
            query = _context.Set<T>();
        }
        public bool Add(T entity)
        {
            if (entity != null)
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }
        }
        public int AddAndGetId(T entity)
        {
            if (entity == null)
            {
                return -1;
            }

            _context.Set<T>().Add(entity);
            _context.SaveChanges();

            // Eğer entity'nin birincil anahtarı "UserID" ise, bu şekilde alınabilir
            var idProperty = _context.Entry(entity).Property("DonorID");
            if (idProperty != null && idProperty.CurrentValue != null)
            {
                return (int)idProperty.CurrentValue;
            }

            // Eğer birincil anahtarın değeri bulunamazsa, bir hata kodu döndür
            return -1;
        }
        public bool Delete(int id)
        {
            T? entity = query.Find(id);
            if (entity != null)
            {
                query.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DeleteSingleByMethod(Func<T, bool> method)
        {
            T? entity = query
                     .Where(method)
                     .Select(m => m)
                     .SingleOrDefault();
            if (entity != null)
            {
                query.Remove(entity);
                _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public bool DeleteSingleByMethod(Func<T, bool> metot, Func<T, bool> metot2)
        {
            T? entity = query
                      .AsEnumerable()
                      .Where(m => metot(m) && metot2(m))
                     .Select(m => m)
                     .SingleOrDefault();
            if (entity != null)
            {
                query.Remove(entity);
                _context.SaveChanges();
                return true;

            }
            return false;
        }
        public bool DeleteList(Func<T, bool> metot)
        {
            var entityList = query
                     .Where(metot)
                     .Select(m => m)
                     .ToList();
            if (entityList != null)
            {
                for (int i = 0; i < entityList.Count; i++)
                {
                    query.Remove(entityList[i]);
                    _context.SaveChanges();
                }

                return true;

            }
            return false;
        }
        public List<T>? GetList(Func<T, bool> method)
        {
            var list = query
                      .Where(method)
                      .Select(m => m)
                      .ToList();
            return list;
        }

        public T? GetSingle(int number)
        {
            return query.Find(number);
        }
        public T? GetSingleByMethod(Func<T, bool> method)
        {
            try
            {
                T? entity = query
                            .Where(method)
                            .Select(m => m)
                            .SingleOrDefault();
                return entity;
            }
            catch (SqlNullValueException)
            {
                return null;
            }


        }
        public T? GetSingleByMethod(Func<T, bool> method, Func<T, bool> method2)
        {

            T? entity = query
                      .AsEnumerable()
                      .Where(m => method(m) && method2(m))
                      .Select(m => m)
                      .SingleOrDefault();



            return entity;
        }

        public async Task<T?> UpdateAsync(Func<T, bool> metot, T updatedEntity)
        {
            try
            {
                T? entity = query
                     .Where(metot)
                     .Select(m => m)
                     .SingleOrDefault();

                if (entity != null)
                {
                    _context.Entry(entity).CurrentValues.SetValues(updatedEntity);
                    await _context.SaveChangesAsync();
                    return updatedEntity;
                }

                return null;

            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<T?> UpdateAsync(T? entity, T updatedEntity)
        {
            try
            {
                if (entity != null)
                {
                    _context.Entry(entity).CurrentValues.SetValues(updatedEntity);
                    await _context.SaveChangesAsync();
                    return updatedEntity;
                }

                return null;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }

}
