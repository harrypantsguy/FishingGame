namespace FishingGame.ContentManagement
{
    public sealed class DefaultContentService : IContentService
    {
        public IContentManager ContentManager { get; }

        public DefaultContentService(in IContentManager contentManager)
        {
            ContentManager = contentManager;
        }
    }
}