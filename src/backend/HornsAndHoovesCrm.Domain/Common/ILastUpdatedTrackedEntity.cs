namespace HornsAndHoovesCrm.Domain.Common;

public interface ILastUpdatedTrackedEntity
{
    public DateTime? LastUpdatedAt { get; set; }
}