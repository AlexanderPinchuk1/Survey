using System;

namespace iTechArt.Survey.Repositories
{
    public interface IUserAnswerRepository
    {
        public void DeleteUserAnswerUsingThreePartKey(Guid id, Guid surveyId, Guid questionId);
    }
}
