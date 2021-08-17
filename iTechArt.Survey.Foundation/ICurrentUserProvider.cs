using System;

namespace iTechArt.Survey.Foundation
{
    public interface ICurrentUserProvider
    {
        public Guid? GetUserId();

        public bool IsAuthenticated();
    }
}