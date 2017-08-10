using System;

namespace Tasks.Data.Models
{
    public interface IVersionedModel
    {
        byte[] Version
        {
            get;
        }
    }
}
