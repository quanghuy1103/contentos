﻿using System;
using System.Collections.Generic;

namespace BatchjobService.Entities
{
    public partial class FanpagesTags
    {
        public int IdFanpage { get; set; }
        public int IdTag { get; set; }
        public bool? IsActive { get; set; }

        public virtual Fanpages IdFanpageNavigation { get; set; }
        public virtual Tags IdTagNavigation { get; set; }
    }
}
