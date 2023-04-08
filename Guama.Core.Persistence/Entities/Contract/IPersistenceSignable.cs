namespace MOROPO.Framework.Guama.Core.Persistence.Entities.Contract
{
    public interface IPersistenceSignable
    {
        [System.ComponentModel.DataAnnotations.Required]
        string Signature { get; set; }

        object GetSignableEntityModel();
    }
}
