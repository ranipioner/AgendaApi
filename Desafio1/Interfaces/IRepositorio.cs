﻿namespace Desafio1.Interfaces
{
    public interface IRepositorio<T, t>
    {
        IEnumerable<T> GetAll();
        T Get(t id);
        T Save(T entity);
        T Update(T entity);
        void Delete(int id);
    }
}
