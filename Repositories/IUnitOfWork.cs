namespace iTechArt.SurveyCreator.Repositories
{
    public interface IUnitOfWork
    {
        public ApplicationContext ApplicationContext { get; }
        public void Save();
    }
}
