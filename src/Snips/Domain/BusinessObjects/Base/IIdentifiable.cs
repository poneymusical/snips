using System;

namespace Snips.Domain.BusinessObjects.Base
{
    public interface IIdentifiable
    {
        Guid Id { get; set; }
    }
}