using System;

namespace iTechArt.Survey.Domain.Surveys
{
    [Flags]
    public enum SurveyOptions
    {
        Anonymity= 1,
        RandomOrderOfQuestions = 2,
        NumberedQuestions = 4,
        NumberedPages = 8,
        MarksRequiredQuestionsWithAnAsterisk = 16,
        ProgressBar = 32
    }
}
