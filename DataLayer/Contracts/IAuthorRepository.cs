﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayerServices {
    public interface IAuthorRepository {

        string Get();

        void Update(DataLayer.Entities.Author author);
    }
}