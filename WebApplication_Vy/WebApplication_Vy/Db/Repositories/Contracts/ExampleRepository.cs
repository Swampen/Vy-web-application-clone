﻿using System.Collections.Generic;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Db.Repositories.Contracts
{
    public interface ExampleRepository
    {
        List<ExampleEntity> findAll();
    }
}