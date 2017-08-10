﻿using System.Collections.Generic;

namespace Tasks.BusinessEntities
{
    public class User
    {
        public long UserId
        {
            get;
            set;
        }
        public string Username
        {
            get;
            set;
        }
        public string Firstname
        {
            get;
            set;
        }
        public string Lastname
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public List<Link> Links
        {
            get;
            set;
        }
    }
}
