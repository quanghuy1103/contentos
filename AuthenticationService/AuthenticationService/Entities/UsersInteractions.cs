﻿using System;
using System.Collections.Generic;

namespace AuthenticationService.Entities
{
    public partial class UsersInteractions
    {
        public int IdTask { get; set; }
        public int IdUser { get; set; }
        public int? Interaction { get; set; }

        public virtual Tasks IdTaskNavigation { get; set; }
        public virtual Users IdUserNavigation { get; set; }
    }
}
