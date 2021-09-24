
namespace ClassroomV2.DataAccessLayer.Entities
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
