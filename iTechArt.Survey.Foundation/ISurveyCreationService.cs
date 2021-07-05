using System;
using System.Collections.Generic;
using iTechArt.Survey.Domain;
using iTechArt.Survey.Domain.Question;
using iTechArt.Survey.Domain.Survey;

namespace iTechArt.Survey.Foundation
{
    public interface ISurveyCreationService
    {
        void AddPage();
        
        List<Page> GetPages();
        
        void SetEditQuestion(Guid questionId);
        
        void CancelEditQuestion(Guid questionId);
        
        void DeleteQuestion(Guid questionId);
        
        void AddQuestion(Guid pageId, QuestionType type);
        
        void EditQuestion(Question question);

        void EditQuestionWithAnswers(QuestionWithAnswers question);

        Guid GetActivePage();

        Guid GetActiveQuestion();

        void ChangeSurveyType(SurveyType type);

        SurveyTypes GetSurveyTypes();

        void DeletePage(Guid pageId);

        void ChangeQuestionPosition(Guid questionId ,bool up);
    }
}
