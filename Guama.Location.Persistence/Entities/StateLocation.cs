namespace MOROPO.Framework.Guama.Location.Persistence.Entities
{
    public class StateLocation : Location
    {
        public State State { get; set; }

        public System.Guid? StateId { get; set; }
    }
}
