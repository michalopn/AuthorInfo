﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorInfo.API.Models
{
    public class AuthorWithoutBooksDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortBio { get; set; }
    }
}
