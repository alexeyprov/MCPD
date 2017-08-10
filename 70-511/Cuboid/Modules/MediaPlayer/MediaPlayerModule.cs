using Microsoft.Practices.Unity;
using Cuboid.Modules.MediaPlayer.Views;
using Cuboid.Shared;
using Prism.Modularity;
using Prism.Regions;

namespace Cuboid.Modules.MediaPlayer
{
    public class MediaPlayerModule : IModule
    {
        #region Private Fields

        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;
        
        #endregion

        #region Construtor

        public MediaPlayerModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }
        
        #endregion

        #region IModule Members

        void IModule.Initialize()
        {
            _regionManager.RegisterViewWithRegion(
                RegionNames.TAB_REGION,
                () => _container.Resolve<AudioPlayerView>());

            _regionManager.RegisterViewWithRegion(
                RegionNames.TAB_REGION,
                () => _container.Resolve<VideoPlayerView>());
        }

        #endregion
    }
}
