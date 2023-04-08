namespace MOROPO.Framework.Guama.Core.Presentation.Models.View
{
    public class SelectionItemInputViewModel<TId>
    {
        public bool Selected { get; set; }

        public TId Id { get; set; }

        public string Label { get; set; }
    }
}
