using System;

namespace iTechArt.Survey.Foundation
{
    public interface ICurrentUserProvider
    {
        Guid GetUserId();
    }
}