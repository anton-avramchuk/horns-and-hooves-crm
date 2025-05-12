namespace HornsAndHoovesCrm.Domain.Common;

public interface ICreatedByUserTrackedEntity
{
    Guid CreatedByUserId { get; set; }
}