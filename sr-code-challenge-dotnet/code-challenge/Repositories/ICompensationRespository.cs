﻿using challenge.Models;
using System;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public interface ICompensationRespository
    {
        Compensation GetById(String id);
        Compensation Add(Compensation employee);
        Task SaveAsync();
    }
}