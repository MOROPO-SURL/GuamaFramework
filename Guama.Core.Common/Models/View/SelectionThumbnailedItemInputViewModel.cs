namespace MOROPO.Framework.Guama.Core.Presentation.Models.View
{
    public class SelectionThumbnailedItemInputViewModel<TId> : SelectionDescriptedItemInputViewModel<TId>
    {
        public string ThumbnailUri { get; set; }

        public bool HasThumbnail { get; set; }
    }
}
